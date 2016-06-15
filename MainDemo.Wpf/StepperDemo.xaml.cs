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
        public List<Step> Steps
        {
            get
            {
                List<Step> steps = new List<Step>();
                steps.Add(new Step() { FirstLevelLabel = "First", Content = new StepperTutorialOneViewModel() });
                steps.Add(new Step() { FirstLevelLabel = "Two", SecondLevelLabel = "Second level header", Content = new StepperTutorialTwoViewModel() });

                return steps;
            }
        }

        public StepperDemo()
        {
            InitializeComponent();

            stepper.DataContext = this;
        }
    }
}
