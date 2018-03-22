using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialDesignColors
{
    /// <summary>
    /// Defines a single colour swatch.
    /// </summary>
    public class Swatch
    {
        public Swatch(string name, IEnumerable<Hue> primaryHues, IEnumerable<Hue> accentHues)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (primaryHues == null) throw new ArgumentNullException(nameof(primaryHues));
            if (accentHues == null) throw new ArgumentNullException(nameof(accentHues));

            var primaryHuesList = primaryHues.ToList();
            if (primaryHuesList.Count == 0) throw new ArgumentException("Non primary hues provided.", nameof(primaryHues));

            Name = name;
            PrimaryHues = primaryHuesList;
            var accentHuesList = accentHues.ToList();
            AccentHues = accentHuesList;
            ExemplarHue = primaryHuesList[Math.Min(5, primaryHuesList.Count - 1)];
            if (IsAccented)
                AccentExemplarHue = accentHuesList[Math.Min(2, accentHuesList.Count - 1)];
        }

        public string Name { get; }

        public Hue ExemplarHue { get; }

        public Hue AccentExemplarHue { get; }

        public IList<Hue> PrimaryHues { get; }

        public IList<Hue> AccentHues { get; }

        public bool IsAccented => AccentHues.Any();

        public override string ToString()
        {
            return Name;
        }
    }
}
