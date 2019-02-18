using System;
using System.Linq;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.XamlPaletteHelper;

namespace MaterialDesignThemes.Wpf
{
    public class CodePaletteHelper
    {
        //public event EventHandler<ThemeSetEventArgs> ThemeChanged;
        //public event EventHandler<PaletteChangedEventArgs> PaletteChanged;

        //public virtual void SetPrimaryPalette(Color color)
        //{
        //    var palette = new ColorPalette(PaletteName.Primary, color);
        //    SetPalette(palette);
        //}

        //public virtual void SetSecondaryPalette(Color color)
        //{
        //    var palette = new ColorPalette(PaletteName.Secondary, color);
        //    SetPalette(palette);

        //    // backwards compatability for now
        //    ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(palette.Mid));
        //    ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(palette.MidForeground));
        //}

        //public virtual void SetPalette(string name, Color color)
        //{
        //    var palette = new ColorPalette(name, color);
        //    SetPalette(palette);
        //}

        //public virtual void SetPalette(ColorPalette palette)
        //{
        //    var name = palette.Name;
        //    ReplaceEntry($"{name}HueLightBrush", new SolidColorBrush(palette.Light));
        //    ReplaceEntry($"{name}HueLightForegroundBrush", new SolidColorBrush(palette.LightForeground));
        //    ReplaceEntry($"{name}HueMidBrush", new SolidColorBrush(palette.Mid));
        //    ReplaceEntry($"{name}HueMidForegroundBrush", new SolidColorBrush(palette.MidForeground));
        //    ReplaceEntry($"{name}HueDarkBrush", new SolidColorBrush(palette.Dark));
        //    ReplaceEntry($"{name}HueDarkForegroundBrush", new SolidColorBrush(palette.DarkForeground));

        //    PaletteChanged?.Invoke(null, new PaletteChangedEventArgs(palette));
        //}

        //public virtual void SetPrimaryForeground(Color color)
        //{
        //    SetForegroundBrushes(PaletteName.Primary, color);
        //}

        //public virtual void SetSecondaryForeground(Color color)
        //{
        //    SetForegroundBrushes(PaletteName.Secondary, color);
        //}

        //public virtual void SetForegroundBrushes(string paletteName, Color color)
        //{
        //    ReplaceEntry($"{paletteName}HueLightForegroundBrush", new SolidColorBrush(color));
        //    ReplaceEntry($"{paletteName}HueMidForegroundBrush", new SolidColorBrush(color));
        //    ReplaceEntry($"{paletteName}HueDarkForegroundBrush", new SolidColorBrush(color));
        //}

        //public virtual void SetTheme(IBaseTheme theme)
        //{
        //    foreach(var p in theme.GetType().GetProperties().Where(o => o.PropertyType == typeof(Color)))
        //    {
        //        ReplaceEntry(p.Name, new SolidColorBrush((Color)p.GetValue(theme)));
        //    }
        //    ThemeChanged?.Invoke(this, new ThemeSetEventArgs(theme));
        //}
    }
}
