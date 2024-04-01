using System.Collections;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

namespace MaterialDesignToolkit.ResourceGeneration;

internal static partial class IconDiff
{
    [GeneratedRegex(@"MaterialDesignThemes\.(?<Version>\d+\.\d+\.\d+)\.nupkg")]
    private static partial Regex FileNameRegex();

    public static async Task RunAsync()
    {
        var nugetDir = Path.Combine(PathHelper.RepositoryRoot, "nugets");
        var nugets = (
            from file in Directory.EnumerateFiles(nugetDir)
            let match = FileNameRegex().Match(Path.GetFileName(file))
            where match.Success
            let version = Version.Parse(match.Groups["Version"].Value)
            orderby version
            select (File: new FileInfo(file), Version: version)).ToList();

        var oldNuget = nugets.First();
        var newNuget = nugets.Last();

        string output = await CompareNuGets(oldNuget.File, oldNuget.Version, newNuget.File, newNuget.Version);

        await File.WriteAllTextAsync(Path.Combine(PathHelper.RepositoryRoot, $"IconChanges-{GetVersionString(oldNuget.Version)}--{GetVersionString(newNuget.Version)}.md"), output);

        static string GetVersionString(Version version)
            => $"{version.Major}.{version.Minor}.{version.Build}";
    }

    private static async Task<string> CompareNuGets(FileInfo previousNuget, Version previousVersion, FileInfo currentNuget, Version currentVersion)
    {
        Console.WriteLine($"Comparing previous {previousNuget.Name} to {currentNuget.Name}");
        var previousValues = await ProcessNuGet(previousNuget) ?? throw new InvalidOperationException($"Failed to find icons in previous NuGet {previousNuget.FullName}");
        var newValues = await ProcessNuGet(currentNuget) ?? throw new InvalidOperationException($"Failed to find icons in current NuGet {currentNuget.FullName}");

        var previousValuesByName = new Dictionary<string, int>();
        foreach (var kvp in previousValues)
        {
            foreach (string aliases in kvp.Value.Aliases)
            {
                previousValuesByName[aliases] = kvp.Key;
            }
        }
        var newValuesByName = new Dictionary<string, int>();
        foreach (var kvp in newValues)
        {
            foreach (string aliases in kvp.Value.Aliases)
            {
                newValuesByName[aliases] = kvp.Key;
            }
        }

        var newItems = newValuesByName.Keys.Except(previousValuesByName.Keys)
            .OrderBy(x => x)
            .ToList();

        var removedItems = previousValuesByName.Keys.Except(newValuesByName.Keys)
            .OrderBy(x => x)
            .ToList();

        var visuallyChanged = newValuesByName.Keys.Intersect(previousValuesByName.Keys)
            .Where(key => newValues[newValuesByName[key]].Path != previousValues[previousValuesByName[key]].Path)
            .OrderBy(x => x)
        .ToList();

        StringBuilder output = new();
        output.AppendLine($"## Pack Icon Changes {previousVersion.ToString(3)} => {currentVersion.ToString(3)}");
        WriteIconChanges("New icons", newItems, newValuesByName);

        WriteIconChanges("Icons with visual changes", visuallyChanged, newValuesByName);

        WriteIconChanges("Removed icons", removedItems, previousValuesByName);

        return output.ToString();

        void WriteIconChanges(string header, List<string> icons, Dictionary<string, int> iconsByName)
        {
            Console.WriteLine($"{header} => {icons.Count}");
            output.AppendLine($"### {header} ({icons.Count})");
            if (icons.Any())
            {
                foreach (var iconGroup in icons.GroupBy(name => iconsByName[name]))
                {
                    output.AppendLine($"- {string.Join(", ", iconGroup)}");
                }
            }
            else
            {
                output.AppendLine("_None_");
            }
        }
    }

    private static async Task<IReadOnlyDictionary<int, (HashSet<string> Aliases, string? Path)>?> ProcessNuGet(FileInfo nuget)
    {
        ZipArchive zipArchive = ZipFile.OpenRead(nuget.FullName);
        var entry = zipArchive.Entries.Where(x => x.FullName.EndsWith("MaterialDesignThemes.Wpf.dll"))
            .OrderByDescending(x => GetMajorTfmVersion(x.FullName))
            .First();

        using MemoryStream ms = new();
        using (var entryStream = entry.Open())
        {
            await entryStream.CopyToAsync(ms);
        }
        ms.Position = 0;
        return ProcessDll(ms);

        //This technically puts netcore before net framework, but since we are already at net7 and only care about latest
        //that does not matter.
        static int? GetMajorTfmVersion(string entryName)
        {
            if (!entryName.StartsWith("lib/net")) return null;
            string tfm = entryName[7..];
            char c = tfm.FirstOrDefault(char.IsDigit);
            if (c != default)
            {
                return int.Parse($"{c}");
            }
            return null;
        }
    }

    private static IReadOnlyDictionary<int, (HashSet<string> Aliases, string? Path)>? ProcessDll(Stream assemblyStream)
    {
        AssemblyLoadContext context = new AssemblyLoadContext(Guid.NewGuid().ToString(), true);

        Assembly assembly = context.LoadFromStream(assemblyStream);

        Type? packIconKind = assembly.GetType("MaterialDesignThemes.Wpf.PackIconKind");
        Type? packIconDataFactory = assembly.GetType("MaterialDesignThemes.Wpf.PackIconDataFactory");

        if (packIconKind is null) return null;
        if (packIconDataFactory is null) return null;

        var rv = new Dictionary<int, (HashSet<string>, string?)>();

        MethodInfo? createMethod = packIconDataFactory.GetMethod("Create", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static);

        var pathDictionary = (IDictionary?)createMethod?.Invoke(null, Array.Empty<object?>());

        if (pathDictionary is null) return null;

        foreach (string enumName in Enum.GetNames(packIconKind))
        {
            object @enum = Enum.Parse(packIconKind, enumName);
            if (rv.TryGetValue((int)@enum, out var found))
            {
                found.Item1.Add(enumName);
                continue;
            }

            string? path = (string?)pathDictionary[@enum];
            rv[(int)@enum] = (new HashSet<string> { enumName }, path);
        }

        context.Unload();

        return rv;
    }
}
