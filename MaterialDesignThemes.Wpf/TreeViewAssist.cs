using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf
{
    public static class TreeViewAssist
    {
        /// <summary>
        /// Allows additional rendering for each tree node, outside of the rippled part of the node which responsds to user selection.                        
        /// </summary>
        /// <remarks>
        /// The content to be rendered is the same of the <see cref="TreeViewItem"/>; i.e the Header property, or
        /// some other content such as a view model, typically when using a <see cref="HierarchicalDataTemplate"/>.
        /// </remarks>
        public static readonly DependencyProperty AdditionalTemplateProperty = DependencyProperty.RegisterAttached(
            "AdditionalTemplate",
            typeof(DataTemplate),
            typeof(TreeViewAssist),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the additional template.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetAdditionalTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(AdditionalTemplateProperty, value);
        }

        /// <summary>
        /// Gets the additional template.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="DataTemplate" />.
        /// </returns>
        public static DataTemplate GetAdditionalTemplate(DependencyObject element)
        {
            return (DataTemplate)element.GetValue(AdditionalTemplateProperty);
        }

        /// <summary>
        /// Allows additional rendering for each tree node, outside of the rippled part of the node which responsds to user selection.                        
        /// </summary>
        /// <remarks>
        /// The content to be rendered is the same of the <see cref="TreeViewItem"/>; i.e the Header property, or
        /// some other content such as a view model, typically when using a <see cref="HierarchicalDataTemplate"/>.
        /// </remarks>
        public static readonly DependencyProperty AdditionalTemplateSelectorProperty = DependencyProperty.RegisterAttached(
            "AdditionalTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(TreeViewAssist),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the additional template selector.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetAdditionalTemplateSelector(DependencyObject element, DataTemplateSelector value)
        {
            element.SetValue(AdditionalTemplateSelectorProperty, value);
        }

        /// <summary>
        /// Gets the additional template selector.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="DataTemplateSelector" />.
        /// </returns>
        public static DataTemplateSelector GetAdditionalTemplateSelector(DependencyObject element)
        {
            return (DataTemplateSelector)element.GetValue(AdditionalTemplateSelectorProperty);
        }

        private static readonly Lazy<DataTemplate> NoAdditionalTemplateProvider = new Lazy<DataTemplate>(CreateEmptyGridDataTemplate);

        /// <summary>
        /// To be used at <see cref="TreeViewItem"/> level, or to be returned by <see cref="AdditionalTemplateSelector"/>
        /// implementors when the additional template associated with a tree should not be used.
        /// </summary>
        public static readonly DataTemplate SuppressAdditionalTemplate = NoAdditionalTemplateProvider.Value;

        public static DataTemplate CreateEmptyGridDataTemplate()
        {
            var xaml = "<DataTemplate><Grid /></DataTemplate>";
            var parserContext = new ParserContext();
            parserContext.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            parserContext.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xaml)))
            {
                return (DataTemplate)XamlReader.Load(memoryStream, parserContext);
            }
        }
    }
}
