using System;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static partial class HintProxyFabric
    {
        private sealed class PasswordBoxHintProxy : IHintProxy
        {
            private readonly PasswordBox _passwordBox;

            public bool IsEmpty() => string.IsNullOrEmpty(_passwordBox.Password);

            public object Content => _passwordBox.Password;

            public bool IsLoaded => _passwordBox.IsLoaded;

            public bool IsVisible => _passwordBox.IsVisible;

            public bool IsFocused() => _passwordBox.IsKeyboardFocused;

            public event EventHandler ContentChanged;
            public event EventHandler IsVisibleChanged;
            public event EventHandler Loaded;
            public event EventHandler FocusedChanged;

            public PasswordBoxHintProxy(PasswordBox passwordBox)
            {
                if (passwordBox == null) throw new ArgumentNullException(nameof(passwordBox));

                _passwordBox = passwordBox;
                _passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
                _passwordBox.Loaded += PasswordBoxLoaded;
                _passwordBox.IsVisibleChanged += PasswordBoxIsVisibleChanged;
                _passwordBox.IsKeyboardFocusedChanged += PasswordBoxIsKeyboardFocusedChanged;
            }

            private void PasswordBoxIsKeyboardFocusedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
            {
                FocusedChanged?.Invoke(this, EventArgs.Empty);
            }

            private void PasswordBoxIsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
            {
                IsVisibleChanged?.Invoke(this, EventArgs.Empty);
            }

            private void PasswordBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
            {
                Loaded?.Invoke(this, EventArgs.Empty);
            }

            private void PasswordBoxPasswordChanged(object sender, System.Windows.RoutedEventArgs e)
            {
                ContentChanged?.Invoke(this, EventArgs.Empty);
            }

            public void Dispose()
            {
                _passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;
                _passwordBox.Loaded -= PasswordBoxLoaded;
                _passwordBox.IsVisibleChanged -= PasswordBoxIsVisibleChanged;
                _passwordBox.IsKeyboardFocusedChanged -= PasswordBoxIsKeyboardFocusedChanged;
            }

        }
    }
}