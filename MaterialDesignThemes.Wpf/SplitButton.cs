using Microsoft.Xaml.Behaviors.Core;

namespace MaterialDesignThemes.Wpf;

[TemplatePart(Name = ButtonPartName, Type = typeof(Button))]
[TemplatePart(Name = PopupBoxPartName, Type = typeof(PopupBox))]
public class SplitButton : Button
{
    public const string ButtonPartName = "PART_Button";
    public const string PopupBoxPartName = "PART_PopupBox";

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
        get => (object)GetValue(PopupContentProperty);
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

    internal static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
        nameof(ButtonStyle), typeof(Style), typeof(SplitButton), new PropertyMetadata(default(Style)));

    internal Style ButtonStyle
    {
        get => (Style) GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    internal static readonly DependencyProperty RightButtonMarginProperty = DependencyProperty.Register(
        nameof(RightButtonMargin), typeof(Thickness), typeof(SplitButton), new PropertyMetadata(default(Thickness)));

    internal Thickness RightButtonMargin
    {
        get => (Thickness) GetValue(RightButtonMarginProperty);
        set => SetValue(RightButtonMarginProperty, value);
    }

    internal static readonly DependencyProperty PopupBoxButtonClickedCommandProperty = DependencyProperty.Register(
        nameof(PopupBoxButtonClickedCommand), typeof(ICommand), typeof(SplitButton), new PropertyMetadata(default(ICommand)));

    internal ICommand PopupBoxButtonClickedCommand
    {
        get => (ICommand) GetValue(PopupBoxButtonClickedCommandProperty);
        set => SetValue(PopupBoxButtonClickedCommandProperty, value);
    }

    private Button? _button;
    private PopupBox? _popupBox;

    public SplitButton()
    {
        PopupBoxButtonClickedCommand = new ActionCommand(OpenPopupBox);
    }

    public override void OnApplyTemplate()
    {
        if (_button is not null)
        {
            _button.Click -= ButtonOnClick;
        }

        base.OnApplyTemplate();

        _button = GetTemplateChild(ButtonPartName) as Button;
        _popupBox = GetTemplateChild(PopupBoxPartName) as PopupBox;

        if (_button is not null)
        {
            _button.Click += ButtonOnClick;
        }
    }

    private void ButtonOnClick(object sender, RoutedEventArgs e)
        => RaiseEvent(new RoutedEventArgs(ClickEvent, this));

    private void OpenPopupBox()
    {
        if (_popupBox is not null)
        {
            _popupBox.IsPopupOpen = true;
        }
    }
}
