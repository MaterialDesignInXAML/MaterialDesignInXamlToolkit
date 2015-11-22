using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class SwatchesProvider
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled();
        public SwatchesProvider ()
	    {
            new SwatchesProvider(true);
	    }
        public SwatchesProvider(bool overflow)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcesName = assembly.GetName().Name + ".g";
            var manager = new ResourceManager(resourcesName, assembly);
            var resourceSet = manager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var dictionaryEntries = resourceSet.OfType<DictionaryEntry>().ToList();
            var assemblyName = assembly.GetName().Name;

            var regex = new Regex(@"^themes\/materialdesigncolor\.(?<name>[a-z]+)\.(?<type>primary|accent)\.baml$");

            Swatches = new List<Swatch>();
            if (overflow && DwmIsCompositionEnabled())
                Swatches.Add(new Swatch("Auto", Swatch.AutoPrimary, Swatch.AutoAccent));
            Swatches.AddRange(
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
                .ToList());
        }

        public List<Swatch> Swatches { get; }

        private static Swatch CreateSwatch(string name, ResourceDictionary primaryDictionary, ResourceDictionary accentDictionary)
        {
            var primaryHues = new List<Hue>();
            var accentHues = new List<Hue>();

            if (primaryDictionary != null)
            {
                foreach (var entry in primaryDictionary.OfType<DictionaryEntry>()
                    .OrderBy(de => de.Key)
                    .Where(de => !de.Key.ToString().EndsWith("Foreground", StringComparison.Ordinal)))
                {
                    var colour = (Color)entry.Value;
                    var foregroundColour = (Color)
                        primaryDictionary.OfType<DictionaryEntry>()
                            .Single(de => de.Key.ToString().Equals(entry.Key.ToString() + "Foreground"))
                            .Value;

                    primaryHues.Add(new Hue(entry.Key.ToString(), colour, foregroundColour));
                }
            }

            if (accentDictionary != null)
            {
                foreach (var entry in accentDictionary.OfType<DictionaryEntry>()
                    .OrderBy(de => de.Key)
                    .Where(de => !de.Key.ToString().EndsWith("Foreground", StringComparison.Ordinal)))
                {
                    var colour = (Color)entry.Value;
                    var foregroundColour = (Color)
                        accentDictionary.OfType<DictionaryEntry>()
                            .Single(de => de.Key.ToString().Equals(entry.Key.ToString() + "Foreground"))
                            .Value;

                    accentHues.Add(new Hue(entry.Key.ToString(), colour, foregroundColour));
                }
            }

            return new Swatch(name, primaryHues, accentHues);
        }

        private static ResourceDictionary Read(string assemblyName, string path)
        {
            if (assemblyName == null || path == null)
                return null;

            return (ResourceDictionary)Application.LoadComponent(new Uri(
                $"/{assemblyName};component/{path.Replace(".baml", ".xaml")}",
                UriKind.RelativeOrAbsolute));
        }
    }
}