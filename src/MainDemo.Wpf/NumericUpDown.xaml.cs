using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignDemo;

public partial class NumericUpDown : UserControl
{
    public NumericUpDown()
    {
        InitializeComponent();
        this.DataContext = new MyVM();
    }
}

public partial class MyVM : ObservableObject
{
    [ObservableProperty]
    private decimal _myDecimal;
}
