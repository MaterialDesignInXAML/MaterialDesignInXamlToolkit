using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignColors.Wpf;

namespace MaterialDesignThemes.Wpf
{
    public static class ResourceDictionaryExtensions
    {
        public static ResourceDictionary WithTheme(this ResourceDictionary resourceDictionary, BaseTheme theme)
        {
            var baseTheme = theme == BaseTheme.Light ? MaterialDesignTheme.Light : MaterialDesignTheme.Dark;
            return resourceDictionary.WithTheme(baseTheme);
        }

        public static ResourceDictionary WithTheme(this ResourceDictionary resourceDictionary, IBaseTheme theme)
        {
            foreach (var p in theme.GetColorProperties())
            {
                resourceDictionary.ReplaceEntry(p.Name, new SolidColorBrush((Color)p.GetValue(theme)));
            }

            return resourceDictionary;
        }

        private static IEnumerable<PropertyInfo> GetColorProperties(this object obj)
        {
            return obj.GetType().GetProperties().Where(o => o.PropertyType == typeof(Color));
        }
        // TODO
        public static ResourceDictionary WithPrimaryColor(this ResourceDictionary resourceDictionary, MaterialDesignColor primaryColor)
        {
            var color = SwatchHelper.Lookup[primaryColor];
            return resourceDictionary.WithPrimaryColor(color);
        }

        public static ResourceDictionary WithPrimaryColor(this ResourceDictionary resourceDictionary, Color color)
        {
            return resourceDictionary.WithPalette(PaletteName.Primary.ToString(), color);
        }

        public static ResourceDictionary WithSecondaryColor(this ResourceDictionary resourceDictionary, MaterialDesignColor secondaryColor)
        {
            var color = SwatchHelper.Lookup[secondaryColor];
            return resourceDictionary.WithSecondaryColor(color);
        }

        public static ResourceDictionary WithSecondaryColor(this ResourceDictionary resourceDictionary, Color color)
        {
            return resourceDictionary.WithPalette(PaletteName.Secondary.ToString(), color);
        }

        public static ResourceDictionary WithPalette(this ResourceDictionary resourceDictionary, string name, Color color)
        {
            return resourceDictionary.WithPalette(new ColorPalette(name, color));
        }

        public static ResourceDictionary WithPalette(this ResourceDictionary resourceDictionary, ColorPalette palette)
        {
            var name = palette.Name;
            resourceDictionary.ReplaceEntry($"{name}HueLightBrush", new SolidColorBrush(palette.Light));
            resourceDictionary.ReplaceEntry($"{name}HueLightForegroundBrush", new SolidColorBrush(palette.LightForeground));
            resourceDictionary.ReplaceEntry($"{name}HueMidBrush", new SolidColorBrush(palette.Mid));
            resourceDictionary.ReplaceEntry($"{name}HueMidForegroundBrush", new SolidColorBrush(palette.MidForeground));
            resourceDictionary.ReplaceEntry($"{name}HueDarkBrush", new SolidColorBrush(palette.Dark));
            resourceDictionary.ReplaceEntry($"{name}HueDarkForegroundBrush", new SolidColorBrush(palette.DarkForeground));

            // moved this yet again, as long as it covers the migrtion path...
            if(name == PaletteName.Secondary.ToString())
            {
                // backwards compatability for now
                resourceDictionary.ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(palette.Mid));
                resourceDictionary.ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(palette.MidForeground));
            }

