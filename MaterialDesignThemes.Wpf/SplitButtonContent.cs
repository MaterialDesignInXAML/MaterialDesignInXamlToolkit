using Microsoft.Xaml.Behaviors.Core;

namespace MaterialDesignThemes.Wpf;

public class SplitButtonContent : ContentControl
{
    static SplitButtonContent() => DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButtonContent), new FrameworkPropertyMetadata(typeof(SplitButtonContent)));

    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
        nameof(IsOpen), typeof(bool), typeof(SplitButtonContent), new PropertyMetadata(default(bool)));

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public static readonly DependencyProperty PopupPlacementTargetProperty = DependencyProperty.Register(
        nameof(PopupPlacementTarget), typeof(UIElement), typeof(SplitButtonContent), new PropertyMetadata(default(UIElement)));

    public UIElement PopupPlacementTarget
    {
        get => (UIElement) GetValue(PopupPlacementTargetProperty);
        set => SetValue(PopupPlacementTargetProperty, value);
    }

    public static readonly DependencyProperty PopupElevationProperty = DependencyProperty.Register(
        nameof(PopupElevation), typeof(Elevation), typeof(SplitButtonContent), new PropertyMetadata(default(Elevation)));

    public Elevation PopupElevation
    {
        get => (Elevation)GetValue(PopupElevationProperty);
        set => SetValue(PopupElevationProperty, value);
    }

    public static readonly DependencyProperty PopupUniformCornerRadiusProperty = DependencyProperty.Register(
        nameof(PopupUniformCornerRadius), typeof(double), typeof(SplitButtonContent), new PropertyMetadata(default(double)));

    public double PopupUniformCornerRadius
    {
        get => (double)GetValue(PopupUniformCornerRadiusProperty);
        set => SetValue(PopupUniformCornerRadiusProperty, value);
    }

    public static readonly DependencyProperty PopupContentStyleProperty = DependencyProperty.Register(
        nameof(PopupContentStyle), typeof(Style), typeof(SplitButtonContent), new PropertyMetadata(default(Style)));

    public Style PopupContentStyle
    {
        get => (Style)GetValue(PopupContentStyleProperty);
        set => SetValue(PopupContentStyleProperty, value);
    }

    public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
        nameof(PopupContent), typeof(object), typeof(SplitButtonContent), new PropertyMetadata(default(object)));

    public object PopupContent
    {
        get => (object)GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
        nameof(PopupContentTemplate), typeof(DataTemplate), typeof(SplitButtonContent), new PropertyMetadata(default(DataTemplate)));

    public DataTemplate PopupContentTemplate
    {
        get => (DataTemplate)GetValue(PopupContentTemplateProperty);
        set => SetValue(PopupContentTemplateProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateSelectorProperty = DependencyProperty.Register(
        nameof(PopupContentTemplateSelector), typeof(DataTemplateSelector), typeof(SplitButtonContent), new PropertyMetadata(default(DataTemplateSelector)));

    public DataTemplateSelector PopupContentTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(PopupContentTemplateSelectorProperty);
        set => SetValue(PopupContentTemplateSelectorProperty, value);
    }

    public ICommand OpenCommand { get; }

    public SplitButtonContent() => OpenCommand = new ActionCommand(() => IsOpen = true);
}
