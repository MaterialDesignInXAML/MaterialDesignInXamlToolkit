using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaterialDesignDemo.Domain
{
    public class SelectableViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _name;
        private string _description;
        private char _code;
        private double _numeric;
        private string _food;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public char Code
        {
            get => _code;
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public double Numeric
        {
            get => _numeric;
            set
            {
                if (Math.Abs(_numeric - value) < 0.01) return;
                _numeric = value;
                OnPropertyChanged();
            }
        }

        public string Food
        {
            get => _food;
            set
            {
                if (_food == value) return;
                _food = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}