using System;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static partial class HintProxyFabric
    {
        private sealed class TextBoxHintProxy : IHintProxy
        {
            private readonly TextBox _textBox;

            public object Content => _textBox.Text;

            public bool IsLoaded => _textBox.IsLoaded;

            public bool IsVisible => _textBox.IsVisible;

            public bool IsEmpty() => string.IsNullOrEmpty(_textBox.Text);

            public bool IsFocused() => _textBox.IsKeyboardFocused;

            public event EventHandler ContentChanged;
            public event EventHandler IsVisibleChanged;
            public event EventHandler Loaded;
            public event EventHandler FocusedChanged;

            public TextBoxHintProxy(TextBox textBox)
            {
                if (textBox == null) throw new ArgumentNullException(nameof(textBox));

                _textBox = textBox;
                _textBox.TextChanged += TextBoxTextChanged;
                _textBox.Loaded += TextBoxLoaded;
                _textBox.IsVisibleChanged += TextBoxIsVisibleChanged;
                _textBox.IsKeyboardFocusedChanged += TextBoxIsKeyboardFocusedChanged;
            }

            private void TextBoxIsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                FocusedChanged?.Invoke(sender, EventArgs.Empty);
            }

            private void TextBoxIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                IsVisibleChanged?.Invoke(sender, EventArgs.Empty);
            }

            private void TextBoxLoaded(object sender, RoutedEventArgs e)
            {
                Loaded?.Invoke(sender, EventArgs.Empty);
            }

            private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
            {
                ContentChanged?.Invoke(sender, EventArgs.Empty);
            }

            public void Dispose()
            {
                _textBox.TextChanged -= TextBoxTextChanged;
                _textBox.Loaded -= TextBoxLoaded;
                _textBox.IsVisibleChanged -= TextBoxIsVisibleChanged;
                _textBox.IsKeyboardFocusedChanged -= TextBoxIsKeyboardFocusedChanged;
            }
        }
    }
}