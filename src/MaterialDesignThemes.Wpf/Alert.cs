using System.ComponentModel;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Displays a short message, tinted with the semantic color matching its <see cref="Severity"/>.
/// </summary>
[TemplatePart(Name = CloseButtonPartName, Type = typeof(ButtonBase))]
public class Alert : ContentControl
{
    public const string CloseButtonPartName = "PART_CloseButton";

    private ButtonBase? _closeButton;

    static Alert()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Alert), new FrameworkPropertyMetadata(typeof(Alert)));
    }

    public Severity Severity
    {
        get => (Severity)GetValue(SeverityProperty);
        set => SetValue(SeverityProperty, value);
    }

    public static readonly DependencyProperty SeverityProperty =
        DependencyProperty.Register(nameof(Severity), typeof(Severity), typeof(Alert), new PropertyMetadata(Severity.None));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Alert), new PropertyMetadata(new CornerRadius(4)));

    /// <summary>
    /// Optional bold heading shown above the content. Null hides it.
    /// </summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(object), typeof(Alert), new PropertyMetadata(null));

    /// <summary>
    /// The icon shown before the content. Defaults to an icon matching <see cref="Severity"/>; null hides the icon.
    /// </summary>
    public PackIconKind? IconKind
    {
        get => (PackIconKind?)GetValue(IconKindProperty);
        set => SetValue(IconKindProperty, value);
    }

    public static readonly DependencyProperty IconKindProperty =
        DependencyProperty.Register(nameof(IconKind), typeof(PackIconKind?), typeof(Alert), new PropertyMetadata(null));

    /// <summary>
    /// Shows a close button; clicking it raises <see cref="CloseClick"/>. Hiding the alert is up to the handler.
    /// </summary>
    public bool ShowCloseButton
    {
        get => (bool)GetValue(ShowCloseButtonProperty);
        set => SetValue(ShowCloseButtonProperty, value);
    }

    public static readonly DependencyProperty ShowCloseButtonProperty =
        DependencyProperty.Register(nameof(ShowCloseButton), typeof(bool), typeof(Alert), new PropertyMetadata(false));

    [Category("Behavior")]
    public event RoutedEventHandler CloseClick
    {
        add => AddHandler(CloseClickEvent, value);
        remove => RemoveHandler(CloseClickEvent, value);
    }

    public static readonly RoutedEvent CloseClickEvent =
        EventManager.RegisterRoutedEvent(nameof(CloseClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Alert));

    public override void OnApplyTemplate()
    {
        _closeButton?.Click -= CloseButtonOnClick;

        _closeButton = GetTemplateChild(CloseButtonPartName) as ButtonBase;

        _closeButton?.Click += CloseButtonOnClick;

        base.OnApplyTemplate();
    }

    private void CloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(CloseClickEvent, this));
        e.Handled = true;
    }
}
