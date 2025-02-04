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
using Xunit.Sdk;

namespace MaterialDesignThemes.UITests.Samples.UpDownControls;

/// <summary>
/// Interaction logic for BoundNumericUpDown.xaml
/// </summary>
public partial class BoundNumericUpDown : UserControl
{
    public BoundNumericUpDown()
    {
        this.ViewModel = new BoundNumericUpDownViewModel();
        this.DataContext = this.ViewModel;
        InitializeComponent();
    }

    //I expose the ViewModel here so I can access it in the tests
    public BoundNumericUpDownViewModel ViewModel { get; }
}
