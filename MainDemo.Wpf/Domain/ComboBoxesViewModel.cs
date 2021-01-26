using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MaterialDesignDemo.Domain
{
    public class ComboBoxesViewModel : INotifyPropertyChanged
    {
        private int? _selectedValueOne;
        private string? _selectedTextTwo;
        private string? _selectedValidationOutlined;
        private string? _selectedValidationFilled;

        public ComboBoxesViewModel()
        {
            LongListToTestComboVirtualization = new List<int>(Enumerable.Range(0, 1000));
            ShortStringList = new[]
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            SelectedValueOne = LongListToTestComboVirtualization.Skip(2).First();
            SelectedTextTwo = null;
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

        public string? SelectedValidationFilled
        {
            get => _selectedValidationFilled;
            set => this.MutateVerbose(ref _selectedValidationFilled, value, RaisePropertyChanged());
        }

        public string? SelectedValidationOutlined
        {
            get => _selectedValidationOutlined;
            set => this.MutateVerbose(ref _selectedValidationOutlined, value, RaisePropertyChanged());
        }

        public IList<int> LongListToTestComboVirtualization { get; }
        public IList<string> ShortStringList { get; }


        public event PropertyChangedEventHandler? PropertyChanged;

        private System.Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}
