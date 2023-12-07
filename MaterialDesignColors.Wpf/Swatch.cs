namespace MaterialDesignColors;

/// <summary>
/// Defines a single colour swatch.
/// </summary>
public class Swatch
{
    public Swatch(string name, IEnumerable<Hue> primaryHues, IEnumerable<Hue> secondaryHues)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (primaryHues is null) throw new ArgumentNullException(nameof(primaryHues));
        if (secondaryHues is null) throw new ArgumentNullException(nameof(secondaryHues));

        var primaryHuesList = primaryHues.ToList();
        if (primaryHuesList.Count == 0) throw new ArgumentException("Non primary hues provided.", nameof(primaryHues));

        Name = name;
        PrimaryHues = primaryHuesList;
        var secondaryHuesList = secondaryHues.ToList();
        SecondaryHues = secondaryHuesList;
        ExemplarHue = primaryHuesList[ExemplarHueIndex];
        if (SecondaryHueIndex >= 0 && SecondaryHueIndex < secondaryHuesList.Count)
        {
            SecondaryExemplarHue = secondaryHuesList[SecondaryHueIndex];
        }
    }

    public string Name { get; }

    public Hue ExemplarHue { get; }

    public Hue? SecondaryExemplarHue { get; }

    public IList<Hue> PrimaryHues { get; }

    public IList<Hue> SecondaryHues { get; }

    public override string ToString() => Name;

    public int ExemplarHueIndex => Math.Min(5, PrimaryHues.Count - 1);

    public int SecondaryHueIndex => Math.Min(2, SecondaryHues.Count - 1);
}
