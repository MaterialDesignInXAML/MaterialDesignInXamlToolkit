using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
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
