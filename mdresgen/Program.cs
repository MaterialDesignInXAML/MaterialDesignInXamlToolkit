using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Xsl;
using Newtonsoft.Json.Linq;

namespace mdresgen
{
    class Program
    {
        private static readonly IDictionary<string, Color> ClassNameToForegroundIndex = new Dictionary<string, Color>()
        {
            {"color", Color.FromArgb((int) (255*0.87), 255, 255, 255)},
            {"color dark divide", Color.FromArgb((int) (255*0.87), 0, 0, 0)},
            {"color dark", Color.FromArgb((int) (255*0.87), 0, 0, 0)},
            {"color dark-strong", Color.FromArgb((int) (255*0.87), 0, 0, 0)},
            {"color light-strong", Color.FromArgb(255, 255, 255)},
            {"color dark-when-small", Color.Black}
        };

        static void Main(string[] args)
        {
			var xDocument = XDocument.Load("MaterialColourSwatchesSnippet.xml");

			if (args.Length > 0 && args.Contains("json"))	        
		        GenerateJson(xDocument);	        
			else
				GenerateXaml(xDocument);
        }

	    private static void GenerateXaml(XDocument xDocument)
	    {		    
		    const string fileFormat = @"..\..\..\Themes\MaterialDesignColor.{0}.xaml";

		    foreach (var colour in xDocument.Root.Elements("section").Select(ToResourceDictionary))
		    {
			    colour.Item2.Save(string.Format(fileFormat, colour.Item1.Replace(" ", "")));
		    }
	    }

	    private static void GenerateJson(XDocument xDocument)
	    {
			const string file = @"..\..\..\web\MaterialDesignPalette.js";


		    var json = new JArray(
			    xDocument.Root.Elements("section")
				    .Select(sectionElement => new JObject(
					    new JProperty("swatch", GetColourName(sectionElement)),
					    new JProperty("colors",
						    new JArray(
							    sectionElement.Element("ul").Elements("li").Skip(1).Select(CreateJsonColourPair)
							    )
						    )
					    ))
			    ).ToString();

			var javaScript = $"var swatches={json};";

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

		    var foreGroundColorHex = string.Format("#{0}{1}{2}{3}",
			    ByteToHex(foregroundColour.A),
			    ByteToHex(foregroundColour.R),
			    ByteToHex(foregroundColour.G),
			    ByteToHex(foregroundColour.B));

		    return new JObject(
			    new JProperty("backgroundName", string.Format("{0}{1}", prefix, name)),
			    new JProperty("backgroundColour", hex),
			    new JProperty("foregroundName", string.Format("{0}{1}Foreground", prefix, name)),
			    new JProperty("foregroundColour", foreGroundColorHex)
			    );
	    }

	    private static Tuple<string, XDocument> ToResourceDictionary(XElement sectionElement)
        {
            var colour = GetColourName(sectionElement);

            XNamespace defaultNamespace = @"http://schemas.microsoft.com/winfx/2006/xaml/presentation";

            var xNamespace = XNamespace.Get("http://schemas.microsoft.com/winfx/2006/xaml");
            var doc =
                new XDocument(new XElement(defaultNamespace + "ResourceDictionary",
                    new XAttribute(XNamespace.Xmlns + "x", xNamespace)));            

            foreach (var liElement in sectionElement.Element("ul").Elements("li").Skip(1))
            {
                AddColour(liElement, defaultNamespace, xNamespace, doc);
            }
            
            return new Tuple<string, XDocument>(colour, doc);

        }

	    private static string GetColourName(XElement sectionElement)
	    {
		    return sectionElement.Element("ul").Element("li").Elements("span").First().Value;
	    }

	    private static void AddColour(XElement liElement, XNamespace defaultNamespace, XNamespace xNamespace, XDocument doc)
        {
            var name = liElement.Elements("span").First().Value;
            var hex = liElement.Elements("span").Last().Value;

            var prefix = "Primary";
            if (name.StartsWith("A"))
            {
                prefix = "Accent";
                name = name.Skip(1).Aggregate("", (current, next) => current + next);
            }

            var backgroundColourElement = new XElement(defaultNamespace + "Color", hex);
            // new XAttribute()
            backgroundColourElement.Add(new XAttribute(xNamespace + "Key", string.Format("{0}{1}", prefix, name)));
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
            foregroundColourElement.Add(new XAttribute(xNamespace + "Key", string.Format("{0}{1}Foreground", prefix, name)));
            doc.Root.Add(foregroundColourElement);
        }

        private static string ByteToHex(byte b)
        {
            var result = b.ToString("X");
            return result.Length == 1 ? "0" + result : result;
        }
    }
}
