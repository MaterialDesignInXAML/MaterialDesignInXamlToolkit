using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Effects;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf
{
    public class ShadowAdorner : Adorner
    {
        private Border _border;
        private DockPanel _child;

        public ShadowAdorner(UIElement element) : base(element)
        {
            _border = new Border() { Margin = new Thickness(16), BorderThickness = new Thickness(1), Effect = new DropShadowEffect { BlurRadius = 20, ShadowDepth = 0, Direction = 0 ,Color = Colors.Black, Opacity = 1 } };
            var dockPanel = new DockPanel() { Background = Brushes.White };
            dockPanel.Children.Add(_border);
            _child = dockPanel;
            AddVisualChild(_child);
        }

        //public ShadowDepth ShadowDepth
        //{
        //    set => _border.Effect = ShadowConverter.Convert(value);
        //}

        protected override int VisualChildrenCount
        {
            get => 1;
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return _child;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var desiredSize = AdornedElement.RenderSize;
            _child.Measure(desiredSize);
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _child.Arrange(new Rect(new Point(0, 0), finalSize));
            return _child.RenderSize;
        }
    }
}
