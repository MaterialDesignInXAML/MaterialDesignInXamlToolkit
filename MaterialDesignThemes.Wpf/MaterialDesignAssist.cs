using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf
{
    public static class MaterialDesignAssist
    {

        public static MaterialDesignTheme WithMaterialDesign(this Application application,
            IBaseTheme theme, Color primaryColor, Color secondaryColor, CodePaletteHelper paletteHelper = null)
        {
            if (paletteHelper == null) paletteHelper = MaterialDesignTheme.DefaultPaletteHelper;
            //NB: When the palettes are changed it hunts through the merged dictionaries.
            //Putting this at the beginning to avoid needing to hunt through all of them.

            var mergedThemeDictionary = FindBaseThemeDictionary(application.Resources.MergedDictionaries, ThemeUriFormat);
            if (mergedThemeDictionary != null)
            {
                application.Resources.MergedDictionaries.Remove(mergedThemeDictionary);
            }
            application.Resources.MergedDictionaries.Add(CreateEmptyThemeDictionary());
            application.Resources.MergedDictionaries.Add(CreateEmptyPaletteDictionary());

            if (FindDictionary(application.Resources.MergedDictionaries, string.Format(ThemeUriFormat, "Defaults")) == null)
            {
                application.Resources.MergedDictionaries.Add(CreateDefaultThemeDictionary());
            }

            var mdTheme = MaterialDesignTheme.CreateFromColors(theme, primaryColor, secondaryColor, paletteHelper);
            paletteHelper.SetTheme(theme);
            paletteHelper.SetPrimaryPalette(primaryColor);
            paletteHelper.SetSecondaryPalette(secondaryColor);

            return mdTheme;
        }

        private static readonly Regex _StringFormatParameterReplacement = new Regex(@"{\d+(?<Alignment>,-?\d*)?(?<Format>:.*?)?}", RegexOptions.Compiled);

        //private const string ColorUriFormat =
        //    @"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/{0}/MaterialDesignColor.{1}.xaml";

        private const string ThemeUriFormat =
            @"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{0}.xaml";


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

        private static ResourceDictionary CreateEmptyThemeDictionary()
        {
            return new ResourceDictionary {
                ["ValidationErrorColor"] = new SolidColorBrush(),
                ["MaterialDesignBackground"] = new SolidColorBrush(),
                ["MaterialDesignPaper"] = new SolidColorBrush(),
                ["MaterialDesignCardBackground"] = new SolidColorBrush(),
                ["MaterialDesignToolBarBackground"] = new SolidColorBrush(),
                ["MaterialDesignBody"] = new SolidColorBrush(),
                ["MaterialDesignBodyLight"] = new SolidColorBrush(),
                ["MaterialDesignColumnHeader"] = new SolidColorBrush(),
                ["MaterialDesignCheckBoxOff"] = new SolidColorBrush(),
                ["MaterialDesignCheckBoxDisabled"] = new SolidColorBrush(),
                ["MaterialDesignTextBoxBorder"] = new SolidColorBrush(),
                ["MaterialDesignDivider"] = new SolidColorBrush(),
                ["MaterialDesignSelection"] = new SolidColorBrush(),
                ["MaterialDesignFlatButtonClick"] = new SolidColorBrush(),
                ["MaterialDesignFlatButtonRipple"] = new SolidColorBrush(),
                ["MaterialDesignToolTipBackground"] = new SolidColorBrush(),
                ["MaterialDesignChipBackground"] = new SolidColorBrush(),
                ["MaterialDesignSnackbarBackground"] = new SolidColorBrush(),
                ["MaterialDesignSnackbarMouseOver"] = new SolidColorBrush(),
                ["MaterialDesignSnackbarRipple"] = new SolidColorBrush(),
                ["MaterialDesignTextFieldBoxBackground"] = new SolidColorBrush(),
                ["MaterialDesignTextFieldBoxHoverBackground"] = new SolidColorBrush(),
                ["MaterialDesignTextFieldBoxDisabledBackground"] = new SolidColorBrush(),
                ["MaterialDesignTextAreaBorder"] = new SolidColorBrush(),
                ["MaterialDesignTextAreaInactiveBorder"] = new SolidColorBrush()
            };
        }

        private static ResourceDictionary CreateEmptyPaletteDictionary()
        {
            return new ResourceDictionary {
                ["PrimaryHueLightBrush"] = new SolidColorBrush(),
                ["PrimaryHueLightForegroundBrush"] = new SolidColorBrush(),
                ["PrimaryHueMidBrush"] = new SolidColorBrush(),
                ["PrimaryHueMidForegroundBrush"] = new SolidColorBrush(),
                ["PrimaryHueDarkBrush"] = new SolidColorBrush(),
                ["PrimaryHueDarkForegroundBrush"] = new SolidColorBrush(),
                ["SecondaryHueLightBrush"] = new SolidColorBrush(),
                ["SecondaryHueLightForegroundBrush"] = new SolidColorBrush(),
                ["SecondaryHueMidBrush"] = new SolidColorBrush(),
                ["SecondaryHueMidForegroundBrush"] = new SolidColorBrush(),
                ["SecondaryHueDarkBrush"] = new SolidColorBrush(),
                ["SecondaryHueDarkForegroundBrush"] = new SolidColorBrush(),
                // Compatability
                ["SecondaryAccentBrush"] = new SolidColorBrush(),
                ["SecondaryAccentForegroundBrush"] = new SolidColorBrush()
            };
        }

        //public static ResourceDictionary WithTheme(this ResourceDictionary resourceDictionary, BaseTheme theme)
        //{
        //    if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
        //    ResourceDictionary existing = resourceDictionary.FindThemeDictionary(theme);
        //    if (existing != null)
        //    {
        //        resourceDictionary.MergedDictionaries.Remove(existing);
        //    }
        //    resourceDictionary.MergedDictionaries.Add(CreateThemeDictionary(theme));
        //    return resourceDictionary;
        //}

        // TODO
        //public static ResourceDictionary WithPrimaryColor(this ResourceDictionary resourceDictionary,
        //    Color primaryColor)
        //{
        //    if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
        //    ResourceDictionary existing = resourceDictionary.FindThemeDictionary(primaryColor);
        //    if (existing != null)
        //    {
        //        resourceDictionary.MergedDictionaries.Remove(existing);
        //    }
        //    resourceDictionary.MergedDictionaries.Add(CreateColorThemeDictionary(primaryColor));
        //    return resourceDictionary;
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

        private static ResourceDictionary CreateDefaultThemeDictionary()
            => new ResourceDictionary { Source = new Uri(string.Format(ThemeUriFormat, "Defaults")) };

        //private static Uri GetUri(BaseTheme theme) => new Uri(string.Format(ThemeUriFormat, theme));
        // TODO
        //private static Uri GetUri(PrimaryColor color) => new Uri(string.Format(ColorUriFormat, "Primary", color));

        //private static Uri GetUri(AccentColor color) => new Uri(string.Format(ColorUriFormat, "Accent", color));

        private static ResourceDictionary FindBaseThemeDictionary(IEnumerable<ResourceDictionary> dictionaries, string formatString)
        {
            var regex = new Regex(GetRegexPatternImpl(formatString, "Light|Dark"));
            return dictionaries.FirstOrDefault(x => regex.IsMatch(x.Source?.OriginalString ?? ""));
        }

        private static ResourceDictionary FindDictionary(IEnumerable<ResourceDictionary> dictionaries, string formatString)
        {
            var regex = new Regex(GetRegexPatternImpl(formatString));
            return dictionaries.FirstOrDefault(x => regex.IsMatch(x.Source?.OriginalString ?? ""));
        }

        private static string GetRegexPatternImpl(string stringFormat, string replacement = ".*")
        {
            return string.Join(replacement, _StringFormatParameterReplacement.Split(stringFormat).Select(Regex.Escape));
        }
    }
}