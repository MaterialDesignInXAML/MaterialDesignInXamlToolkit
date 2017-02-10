using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public enum BadgePlacementMode
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left
    }

    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : ContentControl
    {
        public const string BadgeContainerPartName = "PART_BadgeContainer";

        static Badged()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badged), new FrameworkPropertyMetadata(typeof(Badged)));
        }

        public static readonly DependencyProperty BadgeProperty = DependencyProperty.Register(
            "Badge", typeof(object), typeof(Badged), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.AffectsArrange));

        public object Badge
        {
            get { return (object) GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        public static readonly DependencyProperty BadgeBackgroundProperty = DependencyProperty.Register(
            "BadgeBackground", typeof(Brush), typeof(Badged), new PropertyMetadata(default(Brush)));

        public Brush BadgeBackground
        {
            get { return (Brush) GetValue(BadgeBackgroundProperty); }
            set { SetValue(BadgeBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BadgeForegroundProperty = DependencyProperty.Register(
            "BadgeForeground", typeof(Brush), typeof(Badged), new PropertyMetadata(default(Brush)));

        public Brush BadgeForeground
        {
            get { return (Brush) GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        public static readonly DependencyProperty BadgeColorZoneModeProperty = DependencyProperty.Register(
            "BadgeColorZoneMode", typeof(ColorZoneMode), typeof(Badged), new PropertyMetadata(default(ColorZoneMode)));

        public ColorZoneMode BadgeColorZoneMode
        {
            get { return (ColorZoneMode) GetValue(BadgeColorZoneModeProperty); }
            set { SetValue(BadgeColorZoneModeProperty, value); }
        }

        public static readonly DependencyProperty BadgePlacementModeProperty = DependencyProperty.Register(
            "BadgePlacementMode", typeof(BadgePlacementMode), typeof(Badged), new PropertyMetadata(default(BadgePlacementMode)));

        private FrameworkElement _badgeContainer;

        public BadgePlacementMode BadgePlacementMode
        {
            get { return (BadgePlacementMode) GetValue(BadgePlacementModeProperty); }
            set { SetValue(BadgePlacementModeProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _badgeContainer = GetTemplateChild(BadgeContainerPartName) as FrameworkElement;
        }        

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);

            if (_badgeContainer == null) return result;
            
            var containerDesiredSize = _badgeContainer.DesiredSize;
            if ((containerDesiredSize.Width == 0.0 || containerDesiredSize.Height == 0.0)
                && !double.IsNaN(_badgeContainer.ActualWidth) && !double.IsInfinity(_badgeContainer.ActualWidth)
                && !double.IsNaN(_badgeContainer.ActualHeight) && !double.IsInfinity(_badgeContainer.ActualHeight))
            {
                containerDesiredSize = new Size(_badgeContainer.ActualWidth, _badgeContainer.ActualHeight);
            }

            var h = 0 - containerDesiredSize.Width / 2;
            var v = 0 - containerDesiredSize.Height / 2;
            _badgeContainer.Margin = new Thickness(0);
            _badgeContainer.Margin = new Thickness(h, v, h, v);

            return result;
        }
    }
}
