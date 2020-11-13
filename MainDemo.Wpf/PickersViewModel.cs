using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaterialDesignDemo
{
    public class PickersViewModel : INotifyPropertyChanged
    {
        private DateTime _date;
        private DateTime _time;
        private string? _validatingTime;
        private DateTime? _futureValidatingDate;

        public PickersViewModel()
        {
            Date = DateTime.Now;
            Time = DateTime.Now;
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? ValidatingTime
        {
            get => _validatingTime;
            set
            {
                if (_validatingTime != value)
                {
                    _validatingTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? FutureValidatingDate
        {
            get => _futureValidatingDate;
            set
            {
                if (_futureValidatingDate != value)
                {
                    _futureValidatingDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
