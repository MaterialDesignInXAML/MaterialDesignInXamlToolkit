using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace MaterialDesign.Generators
{
    [Generator]
    public class ThemeExtensionsGenerator : ISourceGenerator
    {
        private static Regex ColorKeyRegex = new(@"MaterialDesign\.Brush\.(?<Property>.+)\.Color");

        public void Execute(GeneratorExecutionContext context)
        {
            //Uncomment to enable debugging
            //System.Diagnostics.Debugger.Launch();

            //Just using this to get the name of the brushes
            GenerateThemeClass(context, "Light");
        }

        private static void GenerateThemeClass(GeneratorExecutionContext context, string theme)
        {
            AdditionalText? resourceDictionary =
                context.AdditionalFiles.SingleOrDefault(file => Path.GetFileName(file.Path) == $"MaterialDesignTheme.{theme}.xaml");

            if (resourceDictionary is null) return;

            using Stream fileStream = File.OpenRead(resourceDictionary.Path);
            XDocument xDoc = XDocument.Load(fileStream);
            StringBuilder code = new();
            code.AppendLine($@"
using System.Windows.Media;
using System;

namespace MaterialDesignThemes.Wpf
{{
    partial class ThemeExtensions
    {{
        public static partial void SetBaseTheme(this Theming.Theme theme, Theming.Theme baseTheme)
        {{
            if (theme is null) throw new ArgumentNullException(nameof(theme));
            if (baseTheme is null) throw new ArgumentNullException(nameof(baseTheme));

");

            XName colorName = XName.Get("Color", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            XName keyName = XName.Get("Key", "http://schemas.microsoft.com/winfx/2006/xaml");
            foreach (XElement element in xDoc.Root.Descendants().Where(x => x.Name == colorName))
            {
                if (element.Attribute(keyName) is { } keyAttribute &&
                    ColorKeyRegex.Match(keyAttribute.Value) is Match keyMatch &&
                    keyMatch.Success)
                {
                    string property = keyMatch.Groups["Property"].Value;

                    code.Append("            ")
                        .AppendLine($"theme.{property} = baseTheme.{property};");
                }
            }
            code.AppendLine(@"
        }
    }
}");
            context.AddSource($"ThemeExtensions.g.cs", code.ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        { }
    }
}
