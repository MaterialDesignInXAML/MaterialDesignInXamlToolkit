using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using VTTests;

namespace MaterialDesignThemes.UITests
{
    public static class VTMixins
    {
        public static async Task InitialzeWithMaterialDesign(this IApp app)
        {
            string applicationResourceXaml = @"<ResourceDictionary 
xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes"">
    <ResourceDictionary.MergedDictionaries>
        <materialDesign:BundledTheme BaseTheme=""Light"" PrimaryColor=""DeepPurple"" SecondaryColor=""Lime"" />

        <ResourceDictionary Source = ""pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml"" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>";

            await app.Initialize(applicationResourceXaml,
                Path.GetFullPath("MaterialDesignColors.dll"),
                Path.GetFullPath("MaterialDesignThemes.Wpf.dll"),
                Assembly.GetExecutingAssembly().Location);
        }
    }
}
