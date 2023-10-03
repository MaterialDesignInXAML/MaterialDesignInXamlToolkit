using System.Collections.Specialized;

namespace MaterialDesignThemes.Wpf;

public class TreeListViewItemContentPresenter : ContentPresenter, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

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
        HasChildren = Children?.Any() == true;
        CollectionChanged?.Invoke(this, e);
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
