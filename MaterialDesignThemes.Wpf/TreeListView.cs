using System.Collections.Specialized;
using System.Windows.Threading;
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
            if (treeListView.InternalItemsSource != null)
            {
                treeListView.InternalItemsSource.MoveRequested -= treeListView.InternalItemsSourceOnMoveRequested;
            }
            var value = treeListView.InternalItemsSource = new(baseValue);
            value.MoveRequested += treeListView.InternalItemsSourceOnMoveRequested;
            return value;
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
            treeListViewItem.IsExpanded = itemsSource.GetIsExpanded(index);
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
                itemsSource.RemoveChildrenOfOffsetAdjustedItem(index);
            }
        }
    }

    internal void ItemsChildrenChanged(TreeListViewItem item, NotifyCollectionChangedEventArgs e)
    {
        if (item.IsExpanded && InternalItemsSource is { } itemsSource)
        {
            int index = ItemContainerGenerator.IndexFromContainer(item);
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
                    int adjustedOldIndex = index + e.OldStartingIndex + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.OldStartingIndex);
                    
                    int additionalOffset = 0;
                    if (e.OldStartingIndex < e.NewStartingIndex)
                    {
                        // When moving down, we need to move past expanded children/grand-children as well
                        additionalOffset = 1;
                    }
                    int adjustedNewIndex = index + e.NewStartingIndex + GetChildrenAndGrandChildrenCountOfPriorSiblings(itemsSource, index, e.NewStartingIndex + additionalOffset);

                    itemsSource.Move(adjustedOldIndex, adjustedNewIndex);

                    //int adjustedOldIndex = index + e.OldStartingIndex;
                    //int adjustedNewIndex = index + e.NewStartingIndex;
                    // Collect expanded children and grand-children of the items being moved; we need to expand them (and the items themselves) again after the move
                    //List<object?> expandedItems = new();
                    //foreach (object? child in item.GetChildren())
                    //{
                    //    expandedItems.AddRange(GetExpandedChildrenAndGrandChildren(child));
                    //    RemoveChildren(child);
                    //}

                    //for (int i = 0; i < e.NewItems?.Count; i++)
                    //{
                    //    itemsSource.MoveOffsetAdjustedItem(adjustedOldIndex + i, adjustedNewIndex + i);
                    //}

                    //foreach (object? dataItem in expandedItems)
                    //{
                    //    // Kind of a hack!
                    //    // When expanding an item, we need to wait until the item is rendered before expanding its children; thus we push this onto the back of the message pump.
                    //    // DispatcherPriority.Loaded is the highest priority we can go. Using DispatcherPriority.Render (one level higher) will not expand grand-children.
                    //    // TODO: This has a UI impact where the TreeListView will "flicker" shortly and the chevrons wil be in the wrong state for a split second. Perhaps we can optimize further on this...
                    //    Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () =>
                    //    {
                    //        if (ItemContainerGenerator.ContainerFromItem(dataItem) is TreeListViewItem container)
                    //        {
                    //            container.IsExpanded = true;
                    //        }
                    //    });
                    //}
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

    private List<object?> GetExpandedChildrenAndGrandChildren(object? dataItem)
    {
        List<object?> expandedChildren = new();
        if (dataItem is null || ItemContainerGenerator.ContainerFromItem(dataItem) is not TreeListViewItem { IsExpanded: true } container)
            return expandedChildren;

        expandedChildren.Add(dataItem);
        foreach (object? grandChild in container.GetChildren())
        {
            expandedChildren.AddRange(GetExpandedChildrenAndGrandChildren(grandChild));
        }
        return expandedChildren;
    }

    private void RemoveChildren(object? child)
    {
        if (child is null || ItemContainerGenerator.ContainerFromItem(child) is not TreeListViewItem container) return;
        int childIndex = ItemContainerGenerator.IndexFromContainer(container);
        if (childIndex >= 0)
        {
            container.IsExpanded = false;
        }
    }

    /// <summary>
    /// This event handler is invoked when a Move() operation is performed on a root-level item. Needs similar handling as the child collections,
    /// but it is slightly simpler. We can probably do a little better with some code reuse.
    /// </summary>
    private void InternalItemsSourceOnMoveRequested(object? sender, MoveEventArgs e)
    {
        if (InternalItemsSource is not { } itemsSource)
            return;

        // Collect expanded children and grand-children of the items being moved; we need to expand them (and the items themselves) again after the move
        List<object?> expandedItems = new();
        var itemsSourceCopy = new List<object?>(ItemsSource.OfType<object?>());
        foreach (object? child in itemsSourceCopy)
        {
            expandedItems.AddRange(GetExpandedChildrenAndGrandChildren(child));
            RemoveChildren(child);
        }

        itemsSource.MoveOffsetAdjustedItem(e.OldIndex, e.NewIndex);

        foreach (object? dataItem in expandedItems)
        {
            // Kind of a hack!
            // When expanding an item, we need to wait until the item is rendered before expanding its children; thus we push this onto the back of the message pump.
            // DispatcherPriority.Loaded is the highest priority we can go. Using DispatcherPriority.Render (one level higher) will not expand grand-children.
            // TODO: This has a UI impact where the TreeListView will "flicker" shortly and the chevrons wil be in the wrong state for a split second. Perhaps we can optimize further on this...
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () =>
            {
                if (ItemContainerGenerator.ContainerFromItem(dataItem) is TreeListViewItem container)
                {
                    container.IsExpanded = true;
                }
            });
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
