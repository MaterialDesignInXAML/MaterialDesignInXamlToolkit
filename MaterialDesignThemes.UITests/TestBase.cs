using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: GenerateHelpers(typeof(SmartHint))]
[assembly: GenerateHelpers(typeof(TimePicker))]
[assembly: GenerateHelpers(typeof(DrawerHost))]

namespace MaterialDesignThemes.UITests;

public abstract class TestBase : IAsyncLifetime
{
    protected ITestOutputHelper Output { get; }

    [NotNull]
    protected IApp? App { get; set; }

    public TestBase(ITestOutputHelper output)
        => Output = output ?? throw new ArgumentNullException(nameof(output));

    protected async Task<Color> GetThemeColor(string name)
    {
        IResource resource = await App.GetResource(name);
        return resource.GetAs<Color?>() ?? throw new Exception($"Failed to convert resource '{name}' to color");
    }

    protected async Task<IVisualElement<T>> LoadXaml<T>(string xaml, params (string namespacePrefix, Type type)[] additionalNamespaceDeclarations)
    {
        await App.InitializeWithMaterialDesign();
        return await App.CreateWindowWith<T>(xaml, additionalNamespaceDeclarations);
    }

    protected async Task<IVisualElement> LoadUserControl<TControl>()
        where TControl : UserControl
    {
        await App.InitializeWithMaterialDesign();
        return await App.CreateWindowWithUserControl<TControl>();
    }

    public async Task InitializeAsync() =>
        App = await XamlTest.App.StartRemote(new AppOptions
        {
            AllowVisualStudioDebuggerAttach = true,
            LogMessage = message => Output.WriteLine(message)
        });
    public async Task DisposeAsync() => await App.DisposeAsync();
}
