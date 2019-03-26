using System;
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

        //TODO: Custom delegates, though convenient, don't align with the normal event pattern. Consider custom EventArgs class rather than custom delegate
        public event ThemeChangeHandler ThemeChanged;
        public event PaletteChangeHandler PaletteChanged;
        public event ColorChangeHandler ColorChanged;

        public ThemeManager(ResourceDictionary resources)
        {
            Resources = resources ?? throw new ArgumentNullException(nameof(resources));
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
            if (theme == null) throw new ArgumentNullException(nameof(theme));

            ThemeChanged?.Invoke(Resources, theme);
        }

        public void ChangePalette(ColorPalette palette)
        {
            if (palette == null) throw new ArgumentNullException(nameof(palette));

            PaletteChanged?.Invoke(Resources, palette);
        }

        public void ChangeColor(ColorChange colorChange)
        {
            if (colorChange == null) throw new ArgumentNullException(nameof(colorChange));

            ColorChanged?.Invoke(Resources, colorChange);
        }

        private void ChangeThemeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is IBaseTheme theme)
            {
                ChangeTheme(theme);
            }
        }

        private void ChangePaletteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is ColorPalette palette)
            {
                ChangePalette(palette);
            }
        }

        private void ChangeColorCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is ColorChange colorChange)
            {
                ChangeColor(colorChange);
            }
        }


    }
}
