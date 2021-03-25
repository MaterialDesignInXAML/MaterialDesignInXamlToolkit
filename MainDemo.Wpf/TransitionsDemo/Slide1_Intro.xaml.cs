using System.Windows.Controls;
using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo.TransitionsDemo
{
    /// <summary>
    /// Interaction logic for Slide1_Intro.xaml
    /// </summary>
    public partial class Slide1_Intro : UserControl
    {
        public Slide1_Intro()
        {
            DataContext = new Slide1ViewModel();
            InitializeComponent();
        }

        public class Slide1ViewModel : ViewModelBase
        {
            private string? _name;
            public string? Name
            {
                get => _name;
                set => SetProperty(ref _name, value);
            }
        }
    }
}
