using System.Collections;
using System.Collections.Specialized;
using MaterialDesignThemes.Wpf.Internal;

namespace MaterialDesignThemes.Wpf;

[System.Diagnostics.DebuggerDisplay("Container for {DataContext}")]
[TemplatePart(Name = ContentPresenterPart, Type = typeof(TreeListViewContentPresenter))]
public class TreeListViewItem : ListViewItem
{
    internal const string ContentPresenterPart = "PART_ContentPresenter";

    public TreeListViewItem()
    { }

    public TreeListViewItem(TreeListView treeListView)
    {
        TreeListView = treeListView;
    }

    private TreeListViewContentPresenter? ContentPresenter { get; set; }
    private TreeListView? TreeListView { get; set; }

    public IEnumerable<object?> GetChildren() => Children?.OfType<object?>() ?? Array.Empty<object?>();

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



    public bool DisableExpandOnDoubleClick
    {
        get => (bool)GetValue(DisableExpandOnDoubleClickProperty);
        set => SetValue(DisableExpandOnDoubleClickProperty, value);
    }

    public static readonly DependencyProperty DisableExpandOnDoubleClickProperty =
        DependencyProperty.Register("DisableExpandOnDoubleClick", typeof(bool), typeof(TreeListViewItem), new PropertyMetadata(false));

    internal IEnumerable? Children
    {
        get => (IEnumerable?)GetValue(ChildrenProperty);
        set => SetValue(ChildrenProperty, value);
    }

    internal static readonly DependencyProperty ChildrenProperty =
        DependencyProperty.Register(nameof(Children), typeof(IEnumerable), typeof(TreeListViewItem),
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
        Level = level;
        TreeListView = treeListView;

        //NB: This can occur as part of TreeListView.PrepareContainerForItemOverride
        //Because this can trigger additional collection changes we enqueue the operation
        //to occur after the current operation has completed.
        Dispatcher.BeginInvoke(() =>
        {
            if (GetTemplate() is HierarchicalDataTemplate { ItemsSource: { } itemsSourceBinding })
            {
                SetBinding(ChildrenProperty, itemsSourceBinding);
            }
            IsExpanded = isExpanded;
        });

        DataTemplate? GetTemplate()
        {
            return ContentTemplate ??
                   ContentTemplateSelector?.SelectTemplate(item, this) ??
                   ContentPresenter?.Template;
        }
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        ContentPresenter = GetTemplateChild(ContentPresenterPart) as TreeListViewContentPresenter;

        if (ContentPresenter is { } contentPresenter)
        {
            WeakEventManager<TreeListViewContentPresenter, EventArgs>.AddHandler(
                contentPresenter, nameof(TreeListViewContentPresenter.TemplateChanged), OnTemplateChanged);

            void OnTemplateChanged(object? sender, EventArgs e)
            {
                PrepareTreeListViewItem(Content, TreeListView!, Level, IsExpanded);
            }
        }
    }

    internal void ClearTreeListViewItem(object _, TreeListView __)
    {
        if (Children is INotifyCollectionChanged collectionChanged)
        {
            CollectionChangedEventManager.RemoveHandler(collectionChanged, CollectionChanged_CollectionChanged);
        }
        TreeListView = null;
    }

    private void UpdateHasChildren()
    {
        SetCurrentValue(HasItemsProperty, GetChildren().Any());
    }

    protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        if (!e.Handled && !DisableExpandOnDoubleClick && e.ChangedButton == MouseButton.Left)
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
                    e.Handled = true;
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
                    e.Handled = true;
                    break;
            }
        }
    }
}
