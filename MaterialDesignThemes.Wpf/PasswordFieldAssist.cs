using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class PasswordFieldAssist
    {
        public static readonly DependencyProperty ManagedProperty = DependencyProperty.RegisterAttached(
            "Managed", typeof(PasswordBox), typeof(PasswordFieldAssist), new PropertyMetadata(default(PasswordBox), ManagedPropertyChangedCallback));

        private static void ManagedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var passwordBox = dependencyPropertyChangedEventArgs.OldValue as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;
            }

            passwordBox = dependencyPropertyChangedEventArgs.NewValue as PasswordBox;
            if (passwordBox != null)
            {
                passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
                ConfigureHint(passwordBox);
            }
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            ConfigureHint((PasswordBox)sender);
        }

        private static void ConfigureHint(PasswordBox passwordBox)
        {
            passwordBox.SetValue(HintVisibilityProperty, passwordBox.SecurePassword.Length == 0 ? Visibility.Visible : Visibility.Hidden);
        }

        public static readonly DependencyProperty HintVisibilityProperty = DependencyProperty.RegisterAttached(
            "HintVisibility", typeof(Visibility), typeof(PasswordFieldAssist), new PropertyMetadata(default(Visibility)));

        public static void SetHintVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(HintVisibilityProperty, value);
        }

        public static Visibility GetHintVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(HintVisibilityProperty);
        }

        public static void SetManaged(DependencyObject element, PasswordBox value)
        {
            element.SetValue(ManagedProperty, value);
        }

        public static PasswordBox GetManaged(DependencyObject element)
        {
            return (PasswordBox)element.GetValue(ManagedProperty);
        }
    }

    public static class ProgressBarAssist
    {

        
    }
}