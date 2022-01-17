using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
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
