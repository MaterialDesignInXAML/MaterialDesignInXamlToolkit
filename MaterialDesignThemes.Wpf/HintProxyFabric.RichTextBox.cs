using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MaterialDesignThemes.Wpf
{
    public static partial class HintProxyFabric
    {
        private sealed class RichTextBoxHintProxy : IHintProxy
        {
            private readonly RichTextBox _richTextBox;

            public object Content => null;

            public bool IsLoaded => _richTextBox.IsLoaded;

            public bool IsVisible => _richTextBox.IsVisible;

            public bool IsEmpty()
            {
                var textRange = new TextRange(
                    _richTextBox.Document.ContentStart,
                    _richTextBox.Document.ContentEnd
                );

                return string.IsNullOrEmpty(textRange.Text) || 
                    textRange.Text == Environment.NewLine;
            }

            public bool IsFocused() => _richTextBox.IsKeyboardFocused;

            public event EventHandler ContentChanged;
            public event EventHandler IsVisibleChanged;
            public event EventHandler Loaded;
            public event EventHandler FocusedChanged;

            public RichTextBoxHintProxy(RichTextBox textBox)
            {
                _richTextBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
                _richTextBox.TextChanged += TextBoxTextChanged;
                _richTextBox.Loaded += TextBoxLoaded;
                _richTextBox.IsVisibleChanged += TextBoxIsVisibleChanged;
                _richTextBox.IsKeyboardFocusedChanged += TextBoxIsKeyboardFocusedChanged;
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
                _richTextBox.TextChanged -= TextBoxTextChanged;
                _richTextBox.Loaded -= TextBoxLoaded;
                _richTextBox.IsVisibleChanged -= TextBoxIsVisibleChanged;
                _richTextBox.IsKeyboardFocusedChanged -= TextBoxIsKeyboardFocusedChanged;
            }
        }
    }
}