using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public interface IPaletteHelper
    {
        void ChangeTheme(BaseTheme theme);
        void ChangeTheme(IBaseTheme theme);

        void ChangePalette(ColorPalette palette);

        void ChangePrimaryColor(Color color);
        void ChangePrimaryColor(MaterialDesignColor color);

        void ChangeSecondaryColor(Color color);
        void ChangeSecondaryColor(MaterialDesignColor color);

        void ChangeColor(ColorName name, Color color);
        void ChangeColor(string name, Color color);
    }
}
