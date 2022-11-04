using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf;

public class PasswordBoxAssist : Behavior<PasswordBox>
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
    
    private static readonly DependencyProperty SelectionProperty = DependencyProperty.RegisterAttached(
        "Selection", typeof(TextSelection), typeof(PasswordBoxAssist), new UIPropertyMetadata(default(TextSelection)));
    private static void SetSelection(DependencyObject obj, TextSelection? value) => obj.SetValue(SelectionProperty, value);
    private static TextSelection? GetSelection(DependencyObject obj) => (TextSelection?)obj.GetValue(SelectionProperty);

    private static readonly DependencyProperty RevealedPasswordTextBoxProperty = DependencyProperty.RegisterAttached(
        "RevealedPasswordTextBox", typeof(TextBox), typeof(PasswordBoxAssist), new UIPropertyMetadata(default(TextBox)));
    private static void SetRevealedPasswordTextBox(DependencyObject obj, TextBox? value) => obj.SetValue(RevealedPasswordTextBoxProperty, value);
    private static TextBox? GetRevealedPasswordTextBox(DependencyObject obj) => (TextBox?)obj.GetValue(RevealedPasswordTextBoxProperty);


    /// <summary>
    /// Handles changes to the 'Password' attached property.
    /// </summary>
    private static void HandlePasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is PasswordBox targetPasswordBox)
        {
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

    private void PasswordBoxLoaded(object sender, RoutedEventArgs e) => SetPassword(AssociatedObject, AssociatedObject.Password);
    
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PasswordChanged += PasswordBoxPasswordChanged;
        AssociatedObject.Loaded += PasswordBoxLoaded;
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
        {
            AssociatedObject.Loaded -= PasswordBoxLoaded;
            AssociatedObject.PasswordChanged -= PasswordBoxPasswordChanged;
        }
        base.OnDetaching();
    }
}
