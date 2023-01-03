using System.Windows.Media;

namespace MaterialDesignDemo;

internal static class VisualTreeUtil
{
    private static T FindVisualParent<T>(UIElement element) where T : UIElement?
    {
        UIElement? parent = element;
        while (parent != null)
        {
            if (parent is T correctlyTyped)
            {
                return correctlyTyped;
            }
            parent = VisualTreeHelper.GetParent(parent) as UIElement;
        }
        return default!;
    }

    internal static T GetElementUnderMouse<T>() where T : UIElement? => FindVisualParent<T>((Mouse.DirectlyOver as UIElement)!);
}
