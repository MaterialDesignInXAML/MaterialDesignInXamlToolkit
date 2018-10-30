using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignColors.WpfExample.Domain;

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
