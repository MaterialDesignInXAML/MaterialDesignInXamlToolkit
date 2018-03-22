using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class RecommendedThemeProvider
    {
        public RecommendedThemeProvider()
            : this(Assembly.GetExecutingAssembly())
        {

        }

        public RecommendedThemeProvider(Assembly assembly)
        {
            var resourcesName = assembly.GetName().Name + ".g";
            var manager = new ResourceManager(resourcesName, assembly);
            var resourceSet = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var dictionaryEntries = resourceSet.OfType<DictionaryEntry>().ToList();
            var assemblyName = assembly.GetName().Name;

            var regex = new Regex(@"^themes\/recommended\/(?<type>((primary)|(accent)))\/materialdesigncolor\.(?<name>[a-z]+)\.baml$");
            
            var swatchProvider = new SwatchesProvider(assembly);

            RecommendedThemes = (from entry in dictionaryEntries
                let match = regex.Match(entry.Key.ToString())
                where match.Success
                let type = match.Groups["type"].Value
                let name = match.Groups["name"].Value
                let resourceDictionary = Read(assemblyName, entry.Key.ToString())
                where resourceDictionary != null
                let swatch = swatchProvider.Swatches.FirstOrDefault(s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0)
                    ?? CreateSwatchFromTheme(name, resourceDictionary)
                let theme = GetTheme(type, resourceDictionary, swatch)
                where theme != null
                select theme).ToArray();

            Theme GetTheme(string type, ResourceDictionary resourceDictionary, Swatch swatch)
            {
                switch (type.ToLowerInvariant())
                {
                    case "primary":
                        Color? lightColor = GetBrushColor("PrimaryHueLightBrush", resourceDictionary);
                        int lightIndex = GetHueIndex(swatch.PrimaryHues, lightColor);

                        Color? midColor = GetBrushColor("PrimaryHueMidBrush", resourceDictionary);
                        int midIndex = GetHueIndex(swatch.PrimaryHues, midColor);

                        Color? darkColor = GetBrushColor("PrimaryHueDarkBrush", resourceDictionary);
                        int darkIndex = GetHueIndex(swatch.PrimaryHues, darkColor);

                        return new PrimaryTheme(swatch, lightIndex, midIndex, darkIndex);
                    case "accent":
                        Color? accentColor = GetBrushColor("SecondaryAccentBrush", resourceDictionary);
                        int accentIndex = GetHueIndex(swatch.AccentHues, accentColor);
                        return new AccentTheme(swatch, accentIndex);
                }

                return null;
            }

            Color? GetBrushColor(string brushName, ResourceDictionary resourceDictionary)
            {
                if (resourceDictionary[brushName] is SolidColorBrush brush)
                {
                    return brush.Color;
                }
                return null;
            }

            int GetHueIndex(IEnumerable<Hue> hues, Color? color)
            {
                if (color == null) return -1;
                int index = 0;
                foreach (Hue hue in hues)
                {
                    if (hue.Color == color)
                        return index;
                    index++;
                }
                return -1;
            }

            Swatch CreateSwatchFromTheme(string name, ResourceDictionary themeDictionary)
            {
                return new Swatch(name, 
                    new[] {"PrimaryHueLightBrush", "PrimaryHueMidBrush", "PrimaryHueDarkBrush"}.Select(x => CreateHue(x, themeDictionary)).Where(x => x != null),
                    new[] {"SecondaryAccentBrush"}.Select(x => CreateHue(x, themeDictionary)).Where(x => x != null));
            }

            Hue CreateHue(string brushName, ResourceDictionary themeDictionary)
            {
                if (themeDictionary[brushName] is SolidColorBrush brush &&
                    themeDictionary[brushName + "Foreground"] is SolidColorBrush foregroundBrush)
                {
                    return new Hue("", brush.Color, foregroundBrush.Color);
                }
                return null;
            }
        }

        public IEnumerable<Theme> RecommendedThemes { get; }

        private static ResourceDictionary Read(string assemblyName, string path)
        {
            if (assemblyName == null || path == null)
                return null;

            return (ResourceDictionary)Application.LoadComponent(new Uri(
                $"/{assemblyName};component/{path.Replace(".baml", ".xaml")}",
                UriKind.RelativeOrAbsolute));
        }

    }

    public abstract class Theme
    {
        public Swatch Swatch { get; }

        protected Theme(Swatch swatch)
        {
            Swatch = swatch;
        }
    }

    public class PrimaryTheme : Theme
    {
        public int LightHueIndex { get; }

        public int MidHueIndex { get; }

        public int DarkHueIndex { get; }

        public PrimaryTheme(Swatch swatch, int lightHueIndex, int midHueIndex, int darkHueIndex) 
            : base(swatch)
        {
            LightHueIndex = lightHueIndex;
            MidHueIndex = midHueIndex;
            DarkHueIndex = darkHueIndex;
        }
    }

    public class AccentTheme : Theme
    {
        public int HueIndex { get; }

        public AccentTheme(Swatch swatch, int hueIndex) : base(swatch)
        {
            HueIndex = hueIndex;
        }
    }
}