using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo;

public partial class DataGrids
{
    public DataGrids()
    {
        DataContext = new ListsAndGridsViewModel(MainWindow.Snackbar.MessageQueue!);
        InitializeComponent();
    }
}
