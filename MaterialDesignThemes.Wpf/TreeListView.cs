using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MaterialDesignThemes.Wpf;

internal class TreeListViewItemsCollection<T> : ObservableCollection<T>
{
    private List<int> ItemLevels { get; } = new();

    public TreeListViewItemsCollection(object wrappedSource)
    {
        if (wrappedSource is IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }
        if (wrappedSource is INotifyCollectionChanged newCollectionChanged)
        {
            newCollectionChanged.CollectionChanged += ItemsSource_CollectionChanged;
        }
    }

    public int GetLevel(int index)
        => ItemLevels[index];

    public void Insert(int index, T item, int level)
    {
        Insert(index, item);
        ItemLevels[index] = level;
        if (Count != ItemLevels.Count)
        {

        }
    }

    protected override void RemoveItem(int index)
    {
        int currentLevel = ItemLevels[index];
        base.RemoveItem(index);
        ItemLevels.RemoveAt(index);
        while(index < Count && ItemLevels[index] > currentLevel)
        {
            RemoveAt(index);
            ItemLevels.RemoveAt(index);
        }
        if (Count != ItemLevels.Count)
        {

        }
    }

    protected override void InsertItem(int index, T item)
    {
        ItemLevels.Insert(index, 0);
        base.InsertItem(index, item);
        if (Count != ItemLevels.Count)
        {

        }
    }

    private void ItemsSource_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                for (int i = 0; i < e.NewItems?.Count; i++)
                {
                    Insert(e.NewStartingIndex + i, (T)e.NewItems[i]!);
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                for (int i = 0; i < e.OldItems?.Count; i++)
                {
                    RemoveAt(e.OldStartingIndex);
                }
                break;
            case NotifyCollectionChangedAction.Replace:
                for (int i = 0; i < e.NewItems?.Count; i++)
                {
                    this[e.NewStartingIndex + i] = (T)e.NewItems[i]!;
                }
                break;
            case NotifyCollectionChangedAction.Move:
                for (int i = 0; i < e.NewItems?.Count; i++)
                {
                    Move(e.OldStartingIndex + i, e.NewStartingIndex + i);
                }
                break;
            case NotifyCollectionChangedAction.Reset:
                Clear();
                ItemLevels.Clear();
                foreach (var item in sender as IEnumerable<T> ?? Enumerable.Empty<T>())
                {
                    Add(item);
                }
                break;
        }
    }
}

//TODO: Implement bindable property for getting selected items
//TODO: Keyboard commands left/right for expand and collapse
//TODO: Double click for toggle expanded
//Disallow setting GridView
public class TreeListView : ListView
{
    public double LevelIndentSize
    {
        get => (double)GetValue(LevelIndentSizeProperty);
        set => SetValue(LevelIndentSizeProperty, value);
    }

    // Using a DependencyProperty as the backing store for LevelIndentSize.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LevelIndentSizeProperty =
        DependencyProperty.Register("LevelIndentSize", typeof(double), typeof(TreeListView), new PropertyMetadata(16.0));

    private TreeListViewItemsCollection<object?>? InternalItemsSource { get; set; }

    static TreeListView()
    {
        var defaultMetadata = ItemsSourceProperty.DefaultMetadata;
        FrameworkPropertyMetadata metadata = new()
        {
            DefaultValue = defaultMetadata.DefaultValue,
            CoerceValueCallback = CoerceItemsSource
        };
        ItemsSourceProperty.OverrideMetadata(typeof(TreeListView), metadata);
    }

    public TreeListView()
    {
    }


    private static object CoerceItemsSource(DependencyObject d, object baseValue)
    {
        if (d is TreeListView treeListView)
        {
            return treeListView.InternalItemsSource = new(baseValue);
        }
        return baseValue;
    }

    protected override DependencyObject GetContainerForItemOverride()
        => new TreeListViewItem(this);

    protected override bool IsItemItsOwnContainerOverride(object? item)
        => item is TreeListViewItem;

    protected override void PrepareContainerForItemOverride(DependencyObject element, object? item)
    {
        base.PrepareContainerForItemOverride(element, item);
        if (element is TreeListViewItem treeListViewItem &&
            InternalItemsSource is { } itemsSource)
        {
            int index = ItemContainerGenerator.IndexFromContainer(treeListViewItem);
            treeListViewItem.Level = itemsSource.GetLevel(index);
        }
    }

