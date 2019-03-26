using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper : IPaletteHelper
    {
        public void ChangeColor(ColorName name, Color color)
        {
            MaterialDesignTheme.ChangeColor(name, color);
        }

        public void ChangeColor(string name, Color color)
        {
            MaterialDesignTheme.ChangeColor(name, color);
        }

        public void ChangePalette(ColorPalette palette)
        {
            MaterialDesignTheme.ChangePalette(palette);
        }

        public void ChangePrimaryColor(Color color)
        {
            MaterialDesignTheme.ChangePrimaryColor(color);
        }

        public void ChangePrimaryColor(MaterialDesignColor color)
        {
            MaterialDesignTheme.ChangePrimaryColor(color);
        }

        public void ChangeSecondaryColor(Color color)
        {
            MaterialDesignTheme.ChangeSecondaryColor(color);
        }

        public void ChangeSecondaryColor(MaterialDesignColor color)
        {
            MaterialDesignTheme.ChangeSecondaryColor(color);
        }

        public void ChangeTheme(BaseTheme theme)
        {
            MaterialDesignTheme.ChangeTheme(theme);
        }

        public void ChangeTheme(IBaseTheme theme)
        {
            MaterialDesignTheme.ChangeTheme(theme);
        }
    }
}
