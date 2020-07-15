using System.Windows;

namespace VTTests
{
    internal static class RectMixins
    {
        public static Point Center(this Rect rect) 
            => new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
    }
}
