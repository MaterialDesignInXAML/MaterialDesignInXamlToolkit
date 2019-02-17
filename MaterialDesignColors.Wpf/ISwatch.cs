using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors
{
    public interface ISwatch
    {
        string Name { get; }
        IEnumerable<Color> Hues { get; }
        IDictionary<MaterialDesignColor, Color> Lookup { get; }
    }
}
