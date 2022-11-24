namespace mdresgen;

public class MdPalette
{
    public string[]? shades { get; set; }
    public Palette[]? palettes { get; set; }

    public class Palette
    {
        public string? name { get; set; }
        public string[]? hexes { get; set; }
    }
}
