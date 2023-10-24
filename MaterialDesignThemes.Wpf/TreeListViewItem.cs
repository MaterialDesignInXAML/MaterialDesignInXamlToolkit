using System.Collections.Specialized;

namespace MaterialDesignThemes.Wpf;

[System.Diagnostics.DebuggerDisplay("Container for {DataContext}")]
public class TreeListViewItem : ListViewItem
{
    public TreeListViewItem()
    {
    }

    private TreeListView? TreeListView { get; set; }

    public IEnumerable<object?> GetChildren() => Children ?? Array.Empty<object?>();

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(TreeListViewItem),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsExpandedChanged));

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TreeListViewItem item)
        {
            item.TreeListView?.ItemExpandedChanged(item);
        }
    }

    public bool HasItems
    {
        get => (bool)GetValue(HasItemsProperty);
        set => SetValue(HasItemsProperty, value);
    }

    public static readonly DependencyProperty HasItemsProperty =
        DependencyProperty.Register(nameof(HasItems), typeof(bool), typeof(TreeListViewItem), new PropertyMetadata(false));

    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register(nameof(Level), typeof(int), typeof(TreeListViewItem), new PropertyMetadata(0));


    internal IEnumerable<object?>? Children
    {
        get => (IEnumerable<object?>)GetValue(ChildrenProperty);
        set => SetValue(ChildrenProperty, value);
    }

    internal static readonly DependencyProperty ChildrenProperty =
        DependencyProperty.Register("Children", typeof(IEnumerable<object?>), typeof(TreeListViewItem),
            new PropertyMetadata(null, OnChildrenChanged));

    private static void OnChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var presenter = (TreeListViewItem)d;
        presenter.OnChildrenChanged(e);
    }

    private void OnChildrenChanged(DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is INotifyCollectionChanged oldCollectionChanged)
        {
            CollectionChangedEventManager.RemoveHandler(oldCollectionChanged, CollectionChanged_CollectionChanged);
        }
        if (e.NewValue is INotifyCollectionChanged collectionChanged)
        {
            CollectionChangedEventManager.AddHandler(collectionChanged, CollectionChanged_CollectionChanged);
        }

        OnChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void CollectionChanged_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        => OnChildrenChanged(e);

    private void OnChildrenChanged(NotifyCollectionChangedEventArgs e)
    {
        UpdateHasChildren();
        TreeListView?.ItemsChildrenChanged(this, e);
    }

    internal void PrepareTreeListViewItem(object? item, TreeListView treeListView, int level, bool isExpanded)
    {
        //TODO: Handle template selector
        if (ContentTemplate is HierarchicalDataTemplate { ItemsSource: { } itemsSourceBinding })
        {
            SetBinding(ChildrenProperty, itemsSourceBinding);
        }
        IsExpanded = isExpanded;
        Level = level;
        TreeListView = treeListView;
    }

    internal void ClearTreeListViewItem(object item, TreeListView treeListView)
    {
        if (Children is INotifyCollectionChanged collectionChanged)
        {
            CollectionChangedEventManager.RemoveHandler(collectionChanged, CollectionChanged_CollectionChanged);
        }
        TreeListView = null;
    }

    private void UpdateHasChildren()
    {
        SetCurrentValue(HasItemsProperty, Children?.Any() == true);
    }

    protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        if (e.ChangedButton == MouseButton.Left)
        {
            IsExpanded = !IsExpanded;
        }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!e.Handled)
        {
            switch (e.Key)
            {
                case Key.Right:
                    IsExpanded = true;
                    break;
                case Key.Left:
                    if (IsExpanded)
                    {
                        IsExpanded = false;
                    }
                    else
                    {
                        TreeListView?.MoveSelectionToParent(this);
                    }
                    break;
            }
        }
    }
}
