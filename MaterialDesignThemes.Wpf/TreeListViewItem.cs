using System.Collections.Specialized;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = "PART_Header", Type = typeof(TreeListViewItemContentPresenter))]
[System.Diagnostics.DebuggerDisplay("Container for {DataContext}")]
public class TreeListViewItem : ListViewItem
{
    private TreeListViewItemContentPresenter? _contentPresenter;

    public TreeListViewItem()
    {
    }

    internal TreeListViewItem(TreeListView treeListView)
    {
        TreeListView = treeListView;
    }

    public TreeListView? TreeListView { get; }

    public IEnumerable<object?> GetChildren()
    {
        if (_contentPresenter is { } presenter)
        {
            return presenter.Children ?? Array.Empty<object?>();
        }
        return Array.Empty<object?>();
    }

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

    // Using a DependencyProperty as the backing store for HasItems.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HasItemsProperty =
        DependencyProperty.Register(nameof(HasItems), typeof(bool), typeof(TreeListViewItem), new PropertyMetadata(false));

    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register(nameof(Level), typeof(int), typeof(TreeListViewItem), new PropertyMetadata(0));

    public override void OnApplyTemplate()
    {
        if (_contentPresenter is { } oldPresenter)
        {
            CollectionChangedEventManager.RemoveHandler(oldPresenter, Presenter_CollectionChanged);
            _contentPresenter = null;
        }
        base.OnApplyTemplate();

        if (GetTemplateChild("PART_Header") is TreeListViewItemContentPresenter presenter)
        {
            _contentPresenter = presenter;
            CollectionChangedEventManager.AddHandler(presenter, Presenter_CollectionChanged);
            UpdateHasChildren();
        }
    }

    private void Presenter_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateHasChildren();
        TreeListView?.ItemsChildrenChanged(this, e);
    }

    private void UpdateHasChildren()
    {
        SetCurrentValue(HasItemsProperty, _contentPresenter?.HasChildren == true);
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
