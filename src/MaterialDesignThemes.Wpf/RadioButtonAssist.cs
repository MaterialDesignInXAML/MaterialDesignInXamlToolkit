using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class RadioButtonAssist
    {
        private const double DefaultRadioButtonSize = 18.0;

        #region AttachedProperty : RadioButtonSizeProperty
        public static readonly DependencyProperty RadioButtonSizeProperty = DependencyProperty.RegisterAttached(
            "RadioButtonSize",
            typeof(double),
            typeof(RadioButtonAssist),
            new PropertyMetadata(DefaultRadioButtonSize)
        );

        public static double GetRadioButtonSize(RadioButton element) => (double)element.GetValue(RadioButtonSizeProperty);

        public static void SetRadioButtonSize(RadioButton element, double RadioButtonSize) =>
            element.SetValue(RadioButtonSizeProperty, RadioButtonSize);
        #endregion

        #region AttachedProperty :RadioButtonForegroundProperty
        public static readonly DependencyProperty RadioButtonForegroundProperty = DependencyProperty.RegisterAttached(
            "RadioButtonForeground",
            typeof(Brush),
            typeof(RadioButtonAssist),
            null
        );

        public static Brush GetRadioButtonForeground(RadioButton element) => (Brush)element.GetValue(RadioButtonForegroundProperty);

        public static void SetRadioButtonForeground(RadioButton element, Brush radioButtonForeground) =>
            element.SetValue(RadioButtonForegroundProperty, radioButtonForeground);
        #endregion
    }
}
