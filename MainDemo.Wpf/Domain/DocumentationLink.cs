using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class DocumentationLink
    {
        public DocumentationLink(DocumentationLinkType type, string url) : this(type, url, null)
        {
        }

        public DocumentationLink(DocumentationLinkType type, string url, string label)
        {
            Label = label ?? type.ToString();
            Url = url;
            Type = type;
            Open = new AnotherCommandImplementation(Execute);
        }        

        public static DocumentationLink WikiLink(string page, string label)
        {
            return new DocumentationLink(DocumentationLinkType.Wiki,
                $"{ConfigurationManager.AppSettings["GitHub"]}/wiki/" + page, label);
        }

        public static DocumentationLink StyleLink(string nameChunk)
        {            
            return new DocumentationLink(
                DocumentationLinkType.StyleSource,
                $"{ConfigurationManager.AppSettings["GitHub"]}/blob/master/MaterialDesignThemes.Wpf/Themes/MaterialDesignTheme.{nameChunk}.xaml",
                nameChunk);
        }

        public static DocumentationLink ApiLink<TClass>(string subNamespace)
        {
            var typeName = typeof(TClass).Name;

            return new DocumentationLink(
                DocumentationLinkType.ControlSource,
                $"{ConfigurationManager.AppSettings["GitHub"]}/blob/master/MaterialDesignThemes.Wpf/{subNamespace}/{typeName}.cs",
                typeName);
        }


        public static DocumentationLink ApiLink<TClass>()
        {
            var typeName = typeof(TClass).Name;

            return new DocumentationLink(
                DocumentationLinkType.ControlSource,
                $"{ConfigurationManager.AppSettings["GitHub"]}/blob/master/MaterialDesignThemes.Wpf/{typeName}.cs",
                typeName);
        }

        public static DocumentationLink DemoPageLink<TDemoPage>()
        {
            return DemoPageLink<TDemoPage>(null);
        }

        public static DocumentationLink DemoPageLink<TDemoPage>(string label)
        {
            var ext = typeof(UserControl).IsAssignableFrom(typeof(TDemoPage))
                ? "xaml"
                : "cs";

            return new DocumentationLink(
                DocumentationLinkType.DemoPageSource,
                $"{ConfigurationManager.AppSettings["GitHub"]}/blob/master/MainDemo.Wpf/{typeof(TDemoPage).Name}.{ext}",
                label ?? typeof(TDemoPage).Name);
        }

        public string Label { get; }

        public string Url { get; }

        public DocumentationLinkType Type { get; }        

        public ICommand Open { get; }

        private void Execute(object o)
        {
            System.Diagnostics.Process.Start(Url);
        }
    }
}