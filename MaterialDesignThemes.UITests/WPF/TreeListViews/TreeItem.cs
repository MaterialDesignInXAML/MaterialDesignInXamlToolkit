using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

[DebuggerDisplay("{Value} (Children: {Children.Count})")]
public class TreeItem
{
    public string Value { get; }

    public TreeItem? Parent { get; }

    //NB: making the assumption changes occur ont he UI thread
    public TestableCollection<TreeItem> Children { get; } = new();

    public ICollectionView ChildrenView { get; }

    public TreeItem(string value, TreeItem? parent)
    {
        Value = value;
        Parent = parent;
        ChildrenView = CollectionViewSource.GetDefaultView(Children);
    }
}
