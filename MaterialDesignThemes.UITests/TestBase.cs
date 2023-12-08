using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: GenerateHelpers(typeof(AutoSuggestBox))]
[assembly: GenerateHelpers(typeof(ColorPicker))]
[assembly: GenerateHelpers(typeof(DialogHost))]
[assembly: GenerateHelpers(typeof(DrawerHost))]
[assembly: GenerateHelpers(typeof(PopupBox))]
[assembly: GenerateHelpers(typeof(SmartHint))]
[assembly: GenerateHelpers(typeof(TimePicker))]
[assembly: GenerateHelpers(typeof(TreeListView))]
[assembly: GenerateHelpers(typeof(TreeListViewItem))]

namespace MaterialDesignThemes.UITests;

public abstract class TestBase(ITestOutputHelper output) : IAsyncLifetime
{
    protected bool AttachedDebuggerToRemoteProcess { get; set; } = true;
    protected ITestOutputHelper Output { get; } = output ?? throw new ArgumentNullException(nameof(output));

    [NotNull]
    protected IApp? App { get; set; }

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

    protected Task<IVisualElement> LoadUserControl<TControl>()
        where TControl : UserControl
        => LoadUserControl(typeof(TControl));

    protected async Task<IVisualElement> LoadUserControl(Type userControlType)
    {
        await App.InitializeWithMaterialDesign();
        return await App.CreateWindowWithUserControl(userControlType);
    }

    public async Task InitializeAsync() =>
        App = await XamlTest.App.StartRemote(new AppOptions
        {
#if !DEBUG
            MinimizeOtherWindows = !System.Diagnostics.Debugger.IsAttached,
#endif
            AllowVisualStudioDebuggerAttach = AttachedDebuggerToRemoteProcess,
            LogMessage = Output.WriteLine
        });
    public async Task DisposeAsync() => await App.DisposeAsync();
}
