using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class TextBlockAssist
    {
        #region Property AutoToolTip

        /// <summary>
        /// Automatic ToolTip for TextBlock or TextBoxBase if containing text is trimmed
        /// </summary>
        public static readonly DependencyProperty AutoToolTipProperty = DependencyProperty.RegisterAttached(
            "AutoToolTip", typeof(bool), typeof(TextBlockAssist), new PropertyMetadata(false, OnAutoToolTipChanged));

        public static void SetAutoToolTip(DependencyObject element, bool value) => element.SetValue(AutoToolTipProperty, value);
        public static bool GetAutoToolTip(DependencyObject element) => (bool) element.GetValue(AutoToolTipProperty);

        private static void OnAutoToolTipChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            if (!(element is TextBlock textBlock))
                return;

            if (Equals(args.NewValue, true))
            {
                textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
                UpdateToolTip(textBlock);
                textBlock.SizeChanged += UpdateToolTipOnSizeChanged;
            }
            else
                textBlock.SizeChanged -= UpdateToolTipOnSizeChanged;
        }

        private static void UpdateToolTipOnSizeChanged(object sender, SizeChangedEventArgs args) => UpdateToolTip((TextBlock) sender);

        private static void UpdateToolTip(TextBlock textBlock)
        {
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            ToolTipService.SetToolTip(textBlock,
                textBlock.TextWrapping == TextWrapping.NoWrap
                && textBlock.TextTrimming != TextTrimming.None
                && textBlock.ActualWidth < textBlock.DesiredSize.Width
                    ? textBlock.Text
                    : null);
        }

        #endregion
    }
}