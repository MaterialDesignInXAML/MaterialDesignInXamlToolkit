using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignColors
{
    //TODO: Better name
    public struct PairedColor
    {
        public Color Color { get; set; }

        /// <summary>
        /// The foreground or opposite color. If left null, this will be calculated for you.
        /// Calculated by calling ColorHelper.ContrastingForegroundColor()
        /// </summary>
        public Color? ForegroundColor { get; set; }

        public static implicit operator PairedColor(Color color) => new PairedColor(color);

        public PairedColor(Color color)
        {
            Color = color;
            ForegroundColor = null;
        }

        public PairedColor(Color color, Color? foreground)
        {
            Color = color;
            ForegroundColor = foreground;
        }

        public Color GetForegroundColor() => ForegroundColor ?? Color.ContrastingForegroundColor();
    }
}