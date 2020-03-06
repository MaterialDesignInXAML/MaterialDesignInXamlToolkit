using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ShadowEdgeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length != 4)
            {
                return Binding.DoNothing;
            }

            if (!(values[0] is double) || !(values[1] is double) || !(values[2] is ShadowDepth) ||
                !(values[3] is ShadowEdges))
            {
                return Binding.DoNothing;
            }

            double width = (double)values[0];
            double height = (double)values[1];
            if (double.IsNaN(width) || double.IsInfinity(width) || double.IsNaN(height) || double.IsInfinity(height))
            {
                return Binding.DoNothing;
            }
            DropShadowEffect dropShadow = ShadowInfo.GetDropShadow((ShadowDepth)values[2]);
            if (dropShadow == null)
            {
                return null;
            }

            ShadowEdges edges = (ShadowEdges)values[3];
            double blurRadius = dropShadow.BlurRadius;

            var rect = new Rect(0, 0, width, height);

            if (edges.HasFlag(ShadowEdges.Left))
            {
                rect = new Rect(rect.X - blurRadius, rect.Y, rect.Width + blurRadius, rect.Height);
            }
            if (edges.HasFlag(ShadowEdges.Top))
            {
                rect = new Rect(rect.X, rect.Y - blurRadius, rect.Width, rect.Height + blurRadius);
            }
            if (edges.HasFlag(ShadowEdges.Right))
            {
                rect = new Rect(rect.X, rect.Y, rect.Width + blurRadius, rect.Height);
            }
            if (edges.HasFlag(ShadowEdges.Bottom))
            {
                rect = new Rect(rect.X, rect.Y, rect.Width, rect.Height + blurRadius);
            }

            var size = new GeometryDrawing(new SolidColorBrush(Colors.White), new Pen(), new RectangleGeometry(rect));
            return new DrawingBrush(size)
            {
                Stretch = Stretch.None,
                TileMode = TileMode.None,
                Viewport = rect,
                ViewportUnits = BrushMappingMode.Absolute,
                Viewbox = rect,
                ViewboxUnits = BrushMappingMode.Absolute
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}