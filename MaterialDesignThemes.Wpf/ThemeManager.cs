using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignThemes.Wpf
{
    public class ThemeManager : ContentControl
    {
        public ThemeManager()
        {
            CommandManager.RegisterClassCommandBinding(typeof(ThemeManager), new CommandBinding(MaterialDesignTheme.ChangeThemeCommand, ChangeThemeCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(ThemeManager), new CommandBinding(MaterialDesignTheme.ChangePaletteCommand, ChangePaletteCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(ThemeManager), new CommandBinding(MaterialDesignTheme.ChangeColorCommand, ChangeColorCommandExecuted));
        }

        public void ChangeThemeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PropagateThemeChange(MaterialDesignTheme.ChangeThemeCommand, e.Parameter);
        }

        public void ChangePaletteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PropagateThemeChange(MaterialDesignTheme.ChangePaletteCommand, e.Parameter);
        }

        public virtual void ChangeColorCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PropagateThemeChange(MaterialDesignTheme.ChangeColorCommand, e.Parameter);
        }

        private void PropagateThemeChange(ICommand command, object parameter)
        {
            if (parameter is ThemeManagerThemeChange) command.Execute(parameter);
            else command.Execute(new ThemeManagerThemeChange { OwningResourceDictionary = Resources, OriginalParameter = parameter });
        }

        private BaseTheme _theme = BaseTheme.Light;

        public BaseTheme Theme
        {
            get => _theme;
            set => Resources.WithTheme(_theme = value);
        }

        private MaterialDesignColor _primaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor PrimaryColor
        {
            get => _primaryColor;
            set => Resources.WithPrimaryColor(_primaryColor = value);
        }

        private MaterialDesignColor _secondaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor SecondaryColor
        {
            get => _secondaryColor;
            set => Resources.WithSecondaryColor(_secondaryColor = value);
        }
    }
}
