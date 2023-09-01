using System.CommandLine;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo;

internal class CommandLineOptions
{
    private static readonly Option<string> PageOption =
        new(name: "--page", description: "Sets the startup page of the Demo app.");

    private static readonly Option<FlowDirection> FlowDirectionOption =
        new(name: "--flowDirection", description: "Sets the startup flow direction of the Demo app.");

    private static readonly Option<BaseTheme> ThemeOption =
        new(name: "--theme", description: "Sets the startup theme of the Demo app.");

    private static readonly Command ConfigureCommand =
        new("configure", "Allows for configuration of default startup values used by the Demo app.")
        {
            PageOption,
            FlowDirectionOption,
            ThemeOption
        };

    private static readonly RootCommand RootCommand =
        new(description: "MaterialDesignInXamlToolkit Demo app command line options.");

    static CommandLineOptions()
    {
        PageOption.AddAlias("-p");
        PageOption.SetDefaultValue("Home");
        FlowDirectionOption.AddAlias("-f");
        FlowDirectionOption.SetDefaultValue(FlowDirection.LeftToRight);
        ThemeOption.AddAlias("-t");
        ThemeOption.SetDefaultValue(BaseTheme.Inherit);

        ConfigureCommand.SetHandler(async (page, flowDirection, theme)
                => await App.SetDefaults(page, flowDirection, theme)
            , PageOption, FlowDirectionOption, ThemeOption);
        RootCommand.AddCommand(ConfigureCommand);
    }

    public static Task ReadCommandLineOptions(string[] args) => RootCommand.InvokeAsync(args);
}
