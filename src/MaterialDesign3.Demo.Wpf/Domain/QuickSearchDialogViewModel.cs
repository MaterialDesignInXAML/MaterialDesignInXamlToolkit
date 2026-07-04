using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain;
public partial class QuickSearchDialogViewModel(IReadOnlyCollection<DemoItem> items) : ObservableObject
{
    private readonly IReadOnlyCollection<DemoItem> _items = items;

    [ObservableProperty]
    private string _searchText = "";

    partial void OnSearchTextChanged(string value)
        => OnPropertyChanged(nameof(FilteredItems));

    [ObservableProperty]
    private DemoItem? _selectedItem;

    partial void OnSelectedItemChanged(DemoItem? value)
    {
        if (value is not null)
        {
            DialogHost.Close("RootDialog", value);
        }
    }

    public IReadOnlyCollection<DemoItem> FilteredItems
        => string.IsNullOrWhiteSpace(SearchText)
        ? _items
        : _items.Where(i => i.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
}
