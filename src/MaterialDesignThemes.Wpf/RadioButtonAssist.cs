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
    }
}
