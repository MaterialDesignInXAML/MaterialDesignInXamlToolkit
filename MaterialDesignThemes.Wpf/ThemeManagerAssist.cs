using System;
using System.Windows;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    public static class ThemeManagerAssist
    {
        //public static ThemeManager_old AttachThemeEventsToWindow(this ThemeManager_old themeManager)
        //{
        //    Register<IBaseTheme>(MaterialDesignTheme.ChangeThemeCommand, themeManager.ChangeTheme);
        //    Register<ColorPalette>(MaterialDesignTheme.ChangePaletteCommand, themeManager.ChangePalette);
        //    Register<ColorChange>(MaterialDesignTheme.ChangeColorCommand, themeManager.ChangeColor);
        //
        //    return themeManager;
        //}

        private static void CanExecute<T>(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter is T;
        }

        private static void Register<T>(ICommand command, Action<T> action)
        {
            CommandManager.RegisterClassCommandBinding(typeof(Window),
                new CommandBinding(command, (_, e) => action((T)e.Parameter), CanExecute<T>));
        }
    }
}