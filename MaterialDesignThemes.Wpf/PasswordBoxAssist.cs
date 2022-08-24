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

    // Internal attached DP used to initially wire up the connection between the masked PasswordBox content and the clear text TextBox content
    internal static readonly DependencyProperty InitialPasswordProperty = DependencyProperty.RegisterAttached(
        "InitialPassword", typeof(string), typeof(PasswordBoxAssist), new PropertyMetadata(default(string)));
    internal static void SetInitialPassword(DependencyObject element, string value) => element.SetValue(InitialPasswordProperty, value);
    internal static string GetInitialPassword(DependencyObject element) => (string)element.GetValue(InitialPasswordProperty);

    private static readonly DependencyProperty IsPasswordInitializedProperty = DependencyProperty.RegisterAttached(
        "IsPasswordInitialized", typeof(bool), typeof(PasswordBoxAssist), new PropertyMetadata(false));

    private static readonly DependencyProperty SettingPasswordProperty = DependencyProperty.RegisterAttached(
        "SettingPassword", typeof(bool), typeof(PasswordBoxAssist), new PropertyMetadata(false));

    private static void HandlePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox)
            return;

        if ((bool)passwordBox.GetValue(SettingPasswordProperty))
            return;

        if (!(bool)passwordBox.GetValue(IsPasswordInitializedProperty))
        {
            passwordBox.SetValue(IsPasswordInitializedProperty, true);
            WeakEventManager<PasswordBox, RoutedEventArgs>.AddHandler(passwordBox, nameof(PasswordBox.PasswordChanged), HandlePasswordChanged);
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