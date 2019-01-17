using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public interface IPaletteHelper
    {
        void SetTheme(IBaseTheme theme);
        void SetPalettes(Color primary, Color secondary);
    }
}
