using System.CommandLine;
using System.CommandLine.Parsing;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign.Shared;

internal static class CommandLineOptions
{
    private static readonly Option<string> PageOption =
        new(aliases: new[] {"--page", "-p"},
            getDefaultValue: () => "Home",
            description: "Sets the startup page of the Demo app.");

    private static readonly Option<FlowDirection> FlowDirectionOption =
        new(aliases: new[] { "--flowDirection", "-f" },
            getDefaultValue: () => FlowDirection.LeftToRight,
            description: "Sets the startup flow direction of the Demo app.");

    private static readonly Option<BaseTheme> ThemeOption =
        new(aliases: new[] { "--theme", "-t" },
            getDefaultValue: () => BaseTheme.Inherit,
            description: "Sets the startup theme of the Demo app.");

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
            parseResult.GetValueForOption(PageOption),
            parseResult.GetValueForOption(FlowDirectionOption),
            parseResult.GetValueForOption(ThemeOption)
        );
    }
}
