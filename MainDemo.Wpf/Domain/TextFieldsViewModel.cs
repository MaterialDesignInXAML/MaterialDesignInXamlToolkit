using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo.Domain
{
    public class FieldsViewModel : INotifyPropertyChanged
    {
        private string? _name;
        private string? _name2;
        private int? _selectedValueOne;
        private string? _selectedTextTwo;

        public FieldsViewModel()
        {
            LongListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));

            SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedTextTwo = null;
        }

        public string? Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }

        public string? Name2
        {
            get => _name2;
            set => this.MutateVerbose(ref _name2, value, RaisePropertyChanged());
        }

        public int? SelectedValueOne
        {
            get => _selectedValueOne;
            set => this.MutateVerbose(ref _selectedValueOne, value, RaisePropertyChanged());
        }

        public string? SelectedTextTwo
        {
            get => _selectedTextTwo;
            set => this.MutateVerbose(ref _selectedTextTwo, value, RaisePropertyChanged());
        }

        public IList<int> LongListToTestComboVirtualization { get; }

        public DemoItem DemoItem => new DemoItem("Mr. Test", null, Enumerable.Empty<DocumentationLink>());

        public event PropertyChangedEventHandler? PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}
