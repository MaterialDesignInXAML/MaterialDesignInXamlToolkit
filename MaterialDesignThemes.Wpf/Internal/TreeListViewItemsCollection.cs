using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MaterialDesignThemes.Wpf.Internal;

public class TreeListViewItemsCollection : ObservableCollection<object?>
{
    private List<int> ItemLevels { get; } = new();
    private List<bool> ItemIsExpanded { get; } = new();

    public TreeListViewItemsCollection(object? wrappedSource)
    {
        if (wrappedSource is IEnumerable items)
        {
            foreach (object? item in items)
            {
                Add(item);
            }
        }
        if (wrappedSource is INotifyCollectionChanged newCollectionChanged)
        {
            CollectionChangedEventManager.AddHandler(newCollectionChanged, ItemsSource_CollectionChanged);
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
                // We've have passed the provided index, which means we've found a non-prior root parentLevel item; bail out.
                break;
            }
        }
        return priorNonRootLevelItems;
    }

    public int GetLevel(int index)
        => ItemLevels[index];

    public bool GetIsExpanded(int index)
        => ItemIsExpanded[index];

    public void SetIsExpanded(int index, bool isExpanded)
        => ItemIsExpanded[index] = isExpanded;

    public void InsertWithLevel(int index, object? item, int level)
    {
        if (level < 0) throw new ArgumentOutOfRangeException(nameof(level), level, "Item level must not be negative");

        //Always allowed to request previous item parentLevel + 1 as this is inserting a "child"
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

        InternalInsertItem(index, item, level);
        if (previousItemLevel >= 0 && previousItemLevel == level - 1)
        {
            ItemIsExpanded[index - 1] = true;
        }
    }

    public object? GetParent(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
        int level = ItemLevels[index];
        if (level == 0) return null;
        for (int i = index - 1; i >= 0; i--)
        {
            if (ItemLevels[i] == level - 1)
            {
                return this[i];
            }
        }
        return null;
    }

    public IEnumerable<int> GetDirectChildrenIndexes(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

        return GetDirectChildrenIndexesImplementation(index);

        IEnumerable<int> GetDirectChildrenIndexesImplementation(int index)
        {
            int parentLevel = ItemLevels[index];

            for (int i = index + 1; i < ItemLevels.Count; i++)
            {
                int level = ItemLevels[i];
                if (level == parentLevel + 1)
                {
                    yield return i;
                }
                if (level <= parentLevel)
                {
                    yield break;
                }
            }
        }
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
        InternalRemoveItem(index);
        while (index < Count && ItemLevels[index] > currentLevel)
        {
            InternalRemoveItem(index);
        }
    }

    internal void RemoveChildrenOfOffsetAdjustedItem(int index)
    {
        int currentLevel = ItemLevels[index];
        if (index + 1 >= Count || ItemLevels[index + 1] < currentLevel + 1)
            return;

        ItemIsExpanded[index] = false;

        index++;
        InternalRemoveItem(index);
        while (index < Count && ItemLevels[index] > currentLevel)
        {
            InternalRemoveItem(index);
        }
    }

    protected override void InsertItem(int index, object? item)
    {
        int priorNonRootLevelItems = GetPriorNonRootLevelItemsCount(index);
        index += priorNonRootLevelItems;
        InternalInsertItem(index, item, 0);
    }

    protected override void MoveItem(int oldIndex, int newIndex)
        => MoveOffsetAdjustedItem(oldIndex, newIndex);

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

        int insertLevel = ItemLevels[newIndex];
        int insertIndex = newIndex;
        while (insertIndex + 1 < Count && ItemLevels[insertIndex + 1] > insertLevel)
        {
            insertIndex++;
        }

        if (oldIndex < newIndex)
        {
            // Moving down
            // Move children/grand-children first
            int oldChildIndex = oldIndex + 1;
            for (int j = 0; j < childrenCount; j++)
            {
                InternalMoveItem(oldChildIndex, insertIndex);
            }

            // Then move the parent
            InternalMoveItem(oldIndex, insertIndex - childrenCount);
        }
        else
        {
            // Moving up
            // Move the parent first
            InternalMoveItem(oldIndex, newIndex);
            // Then move children/grand-children
            for (int j = 0; j < childrenCount; j++)
            {
                int oldChildIndex = oldIndex + 1 + j;
                int newChildIndex = newIndex + 1 + j;
                InternalMoveItem(oldChildIndex, newChildIndex);
            }
        }
    }

    private void InternalInsertItem(int index, object? item, int level)
    {
        ItemIsExpanded.Insert(index, false);
        ItemLevels.Insert(index, level);
        base.InsertItem(index, item);
    }

    private void InternalRemoveItem(int index)
    {
        ItemIsExpanded.RemoveAt(index);
        ItemLevels.RemoveAt(index);
        base.RemoveItem(index);
    }

    private void InternalMoveItem(int oldIndex, int newIndex)
    {
        ItemIsExpanded.MoveItem(oldIndex, newIndex);
        ItemLevels.MoveItem(oldIndex, newIndex);
        base.MoveItem(oldIndex, newIndex);
    }

    internal void ReplaceOffsetAdjustedItem(int index, object? item)
    {
        // NOTE: This is slight change of notification behavior. It now fires at least one Remove (possibly also removing children) and one Add notification on the internal collection; probably not an issue.
        int level = GetLevel(index);
        RemoveChildrenOfOffsetAdjustedItem(index);
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
                    Insert(e.NewStartingIndex + i, e.NewItems[i]!);
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
                    int newIndex = GetAbsoluteIndex(e.NewStartingIndex + i);
                    if (newIndex >= 0)
                    {
                        ReplaceOffsetAdjustedItem(newIndex, e.NewItems[i]!);
                    }
                }
                break;
            case NotifyCollectionChangedAction.Move:
                for (int i = 0; i < e.NewItems?.Count; i++)
                {
                    int oldIndex = GetAbsoluteIndex(e.OldStartingIndex + i);
                    int newIndex = GetAbsoluteIndex(e.NewStartingIndex + i);
                    if (oldIndex >= 0 && newIndex >= 0)
                    {
                        Move(oldIndex, newIndex);
                    }
                }
                break;
            case NotifyCollectionChangedAction.Reset:
                Clear();
                ItemLevels.Clear();
                foreach (object? item in sender as IEnumerable ?? Enumerable.Empty<object?>())
                {
                    Add(item);
                }
                break;
        }

        int GetAbsoluteIndex(int relativeIndex)
        {
            for(int i = 0; i < ItemLevels.Count; i++)
            {
                if (ItemLevels[i] == 0)
                {
                    relativeIndex--;
                }
                if (relativeIndex < 0) return i;
            }
            return -1;
        }
    }
}

file static class ListExtensions
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

public class MoveEventArgs : EventArgs
{
    public int OldIndex { get; }
    public int NewIndex { get; }

    public MoveEventArgs(int oldIndex, int newIndex)
    {
        OldIndex = oldIndex;
        NewIndex = newIndex;
    }
}
