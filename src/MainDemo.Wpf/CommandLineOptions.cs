using System.CommandLine;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign.Shared;

internal static class CommandLineOptions
{
    private static readonly Option<string> PageOption =
        new("--page", "-p")
        {
            DefaultValueFactory = _ => "Home",
            Description = "Sets the startup page of the Demo app."
        };

    private static readonly Option<FlowDirection> FlowDirectionOption =
        new("--flowDirection", "-f")
        {
            DefaultValueFactory = _ => FlowDirection.LeftToRight,
            Description = "Sets the startup flow direction of the Demo app."
        };

    private static readonly Option<BaseTheme> ThemeOption =
        new("--theme", "-t")
        {
            DefaultValueFactory = _ => BaseTheme.Inherit,
            Description = "Sets the startup theme of the Demo app."
        };

    private static readonly RootCommand RootCommand =
        new(description: "MaterialDesignInXamlToolkit Demo app command line options.")
        {
            PageOption,
            FlowDirectionOption,
            ThemeOption
        };

    public static (string? StartPage, FlowDirection FlowDirection, BaseTheme BaseTheme) ParseCommandLine(string[] args)
    {
        ParseResult parseResult = RootCommand.Parse(args);

        return new(
            parseResult.GetValue(PageOption),
            parseResult.GetValue(FlowDirectionOption),
            parseResult.GetValue(ThemeOption)
        );
    }
}
