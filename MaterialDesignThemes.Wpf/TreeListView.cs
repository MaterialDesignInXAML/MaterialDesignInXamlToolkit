using System.Collections.Specialized;
using MaterialDesignThemes.Wpf.Internal;

namespace MaterialDesignThemes.Wpf;

//TODO: Implement bindable property for getting selected items
//TODO: Implement GridView support for having columns
public class TreeListView : ListView
{
    public double LevelIndentSize
    {
        get => (double)GetValue(LevelIndentSizeProperty);
        set => SetValue(LevelIndentSizeProperty, value);
    }

    public static readonly DependencyProperty LevelIndentSizeProperty =
        DependencyProperty.Register(nameof(LevelIndentSize), typeof(double), typeof(TreeListView), new PropertyMetadata(16.0));

    private TreeListViewItemsCollection<object?>? InternalItemsSource { get; set; }

    static TreeListView()
    {
        ItemsSourceProperty.OverrideMetadata(typeof(TreeListView), new FrameworkPropertyMetadata()
        {
            CoerceValueCallback = CoerceItemsSource
        });
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
                    itemsSource.InsertWithLevel(i + index + 1, children[i], parentLevel + 1);
                }
            }
            else
            {
                itemsSource.RemoveOffsetAdjustedItem(index); // InternalItemsSource.RemoveOffsetAdjustedItem(index) will remove the item and all of its children/grand-children
            }
        }
    }

    internal void ItemsChildrenChanged(TreeListViewItem item, NotifyCollectionChangedEventArgs e)
    {
        if (item.IsExpanded && InternalItemsSource is { } itemsSource)
        {
            int index = ItemContainerGenerator.IndexFromContainer(item);    // TODO: Alternatively, we could look up the item index directly in InternalItemsSource, but that would mean we need to deal with equality (Yuck!)
            if (index < 0) return;

            int parentLevel = itemsSource.GetLevel(index);
            // We push the index forward by 1 to be on the first element of the item's children
            index++;
            int adjustedIndex;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    adjustedIndex = index + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.NewStartingIndex);
                    for (int i = 0; i < e.NewItems?.Count; i++)
                    {
                        itemsSource.InsertWithLevel(e.NewStartingIndex + i + adjustedIndex, e.NewItems[i]!, parentLevel + 1);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    adjustedIndex = index + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.OldStartingIndex); ;
                    for (int i = 0; i < e.OldItems?.Count; i++)
                    {
                        itemsSource.RemoveOffsetAdjustedItem(e.OldStartingIndex + adjustedIndex);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    adjustedIndex = index + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.OldStartingIndex);
                    for (int i = 0; i < e.NewItems?.Count; i++)
                    {
                        itemsSource.ReplaceOffsetAdjustedItem(e.OldStartingIndex + i + adjustedIndex, e.NewItems[i]!);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    int additionalOffset = 0;
                    if (e.OldStartingIndex < e.NewStartingIndex)
                    {
                        // When moving down, we need to move past expanded children/grand-children as well
                        additionalOffset = 1;
                    }
                    int adjustedOldIndex = index + e.OldStartingIndex + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.OldStartingIndex);
                    int adjustedNewIndex = index + e.NewStartingIndex + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.NewStartingIndex + additionalOffset);
                    for (int i = 0; i < e.NewItems?.Count; i++)
                    {
                        itemsSource.MoveOffsetAdjustedItem(adjustedOldIndex + i, adjustedNewIndex + i);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    index--;    // Push the index back to the parent
                    int itemLevel = itemsSource.GetLevel(index);
                    var children = item.GetChildren().ToList();

                    // Remove parent element (to remove its children) and add it again (and add children afterwards)
                    itemsSource.RemoveOffsetAdjustedItem(index);    
                    itemsSource.InsertWithLevel(index, item, itemLevel);
                    index++;    // We push the index forward by 1 to be on the first element of the item's children
                    for (int i = 0; i < children.Count; i++)
                    {
                        itemsSource.InsertWithLevel(i + index, children[i], itemLevel + 1);
                    }
                    break;
            }
        }

        /* Helper method used to determine the number of visible items that are prior siblings
         * or children/grand-children of expanded siblings.
         *
         * This is used to determine the correct offset into the InternalItemsSource when adding/removing items
         */
        static int GetChildrenAndGrandChildrenCountOfPriorSiblings(TreeListViewItemsCollection<object?> collection, int startingIndex, int expectedPriorSiblingCount)
        {
            int childrenAndGrandChildrenCount = 0;
            int index = 0;
            int siblingCount = 0;

            // Determine the level expected of siblings (used for comparison)
            int siblingLevel = collection.GetLevel(startingIndex - 1) + 1;

            // Iterate while we haven't:
            //  - Exceeded the expected number of prior siblings, or
            //  - Reached the end of the InternalItemsSource, or
            //  - Reached an item with a level less than the sibling level
            while (siblingCount <= expectedPriorSiblingCount)
            {
                // Bail out if we've reached the end of the itemsSource
                if (startingIndex + index >= collection.Count)
                    break;

                // Bail out if we've reached an item with a level less than the sibling level
                int level = collection.GetLevel(startingIndex + index);
                if (level < siblingLevel)
                    break;

                if (level == siblingLevel)
                {
                    siblingCount++;
                }
                else
                {
                    childrenAndGrandChildrenCount++;
                }
                index++;
            }
            return childrenAndGrandChildrenCount;
        }
    }

    internal void MoveSelectionToParent(TreeListViewItem item)
    {
        if ((IsKeyboardFocused || IsKeyboardFocusWithin)
            && InternalItemsSource is { } itemsSource)
        {
            int index = ItemContainerGenerator.IndexFromContainer(item);
            if (index < 0) return;
            int itemLevel = itemsSource.GetLevel(index);
            for (int i = index; i > 0; i--)
            {
                if (itemsSource.GetLevel(i) == itemLevel - 1)
                {
                    SetSelectedItems(new[] { itemsSource[i] });
                    if (ItemContainerGenerator.ContainerFromIndex(i) is TreeListViewItem container)
                    {
                        container.Focus();
                    }
                    break;
                }
            }
        }
    }
}
