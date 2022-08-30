namespace MaterialDesignThemes.Wpf;

public static partial class HintProxyFabric
{
    private sealed class TextBoxHintProxy : IHintProxy
    {
        private readonly TextBox _textBox;

        public bool IsLoaded => _textBox.IsLoaded;

        public bool IsVisible => _textBox.IsVisible;

        public bool IsEmpty() => string.IsNullOrEmpty(_textBox.Text);

        public bool IsFocused() => _textBox.IsKeyboardFocusWithin;

        public event EventHandler? ContentChanged;
        public event EventHandler? IsVisibleChanged;
        public event EventHandler? Loaded;
        public event EventHandler? FocusedChanged;

        public TextBoxHintProxy(TextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _textBox.TextChanged += TextBoxTextChanged;
            _textBox.Loaded += TextBoxLoaded;
            _textBox.IsVisibleChanged += TextBoxIsVisibleChanged;
            _textBox.IsKeyboardFocusWithinChanged += TextBoxIsKeyboardFocusedChanged;
        }

        private void TextBoxIsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
            => FocusedChanged?.Invoke(sender, EventArgs.Empty);

        private void TextBoxIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
            => IsVisibleChanged?.Invoke(sender, EventArgs.Empty);

        private void TextBoxLoaded(object sender, RoutedEventArgs e)
            => Loaded?.Invoke(sender, EventArgs.Empty);

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
            => ContentChanged?.Invoke(sender, EventArgs.Empty);

        public void Dispose()
        {
            _textBox.TextChanged -= TextBoxTextChanged;
            _textBox.Loaded -= TextBoxLoaded;
            _textBox.IsVisibleChanged -= TextBoxIsVisibleChanged;
            _textBox.IsKeyboardFocusWithinChanged -= TextBoxIsKeyboardFocusedChanged;
        }
    }
}