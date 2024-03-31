using System.Text.Json.Serialization;

namespace mdresgen;

public class MdPalette
{
    [JsonPropertyName("shades")]
    public string[]? Shades { get; set; }

    [JsonPropertyName("palettes")]
    public Palette[]? Palettes { get; set; }
}

public class Palette
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("hexes")]
    public string[]? Hexes { get; set; }
}
