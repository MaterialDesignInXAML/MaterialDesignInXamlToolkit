namespace MaterialDesignThemes.Wpf;

public class MultiSelectTreeView : ListView
{
    protected override DependencyObject GetContainerForItemOverride()
    {
        return new MultiSelectTreeViewItem();
    }

    protected override bool IsItemItsOwnContainerOverride(object? item) => item is MultiSelectTreeViewItem;
}

[TemplatePart(Name = "ItemsHost", Type = typeof(ItemsPresenter))]
[TemplatePart(Name = "PART_Header", Type = typeof(FrameworkElement))]
[TemplateVisualState(GroupName = "ExpansionStates", Name = ExpandedStateName)]
[TemplateVisualState(GroupName = "ExpansionStates", Name = CollapsedStateName)]
public class MultiSelectTreeViewItem : HeaderedItemsControl
{
    public const string ExpandedStateName = "Expanded";
    public const string CollapsedStateName = "Collapsed";

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool), typeof(MultiSelectTreeViewItem), new PropertyMetadata(false, OnIsExpandedChanged));

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element)
        {
            if (e.NewValue is bool isExpanded && isExpanded)
            {
                VisualStateManager.GoToState(element, ExpandedStateName, !TransitionAssist.GetDisableTransitions(d));
            }
            else
            {
                VisualStateManager.GoToState(element, CollapsedStateName, !TransitionAssist.GetDisableTransitions(d));
            }

        }
    }

    public MultiSelectTreeViewItem()
    {
        
    }
}

