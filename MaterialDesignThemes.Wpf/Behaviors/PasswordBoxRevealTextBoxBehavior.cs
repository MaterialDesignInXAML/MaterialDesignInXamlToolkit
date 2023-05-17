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

    private static PropertyInfo SelectionPropertyInfo { get; }
    private static MethodInfo SelectMethodInfo { get; }
    private static MethodInfo GetStartMethodInfo { get; }
    private static MethodInfo GetEndMethodInfo { get; }
    private static PropertyInfo GetOffsetPropertyInfo { get; }

    static PasswordBoxRevealTextBoxBehavior()
    {
        SelectionPropertyInfo = typeof(PasswordBox).GetProperty("Selection", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException("Did not find 'Selection' property on PasswordBox");
        SelectMethodInfo = typeof(PasswordBox).GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("Did not find 'Select' method on PasswordBox");
        Type iTextRange = typeof(PasswordBox).Assembly.GetType("System.Windows.Documents.ITextRange") ?? throw new InvalidOperationException("Failed to find ITextRange");
        GetStartMethodInfo = iTextRange.GetProperty("Start")?.GetGetMethod() ?? throw new InvalidOperationException($"Failed to find 'Start' property on {iTextRange.FullName}");
        GetEndMethodInfo = iTextRange.GetProperty("End")?.GetGetMethod() ?? throw new InvalidOperationException($"Failed to find 'End' property on {iTextRange.FullName}");
        Type passwordTextPointer = typeof(PasswordBox).Assembly.GetType("System.Windows.Controls.PasswordTextPointer") ?? throw new InvalidOperationException("Failed to find PasswordTextPointer");
        GetOffsetPropertyInfo = passwordTextPointer.GetProperty("Offset", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException("Failed to find 'Offset' property on PasswordTextPointer");
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.IsVisibleChanged += AssociatedObjectOnIsVisibleChanged;
        if (PasswordBox != null)
        {
            var selection = SelectionPropertyInfo.GetValue(PasswordBox, null) as TextSelection;
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
        object? start = GetStartMethodInfo.Invoke(selection, null);
        object? end = GetEndMethodInfo.Invoke(selection, null);
        int? startValue = GetOffsetPropertyInfo.GetValue(start, null) as int?;
        int? endValue = GetOffsetPropertyInfo.GetValue(end, null) as int?;
        int selectionStart = startValue ?? 0;
        int selectionLength = 0;
        if (endValue.HasValue)
        {
            selectionLength = endValue.Value - selectionStart;
        }
        return new PasswordBoxSelection(selectionStart, selectionLength);
    }

    private void SetPasswordBoxSelection(int selectionStart, int selectionLength) => SelectMethodInfo.Invoke(PasswordBox, new object[] { selectionStart, selectionLength });

    private record struct PasswordBoxSelection(int SelectionStart, int SelectionEnd);
}
