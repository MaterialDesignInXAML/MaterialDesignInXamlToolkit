namespace MaterialDesignDemo.Domain;

/// <summary>
/// Simple helper class to enable data-binding for <see cref="PasswordBox.Password"/> which is not a <see cref="DependencyProperty"/>
/// </summary>
internal static class PasswordHelper
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached("Password",
        typeof(string), typeof(PasswordHelper),
        new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    public static readonly DependencyProperty AttachProperty =
        DependencyProperty.RegisterAttached("Attach",
        typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, Attach));

    private static readonly DependencyProperty IsUpdatingProperty =
       DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
       typeof(PasswordHelper));
    
    public static void SetAttach(DependencyObject dp, bool value) => dp.SetValue(AttachProperty, value);

    public static bool GetAttach(DependencyObject dp) => (bool)dp.GetValue(AttachProperty);

    public static string GetPassword(DependencyObject dp) => (string)dp.GetValue(PasswordProperty);

    public static void SetPassword(DependencyObject dp, string value) => dp.SetValue(PasswordProperty, value);

    private static bool GetIsUpdating(DependencyObject dp) => (bool)dp.GetValue(IsUpdatingProperty);

    private static void SetIsUpdating(DependencyObject dp, bool value) => dp.SetValue(IsUpdatingProperty, value);

    private static void OnPasswordPropertyChanged(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = (PasswordBox) sender;
        passwordBox.PasswordChanged -= PasswordChanged;

        if (!GetIsUpdating(passwordBox))
        {
            passwordBox.Password = (string)e.NewValue;
        }
        passwordBox.PasswordChanged += PasswordChanged;
    }

    private static void Attach(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = (PasswordBox) sender;

        if ((bool)e.OldValue)
        {
            passwordBox.PasswordChanged -= PasswordChanged;
        }

        if ((bool)e.NewValue)
        {
            passwordBox.PasswordChanged += PasswordChanged;
        }
    }

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordBox passwordBox = (PasswordBox) sender;
        SetIsUpdating(passwordBox, true);
        SetPassword(passwordBox, passwordBox.Password);
        SetIsUpdating(passwordBox, false);
    }
}