            return resourceDictionary;
        }

        public static ResourceDictionary WithColor(this ResourceDictionary resourceDictionary, string name, Color color)
        {
            resourceDictionary.ReplaceEntry(name, new SolidColorBrush(color));
            return resourceDictionary;
        }

        /// <summary>
        /// Replaces a certain entry anywhere in the source dictionary and its merged dictionaries
        /// </summary>
        /// <param name="sourceDictionary">The source dictionary to start with</param>
        /// <param name="name">The entry to replace</param>
        /// <param name="value">The new entry value</param>
        public static void ReplaceEntry(this ResourceDictionary sourceDictionary, object name, object value)
        {
            if (sourceDictionary.Contains(name))
            {
                if (sourceDictionary[name] is SolidColorBrush brush &&
                    !brush.IsFrozen && value is SolidColorBrush newBrush)
                {
                    var animation = new ColorAnimation {
                        From = brush.Color,
                        To = newBrush.Color,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                }
                else
                {
                    sourceDictionary[name] = value; //Set value normally
                }
            }

            foreach (var dictionary in sourceDictionary.MergedDictionaries)
            {
                ReplaceEntry(dictionary, name, value);
            }

        }

        //public static MaterialDesignTheme WithMaterialDesign(this Application application,
        //    IBaseTheme theme, Color primaryColor, Color secondaryColor, CodePaletteHelper paletteHelper = null)
        //{
        //    if (paletteHelper == null) paletteHelper = MaterialDesignTheme.DefaultPaletteHelper;
        //    //NB: When the palettes are changed it hunts through the merged dictionaries.
        //    //Putting this at the beginning to avoid needing to hunt through all of them.

        //    var mergedThemeDictionary = FindBaseThemeDictionary(application.Resources.MergedDictionaries, ThemeUriFormat);
        //    if (mergedThemeDictionary != null)
        //    {
        //        application.Resources.MergedDictionaries.Remove(mergedThemeDictionary);
        //    }
        //    application.Resources.MergedDictionaries.Add(CreateEmptyThemeDictionary());
        //    application.Resources.MergedDictionaries.Add(CreateEmptyPaletteDictionary());

        //    if (FindDictionary(application.Resources.MergedDictionaries, string.Format(ThemeUriFormat, "Defaults")) == null)
        //    {
        //        application.Resources.MergedDictionaries.Add(CreateDefaultThemeDictionary());
        //    }

        //    var mdTheme = MaterialDesignTheme.CreateFromColors(theme, primaryColor, secondaryColor, paletteHelper);
        //    paletteHelper.SetTheme(theme);
        //    paletteHelper.SetPrimaryPalette(primaryColor);
        //    paletteHelper.SetSecondaryPalette(secondaryColor);

        //    return mdTheme;
        //}

        //private static readonly Regex _StringFormatParameterReplacement = new Regex(@"{\d+(?<Alignment>,-?\d*)?(?<Format>:.*?)?}", RegexOptions.Compiled);

        //private const string ColorUriFormat =
        //    @"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/{0}/MaterialDesignColor.{1}.xaml";



        //public static MaterialDesignTheme WithMaterialDesign(this Application application,
        //    IBaseTheme theme, Color primaryColor, Color secondaryColor, IPaletteHelper paletteHelper = null)
        //{
        //    if (paletteHelper == null) paletteHelper = DefaultPaletteHelper;
        //    //NB: When the palettes are changed it hunts through the merged dictionaries.
        //    //Putting this at the beginning to avoid needing to hunt through all of them.
        //    application.Resources.MergedDictionaries.Add(CreateEmptyPaletteDictionary());
        //    if (FindDictionary(application.Resources.MergedDictionaries, string.Format(ThemeUriFormat, "Defaults")) == null)
        //    {
        //        application.Resources.MergedDictionaries.Add(CreateDefaultThemeDictionary());
        //    }
        //    paletteHelper.SetTheme(theme);
        //    paletteHelper.SetPalettes(primaryColor, secondaryColor);
        //    return new MaterialDesignTheme(paletteHelper, theme, primaryColor, secondaryColor);
        //}

        //public static ResourceDictionary WithAccentColor(this ResourceDictionary resourceDictionary,
        //    AccentColor accentColor)
        //{
        //    if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
        //    ResourceDictionary existing = resourceDictionary.FindThemeDictionary(accentColor);
        //    if (existing != null)
        //    {
        //        resourceDictionary.MergedDictionaries.Remove(existing);
        //    }
        //    resourceDictionary.MergedDictionaries.Add(CreateColorThemeDictionary(accentColor));
        //    return resourceDictionary;
        //}

        //public static ResourceDictionary FindThemeDictionary(this ResourceDictionary resourceDictionary,
        //    BaseTheme theme)
        //{
        //    if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
        //    return FindDictionary<BaseTheme>(resourceDictionary.MergedDictionaries, ThemeUriFormat);
        //}
        // TODO
        //public static ResourceDictionary FindThemeDictionary(this ResourceDictionary resourceDictionary,
        //    PrimaryColor primaryColor)
        //{
        //    if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
        //    return FindDictionary<PrimaryColor>(resourceDictionary.MergedDictionaries,
        //        string.Format(ColorUriFormat, "Primary", "{0}"));
        //}

        //public static ResourceDictionary FindThemeDictionary(this ResourceDictionary resourceDictionary,
        //    AccentColor accentColor)
        //{
        //    if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
        //    return FindDictionary<PrimaryColor>(resourceDictionary.MergedDictionaries,
        //        string.Format(ColorUriFormat, "Accent", "{0}"));
        //}



        //private static ResourceDictionary CreateThemeDictionary(BaseTheme theme)
        //    => new ResourceDictionary { Source = GetUri(theme),  };
        // TODO
        //private static ResourceDictionary CreateColorThemeDictionary(PrimaryColor color)
        //    => new ResourceDictionary { Source = GetUri(color) };

        //private static ResourceDictionary CreateColorThemeDictionary(AccentColor color)
        //    => new ResourceDictionary { Source = GetUri(color) };


        //private static Uri GetUri(BaseTheme theme) => new Uri(string.Format(ThemeUriFormat, theme));
        // TODO
        //private static Uri GetUri(PrimaryColor color) => new Uri(string.Format(ColorUriFormat, "Primary", color));

        //private static Uri GetUri(AccentColor color) => new Uri(string.Format(ColorUriFormat, "Accent", color));

        //private static ResourceDictionary FindBaseThemeDictionary(IEnumerable<ResourceDictionary> dictionaries, string formatString)
        //{
        //    var regex = new Regex(GetRegexPatternImpl(formatString, "Light|Dark"));
        //    return dictionaries.FirstOrDefault(x => regex.IsMatch(x.Source?.OriginalString ?? ""));
        //}

        //private static ResourceDictionary FindDictionary(IEnumerable<ResourceDictionary> dictionaries, string formatString)
        //{
        //    var regex = new Regex(GetRegexPatternImpl(formatString));
        //    return dictionaries.FirstOrDefault(x => regex.IsMatch(x.Source?.OriginalString ?? ""));
        //}

        //private static string GetRegexPatternImpl(string stringFormat, string replacement = ".*")
        //{
        //    return string.Join(replacement, _StringFormatParameterReplacement.Split(stringFormat).Select(Regex.Escape));
        //}
    }
}