using System.Collections.ObjectModel;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

/// <summary>
/// Interaction logic for TreeListViewImplicitTemplate.xaml
/// </summary>
public partial class TreeListViewImplicitTemplate
{
    //NB: making the assumption changes occur on the UI thread
    public ObservableCollection<TreeItem> Items { get; } = new();

    public TreeListViewImplicitTemplate()
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
            selectedItem.Children.Add(new TreeItem(selectedItem.Value + "_" + selectedItem.Children.Count, selectedItem));
        }
        else
        {
            Items.Add(new TreeItem(Items.Count.ToString(), null));
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

    private void Replace_OnClick(object sender, RoutedEventArgs e)
    {
        if (TreeListView.SelectedItem is TreeItem selectedItem)
        {
            if (selectedItem is { Parent: { } parent })
            {
                int childIndex = parent.Children.IndexOf(selectedItem);
                parent.Children[childIndex] = new TreeItem(selectedItem.Value + "_r", parent);
            }
            else
            {
                int itemIndex = Items.IndexOf(selectedItem);
                Items[itemIndex] = new TreeItem(selectedItem.Value + "_r", null);
            }
        }
    }

    private void MoveDown_OnClick(object sender, RoutedEventArgs e)
    {
        if (TreeListView.SelectedItem is TreeItem selectedItem)
        {
            if (selectedItem is { Parent: { } parent })
            {
                int childIndex = parent.Children.IndexOf(selectedItem);
                if (childIndex < parent.Children.Count - 1)
                {
                    parent.Children.Move(childIndex, childIndex + 1);
                }
            }
            else
            {
                int itemIndex = Items.IndexOf(selectedItem);
                if (itemIndex < Items.Count - 1)
                {
                    Items.Move(itemIndex, itemIndex + 1);
                }
            }
        }
    }

    private void MoveUp_OnClick(object sender, RoutedEventArgs e)
    {
        if (TreeListView.SelectedItem is TreeItem selectedItem)
        {
            if (selectedItem is { Parent: { } parent })
            {
                int childIndex = parent.Children.IndexOf(selectedItem);
                if (childIndex > 0)
                {
                    parent.Children.Move(childIndex, childIndex - 1);
                }
            }
            else
            {
                int itemIndex = Items.IndexOf(selectedItem);
                if (itemIndex > 0)
                {
                    Items.Move(itemIndex, itemIndex - 1);
                }
            }
        }
    }

    private void Reset_OnClick(object sender, RoutedEventArgs e)
    {
        if (TreeListView.SelectedItem is TreeItem selectedItem)
        {
            var newChildren = selectedItem.Children.Select(x => new TreeItem(x.Value + "_x", x.Parent)).ToArray();
            selectedItem.Children.ReplaceAllItems(newChildren);
        }
    }
}
