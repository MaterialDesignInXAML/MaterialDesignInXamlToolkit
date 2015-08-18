using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;

namespace MaterialDesignColors.WpfExample
{
    public class PaletteSelectorViewModel
    {
        public PaletteSelectorViewModel()
        {
            AscertainCurrentResourceDictionaries();
        }

        private void AscertainCurrentResourceDictionaries()
        {
            var lightDarkResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.AbsolutePath, @"(\/MaterialDesignThemes.Wpf;component\/Themes\/MaterialDesignTheme\.)((Light)|(Dark))").Success);
            if (lightDarkResourceDictionary == null)
                throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");
            var lightDarkDescription = Regex.Match(lightDarkResourceDictionary.Source.AbsolutePath, "(?'desc'(Light)|(Dark))(.xaml)$").Groups[
                "desc"].Value;

            ResourceDictionary primaryColorResourceDictionary;
            if (!TryFindSwatchDictionary(Application.Current.Resources, "PrimaryHueMidBrush", out primaryColorResourceDictionary))
                throw new ApplicationException("Unable to find primary color definition in Application resources.");

            ResourceDictionary accentolorResourceDictionary;
            if (!TryFindSwatchDictionary(Application.Current.Resources, "SecondaryAccentBrush", out accentolorResourceDictionary))
                throw new ApplicationException("Unable to find accent color definition in Application resources.");
        }

        private bool TryFindSwatchDictionary(ResourceDictionary parentDictionary, string expectedBrushName, out ResourceDictionary dictionary)
        {
            dictionary = parentDictionary.MergedDictionaries.SingleOrDefault(rd => rd[expectedBrushName] != null);
            return dictionary != null;
        }
    }    
}
