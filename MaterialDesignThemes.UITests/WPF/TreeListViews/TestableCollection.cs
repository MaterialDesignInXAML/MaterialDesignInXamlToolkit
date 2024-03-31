using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

public class TestableCollection<T> : ObservableCollection<T>
{
    private int _blockCollectionChanges;

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (Interlocked.CompareExchange(ref _blockCollectionChanges, 0, 0) == 0)
        {
            base.OnCollectionChanged(e);
        }
    }

    public void ReplaceAllItems(params T[] newItems)
    {
        Interlocked.Exchange(ref _blockCollectionChanges, 1);

        Clear();
        foreach (T newItem in newItems)
        {
            Add(newItem);
        }

        Interlocked.Exchange(ref _blockCollectionChanges, 0);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
