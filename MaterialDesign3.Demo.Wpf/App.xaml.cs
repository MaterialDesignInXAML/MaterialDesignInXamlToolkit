using MaterialDesignThemes.Wpf;
using ShowMeTheXAML;

namespace MaterialDesign3Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static string? StartupPage { get; set; }
    internal static FlowDirection InitialFlowDirection { get; set; }
    internal static BaseTheme InitialTheme { get; set; }

    internal static Task SetDefaults(string page, FlowDirection flowDirection, BaseTheme theme)
    {
        App.StartupPage = page;
        App.InitialFlowDirection = flowDirection;
        App.InitialTheme = theme;
        return Task.CompletedTask;
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await CommandLineOptions.ReadCommandLineOptions(e.Args);

        //This is an alternate way to initialize MaterialDesignInXAML if you don't use the MaterialDesignResourceDictionary in App.xaml
        //Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
        //Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
        //ITheme theme = Theme.Create(new MaterialDesignLightTheme(), primaryColor, accentColor);
        //Resources.SetTheme(theme);


        //Illustration of setting culture info fully in WPF:
        /*             
        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        */

        XamlDisplay.Init();

        // test setup for Persian culture settings
        /*System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-Ir");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa-Ir");
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));*/

        base.OnStartup(e);
    }
}
