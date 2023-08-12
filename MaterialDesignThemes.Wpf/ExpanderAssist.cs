using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public enum ExpanderPosition
{
    Start,
    End
}

public static class ExpanderAssist
{
    private static readonly Thickness DefaultHorizontalHeaderPadding = new Thickness(24, 12, 24, 12);
    private static readonly Thickness DefaultVerticalHeaderPadding = new Thickness(12, 24, 12, 24);

    #region AttachedProperty : HorizontalHeaderPaddingProperty
    public static readonly DependencyProperty HorizontalHeaderPaddingProperty
        = DependencyProperty.RegisterAttached("HorizontalHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
            new FrameworkPropertyMetadata(DefaultHorizontalHeaderPadding, FrameworkPropertyMetadataOptions.Inherits));

    public static Thickness GetHorizontalHeaderPadding(Expander element)
        => (Thickness)element.GetValue(HorizontalHeaderPaddingProperty);
    public static void SetHorizontalHeaderPadding(Expander element, Thickness value)
        => element.SetValue(HorizontalHeaderPaddingProperty, value);
    #endregion

    #region AttachedProperty : VerticalHeaderPaddingProperty
    public static readonly DependencyProperty VerticalHeaderPaddingProperty
        = DependencyProperty.RegisterAttached("VerticalHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
            new FrameworkPropertyMetadata(DefaultVerticalHeaderPadding, FrameworkPropertyMetadataOptions.Inherits));

    public static Thickness GetVerticalHeaderPadding(Expander element)
        => (Thickness)element.GetValue(VerticalHeaderPaddingProperty);
    public static void SetVerticalHeaderPadding(Expander element, Thickness value)
        => element.SetValue(VerticalHeaderPaddingProperty, value);
    #endregion

    #region AttachedProperty : HeaderFontSizeProperty
    public static readonly DependencyProperty HeaderFontSizeProperty
        = DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(ExpanderAssist),
            new FrameworkPropertyMetadata(15.0));

    public static double GetHeaderFontSize(Expander element)
        => (double)element.GetValue(HeaderFontSizeProperty);
    public static void SetHeaderFontSize(Expander element, double value)
        => element.SetValue(HeaderFontSizeProperty, value);
    #endregion

    #region AttachedProperty : HeaderBackgroundProperty
    public static readonly DependencyProperty HeaderBackgroundProperty
        = DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(ExpanderAssist));

    public static Brush? GetHeaderBackground(Expander element)
        => (Brush?)element.GetValue(HeaderBackgroundProperty);
    public static void SetHeaderBackground(Expander element, Brush? value)
        => element.SetValue(HeaderBackgroundProperty, value);
    #endregion

    #region AttachedProperty : ExpandedIconProperty
    public static readonly DependencyProperty ExpandedIconProperty
        = DependencyProperty.RegisterAttached("ExpandedIcon", typeof(object), typeof(ExpanderAssist));

    public static object? GetExpandedIcon(Expander element)
        => (object?)element.GetValue(ExpandedIconProperty);
    public static void SetExpandedIcon(Expander element, object? value)
        => element.SetValue(ExpandedIconProperty, value);
    #endregion

    #region AttachedProperty : ExpanderCollapcedIconProperty
    public static readonly DependencyProperty CollapcedIconProperty
        = DependencyProperty.RegisterAttached("CollapcedIcon", typeof(object), typeof(ExpanderAssist));

    public static object? GetCollapcedIcon(Expander element)
        => (object?)element.GetValue(CollapcedIconProperty);
    public static void SetCollapcedIcon(Expander element, object? value)
        => element.SetValue(CollapcedIconProperty, value);
    #endregion

    #region AttachedProperty : ExpanderPositionProperty
    public static readonly DependencyProperty ExpanderPositionProperty
        = DependencyProperty.RegisterAttached("ExpanderPosition", typeof(ExpanderPosition), typeof(ExpanderAssist), new PropertyMetadata(ExpanderPosition.End));

    public static ExpanderPosition GetExpanderPosition(Expander element)
        => (ExpanderPosition)element.GetValue(ExpanderPositionProperty);
    public static void SetExpanderPosition(Expander element, ExpanderPosition value)
        => element.SetValue(ExpanderPositionProperty, value);
    #endregion
}
