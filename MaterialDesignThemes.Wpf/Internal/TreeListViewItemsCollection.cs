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

    public int GetLevel(int index)
        => ItemLevels[index];

    public void Insert(int index, T item, int level)
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

        Insert(index, item);
        ItemLevels[index] = level;
    }

    protected override void RemoveItem(int index)
    {
        int currentLevel = ItemLevels[index];
        ItemLevels.RemoveAt(index);
        base.RemoveItem(index);
        while (index < Count && ItemLevels[index] > currentLevel)
        {
            RemoveItem(index);
        }
    }

    protected override void InsertItem(int index, T item)
    {
        ItemLevels.Insert(index, 0);
        base.InsertItem(index, item);
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
