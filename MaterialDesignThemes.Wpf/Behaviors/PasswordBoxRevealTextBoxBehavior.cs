using System.Reflection;
using System.Windows.Documents;
using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors;

internal class PasswordBoxRevealTextBoxBehavior : Behavior<TextBox>
{
    private static readonly DependencyProperty SelectionProperty = DependencyProperty.RegisterAttached(
        "Selection", typeof(TextSelection), typeof(PasswordBoxRevealTextBoxBehavior), new UIPropertyMetadata(default(TextSelection)));
    private static void SetSelection(DependencyObject obj, TextSelection? value) => obj.SetValue(SelectionProperty, value);
    private static TextSelection? GetSelection(DependencyObject obj) => (TextSelection?)obj.GetValue(SelectionProperty);

    internal static readonly DependencyProperty PasswordBoxProperty = DependencyProperty.Register(
        nameof(PasswordBox), typeof(PasswordBox), typeof(PasswordBoxRevealTextBoxBehavior), new PropertyMetadata(default(PasswordBox)));

    internal PasswordBox? PasswordBox
    {
        get => (PasswordBox) GetValue(PasswordBoxProperty);
        set => SetValue(PasswordBoxProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.IsVisibleChanged += AssociatedObjectOnIsVisibleChanged;
        if (PasswordBox != null)
        {
            var info = typeof(PasswordBox).GetProperty("Selection", BindingFlags.NonPublic | BindingFlags.Instance);
            var selection = info?.GetValue(PasswordBox, null) as TextSelection;
            SetSelection(AssociatedObject, selection);
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
        {
            AssociatedObject.ClearValue(SelectionProperty);
            AssociatedObject.IsVisibleChanged -= AssociatedObjectOnIsVisibleChanged;
        }
    }

    private void AssociatedObjectOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (AssociatedObject.IsVisible)
        {
            AssociatedObject.SelectionLength = 0;
            var selection = GetPasswordBoxSelection();
            AssociatedObject.SelectionStart = selection.SelectionStart;
            AssociatedObject.SelectionLength = selection.SelectionEnd;
            Keyboard.Focus(AssociatedObject);
        }
        else if (PasswordBox != null)
        {
            SetPasswordBoxSelection(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
            Keyboard.Focus(PasswordBox);
        }
    }

    private PasswordBoxSelection GetPasswordBoxSelection()
    {
        var selection = GetSelection(AssociatedObject);
        var typeTextRange = selection?.GetType().GetInterfaces().FirstOrDefault(i => i.Name == "ITextRange");
        object? start = typeTextRange?.GetProperty("Start")?.GetGetMethod()?.Invoke(selection, null);
        object? end = typeTextRange?.GetProperty("End")?.GetGetMethod()?.Invoke(selection, null);
        int? startValue = start?.GetType().GetProperty("Offset", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(start, null) as int?;
        int? endValue = end?.GetType().GetProperty("Offset", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(end, null) as int?;
        int selectionStart = startValue.GetValueOrDefault(0);
        int selectionLength = 0;
        if (endValue.HasValue)
        {
            selectionLength = endValue.Value - startValue.GetValueOrDefault(0);
        }
        return new PasswordBoxSelection(selectionStart, selectionLength);
    }

    private void SetPasswordBoxSelection(int selectionStart, int selectionLength) => typeof(PasswordBox).GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(PasswordBox, new object[] { selectionStart, selectionLength });

    private struct PasswordBoxSelection
    {
        public readonly int SelectionStart;
        public readonly int SelectionEnd;

        public PasswordBoxSelection(int selectionStart, int selectionEnd)
        {
            SelectionStart = selectionStart;
            SelectionEnd = selectionEnd;
        }
    }
}
