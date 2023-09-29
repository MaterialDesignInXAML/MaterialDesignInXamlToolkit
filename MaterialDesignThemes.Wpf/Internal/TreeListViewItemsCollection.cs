using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MaterialDesignThemes.Wpf.Internal;

public class TreeListViewItemsCollection<T> : ObservableCollection<T>
{
    private List<int> ItemLevels { get; } = new();

    public TreeListViewItemsCollection(object? wrappedSource)
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

    private int GetPriorNonRootLevelItemsCount(int index, int startingIndex = 0)
    {
        if (index == 0)
            return 0;

        int priorRootLevelItems = 0;
        int priorNonRootLevelItems = 0;
        for (int i = startingIndex; i < ItemLevels.Count; i++)
        {
            int itemLevel = ItemLevels[i];
            if (itemLevel == 0)
            {
                priorRootLevelItems++;
            }
            else
            {
                priorNonRootLevelItems++;
            }

            if (priorRootLevelItems > index)
            {
                // We've have passed the provided index, which means we've found a non-prior root level item; bail out.
                break;
            }
        }
        return priorNonRootLevelItems;
    }

    public int GetLevel(int index)
        => ItemLevels[index];

    public void InsertWithLevel(int index, T item, int level)
    {
        if (level < 0) throw new ArgumentOutOfRangeException(nameof(level), level, "Item level must not be negative");

        //Always allowed to request previous item level + 1 as this is inserting a "child"
        int previousItemLevel = index > 0 ? ItemLevels[index - 1] : -1;
        if (level > previousItemLevel + 1)
        {
            throw new ArgumentOutOfRangeException(nameof(level), level, $"Item level must not be more than one level greater the previous item ({previousItemLevel})");
        }

        int nextItemLevel = index < Count ? ItemLevels[index] : 0;
        if (level < nextItemLevel)
        {
            throw new ArgumentOutOfRangeException(nameof(level), level, $"Item level must not be less than the level item after it ({nextItemLevel})");
        }

        base.InsertItem(index, item);
        ItemLevels.Insert(index, level);
    }

    protected override void RemoveItem(int index)
    {
        int priorNonRootLevelItems = GetPriorNonRootLevelItemsCount(index);
        int adjustedIndex = index + priorNonRootLevelItems;
        RemoveOffsetAdjustedItem(adjustedIndex);
    }

    internal void RemoveOffsetAdjustedItem(int index)
    {
        int currentLevel = ItemLevels[index];
        ItemLevels.RemoveAt(index);
        base.RemoveItem(index);
        while (index < Count && ItemLevels[index] > currentLevel)
        {
            ItemLevels.RemoveAt(index);
            base.RemoveItem(index);
        }
    }

    protected override void InsertItem(int index, T item)
    {
        int priorNonRootLevelItems = GetPriorNonRootLevelItemsCount(index);
        int adjustedIndex = index + priorNonRootLevelItems;
        InsertOffsetAdjustedItem(adjustedIndex, item);
    }

    internal void InsertOffsetAdjustedItem(int index, T item)
    {
        ItemLevels.Insert(index, 0);
        base.InsertItem(index, item);
    }

    protected override void MoveItem(int oldIndex, int newIndex)
    {
        // When moving down, we need to move past the children/grand-children of the item at newIndex so we look for the next root level item.
        int additionalOffset = newIndex > oldIndex ? 1 : 0; 
        int adjustedOldIndex = oldIndex + GetPriorNonRootLevelItemsCount(oldIndex);
        int adjustedNewIndex = newIndex + GetPriorNonRootLevelItemsCount(newIndex + additionalOffset);
        MoveOffsetAdjustedItem(adjustedOldIndex, adjustedNewIndex);
    }

    internal void MoveOffsetAdjustedItem(int oldIndex, int newIndex)
    {
        // Figure out how many items to move (1 + any children/grand-children)
        int itemLevel = ItemLevels[oldIndex];
        int childIndex = oldIndex + 1;
        int childrenCount = 0;
        while (childIndex < Count && ItemLevels[childIndex] > itemLevel)
        {
            childIndex++;
            childrenCount++;
        }

        if (oldIndex < newIndex)
        {
            // Moving down
            // Move children/grand-children first
            int oldChildIndex = oldIndex + 1;
            for (int j = 0; j < childrenCount; j++)
            {
                int newChildIndex = newIndex;
                ItemLevels.MoveItem(oldChildIndex, newChildIndex);
                base.MoveItem(oldChildIndex, newChildIndex);
                
            }

            // Then move the parent
            int newParentIndex = newIndex - childrenCount;
            ItemLevels.MoveItem(oldIndex, newParentIndex);
            base.MoveItem(oldIndex, newParentIndex);
            
        }
        else
        {
            // Moving up
            // Move the parent first
            ItemLevels.MoveItem(oldIndex, newIndex);
            base.MoveItem(oldIndex, newIndex);
            // Then move children/grand-children
            for (int j = 0; j < childrenCount; j++)
            {
                int oldChildIndex = oldIndex + 1 + j;
                int newChildIndex = newIndex + 1 + j;
                ItemLevels.MoveItem(oldChildIndex, newChildIndex);
                base.MoveItem(oldChildIndex, newChildIndex);
            }
        }
    }

    private void ReplaceItem(int index, T item)
    {
        int priorNonRootLevelItems = GetPriorNonRootLevelItemsCount(index);
        int adjustedIndex = index + priorNonRootLevelItems;
        ReplaceOffsetAdjustedItem(adjustedIndex, item);
    }

    internal void ReplaceOffsetAdjustedItem(int index, T item)
    {
        // NOTE: This is slight change of notification behavior. It now fires at least one Remove (possibly also removing children) and one Add notification on the internal collection; probably not an issue.
        int level = GetLevel(index);
        RemoveOffsetAdjustedItem(index);
        InsertWithLevel(index, item, level);
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
                    RemoveAt(e.OldStartingIndex + i);
                }
                break;
            case NotifyCollectionChangedAction.Replace:
                for (int i = 0; i < e.NewItems?.Count; i++)
                {
                    ReplaceItem(e.NewStartingIndex + i, (T)e.NewItems[i]!);
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

internal static class ListExtensions
{
    public static void MoveItem(this IList list, int oldIndex, int newIndex)
    {
        if (oldIndex == newIndex)
            return;
        object item = list[oldIndex]!;
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
    }
}
