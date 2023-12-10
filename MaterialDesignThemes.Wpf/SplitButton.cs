namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = PopupBoxPartName, Type = typeof(PopupBox))]
[TemplatePart(Name = RightButtonPartName, Type = typeof(Button))]
public class SplitButton : Button
{
    public const string PopupBoxPartName = "PART_PopupBox";
    public const string RightButtonPartName = "PART_RightButton";

    static SplitButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
    }

    public static readonly DependencyProperty PopupPlacementModeProperty = DependencyProperty.Register(
        nameof(PopupPlacementMode), typeof(PopupBoxPlacementMode), typeof(SplitButton), new PropertyMetadata(default(PopupBoxPlacementMode)));

    public PopupBoxPlacementMode PopupPlacementMode
    {
        get => (PopupBoxPlacementMode) GetValue(PopupPlacementModeProperty);
        set => SetValue(PopupPlacementModeProperty, value);
    }

    public static readonly DependencyProperty PopupElevationProperty = DependencyProperty.Register(
        nameof(PopupElevation), typeof(Elevation), typeof(SplitButton), new PropertyMetadata(default(Elevation)));

    public Elevation PopupElevation
    {
        get => (Elevation)GetValue(PopupElevationProperty);
        set => SetValue(PopupElevationProperty, value);
    }

    public static readonly DependencyProperty PopupUniformCornerRadiusProperty = DependencyProperty.Register(
        nameof(PopupUniformCornerRadius), typeof(double), typeof(SplitButton), new PropertyMetadata(default(double)));

    public double PopupUniformCornerRadius
    {
        get => (double)GetValue(PopupUniformCornerRadiusProperty);
        set => SetValue(PopupUniformCornerRadiusProperty, value);
    }

    public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
        nameof(PopupContent), typeof(object), typeof(SplitButton), new PropertyMetadata(default(object)));

    public object PopupContent
    {
        get => GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly DependencyProperty PopupContentStringFormatProperty = DependencyProperty.Register(
        nameof(PopupContentStringFormat), typeof(string), typeof(SplitButton), new PropertyMetadata(default(string)));

    public string PopupContentStringFormat
    {
        get => (string)GetValue(PopupContentStringFormatProperty);
        set => SetValue(PopupContentStringFormatProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
        nameof(PopupContentTemplate), typeof(DataTemplate), typeof(SplitButton), new PropertyMetadata(default(DataTemplate)));

    public DataTemplate PopupContentTemplate
    {
        get => (DataTemplate)GetValue(PopupContentTemplateProperty);
        set => SetValue(PopupContentTemplateProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateSelectorProperty = DependencyProperty.Register(
        nameof(PopupContentTemplateSelector), typeof(DataTemplateSelector), typeof(SplitButton), new PropertyMetadata(default(DataTemplateSelector)));

    public DataTemplateSelector PopupContentTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(PopupContentTemplateSelectorProperty);
        set => SetValue(PopupContentTemplateSelectorProperty, value);
    }

    public static readonly DependencyProperty SplitContentProperty = DependencyProperty.Register(
        nameof(SplitContent), typeof(object), typeof(SplitButton), new PropertyMetadata(default(object)));

    public object SplitContent
    {
        get => GetValue(SplitContentProperty);
        set => SetValue(SplitContentProperty, value);
    }

    public static readonly DependencyProperty SplitContentStringFormatProperty = DependencyProperty.Register(
        nameof(SplitContentStringFormat), typeof(string), typeof(SplitButton), new PropertyMetadata(default(string)));

    public string SplitContentStringFormat
    {
        get => (string)GetValue(SplitContentStringFormatProperty);
        set => SetValue(SplitContentStringFormatProperty, value);
    }

    public static readonly DependencyProperty SplitContentTemplateProperty = DependencyProperty.Register(
        nameof(SplitContentTemplate), typeof(DataTemplate), typeof(SplitButton), new PropertyMetadata(default(DataTemplate)));

    public DataTemplate SplitContentTemplate
    {
        get => (DataTemplate)GetValue(SplitContentTemplateProperty);
        set => SetValue(SplitContentTemplateProperty, value);
    }

    public static readonly DependencyProperty SplitContentTemplateSelectorProperty = DependencyProperty.Register(
        nameof(SplitContentTemplateSelector), typeof(DataTemplateSelector), typeof(SplitButton), new PropertyMetadata(default(DataTemplateSelector)));

    public DataTemplateSelector SplitContentTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(SplitContentTemplateSelectorProperty);
        set => SetValue(SplitContentTemplateSelectorProperty, value);
    }

    public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
        nameof(ButtonStyle), typeof(Style), typeof(SplitButton), new PropertyMetadata(default(Style)));

    public Style ButtonStyle
    {
        get => (Style) GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    private PopupBox? _popupBox;
    private Button? _rightButton;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _popupBox = GetTemplateChild(PopupBoxPartName) as PopupBox;
        _rightButton = GetTemplateChild(RightButtonPartName) as Button;

        if (_rightButton is not null)
        {
            WeakEventManager<Button, RoutedEventArgs>.RemoveHandler(_rightButton, nameof(Click), OpenPopupBox);
            WeakEventManager<Button, RoutedEventArgs>.AddHandler(_rightButton, nameof(Click), OpenPopupBox);
        }

        void OpenPopupBox(object? sender, RoutedEventArgs e)
        {
            if (_popupBox is not null)
            {
                _popupBox.IsPopupOpen = true;
                e.Handled = true;
            }
        }
    }
}
