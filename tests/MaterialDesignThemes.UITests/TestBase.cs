using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using MaterialDesignThemes.UITests;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using TUnit.Core;
using TUnit.Core.Interfaces;

[assembly: ParallelLimiter<SingleParallelLimit>]

[assembly: GenerateHelpers(typeof(AutoSuggestBox))]
[assembly: GenerateHelpers(typeof(ColorPicker))]
[assembly: GenerateHelpers(typeof(DecimalUpDown))]
[assembly: GenerateHelpers(typeof(DialogHost))]
[assembly: GenerateHelpers(typeof(DrawerHost))]
[assembly: GenerateHelpers(typeof(NumericUpDown))]
[assembly: GenerateHelpers(typeof(PopupBox))]
[assembly: GenerateHelpers(typeof(SmartHint))]
[assembly: GenerateHelpers(typeof(TimePicker))]
[assembly: GenerateHelpers(typeof(TreeListView))]
[assembly: GenerateHelpers(typeof(TreeListViewItem))]

namespace MaterialDesignThemes.UITests;

public record SingleParallelLimit : IParallelLimit
{
    public int Limit => 1;
}

public abstract class TestBase
{
    public bool AttachedDebuggerToRemoteProcess { get; set; } = true;
    protected static TextWriter Output => TestContext.Current?.OutputWriter ?? throw new InvalidOperationException("Could not find output writer");

    [NotNull]
    public IApp? App { get; set; }

    [NotNull]
    public TestRecorder? Recorder { get; set; }

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

    public Task<IApp> StartApp()
    {
        return XamlTest.App.StartRemote(new AppOptions
        {
#if !DEBUG
            MinimizeOtherWindows = !System.Diagnostics.Debugger.IsAttached,
#endif
            AllowVisualStudioDebuggerAttach = AttachedDebuggerToRemoteProcess,
            LogMessage = Output.WriteLine
        });
    }
}
