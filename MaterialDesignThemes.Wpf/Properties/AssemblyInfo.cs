using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
[assembly: XmlnsPrefix("http://materialdesigninxaml.net/winfx/xaml/themes", "materialDesign")]
[assembly: XmlnsDefinition("http://materialdesigninxaml.net/winfx/xaml/themes", "MaterialDesignThemes.Wpf")]
[assembly: XmlnsDefinition("http://materialdesigninxaml.net/winfx/xaml/themes", "MaterialDesignThemes.Wpf.Transitions")]
[assembly: XmlnsDefinition("http://materialdesigninxaml.net/winfx/xaml/themes", "MaterialDesignThemes.Wpf.Converters")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: InternalsVisibleTo("MaterialDesignThemes.Wpf.Tests, PublicKey=" +
    "002400000480000094000000060200000024000052534131000400000100010049bc83151c3134" +
    "36a8868431b570bc5d626bc74d2d414b3147ae4e5945b69da039e5d22906b224557e5a8dfb3f86" +
    "0f0bec65b13217a0eaa37f0cced961386e92fd4d920179893d348b0210660f73e47adbc481c7b5" +
    "b63afbf3e60199685794599d28e915a30ca7dc879d9ad0dd0a0e7ea5e99937d637c4b1cb671fa4" +
    "425248dd")]