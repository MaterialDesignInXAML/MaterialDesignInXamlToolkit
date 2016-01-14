using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class PasswordFieldAssist
    {
        private static readonly DependencyPropertyKey IsNullOrEmptyPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsNullOrEmpty", typeof(bool), typeof(PasswordFieldAssist), new PropertyMetadata(true));
        private static readonly DependencyProperty IsNullOrEmptyProperty = IsNullOrEmptyPropertyKey.DependencyProperty;

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
                // call listener immediately, maybe passwordbox was not empty
                PasswordBoxOnPasswordChanged(passwordBox, new RoutedEventArgs());
            }
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            var passwordBox = (PasswordBox)sender;
            ConfigureHint(passwordBox);

            var frameworkElement = (FrameworkElement)sender;
            if (frameworkElement == null)
                return;

            var state = string.IsNullOrEmpty((passwordBox.Password ?? ""))
                ? "MaterialDesignStateTextEmpty"
                : "MaterialDesignStateTextNotEmpty";

            if (frameworkElement.IsLoaded)
            {
                VisualStateManager.GoToState(frameworkElement, state, true);
            }
            else
            {
                frameworkElement.Loaded += (s, args) => VisualStateManager.GoToState(frameworkElement, state, false);
            }
            SetIsNullOrEmpty(frameworkElement, string.IsNullOrEmpty((passwordBox.Password ?? "")));
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

        private static void SetIsNullOrEmpty(DependencyObject element, bool value)
        {
            element.SetValue(IsNullOrEmptyPropertyKey, value);
        }

        public static bool GetIsNullOrEmpty(DependencyObject element)
        {
            return (bool)element.GetValue(IsNullOrEmptyProperty);
        }
    }
    
}