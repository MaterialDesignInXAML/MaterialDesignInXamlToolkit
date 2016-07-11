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

using MaterialDesignDemo.Domain;

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

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
                nameof(Orientation), typeof(StepperOrientation), typeof(StepperDemo), new PropertyMetadata(StepperOrientation.Horizontal));

        public StepperOrientation Orientation
        {
            get
            {
                return (StepperOrientation)GetValue(OrientationProperty);
            }

            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        public StepperOrientation[] Orientations
        {
            get
            {
                return new StepperOrientation[] { StepperOrientation.Horizontal, StepperOrientation.Vertical };
            }
        }

        public List<IStep> Steps
        {
            get
            {
                List<IStep> steps = new List<IStep>();
                steps.Add(new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "One" }, Content = new StepperTutorialOneViewModel() });
                steps.Add(new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Two", SecondLevelTitle = "Second level header" }, Content = new StepperTutorialTwoViewModel() });
                steps.Add(new Step() { Header = new StepTitleHeader() { FirstLevelTitle = "Three" }, Content = new StepperTutorialThreeViewModel() });

                return steps;
            }
        }

        public StepperDemo()
        {
            InitializeComponent();

            cbOrientation.DataContext = this;
            cbIsLinear.DataContext = this;
            stepper.DataContext = this;
        }
    }
}
