using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
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
