namespace mdresgen
{
    internal class Icon
    {
        public Icon(string name, string data, IList<string> aliases)
        {
            Ordinal = EnumStringToUniqueSimpleType();
            Name = name;
            Data = data;
            Aliases = aliases ?? new List<string>();

            int EnumStringToUniqueSimpleType()
            {
                // TODO: .GetHashCode() will not work as it changes for each new run
                // Ideally we use a hash-function of some sort, but I don't think any of them will fit within any of the simple types (long/ulong probably the largest)
                return name.GetHashCode();
            }
        }

        public long Ordinal { get; }
        public string Name { get; }
        public string Data { get; }
        public IList<string> Aliases { get; }
    }
}
