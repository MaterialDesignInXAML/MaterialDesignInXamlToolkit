using MaterialDesign.Shared;
using MaterialDesignThemes.Wpf;
using ShowMeTheXAML;

namespace MaterialDesign3Demo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal string? StartupPage { get; set; }
    internal FlowDirection InitialFlowDirection { get; set; }
    internal BaseTheme InitialTheme { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        (StartupPage, InitialFlowDirection, InitialTheme) = CommandLineOptions.ParseCommandLine(e.Args);

        //This is an alternate way to initialize MaterialDesignInXAML if you don't use the MaterialDesignResourceDictionary in App.xaml
        //Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
        //Color secondaryColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
        //ITheme theme = Theme.Create(new MaterialDesignLightTheme(), primaryColor, secondaryColor);
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
