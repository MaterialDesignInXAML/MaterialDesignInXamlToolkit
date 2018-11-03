using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using static MaterialDesignThemes.Wpf.PaletteHelper;

namespace MaterialDesignThemes.Wpf
{
    public static class EnhancedPaletteHelper
    {
        public static void SetPalettes(Color primary, Color secondary)
        {
            SetPrimaryPalette(primary);
            SetSecondaryPalette(secondary);
        }

        public static void SetPrimaryForeground(Color color)
        {
            SetForegroundBrushes(color, "Primary");
        }

        public static void SetSecondaryForeground(Color color)
        {
            SetForegroundBrushes(color, "Secondary");
        }

        private static void SetForegroundBrushes(Color color, string scheme)
        {
            ReplaceEntry($"{scheme}HueLightForegroundBrush", new SolidColorBrush(color));
            ReplaceEntry($"{scheme}HueMidForegroundBrush", new SolidColorBrush(color));
            ReplaceEntry($"{scheme}HueDarkForegroundBrush", new SolidColorBrush(color));
        }

        public static void SetPrimaryPalette(Color color)
        {
            var light = color.Lighten();
            var mid = color;
            var dark = color.Darken();

            var darkForeground = ColorHelper.ContrastingForeGroundColor(dark);

            SetPalette(color, "Primary");

            ReplaceEntry("HighlightBrush", new SolidColorBrush(dark));
            ReplaceEntry("AccentColorBrush", new SolidColorBrush(dark));
            ReplaceEntry("AccentColorBrush2", new SolidColorBrush(mid));
            ReplaceEntry("AccentColorBrush3", new SolidColorBrush(light));
            ReplaceEntry("AccentColorBrush4", new SolidColorBrush(light) { Opacity = .82 });
            ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark));
            ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(darkForeground));
            ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark, mid, 90.0));
            ReplaceEntry("CheckmarkFill", new SolidColorBrush(dark));
            ReplaceEntry("RightArrowFill", new SolidColorBrush(dark));
            ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(darkForeground));
            ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark) { Opacity = .4 });
        }

        public static void SetSecondaryPalette(Color color)
        {
            SetPalette(color, "Secondary");

            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(ColorHelper.ContrastingForeGroundColor(color)));
        }

        private static void SetPalette(Color color, string scheme)
        {
            var light = color.Lighten();
            var mid = color;
            var dark = color.Darken();

            var lightForeground = ColorHelper.ContrastingForeGroundColor(light);
            var midForeground = ColorHelper.ContrastingForeGroundColor(mid);
            var darkForeground = ColorHelper.ContrastingForeGroundColor(dark);

            ReplaceEntry($"{scheme}HueLightBrush", new SolidColorBrush(light));
            ReplaceEntry($"{scheme}HueLightForegroundBrush", new SolidColorBrush(lightForeground));
            ReplaceEntry($"{scheme}HueMidBrush", new SolidColorBrush(mid));
            ReplaceEntry($"{scheme}HueMidForegroundBrush", new SolidColorBrush(midForeground));
            ReplaceEntry($"{scheme}HueDarkBrush", new SolidColorBrush(dark));
            ReplaceEntry($"{scheme}HueDarkForegroundBrush", new SolidColorBrush(darkForeground));
        }
    }
}
