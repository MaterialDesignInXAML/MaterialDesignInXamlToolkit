using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public delegate void ThemeChangeHandler(ResourceDictionary resources, IBaseTheme theme);
    public delegate void PaletteChangeHandler(ResourceDictionary resources, ColorPalette palette);
    public delegate void ColorChangeHandler(ResourceDictionary resources, ColorChange colorChange);

    public class ThemeManager
    {
        public ResourceDictionary Resources { get; }

        public event ThemeChangeHandler ThemeChangeHandlers;
        public event PaletteChangeHandler PaletteChangeHandlers;
        public event ColorChangeHandler ColorChangeHandlers;

        public ThemeManager(ResourceDictionary resources)
        {
            Resources = resources ?? throw new ArgumentNullException(nameof(resources));
        }

        public void ChangeTheme(IBaseTheme theme)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            ThemeChangeHandlers?.Invoke(Resources, theme);
        }

        public void ChangePalette(ColorPalette palette)
        {
            if (palette == null) throw new ArgumentNullException(nameof(palette));
            PaletteChangeHandlers?.Invoke(Resources, palette);
        }

        public void ChangeColor(ColorChange colorChange)
        {
            if (colorChange == null) throw new ArgumentNullException(nameof(colorChange));
            ColorChangeHandlers?.Invoke(Resources, colorChange);
        }
    }
}
