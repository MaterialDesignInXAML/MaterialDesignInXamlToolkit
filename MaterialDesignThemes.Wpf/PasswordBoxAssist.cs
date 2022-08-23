using System.Windows.Data;

namespace MaterialDesignThemes.Wpf;

public static class PasswordBoxAssist
{
    public static readonly DependencyProperty PasswordMaskedIconProperty = DependencyProperty.RegisterAttached(
        "PasswordMaskedIcon", typeof(PackIconKind), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(PackIconKind.EyeOff, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetPasswordMaskedIcon(DependencyObject element, PackIconKind value) => element.SetValue(PasswordMaskedIconProperty, value);
    public static PackIconKind GetPasswordMaskedIcon(DependencyObject element) => (PackIconKind) element.GetValue(PasswordMaskedIconProperty);

    public static readonly DependencyProperty PasswordRevealedIconProperty = DependencyProperty.RegisterAttached(
        "PasswordRevealedIcon", typeof(PackIconKind), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(PackIconKind.Eye, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetPasswordRevealedIcon(DependencyObject element, PackIconKind value) => element.SetValue(PasswordRevealedIconProperty, value);
    public static PackIconKind GetPasswordRevealedIcon(DependencyObject element) => (PackIconKind) element.GetValue(PasswordRevealedIconProperty);

    public static readonly DependencyProperty IsPasswordRevealedProperty = DependencyProperty.RegisterAttached(
        "IsPasswordRevealed", typeof(bool), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetIsPasswordRevealed(DependencyObject element, bool value) => element.SetValue(IsPasswordRevealedProperty, value);
    public static bool GetIsPasswordRevealed(DependencyObject element) => (bool) element.GetValue(IsPasswordRevealedProperty);

    public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
        "Password", typeof(string), typeof(PasswordBoxAssist), new FrameworkPropertyMetadata(null, HandlePasswordChanged) { BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus });

    public static void SetPassword(DependencyObject element, string value) => element.SetValue(PasswordProperty, value);
    public static string GetPassword(DependencyObject element) => (string) element.GetValue(PasswordProperty);

    private static readonly DependencyProperty IsPasswordInitializedProperty = DependencyProperty.RegisterAttached(
        "IsPasswordInitialized", typeof(bool), typeof(PasswordBoxAssist), new PropertyMetadata(false));

    private static readonly DependencyProperty SettingPasswordProperty = DependencyProperty.RegisterAttached(
        "SettingPassword", typeof(bool), typeof(PasswordBoxAssist), new PropertyMetadata(false));

    private static void HandlePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var passwordBox = d as PasswordBox;
        if (passwordBox == null)
            return;

        // If we're being called because we set the value of the property we're bound to (from inside 
        // HandlePasswordChanged, then do nothing - we already have the latest value).
        if ((bool)passwordBox.GetValue(SettingPasswordProperty))
            return;

        // If this is the initial set (see the comment on PasswordProperty), set ourselves up
        if (!(bool)passwordBox.GetValue(IsPasswordInitializedProperty))
        {
            passwordBox.SetValue(IsPasswordInitializedProperty, true);
            passwordBox.PasswordChanged += HandlePasswordChanged;
        }
        passwordBox.Password = e.NewValue as string;
    }

    private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
    {
        var passwordBox = (PasswordBox)sender;
        passwordBox.SetValue(SettingPasswordProperty, true);
        string currentPassword = GetPassword(passwordBox);
        if (currentPassword != passwordBox.Password)
        {
            SetPassword(passwordBox, passwordBox.Password);
        }
        passwordBox.SetValue(SettingPasswordProperty, false);
    }
}