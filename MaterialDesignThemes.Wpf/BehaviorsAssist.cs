using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf;

/* This class is a copy of the StylizedBehaviors.cs from the MahApps.Metro repository with very few changes to it:
 * https://github.com/MahApps/MahApps.Metro/blob/6bce9f8733318a7f1f50ff112f834e917e766ae8/src/MahApps.Metro/Behaviors/StylizedBehaviors.cs
 */
public static class BehaviorsAssist
{
    private static readonly DependencyProperty OriginalBehaviorProperty = DependencyProperty.RegisterAttached(
        "OriginalBehavior", typeof(Behavior), typeof(BehaviorsAssist), new UIPropertyMetadata(null));
    private static void SetOriginalBehavior(DependencyObject obj, Behavior? value) => obj.SetValue(OriginalBehaviorProperty, value);
    private static Behavior? GetOriginalBehavior(DependencyObject obj) => (Behavior?)obj.GetValue(OriginalBehaviorProperty);

    public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
        "Behaviors", typeof(BehaviorCollection), typeof(BehaviorsAssist), new FrameworkPropertyMetadata(null, OnPropertyChanged));
    public static void SetBehaviors(DependencyObject uie, BehaviorCollection? value) => uie.SetValue(BehaviorsProperty, value);
    public static BehaviorCollection? GetBehaviors(DependencyObject uie) => (BehaviorCollection?)uie.GetValue(BehaviorsProperty);

    private static void OnPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
    {
        if (dpo is not FrameworkElement frameworkElement)
        {
            return;
        }

        var newBehaviors = e.NewValue as BehaviorCollection;
        var oldBehaviors = e.OldValue as BehaviorCollection;
        if (newBehaviors == oldBehaviors)
        {
            return;
        }

        var itemBehaviors = Interaction.GetBehaviors(frameworkElement);
        frameworkElement.Unloaded -= FrameworkElementUnloaded;
        if (oldBehaviors != null)
        {
            foreach (var behavior in oldBehaviors)
            {
                int index = GetIndexOf(itemBehaviors, behavior);
                if (index >= 0)
                {
                    itemBehaviors.RemoveAt(index);
                }
            }
        }

        if (newBehaviors != null)
        {
            foreach (var behavior in newBehaviors)
            {
                int index = GetIndexOf(itemBehaviors, behavior);
                if (index < 0)
                {
                    var clone = (Behavior)behavior.Clone();
                    SetOriginalBehavior(clone, behavior);
                    itemBehaviors.Add(clone);
                }
            }
        }

        if (itemBehaviors.Count > 0)
        {
            frameworkElement.Unloaded += FrameworkElementUnloaded;
        }
    }

    private static void FrameworkElementUnloaded(object sender, RoutedEventArgs e)
    {
        // BehaviorCollection doesn't call Detach, so we do this
        if (sender is not FrameworkElement frameworkElement)
        {
            return;
        }

        var itemBehaviors = Interaction.GetBehaviors(frameworkElement);
        foreach (var behavior in itemBehaviors)
        {
            behavior.Detach();
        }

        frameworkElement.Loaded += FrameworkElementLoaded;
    }

    private static void FrameworkElementLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement frameworkElement)
        {
            return;
        }

        frameworkElement.Loaded -= FrameworkElementLoaded;
        var itemBehaviors = Interaction.GetBehaviors(frameworkElement);
        foreach (var behavior in itemBehaviors)
        {
            behavior.Attach(frameworkElement);
        }
    }

    private static int GetIndexOf(Microsoft.Xaml.Behaviors.BehaviorCollection itemBehaviors, Behavior behavior)
    {
        int index = -1;

        var originalBehavior = GetOriginalBehavior(behavior);

        for (int i = 0; i < itemBehaviors.Count; i++)
        {
            var currentBehavior = itemBehaviors[i];
            if (currentBehavior == behavior || currentBehavior == originalBehavior)
            {
                index = i;
                break;
            }

            var currentOriginalBehavior = GetOriginalBehavior(currentBehavior);
            if (currentOriginalBehavior == behavior || currentOriginalBehavior == originalBehavior)
            {
                index = i;
                break;
            }
        }

        return index;
    }
}
