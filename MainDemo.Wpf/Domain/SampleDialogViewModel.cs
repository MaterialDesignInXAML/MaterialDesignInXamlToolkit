using System;
using System.ComponentModel;
using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo.Domain
{
    public class SampleDialogViewModel : INotifyPropertyChanged
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set => this.MutateVerbose(ref _name, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
            => args => PropertyChanged?.Invoke(this, args);
    }
}