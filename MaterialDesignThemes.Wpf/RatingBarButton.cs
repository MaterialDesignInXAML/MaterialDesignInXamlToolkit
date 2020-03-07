using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public class RatingBarButton : ButtonBase
    {
        static RatingBarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingBarButton), new FrameworkPropertyMetadata(typeof(RatingBarButton)));
        }

        private static readonly DependencyPropertyKey ValuePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Value", typeof(int), typeof(RatingBarButton),
                new PropertyMetadata(default(int)));

        public static readonly DependencyProperty ValueProperty =
            ValuePropertyKey.DependencyProperty;

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            internal set { SetValue(ValuePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey IsWithinValuePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsWithinSelectedValue", typeof(bool), typeof(RatingBarButton),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsWithinSelectedValueProperty =
            IsWithinValuePropertyKey.DependencyProperty;

        public bool IsWithinSelectedValue
        {
            get { return (bool)GetValue(IsWithinSelectedValueProperty); }
            internal set { SetValue(IsWithinValuePropertyKey, value); }
        }
    }
}