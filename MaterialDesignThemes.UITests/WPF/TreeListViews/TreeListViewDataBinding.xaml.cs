using System.Collections.ObjectModel;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

/// <summary>
/// Interaction logic for TreeListViewDataBinding.xaml
/// </summary>
public partial class TreeListViewDataBinding 
{
    //NB: making the assumption changes occur ont he UI thread
    public ObservableCollection<TreeItem> Items { get; } = new();

    public TreeListViewDataBinding()
    {
        InitializeComponent();
        AddItem();
        AddItem();
        AddItem();
    }

    public void Add_OnClick(object sender, EventArgs e) => AddItem();

    private void AddItem()
    {
        if (TreeListView.SelectedItem is TreeItem selectedItem)
        {
            selectedItem.Children.Add(new TreeItem(selectedItem.Value + "_" + selectedItem.Children.Count));
        }
        else
        {
            Items.Add(new TreeItem(Items.Count.ToString()));
        }
    }

    public void Remove_OnClick(object sender, EventArgs e)
    {
        if (TreeListView.SelectedItem is TreeItem selectedItem)
        {
            RemoveItem(Items, selectedItem);
        }
    }

    private void RemoveItem(IList<TreeItem> items, TreeItem toRemove)
    {
        if (items.Contains(toRemove))
        {
            items.Remove(toRemove);
        }
        foreach (TreeItem item in items)
        {
            RemoveItem(item.Children, toRemove);
        }
    }
}

public class TreeItem
{
    public string Value { get; }

    //NB: making the assumption changes occur ont he UI thread
    public ObservableCollection<TreeItem> Children { get; } = new();

    public TreeItem(string value)
    {
        Value = value;
    }
}
