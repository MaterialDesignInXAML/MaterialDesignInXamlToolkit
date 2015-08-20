using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class SwatchesProvider
    {
        public SwatchesProvider()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcesName = assembly.GetName().Name + ".g";
            var manager = new ResourceManager(resourcesName, assembly);
            var resourceSet = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var dictionaryEntries = resourceSet.OfType<DictionaryEntry>().ToList();
            var assemblyName = assembly.GetName().Name;

            var regex = new Regex(@"^themes\/materialdesigncolor\.(?<name>[a-z]+)\.baml$");

            Swatches = dictionaryEntries.Select(de =>
                new
                {
                    key = de.Key.ToString(),
                    match = regex.Match(de.Key.ToString()),                    
                }).Where(a => a.match.Success && a.match.Groups["name"].Value != "black")
                .Select(a => CreateSwatch(a.match.Groups["name"].Value, Read(assemblyName, a.key)))
                .OrderBy(s => s.Name)
                .ToList();                        
        }

        public IEnumerable<Swatch> Swatches { get; }

        private static Swatch CreateSwatch(string name, ResourceDictionary resourceDictionary)
        {
            var primaryHues = new List<Hue>();
            var accentHues = new List<Hue>();

            foreach (var entry in resourceDictionary.OfType<DictionaryEntry>()
                .OrderBy(de => de.Key)
                .Where(de => !de.Key.ToString().EndsWith("Foreground")))
            {
                var targetList = (entry.Key.ToString().StartsWith("Primary") ? primaryHues : accentHues);

                var colour = (Color) entry.Value;
                var foregroundColour = (Color)
                    resourceDictionary.OfType<DictionaryEntry>()
                        .Single(de => de.Key.ToString().Equals(entry.Key.ToString() + "Foreground"))
                        .Value;

                targetList.Add(new Hue(entry.Key.ToString(), colour, foregroundColour));
            }

            return new Swatch(name, primaryHues, accentHues);
        }

        private static ResourceDictionary Read(string assemblyName, string path)
        {
            return (ResourceDictionary)Application.LoadComponent(new Uri(
                $"/{assemblyName};component/{path.Replace(".baml", ".xaml")}",
                UriKind.RelativeOrAbsolute));
        }
    }
}