    internal void ItemExpandedChanged(TreeListViewItem item)
    {
        if (InternalItemsSource is { } itemsSource)
        {
            int index = ItemContainerGenerator.IndexFromContainer(item);
            var children = item.GetChildren().ToList();
            if (item.IsExpanded)
            {
                int parentLevel = itemsSource.GetLevel(index);
                for (int i = 0; i < children.Count; i++)
                {
                    itemsSource.Insert(i + index + 1, children[i], parentLevel + 1);
                }
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    itemsSource.RemoveAt(index + 1);
                }
            }
        }
    }

    internal void ItemsChildrenChanged(TreeListViewItem item)
    {
        if (item.IsExpanded && InternalItemsSource is { } itemsSource)
        {
            int index = ItemContainerGenerator.IndexFromContainer(item);
            if (index < 0) return;
            var children = item.GetChildren().ToList();
            int itemLevel = itemsSource.GetLevel(index);

            while (index + 1 < InternalItemsSource?.Count &&
                  itemsSource.GetLevel(index + 1) == itemLevel + 1)
            {
                itemsSource.RemoveAt(index + 1);
            }

            for (int i = 0; i < children.Count; i++)
            {
                itemsSource.Insert(i + index + 1, children[i], itemLevel + 1);
            }
        }
    }
}

public class TreeListViewItemContentPresenter : ContentPresenter
{
    public event EventHandler? ChildrenChanged;

    public bool HasChildren { get; private set; }

    internal IEnumerable<object?>? Children
    {
        get => (IEnumerable<object?>)GetValue(ChildrenProperty);
        set => SetValue(ChildrenProperty, value);
    }

    internal static readonly DependencyProperty ChildrenProperty =
        DependencyProperty.Register("Children", typeof(IEnumerable<object?>), typeof(TreeListViewItemContentPresenter),
            new PropertyMetadata(null, OnChildrenChanged));

    private static void OnChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var presenter = (TreeListViewItemContentPresenter)d;
        if (e.OldValue is INotifyCollectionChanged oldCollectionChanged)
        {
            oldCollectionChanged.CollectionChanged -= presenter.CollectionChanged_CollectionChanged;
        }
        if (e.NewValue is INotifyCollectionChanged collectionChanged)
        {
            collectionChanged.CollectionChanged += presenter.CollectionChanged_CollectionChanged;
        }

        presenter.OnChildrenChanged();
    }

    private void CollectionChanged_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        => OnChildrenChanged();

    private void OnChildrenChanged()
    {
        HasChildren = Children?.Any() == true;
        ChildrenChanged?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnContentTemplateChanged(DataTemplate oldContentTemplate, DataTemplate newContentTemplate)
        => base.OnContentTemplateChanged(oldContentTemplate, newContentTemplate);

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    protected override void OnTemplateChanged(DataTemplate oldTemplate, DataTemplate newTemplate)
    {
        base.OnTemplateChanged(oldTemplate, newTemplate);

        if (newTemplate is HierarchicalDataTemplate hierarchical)
        {
            SetBinding(ChildrenProperty, hierarchical.ItemsSource);
        }
        else
        {
            ClearValue(ChildrenProperty);
        }
    }
}

[TemplatePart(Name = "PART_Header", Type = typeof(TreeListViewItemContentPresenter))]
public class TreeListViewItem : ListViewItem
{
    private TreeListViewItemContentPresenter? _contentPresenter;

    public TreeListViewItem()
    { }

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
        DependencyProperty.Register("IsExpanded", typeof(bool), typeof(TreeListViewItem),
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
        DependencyProperty.Register("HasItems", typeof(bool), typeof(TreeListViewItem), new PropertyMetadata(false));

    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register("Level", typeof(int), typeof(TreeListViewItem), new PropertyMetadata(0));

    public override void OnApplyTemplate()
    {
        if (_contentPresenter is { } oldPresenter)
        {
            oldPresenter.ChildrenChanged -= Presenter_ChildrenChanged;
            _contentPresenter = null;
        }
        base.OnApplyTemplate();

        if (GetTemplateChild("PART_Header") is TreeListViewItemContentPresenter presenter)
        {
            _contentPresenter = presenter;
            presenter.ChildrenChanged += Presenter_ChildrenChanged;
            UpdateHasChildren();
        }
    }

    private void Presenter_ChildrenChanged(object? sender, EventArgs e)
    {
        UpdateHasChildren();
        TreeListView?.ItemsChildrenChanged(this);
    }

    private void UpdateHasChildren()
    {
        SetCurrentValue(HasItemsProperty, _contentPresenter?.HasChildren == true);
    }
}
