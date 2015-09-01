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

namespace MaterialDesignThemes.Wpf
{
    public enum ColorZoneMode
    {
        Standard,
        Inverted,
        PrimaryLight,
        PrimaryMid,
        PrimaryDark,
        Accent
    }

    /// <summary>
    /// User a colour zone to easily switch the background and foreground colours, whilst still remaining within the selected Material Design palette.
    /// </summary>
    public class ColorZone : ContentControl
    {
        static ColorZone()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorZone), new FrameworkPropertyMetadata(typeof(ColorZone)));
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode", typeof (ColorZoneMode), typeof (ColorZone), new PropertyMetadata(default(ColorZoneMode)));

        public ColorZoneMode Mode
        {
            get { return (ColorZoneMode) GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
    }
}
