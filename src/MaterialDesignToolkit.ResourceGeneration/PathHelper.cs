namespace MaterialDesignToolkit.ResourceGeneration;

public static class PathHelper
{
    private static readonly Lazy<string> _repoRoot = new(FindRepoRoot);
    public static string RepositoryRoot => _repoRoot.Value;


    private static string FindRepoRoot()
    {
        for (string? currentDirectory = Path.GetFullPath(".");
            !string.IsNullOrEmpty(Path.GetDirectoryName(currentDirectory));
            currentDirectory = Path.GetDirectoryName(currentDirectory))
        {
            if (Directory.Exists(Path.Combine(currentDirectory!, ".git")))
            {
                return currentDirectory!;
            }
        }
        throw new InvalidOperationException("Did not find repository root");
    }
}
