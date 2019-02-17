using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors.Recommended;
using MaterialDesignColors.Wpf;

namespace MaterialDesignThemes.Wpf
{
    // TODO
    public class MaterialDesignResourceDictionary : ResourceDictionary
    {
        public MaterialDesignResourceDictionary()
        {
            Application.Current.Resources = this;
            MergedDictionaries.Add(MaterialDesignAssist.CreateDefaultThemeDictionary());
            foreach (var p in Theme.GetType().GetProperties().Where(o => o.PropertyType == typeof(Color)))
            {
                if (Application.Current.Resources.Contains(p.Name)) continue;
                Application.Current.Resources[p.Name] = new SolidColorBrush((Color)p.GetValue(Theme));
            }

            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(NavigationCommands.FirstPage, FirstPageExecuted));
        }

        private void FirstPageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private BaseTheme _theme = BaseTheme.Light;

        public BaseTheme Theme
        {
            get => _theme;
            set => this.WithTheme(_theme = value);
        }

        private MaterialDesignColor _primaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor PrimaryColor
        {
            get => _primaryColor;
            set => this.WithPrimaryColor(_primaryColor = value);
        }

        private MaterialDesignColor _secondaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor SecondaryColor
        {
            get => _secondaryColor;
            set => this.WithPrimaryColor(_secondaryColor = value);
        }
    }
}