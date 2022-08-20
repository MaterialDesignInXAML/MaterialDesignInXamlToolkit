using System.Collections.ObjectModel;

namespace MaterialDesignDemo.Domain;

internal class TabsViewModel : ViewModelBase
{
    public ObservableCollection<CustomTab> CustomTabs { get; }

    public CustomTab? SelectedTab { get; set; }

    public TabsViewModel() =>
        CustomTabs = new()
        {
            new CustomTab {CustomHeader = "Custom tab 1", CustomContent = "Custom tab 1 content"},
            new CustomTab {CustomHeader = "Custom tab 2", CustomContent = "Custom tab 2 content"}
        };
}

internal class CustomTab
{
    public string? CustomHeader { get; set; }

    public string? CustomContent { get; set; }
}