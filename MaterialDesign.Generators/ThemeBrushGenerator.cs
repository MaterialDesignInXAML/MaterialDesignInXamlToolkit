using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace MaterialDesign.Generators;

[Generator]
public class ThemeBrushGenerator : ISourceGenerator
{
    private static Regex ColorKeyRegex = new(@"MaterialDesign\.Brush\.(?<Property>.+)\.Color");

    public void Execute(GeneratorExecutionContext context)
    {
        //Uncomment to enable debugging
        //System.Diagnostics.Debugger.Launch();

        GenerateThemeClass(context, "Light");
        GenerateThemeClass(context, "Dark");
    }

    private static void GenerateThemeClass(GeneratorExecutionContext context, string theme)
    {
        AdditionalText? resourceDictionary =
            context.AdditionalFiles.SingleOrDefault(file => Path.GetFileName(file.Path) == $"MaterialDesignTheme.{theme}.xaml");

        if (resourceDictionary is null) return;

        using Stream fileStream = File.OpenRead(resourceDictionary.Path);
        XDocument xDoc = XDocument.Load(fileStream);
        StringBuilder code = new();
        code.AppendLine($@"
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Theming
{{
    partial class {theme}Theme
    {{
        public {theme}Theme()
        {{
");

        XName colorName = XName.Get("Color", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
        XName keyName = XName.Get("Key", "http://schemas.microsoft.com/winfx/2006/xaml");
        foreach (XElement element in xDoc.Root.Descendants().Where(x => x.Name == colorName))
        {
            if (element.Attribute(keyName) is { } keyAttribute &&
                ColorKeyRegex.Match(keyAttribute.Value) is Match keyMatch &&
                keyMatch.Success)
            {
                string property = keyMatch.Groups["Property"].Value;
                string colorValue = element.Value;

                code.Append("            ")
                    .AppendLine($"{property} = (Color)ColorConverter.ConvertFromString(\"{colorValue}\");");
            }
        }
        code.AppendLine(@"
        }
    }
}");
        context.AddSource($"{theme}Theme.g.cs", code.ToString());
    }

    public void Initialize(GeneratorInitializationContext context)
    { }
}
