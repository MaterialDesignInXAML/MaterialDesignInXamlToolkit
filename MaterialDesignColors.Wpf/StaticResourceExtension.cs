namespace MaterialDesignColors;

/// <summary>
/// Implements a markup extension that supports static (XAML load time) resource references made from XAML.
/// Based on class from MahApps
/// </summary>
[MarkupExtensionReturnType(typeof(object))]
[Localizability(LocalizationCategory.NeverLocalize)]
public class StaticResourceExtension : System.Windows.StaticResourceExtension
{
    public StaticResourceExtension()
    {
    }

    public StaticResourceExtension(object resourceKey)
        : base(resourceKey)
    {
    }
}
