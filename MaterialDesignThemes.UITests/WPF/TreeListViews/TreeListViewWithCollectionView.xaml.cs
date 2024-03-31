using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

/// <summary>
/// Interaction logic for TreeListViewWithCollectionView.xaml
/// </summary>
public partial class TreeListViewWithCollectionView : UserControl
{
    private ObservableCollection<TreeItem> ItemsSource { get; } = new();

    public ICollectionView Items { get; }

    public TreeListViewWithCollectionView()
    {
        Items = CollectionViewSource.GetDefaultView(ItemsSource);

        InitializeComponent();

        AddChildren("Foo");
        AddChildren("42");
        AddChildren("24", "a", "b", "c");
        AddChildren("Bar", "1", "2", "3");

        void AddChildren(string root, params string[] children)
        {
            TreeItem item = new(root, null);
            foreach (string child in children)
            {
                item.Children.Add(new TreeItem(child, item));
            }
            ItemsSource.Add(item);
        }
    }
}
