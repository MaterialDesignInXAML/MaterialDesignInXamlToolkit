using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HexGridControl.Converters
{
    public class HexClipConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double w = (double)values[0];
            double h = (double)values[1];
            Orientation o = (Orientation) values[2];

            if (w <= 0 || h <= 0)
                return null;

            PathFigure figure = o == Orientation.Horizontal
                ? new PathFigure
                  {
                      StartPoint = new Point(0, h*0.5),
                      Segments =
                      {
                          new LineSegment {Point = new Point(w*0.25, 0)},
                          new LineSegment {Point = new Point(w*0.75, 0)},
                          new LineSegment {Point = new Point(w, h*0.5)},
                          new LineSegment {Point = new Point(w*0.75, h)},
                          new LineSegment {Point = new Point(w*0.25, h)},
                      }
                  }
                : new PathFigure
                  {
                      StartPoint = new Point(w*0.5, 0),
                      Segments =
                      {
                          new LineSegment {Point = new Point(w, h*0.25)},
                          new LineSegment {Point = new Point(w, h*0.75)},
                          new LineSegment {Point = new Point(w*0.5, h)},
                          new LineSegment {Point = new Point(0, h*0.75)},
                          new LineSegment {Point = new Point(0, h*0.25)},
                      }
                  };
            return new PathGeometry { Figures = { figure } };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
