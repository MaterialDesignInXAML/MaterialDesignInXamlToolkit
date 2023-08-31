using System.ComponentModel;

namespace MaterialDesignThemes.Wpf;

public class MultiSelectTreeView : ListView
{
    public MultiSelectTreeView()
    {
        SelectionMode = SelectionMode.Single;
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new MultiSelectTreeViewItem();
    }

    protected override bool IsItemItsOwnContainerOverride(object? item) => item is MultiSelectTreeViewItem;

    protected internal virtual void NotifyListItemClicked(MultiSelectTreeViewItem item, MouseButton mouseButton)
    {
        // When a ListBoxItem is left clicked, we should take capture
        // so we can auto scroll through the list.
        if (mouseButton == MouseButton.Left && Mouse.Captured != this)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            //SetInitialMousePosition(); // Start tracking mouse movement
        }

        switch (SelectionMode)
        {
            case SelectionMode.Single:
                {
                    if (!item.IsSelected)
                    {
                        item.SetCurrentValue(IsSelectedProperty, true);
                    }
                    else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {
                        item.SetCurrentValue(IsSelectedProperty, false);
                    }

                    //UpdateAnchorAndActionItem(ItemInfoFromContainer(item));
                }
                break;

            case SelectionMode.Multiple:
                item.SetCurrentValue(IsSelectedProperty, !item.IsSelected);
                break;

            case SelectionMode.Extended:
                // Extended selection works only with Left mouse button
                //if (mouseButton == MouseButton.Left)
                //{
                //    if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
                //    {
                //        MakeAnchorSelection(item, false);
                //    }
                //    else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                //    {
                //        MakeToggleSelection(item);
                //    }
                //    else if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                //    {
                //        MakeAnchorSelection(item, true);
                //    }
                //    else
                //    {
                //        MakeSingleSelection(item);
                //    }
                //}
                //else if (mouseButton == MouseButton.Right) // Right mouse button
                //{
                //    // Shift or Control combination should not trigger any action
                //    // If only Right mouse button is pressed we should move the anchor
                //    // and select the item only if element under the mouse is not selected
                //    if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == 0)
                //    {
                //        if (item.IsSelected)
                //            UpdateAnchorAndActionItem(ItemInfoFromContainer(item));
                //        else
                //            MakeSingleSelection(item);
                //    }
                //}

                break;
        }
    }
}

[TemplatePart(Name = "ItemsHost", Type = typeof(ItemsPresenter))]
[TemplatePart(Name = "PART_Header", Type = typeof(FrameworkElement))]
[TemplateVisualState(GroupName = "ExpansionStates", Name = ExpandedStateName)]
[TemplateVisualState(GroupName = "ExpansionStates", Name = CollapsedStateName)]
[TemplateVisualState(GroupName = "SelectionStates", Name = SelectedStateName)]
[TemplateVisualState(GroupName = "SelectionStates", Name = UnselectedStateName)]
public class MultiSelectTreeViewItem : HeaderedItemsControl
{
    public const string ExpandedStateName = "Expanded";
    public const string CollapsedStateName = "Collapsed";
    public const string SelectedStateName = "Selected";
    public const string UnselectedStateName = "Unselected";

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(MultiSelectTreeViewItem), new PropertyMetadata(false, OnIsExpandedChanged));

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

    /// <summary>
    ///     Indicates whether this ListBoxItem is selected.
    /// </summary>
    public static readonly DependencyProperty IsSelectedProperty =
            Selector.IsSelectedProperty.AddOwner(typeof(MultiSelectTreeViewItem),
                    new FrameworkPropertyMetadata(false,
                            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                            new PropertyChangedCallback(OnIsSelectedChanged)));

    [Bindable(true), Category("Appearance")]
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }


    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MultiSelectTreeViewItem element)
        {
            bool isSelected = (bool)e.NewValue;
            if (isSelected)
            {
                VisualStateManager.GoToState(element, SelectedStateName, !TransitionAssist.GetDisableTransitions(d));
            }
            else
            {
                VisualStateManager.GoToState(element, UnselectedStateName, !TransitionAssist.GetDisableTransitions(d));
            }

            //Selector? parentSelector = element.ParentSelector;
            //if (parentSelector != null)
            //{
            //    parentSelector.RaiseIsSelectedChangedAutomationEvent(element, isSelected);
            //}

            element.HandleIsSelectedChanged(isSelected);
        }

        
    }

    /// <summary>
    ///     Event indicating that the IsSelected property is now true.
    /// </summary>
    /// <param name="e">Event arguments</param>
    protected virtual void OnSelected() => HandleIsSelectedChanged(true);

    /// <summary>
    ///     Event indicating that the IsSelected property is now false.
    /// </summary>
    /// <param name="e">Event arguments</param>
    protected virtual void OnUnselected() => HandleIsSelectedChanged(false);

    private void HandleIsSelectedChanged(bool newValue)
    {
        if (newValue)
        {
            RaiseEvent(new RoutedEventArgs(Selector.SelectedEvent, this));
        }
        else
        {
            RaiseEvent(new RoutedEventArgs(Selector.UnselectedEvent, this));
        }
    }


    /// <summary>
    ///     Raised when the item's IsSelected property becomes true.
    /// </summary>
    public static readonly RoutedEvent SelectedEvent = Selector.SelectedEvent.AddOwner(typeof(MultiSelectTreeViewItem));

    /// <summary>
    ///     Raised when the item's IsSelected property becomes true.
    /// </summary>
    public event RoutedEventHandler Selected
    {
        add => AddHandler(SelectedEvent, value);
        remove => RemoveHandler(SelectedEvent, value);
    }

    /// <summary>
    ///     Raised when the item's IsSelected property becomes false.
    /// </summary>
    public static readonly RoutedEvent UnselectedEvent = Selector.UnselectedEvent.AddOwner(typeof(ListBoxItem));

    /// <summary>
    ///     Raised when the item's IsSelected property becomes false.
    /// </summary>
    public event RoutedEventHandler Unselected
    {
        add => AddHandler(UnselectedEvent, value);
        remove => RemoveHandler(UnselectedEvent, value);
    }

    public MultiSelectTreeViewItem()
    {
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (!e.Handled)
        {
            // turn this into a Selector.ItemClicked thing?
            e.Handled = true;
            HandleMouseButtonDown(MouseButton.Left);
        }
        base.OnMouseLeftButtonDown(e);
    }

    private void HandleMouseButtonDown(MouseButton mouseButton)
    {
        if (Focus())
        {
            if (ParentTreeView is { } treeView)
            {
                treeView.NotifyListItemClicked(this, mouseButton);
            }
        }
    }


    /// <summary>
    ///     Walks up the parent chain of TreeViewItems to the top TreeView.
    /// </summary>
    internal MultiSelectTreeView? ParentTreeView
    {
        get
        {
            ItemsControl? parent = ParentItemsControl;
            while (parent != null)
            {
                if (parent is MultiSelectTreeView tv)
                {
                    return tv;
                }

                parent = ItemsControlFromItemContainer(parent);
            }

            return null;
        }
    }

    /// <summary>
    ///     Returns the immediate parent TreeViewItem. Null if the parent is a TreeView.
    /// </summary>
    internal TreeViewItem? ParentTreeViewItem => ParentItemsControl as TreeViewItem;

    /// <summary>
    ///     Returns the immediate parent ItemsControl.
    /// </summary>
    internal ItemsControl? ParentItemsControl => ItemsControlFromItemContainer(this);

}

