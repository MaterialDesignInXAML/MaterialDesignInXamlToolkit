using System.ComponentModel;
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

        public class Slide1ViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            private string? _name;
            public string? Name
            {
                get => _name;
                set => this.MutateVerbose(ref _name, value, args => PropertyChanged?.Invoke(this, args));
            }
        }
    }
}
