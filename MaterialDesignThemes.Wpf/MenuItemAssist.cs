using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public static class MenuItemAssist
{
    public static Brush? GetHighlightedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(HighlightedBackgroundProperty);

    public static void SetHighlightedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(HighlightedBackgroundProperty, value);

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HighlightedBackgroundProperty =
        DependencyProperty.RegisterAttached("HighlightedBackground", typeof(Brush), typeof(MenuItemAssist), new PropertyMetadata(null));
}
