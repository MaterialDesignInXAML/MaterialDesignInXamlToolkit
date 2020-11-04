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
    /// <summary>
    /// Provides access to all colour swatches.  For information regarding Material Design colours see https://www.google.com/design/spec/style/color.html
    /// </summary>
    public class SwatchesProvider
    {
        /// <summary>
        /// Generates an instance reading swatches from the provided assembly, allowing 
        /// colours outside of the standard material palette to be loaded provided the are stored in the expected XAML format.
        /// </summary>
        /// <param name="assembly"></param>
        public SwatchesProvider(Assembly assembly)
        {
            var resourcesName = assembly.GetName().Name + ".g";
            var manager = new ResourceManager(resourcesName, assembly);
            var resourceSet = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var dictionaryEntries = resourceSet.OfType<DictionaryEntry>().ToList();
            string? assemblyName = assembly.GetName().Name;

            var regex = new Regex(@"^themes\/materialdesigncolor\.(?<name>[a-z]+)\.(?<type>primary|accent)\.baml$");

            Swatches =
                dictionaryEntries
                .Select(x => new { key = x.Key.ToString(), match = regex.Match(x.Key.ToString()) })
                .Where(x => x.match.Success && x.match.Groups["name"].Value != "black")
                .GroupBy(x => x.match.Groups["name"].Value)
                .Select(x =>
                CreateSwatch
                (
                    x.Key,
                    Read(assemblyName, x.SingleOrDefault(y => y.match.Groups["type"].Value == "primary")?.key),
                    Read(assemblyName, x.SingleOrDefault(y => y.match.Groups["type"].Value == "accent")?.key)
                ))
                .ToList();
        }

        /// <summary>
        /// Creates a new swatch provider based on standard Material Design colors.
        /// </summary>
        public SwatchesProvider() : this(Assembly.GetExecutingAssembly())
        { }

        public IEnumerable<Swatch> Swatches { get; }

        private static Swatch CreateSwatch(string name, ResourceDictionary? primaryDictionary, ResourceDictionary? accentDictionary)
        {
            return new Swatch(name, GetHues(primaryDictionary), GetHues(accentDictionary));

            static List<Hue> GetHues(ResourceDictionary? resourceDictionary)
            {
                var hues = new List<Hue>();
                if (resourceDictionary != null)
                {
                    foreach (var entry in resourceDictionary.OfType<DictionaryEntry>()
                        .OrderBy(de => de.Key)
                        .Where(de => !(de.Key.ToString() ?? "").EndsWith("Foreground", StringComparison.Ordinal)))
                    {

                        hues.Add(GetHue(resourceDictionary, entry));
                    }
                }
                return hues;
            }

            static Hue GetHue(ResourceDictionary dictionary, DictionaryEntry entry)
            {
                if (!(entry.Value is Color colour))
                {
                    throw new InvalidOperationException($"Entry {entry.Key} was not of type {nameof(Color)}");
                }
                string foregroundKey = entry.Key?.ToString() + "Foreground";
                if (!(dictionary.OfType<DictionaryEntry>()
                        .Single(de => string.Equals(de.Key.ToString(), foregroundKey, StringComparison.Ordinal))
                        .Value is Color foregroundColour))
                {
                    throw new InvalidOperationException($"Entry {foregroundKey} was not of type {nameof(Color)}");
                }

                return new Hue(entry.Key?.ToString() ?? "", colour, foregroundColour);
            }
        }

        private static ResourceDictionary? Read(string? assemblyName, string? path)
        {
            if (assemblyName is null || path is null)
                return null;

            return (ResourceDictionary)Application.LoadComponent(new Uri(
                $"/{assemblyName};component/{path.Replace(".baml", ".xaml")}",
                UriKind.RelativeOrAbsolute));
        }
    }
}