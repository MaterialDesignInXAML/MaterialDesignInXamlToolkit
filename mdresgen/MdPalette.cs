using System.Text.Json.Serialization;

namespace mdresgen;

public class MdPalette
{
    [JsonPropertyName("shades")]
    public string[]? shades { get; set; }

    [JsonPropertyName("palettes")]
    public Palette[]? palettes { get; set; }
}

public class Palette
{
    [JsonPropertyName("name")]
    public string? name { get; set; }
    [JsonPropertyName("hexes")]
    public string[]? hexes { get; set; }
}
