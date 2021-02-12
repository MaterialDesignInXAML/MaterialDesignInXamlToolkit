using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
{
    public partial class ColorTool
    {
        public ColorTool()
        {
            DataContext = new ColorToolViewModel();
            InitializeComponent();
        }
    }
}
