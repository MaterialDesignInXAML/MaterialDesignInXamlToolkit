namespace MaterialDesignThemes.Wpf.Theming;

partial class ThemeManager
{

    private static void SetThemeBrushes(ResourceDictionary resourceDictionary, Theme theme)
    {
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Background", theme.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Button.FlatClick", theme.Button.FlatClick);
    }

    private static Theme GetThemeBrushes(ResourceDictionary resourceDictionary)
    {
        return new Theme
        {
            Background = GetColor(resourceDictionary, "MaterialDesign.Brush.Background"),
            Button =
            {
                FlatClick = GetColor(resourceDictionary, "MaterialDesign.Brush.Button.FlatClick"),
            }
        };
    }

}
