using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.DialogHost
{
    public class ClosingEventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _dialogIsOpen;
        public bool DialogIsOpen
        {
            get => _dialogIsOpen;
            set => SetProperty(ref _dialogIsOpen, value);
        }

        private ICommand? _closeDialogCommand;
        public ICommand CloseDialogCommand => _closeDialogCommand ??= new RelayCommand(CloseDialog);

        private ICommand? _openDialogCommand;
        public ICommand OpenDialogCommand => _openDialogCommand ??= new RelayCommand(OpenDialog);

        private void OpenDialog()
            => DialogIsOpen = true;

        private void CloseDialog()
            => DialogIsOpen = false;

        private bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = (newValue);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}