namespace MaterialDesignThemes.Wpf;

public static class Constants
{
    public static readonly Thickness TextBoxDefaultPadding = new(0, 4, 0, 4);
    public static readonly Thickness FilledTextBoxDefaultPadding = new(16, 8, 12, 8);
    public static readonly Thickness OutlinedTextBoxDefaultPadding = new(16, 16, 12, 16);
    public static readonly Thickness DefaultTextBoxViewMargin = new(1, 0, 1, 0);
    public static readonly Thickness DefaultTextBoxViewMarginStretch = new(1, 18, 1, 0);
    public static readonly Thickness DefaultTextBoxViewMarginEmbedded = new(0);
    public const double TextBoxNotEnabledOpacity = 0.56;
    public const double TextBoxInnerButtonSpacing = 2;
    public const double ComboBoxArrowSize = 8;

    /// <summary>
    /// Contains temporary constants needed until all styles leveraging SmartHint adopt the new approach.
    /// At that point, they should all use the constants above, the values should be changed to the ones in this class,
    /// and the class can be deleted.
    ///
    /// NOTE: XAML bindings only work on public properties/fields, so to make these fields "internal", wrapping them
    /// in an internal class is an acceptable compromise.
    /// </summary>
    internal static class TemporaryConstants
    {
        public static readonly Thickness TextBoxDefaultPaddingNew = new(0, 8, 0, 4);
        public static readonly Thickness FilledTextBoxDefaultPaddingNew = new(16, 12, 12, 8);
    }
}
