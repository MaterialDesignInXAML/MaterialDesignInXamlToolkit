using System.Windows.Data;

namespace MaterialDesignThemes.Wpf;

public static class PasswordBoxAssist
{
    public static readonly DependencyProperty PasswordMaskedIconProperty = DependencyProperty.RegisterAttached(
        "PasswordMaskedIcon", typeof(PackIconKind), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(PackIconKind.EyeOff, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetPasswordMaskedIcon(DependencyObject element, PackIconKind value) => element.SetValue(PasswordMaskedIconProperty, value);
    public static PackIconKind GetPasswordMaskedIcon(DependencyObject element) => (PackIconKind)element.GetValue(PasswordMaskedIconProperty);

    public static readonly DependencyProperty PasswordRevealedIconProperty = DependencyProperty.RegisterAttached(
        "PasswordRevealedIcon", typeof(PackIconKind), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(PackIconKind.Eye, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetPasswordRevealedIcon(DependencyObject element, PackIconKind value) => element.SetValue(PasswordRevealedIconProperty, value);
    public static PackIconKind GetPasswordRevealedIcon(DependencyObject element) => (PackIconKind)element.GetValue(PasswordRevealedIconProperty);

    public static readonly DependencyProperty IsPasswordRevealedProperty = DependencyProperty.RegisterAttached(
        "IsPasswordRevealed", typeof(bool), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetIsPasswordRevealed(DependencyObject element, bool value) => element.SetValue(IsPasswordRevealedProperty, value);
    public static bool GetIsPasswordRevealed(DependencyObject element) => (bool)element.GetValue(IsPasswordRevealedProperty);

    public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
        "Password", typeof(string), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(null, HandlePasswordChanged) { BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus });
    public static void SetPassword(DependencyObject element, string value) => element.SetValue(PasswordProperty, value);
    public static string GetPassword(DependencyObject element) => (string)element.GetValue(PasswordProperty);

    private static readonly DependencyProperty IsChangingProperty = DependencyProperty.RegisterAttached(
        "IsChanging", typeof(bool), typeof(PasswordBoxAssist), new UIPropertyMetadata(false));
    private static void SetIsChanging(UIElement element, bool value) => element.SetValue(IsChangingProperty, value);
    private static bool GetIsChanging(UIElement element) => (bool)element.GetValue(IsChangingProperty);

    // Attached property used by the "reveal" Style to enforce the wiring-up of the PasswordChanged event handler; needed for the "reveal" TextBox.
    internal static readonly DependencyProperty SuppressBindingGuardProperty = DependencyProperty.RegisterAttached(
        "SuppressBindingGuard", typeof(bool), typeof(PasswordBoxAssist), new PropertyMetadata(default(bool)));
    internal static void SetSuppressBindingGuard(DependencyObject element, bool value) => element.SetValue(SuppressBindingGuardProperty, value);
    internal static bool GetSuppressBindingGuard(DependencyObject element) => (bool) element.GetValue(SuppressBindingGuardProperty);

    /// <summary>
    /// Handles changes to the 'Password' attached property.
    /// </summary>
    private static void HandlePasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is PasswordBox targetPasswordBox &&
            (GetSuppressBindingGuard(targetPasswordBox) || BindingOperations.GetBindingBase(targetPasswordBox, PasswordProperty) != null))
        {
            // If the PasswordBox is either "reveal" style (ie. SuppressBindingGuard=true) or the user has set a binding on the attached property, we wire up the PasswordChanged event handler.
            targetPasswordBox.PasswordChanged -= PasswordBoxPasswordChanged;
            if (!GetIsChanging(targetPasswordBox))
            {
                targetPasswordBox.Password = (string)e.NewValue;
            }
            targetPasswordBox.PasswordChanged += PasswordBoxPasswordChanged;
        }
    }

    /// <summary>
    /// Handle the 'PasswordChanged'-event on the PasswordBox
    /// </summary>
    private static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            SetIsChanging(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsChanging(passwordBox, false);
        }
    }
}
