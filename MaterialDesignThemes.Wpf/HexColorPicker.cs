using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HexGridControl;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = HexGridPartName, Type = typeof(HexGrid))]
    [TemplatePart(Name = HsvValueSliderPartName, Type = typeof(Slider))]
    public class HexColorPicker : Control
    {
        public const string HexGridPartName = "PART_ColorHexGrid";
        public const string HsvValueSliderPartName = "PART_HsvValueSlider";


        static HexColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexColorPicker), new FrameworkPropertyMetadata(typeof(HexColorPicker)));
        }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius), typeof(int), typeof(HexColorPicker), new PropertyMetadata(3));

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty HsvValueProperty = DependencyProperty.Register(
            nameof(HsvValue), typeof(double), typeof(HexColorPicker), new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(HsvValueChanged)));

        private static void HsvValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hexColorPicker = (HexColorPicker)d;

            foreach (var c in hexColorPicker.Colors)
            {
                c.ColorBrush = new SolidColorBrush(ColorHelper.Hsv2Rgb(new Hsv(c.Hsv.H, c.Hsv.S, (double)e.NewValue)));
            }
        }

        public double HsvValue
        {
            get { return (double)GetValue(HsvValueProperty); }
            set { SetValue(HsvValueProperty, value); }
        }

        public static readonly DependencyProperty ColorSelectedCommandProperty = DependencyProperty.Register(
            nameof(ColorSelectedCommand), typeof(ICommand), typeof(HexColorPicker));

        public ICommand ColorSelectedCommand
        {
            get { return (ICommand)GetValue(ColorSelectedCommandProperty); }
            set { SetValue(ColorSelectedCommandProperty, value); }
        }

        public static readonly DependencyProperty ColorsProperty = DependencyProperty.Register(
            nameof(Colors), typeof(List<HexColorItem>), typeof(HexColorPicker), new PropertyMetadata(new List<HexColorItem>()));

        public List<HexColorItem> Colors
        {
            get { return (List<HexColorItem>)GetValue(ColorsProperty); }
            set { SetValue(ColorsProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            GenerateColors();

            base.OnApplyTemplate();
        }

        private void GenerateColors()
        {
            var hexList = GetTemplateChild(HexGridPartName) as HexList;

            hexList.SelectionChanged += delegate
            {
                var hsv = ((HexColorItem)hexList.SelectedItem).Hsv;
                var color = ColorHelper.Hsv2Rgb(new Hsv(hsv.H, hsv.S, HsvValue));
                ColorSelectedCommand.Execute(color);
            };

            var slider = GetTemplateChild(HsvValueSliderPartName) as Slider;

            slider.ValueChanged += delegate
            {
                var hsv = ((HexColorItem)hexList.SelectedItem).Hsv;
                var color = ColorHelper.Hsv2Rgb(new Hsv(hsv.H, hsv.S, HsvValue));
                ColorSelectedCommand.Execute(color);
            };

            var rolCols = (int)(((Radius + 1) - 0.5) * 2);

            hexList.RowCount = rolCols;
            hexList.ColumnCount = rolCols;

            var shift = Radius % 2 == 0;

            for (var q = -Radius; q <= Radius; q++)
            {
                var r1 = Math.Max(-Radius, -q - Radius);
                var r2 = Math.Min(Radius, -q + Radius);
                for (var r = r1; r <= r2; r++)
                {
                    var x = q;
                    var y = r;
                    var z = -q - r;

                    var d = (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;

                    var col = shift ? x + (z - (z & 1)) / 2 : x + (z + (z & 1)) / 2;
                    var row = z;

                    var colShifted = col + Radius;
                    var rowShifted = row + Radius;
                    
                    double h = 1;
                    double w = Math.Sqrt(3) *(h/2);

                    var hoz = (x + (z / 2.0)) * w;
                    var vert = z * (3.0 / 4) * h;

                    //var dist = Math.Sqrt(Math.Pow(hoz, 2) + Math.Pow(vert, 2));

                    var rads = Math.Atan2(vert, hoz);
                    var angle = Math.Round((180 / Math.PI) * rads);
                    angle -= 60;
                    var hexDist = (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2.0;

                    var hsv = new Hsv(angle, hexDist / Radius, HsvValue);
                    var color = ColorHelper.Hsv2Rgb(hsv);

                    Colors.Add(new HexColorItem { Column = colShifted, Row = rowShifted, Hsv = hsv, ColorBrush = new SolidColorBrush(color) });
                }
            }
        }
    }
}
