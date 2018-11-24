namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public MaterialDesignTheme(BaseTheme theme, PrimaryColor primaryColor, AccentColor accentColor, Palette palette)
        {
            BaseTheme = theme;
            PrimaryColor = primaryColor;
            AccentColor = accentColor;
            Palette = palette;
        }

        public BaseTheme BaseTheme { get; }
        public PrimaryColor PrimaryColor { get; }
        public AccentColor AccentColor { get; }
        public Palette Palette { get; }
    }

    public enum AccentColor
    {
        Amber,
        Blue,
        Cyan,
        DeepOrange,
        DeepPurple,
        Green,
        Indigo,
        LightBlue,
        LightGreen,
        Lime,
        Orange,
        Pink,
        Purple,
        Red,
        Teal,
        Yellow
    }

    public enum PrimaryColor
    {
        Amber,
        Blue,
        BlueGrey,
        Brown,
        Cyan,
        DeepOrange,
        DeepPurple,
        Green,
        Grey,
        Indigo,
        LightBlue,
        LightGreen,
        Lime,
        Orange,
        Pink,
        Purple,
        Red,
        Teal,
        Yellow
    }
}