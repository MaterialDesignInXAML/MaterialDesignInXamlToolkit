using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace mdresgen
{
    class Program
    {
        private const string BaseSnippetLocation = "MaterialColourSwatchesSnippet.xml";
        private const string MdPaletteJsonLocation = "MdPaletteJson.json";

        // Legacy
        private const string OldXamlFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\MaterialDesignColor.{0}.xaml";
        private const string OldXamlNamedFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\MaterialDesignColor.{0}.Named.xaml";


        private const string XamlPrimaryFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\MaterialDesignColor.{0}.Primary.xaml";
        private const string XamlAccentFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\MaterialDesignColor.{0}.Accent.xaml";
        private const string XamlPrimaryNamedFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\MaterialDesignColor.{0}.Named.Primary.xaml";
        private const string XamlAccentNamedFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\MaterialDesignColor.{0}.Named.Accent.xaml";

        private const string RecommendedPrimaryFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\Recommended\Primary\MaterialDesignColor.{0}.xaml";
        private const string RecommendedAccentFileFormat = @"..\..\..\MaterialDesignColors.Wpf\Themes\Recommended\Accent\MaterialDesignColor.{0}.xaml";
        private const string RecommendedPrimaryTemplateLocation = "RecommendedPrimaryTemplate.xaml";
        private const string RecommendedAccentTemplateLocation = "RecommendedAccentTemplate.xaml";

        private static readonly IDictionary<string, Color> ClassNameToForegroundIndex = new Dictionary<string, Color>()
        {
            {"color", Color.FromArgb((int) (255*0.87), 255, 255, 255)},
            {"color ", Color.FromArgb((int) (255*0.87), 255, 255, 255)},
            {"color dark divide", Color.FromArgb((int) (255*0.87), 0, 0, 0)},
            {"color dark", Color.FromArgb((int) (255*0.87), 0, 0, 0)},
            {"color dark-strong", Color.FromArgb((int) (255*0.87), 0, 0, 0)},
            {"color light-strong", Color.FromArgb(255, 255, 255)},
            {"color dark-when-small", Color.Black}
        };

        static void Main(string[] args)
        {
            var xDocument = XDocument.Load(BaseSnippetLocation);
            var palette = JsonConvert.DeserializeObject<MdPalette>(File.ReadAllText(MdPaletteJsonLocation));

            //https://coolsubhash-tech.blogspot.com/2016/10/resolved-existing-connection-was.html
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (args.Length == 0)
                GenerateXaml(xDocument, false);
            else if (args.Contains("class-swatches"))
                GenerateClasses(palette);
            else if (args.Contains("all-swatches"))
            {
                GenerateXaml(xDocument);
                GenerateXaml(xDocument, true);
                GenerateOldXaml(xDocument);
                GenerateOldXaml(xDocument, true);
            }
            else if (args.Contains("json"))
                GenerateJson(xDocument);
            else if (args.Contains("named"))
                GenerateXaml(xDocument, true);
            else if (args.Contains("old-named"))
                GenerateOldXaml(xDocument, true);
            else if (args.Contains("old"))
                GenerateOldXaml(xDocument, false);
            else if (args.Contains("icons"))
                IconThing.Run();
            else
                GenerateXaml(xDocument, false);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("FINISHED");
            Console.ReadKey();
        }


        private static void GenerateClasses(MdPalette palettes)
        {
            foreach(var palette in palettes.palettes)
            {
                var sb = new StringBuilder();

                var colors = new StringBuilder();
                var hueNames = new StringBuilder();
                var shortName = palette.name.Replace(" ", "");

                for (var i = 0; i < palette.hexes.Length; i++)
                {
                    var colorName = shortName + palettes.shades[i];
                    colors.AppendLine($"\t\tpublic static Color {colorName} {{ get; }} = (Color)ColorConverter.ConvertFromString(\"{palette.hexes[i]}\");");
                    hueNames.AppendLine($"\t\t\t{{ MaterialDesignColor.{colorName}, {colorName} }},");
                }

                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Windows.Media;");
                sb.AppendLine("using MaterialDesignColors.Wpf;");
                sb.AppendLine();
                sb.AppendLine("namespace MaterialDesignColors.Recommended");
                sb.AppendLine("{"); 
                sb.AppendLine($"\tpublic class {shortName}Swatch : ISwatch");
                sb.AppendLine("\t{");
                sb.Append(colors.ToString());
                sb.AppendLine();
                sb.AppendLine($"\t\tpublic string Name {{ get; }} = \"{palette.name}\";");
                sb.AppendLine();
                sb.AppendLine("\t\tpublic IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>");
                sb.AppendLine("\t\t{");
                sb.Append(hueNames.ToString());
                sb.AppendLine("\t\t};"); // Lookup
                sb.AppendLine();
                sb.AppendLine("\t\tpublic IEnumerable<Color> Hues => Lookup.Values;");
                sb.AppendLine("\t}"); // class
                sb.AppendLine("}"); // namespace

                File.WriteAllText($@"..\..\..\MaterialDesignColors.Wpf\Recommended\{shortName}Swatch.cs",
                    sb.ToString());
            }
        }

        private static void GenerateXaml(XDocument xDocument, bool named = false)
        {
            Console.WriteLine("Generating {0} XAMLs & recommended colors", named ? "named" : "regular");
            Console.WriteLine();

            var recommendedPrimary = File.ReadAllText(RecommendedPrimaryTemplateLocation);
            var recommendedAccent = File.ReadAllText(RecommendedAccentTemplateLocation);

            foreach (var color in xDocument.Root.Elements("section"))
            {
                bool primaryEmpty;
                bool accentEmpty;

                var primary = ToResourceDictionary(color, out primaryEmpty, named, ColorMode.PrimaryOnly);
                var accent = ToResourceDictionary(color, out accentEmpty, named, ColorMode.AccentOnly);

                var longcolor = primary.Item1;
                var shortcolor = longcolor.Replace(" ", "");

                if (string.Compare(shortcolor, "black", StringComparison.InvariantCultureIgnoreCase) == 0) continue;

                Console.WriteLine("{0} \t Primary: {1} \t Accent: {2}", longcolor.PadRight(15, ' '), !primaryEmpty, !accentEmpty);

                if (!primaryEmpty)
                {
                    primary.Item2.Save(
                        string.Format(
                            named ? XamlPrimaryNamedFileFormat : XamlPrimaryFileFormat,
                            shortcolor
                            ));

                    File.WriteAllText(
                        string.Format(RecommendedPrimaryFileFormat, shortcolor),
                        recommendedPrimary.Replace("$COLOR", shortcolor).Replace("$LONG_COLOR", longcolor)
                        );
                }

                if (!accentEmpty)
                {
                    accent.Item2.Save(
                        string.Format(
                            named ? XamlAccentNamedFileFormat : XamlAccentFileFormat,
                            shortcolor
                            ));

                    File.WriteAllText(
                        string.Format(RecommendedAccentFileFormat, shortcolor),
                        recommendedAccent.Replace("$COLOR", shortcolor).Replace("$LONG_COLOR", longcolor)
                        );
                }
            }
        }

        private static void GenerateOldXaml(XDocument xDocument, bool named = false)
        {
            Console.WriteLine("Generating old {0} XAMLs", named ? "named" : "regular");

            bool dummy;

            foreach (var color in xDocument.Root.Elements("section").Select(el => ToResourceDictionary(el, out dummy, named)))
            {
                color.Item2.Save(
                    string.Format(
                        named ? OldXamlNamedFileFormat : OldXamlFileFormat,
                        color.Item1.Replace(" ", "")
                        ));
            }
        }

        private static void GenerateJson(XDocument xDocument)
        {
            const string file = @"..\..\..\web\scripts\Swatches.js";


            var json = new JArray(
                xDocument.Root.Elements("section")
                    .Select(sectionElement => new JObject(
                        new JProperty("name", GetColourName(sectionElement)),
                        new JProperty("colors",
                            new JArray(
                                sectionElement.Element("ul").Elements("li").Skip(1).Select(CreateJsonColourPair)
                                )
                            )
                        ))
                ).ToString();

            //var javaScript = $"var swatches={json};";
            var javaScript = string.Format("var swatches={0};", json);

            File.WriteAllText(file, javaScript);
        }

        private static JObject CreateJsonColourPair(XElement liElement)
        {
            var name = liElement.Elements("span").First().Value;
            var hex = liElement.Elements("span").Last().Value;

            var prefix = "Primary";
            if (name.StartsWith("A"))
            {
                prefix = "Accent";
                name = name.Skip(1).Aggregate("", (current, next) => current + next);
            }

            var liClass = liElement.Attribute("class").Value;
            Color foregroundColour;
            if (!ClassNameToForegroundIndex.TryGetValue(liClass, out foregroundColour))
                throw new Exception("Unable to map foreground color from class " + liClass);

            var foreGroundColorHex = string.Format("#{0}{1}{2}",
                ByteToHex(foregroundColour.R),
                ByteToHex(foregroundColour.G),
                ByteToHex(foregroundColour.B));

            var foregroundOpacity = Math.Round((double)foregroundColour.A / (255.0), 2);

            return new JObject(
                new JProperty("backgroundName", string.Format("{0}{1}", prefix, name)),
                new JProperty("backgroundColour", hex),
                new JProperty("foregroundName", string.Format("{0}{1}Foreground", prefix, name)),
                new JProperty("foregroundColour", foreGroundColorHex),
                new JProperty("foregroundOpacity", foregroundOpacity)
                );
        }

        private static Tuple<string, XDocument> ToResourceDictionary(XElement sectionElement, out bool empty, bool named = false, ColorMode mode = ColorMode.All)
        {
            empty = true;

            var colour = GetColourName(sectionElement);

            XNamespace defaultNamespace = @"http://schemas.microsoft.com/winfx/2006/xaml/presentation";

            var xNamespace = XNamespace.Get("http://schemas.microsoft.com/winfx/2006/xaml");
            var doc =
                new XDocument(new XElement(defaultNamespace + "ResourceDictionary",
                    new XAttribute(XNamespace.Xmlns + "x", xNamespace)));

            foreach (var liElement in sectionElement.Element("ul").Elements("li").Skip(1))
            {
                if (AddColour(liElement, defaultNamespace, xNamespace, doc, named ? colour : "", mode))
                    empty = false;
            }

            return new Tuple<string, XDocument>(colour, doc);

        }

        private static string GetColourName(XElement sectionElement)
        {
            return sectionElement.Element("ul").Element("li").Elements("span").First().Value;
        }

        private static bool AddColour(XElement liElement, XNamespace defaultNamespace, XNamespace xNamespace, XDocument doc, string swatchName = "", ColorMode mode = ColorMode.All)
        {
            var name = liElement.Elements("span").First().Value;
            var hex = liElement.Elements("span").Last().Value;

            var prefix = "Primary";
            if (name.StartsWith("A", StringComparison.Ordinal))
            {
                if (mode == ColorMode.PrimaryOnly)
                    return false;

                prefix = "Accent";
                name = name.Skip(1).Aggregate("", (current, next) => current + next);
            }
            else
            {
                if (mode == ColorMode.AccentOnly)
                    return false;
            }

            var backgroundColourElement = new XElement(defaultNamespace + "Color", hex);
            // new XAttribute()
            backgroundColourElement.Add(new XAttribute(xNamespace + "Key", string.Format("{0}{1}{2}", swatchName, prefix, name)));
            doc.Root.Add(backgroundColourElement);

            var liClass = liElement.Attribute("class").Value;
            Color foregroundColour;
            if (!ClassNameToForegroundIndex.TryGetValue(liClass, out foregroundColour))
                throw new Exception("Unable to map foreground color from class " + liClass);

            var foreGroundColorHex = string.Format("#{0}{1}{2}{3}",
                ByteToHex(foregroundColour.A),
                ByteToHex(foregroundColour.R),
                ByteToHex(foregroundColour.G),
                ByteToHex(foregroundColour.B));

            var foregroundColourElement = new XElement(defaultNamespace + "Color", foreGroundColorHex);
            foregroundColourElement.Add(new XAttribute(xNamespace + "Key", string.Format("{0}{1}{2}Foreground", swatchName, prefix, name)));
            doc.Root.Add(foregroundColourElement);

            return true;
        }

        private static string ByteToHex(byte b)
        {
            var result = b.ToString("X");
            return result.Length == 1 ? "0" + result : result;
        }

        enum ColorMode
        {
            All,
            PrimaryOnly,
            AccentOnly
        }
    }
}
