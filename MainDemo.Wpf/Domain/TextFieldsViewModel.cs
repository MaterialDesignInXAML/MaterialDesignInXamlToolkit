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
        private readonly IList<int> _longListToTestComboVirtualization;

        private string _name;
        private int? _selectedValueOne;
        private string _selectedTextTwo;

        public TextFieldsViewModel()
        {
            _longListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));

            SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedTextTwo = null;
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

        public int? SelectedValueOne
        {
            get { return _selectedValueOne; }
            set
            {
                _selectedValueOne = value;
                OnPropertyChanged();
            }
        }        

        public string SelectedTextTwo
        {
            get { return _selectedTextTwo; }
            set
            {
                this.MutateVerbose(ref _selectedTextTwo, value, RaisePropertyChanged());
            }
        }

        public IList<int> LongListToTestComboVirtualization => _longListToTestComboVirtualization;

        public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}

