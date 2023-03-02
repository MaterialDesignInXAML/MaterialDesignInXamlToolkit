using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors;

internal class PasswordBoxBehavior : Behavior<PasswordBox>
{
    private void PasswordBoxLoaded(object sender, RoutedEventArgs e) => PasswordBoxAssist.SetPassword(AssociatedObject, AssociatedObject.Password);

    private void PasswordBoxPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (PasswordBoxAssist.GetIsPasswordRevealed(AssociatedObject) &&
            AssociatedObject.FindChild<TextBox>("RevealPasswordTextBox") is { } revealPasswordTextBox)
        {
            if (ReferenceEquals(e.OldFocus, revealPasswordTextBox) && ReferenceEquals(e.NewFocus, AssociatedObject))
            {
                // When password box receives keyboard focus, but it came from the nested reveal TextBox. We request focus transfer to the previous element from the password box's POV
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Previous);
                AssociatedObject.MoveFocus(request);
                e.Handled = true;
            }
            else if (!ReferenceEquals(e.OriginalSource, revealPasswordTextBox))
            {
                // When password box receives keyboard focus while the password is revealed, we transfer the focus to the nested reveal TextBox.
                revealPasswordTextBox.Focus();
                e.Handled = true;
            }
        }
        
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += PasswordBoxLoaded;
        AssociatedObject.PreviewGotKeyboardFocus += PasswordBoxPreviewGotKeyboardFocus;
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
        {
            AssociatedObject.Loaded -= PasswordBoxLoaded;
            AssociatedObject.PreviewGotKeyboardFocus -= PasswordBoxPreviewGotKeyboardFocus;
        }
        base.OnDetaching();
    }
}
