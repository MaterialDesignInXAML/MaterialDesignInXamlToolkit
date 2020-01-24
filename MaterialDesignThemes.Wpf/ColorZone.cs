using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public enum ColorZoneMode
    {
        Standard,
        Inverted,
        PrimaryLight,
        PrimaryMid,
        PrimaryDark,
        Accent,
        Light,
        Dark,
        Custom
    }

    /// <summary>
    /// User a colour zone to easily switch the background and foreground colours, from selected Material Design palette or custom ones.
    /// </summary>
    public class ColorZone : ContentControl
    {
        static ColorZone()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorZone), new FrameworkPropertyMetadata(typeof(ColorZone)));
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            nameof(Mode), typeof(ColorZoneMode), typeof(ColorZone), new PropertyMetadata(default(ColorZoneMode)));

        public ColorZoneMode Mode
        {
            get { return (ColorZoneMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius), typeof(CornerRadius), typeof(ColorZone), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CustomBackgroundProperty = DependencyProperty.Register(
            nameof(CustomBackground), typeof(Brush), typeof(ColorZone), new PropertyMetadata(default(Brush)));

        public Brush CustomBackground
        {
            get { return (Brush)GetValue(CustomBackgroundProperty); }
            set { SetValue(CustomBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CustomForegroundProperty = DependencyProperty.Register(
            nameof(CustomForeground), typeof(Brush), typeof(ColorZone), new PropertyMetadata(default(Brush)));

        public Brush CustomForeground
        {
            get { return (Brush)GetValue(CustomForegroundProperty); }
            set { SetValue(CustomForegroundProperty, value); }
        }
    }
}
