using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class Lists
    {
        public Lists()
        {
            DataContext = new ListsAndGridsViewModel();
            InitializeComponent();
        }
    }
}
