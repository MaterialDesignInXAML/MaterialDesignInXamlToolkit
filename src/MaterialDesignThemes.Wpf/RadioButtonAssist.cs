namespace MaterialDesignThemes.Wpf
{
    public class RadioButtonAssist
    {
        private const double DefaultRadioButtonSize = 18.0;

        #region AttachedProperty : RadioButtonSizeProperty
        public static readonly DependencyProperty RadioButtonSizeProperty =
            DependencyProperty.RegisterAttached(
                "RadioButtonSize",
                typeof(double),
                typeof(RadioButtonAssist),
                new PropertyMetadata(DefaultRadioButtonSize)
            );

        public static double GetRadioButtonSize(RadioButton element) =>
            (double)element.GetValue(RadioButtonSizeProperty);

        public static void SetRadioButtonSize(RadioButton element, double checkBoxSize) =>
            element.SetValue(RadioButtonSizeProperty, checkBoxSize);
        #endregion

        #region AttachedProperty : ContentPlacementProperty
        /// <summary>
        /// Where the content is placed relative to the radio button glyph.
        /// </summary>
        public static readonly DependencyProperty ContentPlacementProperty
            = DependencyProperty.RegisterAttached("ContentPlacement", typeof(Dock), typeof(RadioButtonAssist), new PropertyMetadata(Dock.Right));

        public static Dock GetContentPlacement(RadioButton element)
            => (Dock)element.GetValue(ContentPlacementProperty);

        public static void SetContentPlacement(RadioButton element, Dock value)
            => element.SetValue(ContentPlacementProperty, value);
        #endregion
    }
}
