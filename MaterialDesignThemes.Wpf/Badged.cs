using System.Windows;
using ControlzEx;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : BadgedEx
    {
        static Badged()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badged), new FrameworkPropertyMetadata(typeof(Badged)));
        }

        public static readonly DependencyProperty BadgeColorZoneModeProperty = DependencyProperty.Register(
            "BadgeColorZoneMode", typeof(ColorZoneMode), typeof(Badged), new PropertyMetadata(default(ColorZoneMode)));

        public ColorZoneMode BadgeColorZoneMode
        {
            get { return (ColorZoneMode) GetValue(BadgeColorZoneModeProperty); }
            set { SetValue(BadgeColorZoneModeProperty, value); }
        }
    }
}
