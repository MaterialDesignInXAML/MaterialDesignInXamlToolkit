using System.Collections.ObjectModel;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

/// <summary>
/// Interaction logic for TreeListViewTemplateSelector.xaml
/// </summary>
public partial class TreeListViewTemplateSelector
{
    public ObservableCollection<TreeItem> Items { get; } = new();


    public TreeListViewTemplateSelector()
    {
        InitializeComponent();

        AddChildren("Foo");
        AddChildren("42");
        AddChildren("24", "a", "b", "c");
        AddChildren("Bar", "1", "2", "3");

        void AddChildren(string root, params string[] children)
        {
            TreeItem item = new(root, null);
            foreach(string child in children)
            {
                item.Children.Add(new TreeItem(child, item));
            }
            Items.Add(item);
        }
    }
}

public class TypeTemplateSelector : DataTemplateSelector
{
    public DataTemplate? NumberTemplate { get; set; }
    public DataTemplate? StringTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is TreeItem treeItem)
        {
            if (int.TryParse(treeItem.Value, out _))
            {
                return NumberTemplate;
            }
            return StringTemplate;
        }
        return null;
    }
}
