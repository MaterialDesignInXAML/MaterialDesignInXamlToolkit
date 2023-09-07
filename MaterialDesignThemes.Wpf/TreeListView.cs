using System.Collections.ObjectModel;

namespace MaterialDesignThemes.Wpf;

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

    private ObservableCollection<object?> InternalItemsSource { get; } = new();
    private List<int> ItemLevels { get; } = new();

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
            treeListView.InternalItemsSource.Clear();
            treeListView.ItemLevels.Clear();
            if (baseValue is IEnumerable<object?> items)
            {
                foreach (object? item in items)
                {
                    treeListView.InternalItemsSource.Add(item);
                    treeListView.ItemLevels.Add(0);
                }
            }
            return treeListView.InternalItemsSource;
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
        if (element is TreeListViewItem treeListViewItem)
        {
            int index = ItemContainerGenerator.IndexFromContainer(treeListViewItem);
            treeListViewItem.Level = ItemLevels[index];
        }
    }

    internal void ItemExpandedChanged(TreeListViewItem item, bool isExpanded)
    {
        int index = ItemContainerGenerator.IndexFromContainer(item);
        var children = item.GetChildren().ToList();
        if (isExpanded)
        {
            for(int i = 0; i < children.Count; i++)
            {
                ItemLevels.Insert(i + index + 1, ItemLevels[index] + 1);
                InternalItemsSource.Insert(i + index + 1, children[i]);
            }
        }
        else
        {
            for (int i = 0; i < children.Count; i++)
            {
                ItemLevels.RemoveAt(index + 1);
                InternalItemsSource.RemoveAt(index + 1);
            }
        }
    }
}

public class TreeListViewItemContentPresenter : ContentPresenter
{
    internal IEnumerable<object?>? Children
    {
        get => (IEnumerable<object?>)GetValue(ChildrenProperty);
        set => SetValue(ChildrenProperty, value);
    }

    internal static readonly DependencyProperty ChildrenProperty =
        DependencyProperty.Register("Children", typeof(IEnumerable<object?>), typeof(TreeListViewItemContentPresenter),
            new PropertyMetadata(null));

    public DataTemplate? Template { get; set; }

    protected override void OnTemplateChanged(DataTemplate oldTemplate, DataTemplate newTemplate)
    {
        base.OnTemplateChanged(oldTemplate, newTemplate);
        Template = newTemplate;
        
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
        DependencyProperty.Register("IsExpanded", typeof(bool), typeof(TreeListViewItem),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsExpandedChanged));

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TreeListViewItem item)
        {
            item.TreeListView?.ItemExpandedChanged(item, (bool)e.NewValue);
        }
    }

    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register("Level", typeof(int), typeof(TreeListViewItem), new PropertyMetadata(0));

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _contentPresenter = GetTemplateChild("PART_Header") as TreeListViewItemContentPresenter;
    }
}
