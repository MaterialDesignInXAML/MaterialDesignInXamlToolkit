using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
{
    public partial class Sliders
    {
        public Sliders()
        {
            DataContext = new SlidersViewModel();
            InitializeComponent();
        }
    }
}
