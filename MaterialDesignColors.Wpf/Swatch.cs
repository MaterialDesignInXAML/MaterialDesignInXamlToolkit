using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class Swatch
    {
        #region Private
        #region Win32
            [DllImport("dwmapi.dll", PreserveSig = false)]
            private static extern bool DwmIsCompositionEnabled();

            [DllImport("dwmapi.dll", EntryPoint = "#127")]
            static extern void DwmGetColorizationParameters(ref DWMCOLORIZATIONPARAMS dp);

            [StructLayout(LayoutKind.Sequential)]
            struct DWMCOLORIZATIONPARAMS
            {
                public UInt32 ColorizationColor;
                public UInt32 ColorizationAfterglow;
                public UInt32 ColorizationColorBalance;
                public UInt32 ColorizationAfterglowBalance;
                public UInt32 ColorizationBlurBalance;
                public UInt32 ColorizationGlassReflectionIntensity;
                public UInt32 ColorizationOpaqueBlend;
            }
        #endregion
        private static Color ToColor(UInt32 value) => Color.FromArgb(255,
                (byte) (value >> 16),
                (byte) (value >> 8),
                (byte) value
                );
        /// <summary>
        /// Matches a non material color to a material color
        /// </summary>
        /// <param name="baseColor">The color to match</param>
        /// <param name="accent">If the color is accent</param>
        /// <returns></returns>
        public static Swatch GetClosestSwatch(Color baseColor, bool accent)
        {
            var colors = new SwatchesProvider(false).Swatches.Select(x => new {Value = x, Diff = GetDiff(x.ExemplarHue.Color, baseColor)}).ToList();
            var min = colors.Min(x => x.Diff);
            var color = colors.FindIndex(x => x.Diff == min);
            return accent ? new SwatchesProvider(false).Swatches.ElementAtOrDefault(color + 1) != null ? new SwatchesProvider(false).Swatches.ElementAtOrDefault(color + 1) : new SwatchesProvider().Swatches.ElementAtOrDefault(color - 1) : new SwatchesProvider(false).Swatches.ElementAt(color);
        }
        private static int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return a*a + r*r + g*g + b*b;
        }
#endregion

        /// <summary>
        /// Primary color generated from DWM color
        /// </summary>
        public static IEnumerable<Hue> AutoPrimary
        {
            get
            {
                if (DwmIsCompositionEnabled())
                {
                    var colorizationParams = new DWMCOLORIZATIONPARAMS();
                    DwmGetColorizationParameters(ref colorizationParams);
                    return GetClosestSwatch(ToColor(colorizationParams.ColorizationColor), false).PrimaryHues;
                }
                else
                    return GetClosestSwatch(Colors.Purple, false).PrimaryHues; //If you use a slow old computer.
            }
        }
        /// <summary>
        /// Accent color generated from DWM color
        /// </summary>
        public static IEnumerable<Hue> AutoAccent
        {
            get
            {
                if (DwmIsCompositionEnabled())
                {
                    var colorizationParams = new DWMCOLORIZATIONPARAMS();
                    DwmGetColorizationParameters(ref colorizationParams);
                    return GetClosestSwatch(ToColor(colorizationParams.ColorizationColor), true).AccentHues;
                }
                else
                    return GetClosestSwatch(Colors.Green, true).AccentHues; //If you use a slow old computer.
            }
        }

        public Swatch(string name, IEnumerable<Hue> primaryHues, IEnumerable<Hue> accentHues)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (primaryHues == null) throw new ArgumentNullException(nameof(primaryHues));
            if (accentHues == null) throw new ArgumentNullException(nameof(accentHues));

            var primaryHuesList = primaryHues.ToList();            
            if (primaryHuesList.Count == 0) throw new ArgumentException("Non primary hues provided.", nameof(primaryHues));

            Name = name;
            PrimaryHues = primaryHuesList;
            var accentHuesList = accentHues.ToList();
            AccentHues = accentHuesList;
            ExemplarHue = primaryHuesList[Math.Min(5, primaryHuesList.Count-1)];
            if (IsAccented)
                AccentExemplarHue = accentHuesList[Math.Min(2, accentHuesList.Count - 1)];
        }

        public string Name { get; }

        public Hue ExemplarHue { get; }

        public Hue AccentExemplarHue { get; }

        public IEnumerable<Hue> PrimaryHues { get; }

        public IEnumerable<Hue> AccentHues { get; }

        public bool IsAccented => AccentHues.Any();
    }
}
