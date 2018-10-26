namespace mdresgen
{
    class MdPalette
    {
        public string[] shades { get; set; }
        public palette[] palettes { get; set; }

        public class palette
        {
            public string name { get; set; }
            public string[] hexes { get; set; }
        }
    }
}
