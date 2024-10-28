using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors;

public class SmartHintBehavior : Behavior<SmartHint>
{
    public static readonly DependencyProperty YOffsetProperty =
        DependencyProperty.RegisterAttached("YOffset", typeof(double), typeof(SmartHintBehavior), new PropertyMetadata(0.0));
    public static double GetYOffset(DependencyObject obj)
        => (double)obj.GetValue(YOffsetProperty);
    public static void SetYOffset(DependencyObject obj, double value)
        => obj.SetValue(YOffsetProperty, value);

    private void UpdateSmartHintLocationRecalculationTrigger()
    {
        if (AssociatedObject?.FloatingTarget is null) return;

        double yOffset = AssociatedObject.FloatingTarget.TranslatePoint(new Point(0, 0), AssociatedObject).Y;
        AssociatedObject.SetCurrentValue(YOffsetProperty, yOffset);
    }

    private void HintHostOnLayoutUpdated(object? sender, EventArgs e)
        => UpdateSmartHintLocationRecalculationTrigger();

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.LayoutUpdated += HintHostOnLayoutUpdated;
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
        {
            AssociatedObject.LayoutUpdated -= HintHostOnLayoutUpdated;
        }
        base.OnDetaching();
    }
}
