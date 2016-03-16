using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class PasswordFieldAssist
    {
        /// <summary>
        /// Framework use only.
        /// </summary>
        public static readonly DependencyProperty HintVisibilityProperty = DependencyProperty.RegisterAttached(
            "HintVisibility", typeof(Visibility), typeof(PasswordFieldAssist), new PropertyMetadata(default(Visibility)));

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static void SetHintVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(HintVisibilityProperty, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static Visibility GetHintVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(HintVisibilityProperty);
        }
    }
    
}