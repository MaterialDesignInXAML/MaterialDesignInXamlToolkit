using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class TextFieldsViewModel : INotifyPropertyChanged
    {
        private string _name;
        private int _selectedValueOne;
        private int? _selectedValueTwo;

        public TextFieldsViewModel()
        {
            LongListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));

            SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedValueTwo = null;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                this.MutateVerbose(ref _name, value, RaisePropertyChanged());
            }
        }

        public int SelectedValueOne
        {
            get { return _selectedValueOne; }
            set
            {
                this.MutateVerbose(ref _selectedValueOne, value, RaisePropertyChanged());
            }
        }

        public int? SelectedValueTwo
        {
            get { return _selectedValueTwo; }
            set
            {
                this.MutateVerbose(ref _selectedValueTwo, value, RaisePropertyChanged());
            }
        }

        public IList<int> LongListToTestComboVirtualization { get; }

        public DemoItem DemoItem => new DemoItem { Name = "Mr. Test"};

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
