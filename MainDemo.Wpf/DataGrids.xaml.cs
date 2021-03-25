using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
{
    public partial class DataGrids
    {
        public DataGrids()
        {
            DataContext = new ListsAndGridsViewModel();
            InitializeComponent();
        }
    }
}
