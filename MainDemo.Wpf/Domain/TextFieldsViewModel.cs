using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class TextFieldsViewModel : INotifyPropertyChanged
    {
        private readonly IList<int> _longListToTestComboVirtualization;
            
        private string _name;

        public TextFieldsViewModel()
        {
            _longListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));            
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

        public IList<int> LongListToTestComboVirtualization => _longListToTestComboVirtualization;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
