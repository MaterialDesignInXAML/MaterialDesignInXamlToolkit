using System.Windows.Media;

namespace MaterialDesignColors;

public interface ISwatch
{
    string Name { get; }
    IDictionary<MaterialDesignColor, Color> Lookup { get; }
}
