using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for ProvingGround.xaml
    /// </summary>
    public partial class ProvingGround : UserControl
    {
        public ProvingGround()
        {
            InitializeComponent();
            DataContext = new ProvingGroundViewModel
            {
                SelectedTime = new DateTime(2000, 1, 1, 3, 15, 0)
            };
        }        

        private void SnackBar3_OnClick(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = MessageTextBox.Text;

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void SnackBar4_OnClick(object sender, RoutedEventArgs e)
        {
            var id = "A1A";
            SnackbarFour.MessageQueue.Enqueue(
                $"Deleted {id}", 
                "UNDO",  
                param => Trace.WriteLine("Undo delete of " + param),
                id);            
        }
    }

    public class ProvingGroundViewModel : INotifyPropertyChanged
	{
		private string _name;
        private DateTime? _selectedTime;
        public ICommand ClearItems { get; }

        public ProvingGroundViewModel()
        {
             ClearItems = new AnotherCommandImplementation(_ => Items.Clear());
        }

        public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

        public ObservableCollection<SelectableViewModel> Items { get; } = CreateData();

        public DateTime? SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                System.Diagnostics.Debug.WriteLine(((object)_selectedTime ?? "NULL").ToString());
                OnPropertyChanged();
            }
        }

        private static ObservableCollection<SelectableViewModel> CreateData()
        {
            return new ObservableCollection<SelectableViewModel>
            {
                new SelectableViewModel
                {
                    Code = 'M',
                    Name = "Material Design",
                    Description = "Material Design in XAML Toolkit"
                },
                new SelectableViewModel
                {
                    Code = 'D',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control"
                },
                new SelectableViewModel
                {
                    Code = 'P',
                    Name = "Predator",
                    Description = "If it bleeds, we can kill it"
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
