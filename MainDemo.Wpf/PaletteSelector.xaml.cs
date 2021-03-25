using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
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
