using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using static MaterialDesignThemes.Wpf.PaletteHelper;

namespace MaterialDesignThemes.Wpf
{
    public class DefaultPaletteHelper : IPaletteHelper
    {
        public event EventHandler<ThemeSetEventArgs> ThemeChanged;
        public event EventHandler<PaletteChangedEventArgs> PaletteChanged;

        public void SetPrimaryForeground(Color color)
        {
            SetForegroundBrushes(PaletteName.Primary, color);
        }

        public void SetSecondaryForeground(Color color)
        {
            SetForegroundBrushes(PaletteName.Secondary, color);
        }

        public void SetForegroundBrushes(string name, Color color)
        {
            ReplaceEntry($"{name}HueLightForegroundBrush", new SolidColorBrush(color));
            ReplaceEntry($"{name}HueMidForegroundBrush", new SolidColorBrush(color));
            ReplaceEntry($"{name}HueDarkForegroundBrush", new SolidColorBrush(color));
        }

        public void SetPrimaryPalette(Color color)
        {
            var palette = new ColorPalette(PaletteName.Primary, color);
            SetPalette(palette);

            // TODO think this is safe to remove now
            //var light = palette.Light;
            //var mid = palette.Mid;
            //var dark = palette.Dark;

            //var darkForeground = palette.DarkForeground;
            
            //ReplaceEntry("HighlightBrush", new SolidColorBrush(dark));
            //ReplaceEntry("AccentColorBrush", new SolidColorBrush(dark));
            //ReplaceEntry("AccentColorBrush2", new SolidColorBrush(mid));
            //ReplaceEntry("AccentColorBrush3", new SolidColorBrush(light));
            //ReplaceEntry("AccentColorBrush4", new SolidColorBrush(light) { Opacity = .82 });
            //ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark));
            //ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(darkForeground));
            //ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark, mid, 90.0));
            //ReplaceEntry("CheckmarkFill", new SolidColorBrush(dark));
            //ReplaceEntry("RightArrowFill", new SolidColorBrush(dark));
            //ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(darkForeground));
            //ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark) { Opacity = .4 });
        }

        public void SetSecondaryPalette(Color color)
        {
            var palette = new ColorPalette(PaletteName.Secondary, color);
            SetPalette(palette);

            // backwards compatability for now
            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(palette.Mid));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(palette.MidForeground));
        }

        public void SetPalette(string name, Color color)
        {
            var palette = new ColorPalette(name, color);
            SetPalette(palette);
        }

        public void SetPalette(ColorPalette palette)
        {
            var name = palette.Name;
            ReplaceEntry($"{name}HueLightBrush", new SolidColorBrush(palette.Light));
            ReplaceEntry($"{name}HueLightForegroundBrush", new SolidColorBrush(palette.LightForeground));
            ReplaceEntry($"{name}HueMidBrush", new SolidColorBrush(palette.Mid));
            ReplaceEntry($"{name}HueMidForegroundBrush", new SolidColorBrush(palette.MidForeground));
            ReplaceEntry($"{name}HueDarkBrush", new SolidColorBrush(palette.Dark));
            ReplaceEntry($"{name}HueDarkForegroundBrush", new SolidColorBrush(palette.DarkForeground));

            PaletteChanged?.Invoke(null, new PaletteChangedEventArgs(palette));
        }

        public void SetTheme(IBaseTheme theme)
        {
            foreach(var p in theme.GetType().GetProperties())
            {
                ReplaceEntry(p.Name, new SolidColorBrush((Color)p.GetValue(theme)));
            }
            ThemeChanged?.Invoke(this, new ThemeSetEventArgs(theme));
        }
    }
}
