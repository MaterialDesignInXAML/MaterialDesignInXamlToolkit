using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
        [Obsolete]
        public virtual void SetLightDark(bool isDark)
        {
            if (GetTheme() is ITheme theme)
            {
                theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light);
                SetTheme(theme);
            }
        }

        /// <summary>
        /// Replaces the entire palette
        /// </summary>
        [Obsolete]
        public virtual void ReplacePalette(Palette palette)
        {
            if (palette == null) throw new ArgumentNullException(nameof(palette));

            ITheme theme = GetTheme();
            theme.SetPalette(palette);
            SetTheme(theme);

            foreach (Hue color in palette.PrimarySwatch.PrimaryHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }
            foreach (var color in palette.AccentSwatch.AccentHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }

            var accentHue = palette.AccentSwatch.AccentHues.ElementAt(palette.AccentHueIndex);
            ReplaceEntry("SecondaryAccentBrush", accentHue.Color);
            ReplaceEntry("SecondaryAccentForegroundBrush", accentHue.Foreground);
        }


        /// <summary>
        /// Replaces the primary color, selecting a balanced set of hues for the light, mid and dark hues.
        /// </summary>
        /// <param name="swatch"></param>
        [Obsolete]
        public virtual void ReplacePrimaryColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var palette = QueryPalette();

            ReplacePalette(new Palette(swatch, palette.AccentSwatch, palette.PrimaryLightHueIndex, palette.PrimaryMidHueIndex, palette.PrimaryDarkHueIndex, palette.AccentHueIndex));
        }

        [Obsolete]
        public virtual void ReplacePrimaryColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (swatch == null)
                throw new ArgumentException($"No such swatch '{name}'", nameof(name));

            ReplacePrimaryColor(swatch);
        }

        [Obsolete]
        public virtual void ReplaceAccentColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var palette = QueryPalette();

            ReplacePalette(new Palette(palette.PrimarySwatch, swatch, palette.PrimaryLightHueIndex, palette.PrimaryMidHueIndex, palette.PrimaryDarkHueIndex, palette.AccentHueIndex));
        }

        [Obsolete]
        public virtual void ReplaceAccentColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0 && s.IsAccented);

            if (swatch == null)
                throw new ArgumentException($"No such accented swatch '{name}'", nameof(name));

            ReplaceAccentColor(swatch);

        }

        /// <summary>
        /// Attempts to query the current palette configured in the application's resources.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if there is any ambiguity regarding the palette. Provided
        /// standard guidelines have been followed for palette configuration, this should not happen.</exception>
        [Obsolete("This will be removed in a future version. Use one of the other methods.")]
        public Palette QueryPalette()
        {
            ITheme theme = GetTheme();

            //it's not safe to to query for the included swatches, so we find the mid (or accent) colour, 
            //& cross match it with the entirety of all available hues to find the owning swatch.

            //TODO could cache this statically
            var swatchesProvider = new SwatchesProvider();
            var swatchByPrimaryHueIndex = swatchesProvider
                .Swatches
                .SelectMany(s => s.PrimaryHues.Select(h => new { s, h }))
                .ToDictionary(a => a.h.Color, a => a.s);
            var swatchByAccentHueIndex = swatchesProvider
                .Swatches
                .Where(s => s.IsAccented)
                .SelectMany(s => s.AccentHues.Select(h => new { s, h }))
                .ToDictionary(a => a.h.Color, a => a.s);


            if (!swatchByPrimaryHueIndex.TryGetValue(theme.PrimaryMid.Color, out var primarySwatch))
                throw new InvalidOperationException("PrimaryHueMidBrush is not from standard swatches");
            if (!swatchByAccentHueIndex.TryGetValue(theme.SecondaryMid.Color, out var accentSwatch))
                throw new InvalidOperationException("SecondaryAccentBrush is not from standard swatches");


            var primaryLightHueIndex = GetHueIndex(primarySwatch, theme.PrimaryLight.Color, false);
            var primaryMidHueIndex = GetHueIndex(primarySwatch, theme.PrimaryMid.Color, false);
            var primaryDarkHueIndex = GetHueIndex(primarySwatch, theme.PrimaryDark.Color, false);
            var accentHueIndex = GetHueIndex(accentSwatch, theme.SecondaryMid.Color, true);

            return new Palette(primarySwatch, accentSwatch, primaryLightHueIndex, primaryMidHueIndex, primaryDarkHueIndex, accentHueIndex);

            int GetHueIndex(Swatch swatch, Color color, bool isAccent)
            {
                var hues = (isAccent ? swatch.AccentHues : swatch.PrimaryHues);
                var x = hues.Select((h, i) => new { h, i })
                    .FirstOrDefault(a => a.h.Color == color) ??
                    hues.Select((h, i) => new { h, d = h.Color.Difference(color), i })
                        .Where(a => a.d < 2.0)
                        .OrderBy(a => a.d)
                        .Select(a => new { a.h, a.i })
                        .FirstOrDefault();

                if (x == null)
                {
                    throw new InvalidOperationException($"Color {color} not found in swatch {swatch.Name}.");
                }
                return x.i;
            }
        }

        private static void ReplaceEntry(string name, Color color)
        {
            Application.Current.Resources.SetSolidColorBrush(name, color);
        }

        public virtual ITheme GetTheme() => Application.Current.Resources.GetTheme();

        public virtual void SetTheme(ITheme theme)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            Application.Current.Resources.SetTheme(theme);
        }
    }
}
