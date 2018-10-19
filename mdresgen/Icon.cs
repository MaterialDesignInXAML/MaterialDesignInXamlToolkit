using System.Collections.Generic;

namespace mdresgen
{
    internal class Icon
    {
        public Icon(string name, string data, IList<string> aliases)
        {
            Name = name;
            Data = data;
            Aliases = aliases ?? new List<string>();
        }

        public string Name { get; }
        public string Data { get; }
        public IList<string> Aliases { get; }
    }
}