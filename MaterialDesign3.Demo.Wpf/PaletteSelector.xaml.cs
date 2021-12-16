using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class PaletteSelector
    {
        public PaletteSelector()
        {
            DataContext = new PaletteSelectorViewModel();
            InitializeComponent();
        }
    }
}
