using System.Collections.ObjectModel;

namespace MaterialDesignDemo.Domain;

internal class TabsViewModel : ViewModelBase
{
    public ObservableCollection<CustomTab> CustomTabs { get; }

    public CustomTab? SelectedTab { get; set; }

    public TabsViewModel() =>
        CustomTabs = new()
        {
            new CustomTab {CustomHeader = "Custom tab 1"},
            new CustomTab {CustomHeader = "Custom tab 2"}
        };
}

internal class CustomTab: ViewModelBase
{
    public ICommand CloseCommand { get; }

    public string? CustomHeader { get; set; }

    public string? CustomContent => CustomHeader + ", close clicked: " + CloseClickCount;

    public int CloseClickCount { get; private set; }

    internal CustomTab()
    {
        CloseCommand = new AnotherCommandImplementation(_ =>
            {
                CloseClickCount++;
                OnPropertyChanged(nameof(CustomContent));
            });
    }
}