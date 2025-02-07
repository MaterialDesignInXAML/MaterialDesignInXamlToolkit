namespace MaterialDesignThemes.UITests.Samples.UpDownControls;

/// <summary>
/// Interaction logic for BoundNumericUpDown.xaml
/// </summary>
public partial class BoundNumericUpDown : UserControl
{
    public BoundNumericUpDown()
    {
        ViewModel = new BoundNumericUpDownViewModel();
        DataContext = ViewModel;
        InitializeComponent();
    }

    //I expose the ViewModel here so I can access it in the tests
    public BoundNumericUpDownViewModel ViewModel { get; }
}
