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
using System.Windows.Shapes;

namespace MaterialDesignThemes.UITests.Samples.UpDownControls;

/// <summary>
/// Interaction logic for BoundNumericUpDownWindow.xaml
/// </summary>
public partial class BoundNumericUpDownWindow : Window
{
    public BoundNumericUpDownWindow()
    {
        InitializeComponent();
    }

    private void BoundNumericUpDownWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        Activate();
        Topmost = true;
        Topmost = false;
        Focus();
    }
}
