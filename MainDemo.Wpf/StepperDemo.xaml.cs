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

using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    public partial class StepperDemo : UserControl
    {
        public static readonly DependencyProperty IsLinearProperty = DependencyProperty.Register(
                nameof(IsLinear), typeof(bool), typeof(StepperDemo), new PropertyMetadata(false));

        public bool IsLinear
        {
            get
            {
                return (bool)GetValue(IsLinearProperty);
            }

            set
            {
                SetValue(IsLinearProperty, value);
            }
        }

        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register(
                nameof(Layout), typeof(StepperLayout), typeof(StepperDemo), new PropertyMetadata(StepperLayout.Horizontal));

        public StepperLayout Layout
        {
            get
            {
                return (StepperLayout)GetValue(LayoutProperty);
            }

            set
            {
                SetValue(LayoutProperty, value);
            }
        }

        public StepperLayout[] Layouts
        {
            get
            {
                return new StepperLayout[] { StepperLayout.Horizontal, StepperLayout.Vertical };
            }
        }

        public static readonly DependencyProperty ContentAnimationsEnabledProperty = DependencyProperty.Register(
                nameof(ContentAnimationsEnabled), typeof(bool), typeof(StepperDemo), new PropertyMetadata(true));

        public bool ContentAnimationsEnabled
        {
            get
            {
                return (bool)GetValue(ContentAnimationsEnabledProperty);
            }

            set
            {
                SetValue(ContentAnimationsEnabledProperty, value);
            }
        }

        public StepperDemo()
        {
            InitializeComponent();

            cbOrientation.DataContext = this;
            cbIsLinear.DataContext = this;
            cbContentAnimationsEnabled.DataContext = this;
            stepper.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://material.io/guidelines/components/steppers.html");
        }
    }
}
