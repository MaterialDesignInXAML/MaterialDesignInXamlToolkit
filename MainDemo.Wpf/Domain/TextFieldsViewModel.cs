using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MaterialDesignDemo.Domain;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class FieldsViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _name2;
        private int? _selectedValueOne;
        private string _selectedTextTwo;

        public FieldsViewModel()
        {
            LongListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));

            SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedTextTwo = null;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                this.MutateVerbose(ref _name, value, RaisePropertyChanged());
            }
        }

        public string Name2
        {
            get { return _name2; }
            set
            {
                this.MutateVerbose(ref _name2, value, RaisePropertyChanged());
            }
        }

        public int? SelectedValueOne
        {
            get { return _selectedValueOne; }
            set
            {
                this.MutateVerbose(ref _selectedValueOne, value, RaisePropertyChanged());
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

        public IList<int> LongListToTestComboVirtualization { get; }

        public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
