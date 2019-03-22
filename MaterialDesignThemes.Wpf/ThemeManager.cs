using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    public delegate void ThemeChangeHandler(ResourceDictionary resources, IBaseTheme theme);
    public delegate void PaletteChangeHandler(ResourceDictionary resources, ColorPalette palette);
    public delegate void ColorChangeHandler(ResourceDictionary resources, ColorChange colorChange);

    public class ThemeManager
    {
        public ResourceDictionary Resources { get; }

        public IList<ThemeChangeHandler> ThemeChangeHandlers { get; } = new List<ThemeChangeHandler>();
        public IList<PaletteChangeHandler> PaletteChangeHandlers { get; } = new List<PaletteChangeHandler>();
        public IList<ColorChangeHandler> ColorChangeHandlers { get; } = new List<ColorChangeHandler>();

        public ThemeManager(ResourceDictionary resources)
        {
            Resources = resources;
        }

        public ThemeManager AttachThemeEventsToWindow()
        {
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangeThemeCommand, ChangeThemeCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangePaletteCommand, ChangePaletteCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangeColorCommand, ChangeColorCommandExecuted));

            return this;
        }

        public void ChangeTheme(IBaseTheme theme)
        {
            foreach (var handler in ThemeChangeHandlers) handler(Resources, theme);
        }

        public void ChangePalette(ColorPalette palette)
        {
            foreach (var handler in PaletteChangeHandlers) handler(Resources, palette);
        }

        public void ChangeColor(ColorChange colorChange)
        {
            foreach (var handler in ColorChangeHandlers) handler(Resources, colorChange);
        }

        private void ChangeThemeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var theme = (IBaseTheme)e.Parameter;
            ChangeTheme(theme);
        }

        private void ChangePaletteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var palette = (ColorPalette)e.Parameter;
            ChangePalette(palette);
        }

        private void ChangeColorCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var colorChange = (ColorChange)e.Parameter;
            ChangeColor(colorChange);
        }


    }
}
