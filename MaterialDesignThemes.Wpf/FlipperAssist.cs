namespace MaterialDesignThemes.Wpf;

public static class FlipperAssist
{
    #region AttachedProperty : UniformCornerRadiusProperty
    /// <summary>
    /// Controls the (uniform) corner radius of the contained card
    /// </summary>
    public static readonly DependencyProperty UniformCornerRadiusProperty
        = DependencyProperty.RegisterAttached("UniformCornerRadius", typeof(double), typeof(FlipperAssist),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

    public static void SetUniformCornerRadius(DependencyObject element, double value) => element.SetValue(UniformCornerRadiusProperty, value);
    public static double GetUniformCornerRadius(DependencyObject element) => (double)element.GetValue(UniformCornerRadiusProperty);
    #endregion

    #region AttachedProperty : CardStyleProperty
    /// <summary>
    /// Controls the style of the contained card
    /// </summary>
    public static readonly DependencyProperty CardStyleProperty
        = DependencyProperty.RegisterAttached("CardStyle", typeof(Style), typeof(FlipperAssist),
            new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits, null, CoerceCardStyleCallback));
    private static object? CoerceCardStyleCallback(DependencyObject d, object baseValue)
    {
        if (baseValue is Style style && !typeof(Card).IsAssignableFrom(style.TargetType))
            return default(Style);
        return baseValue;
    }

    public static void SetCardStyle(DependencyObject element, Style value) => element.SetValue(CardStyleProperty, value);
    public static Style GetCardStyle(DependencyObject element) => (Style)element.GetValue(CardStyleProperty);
    #endregion
}
