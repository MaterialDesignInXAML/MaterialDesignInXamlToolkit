using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    public class Step : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private object _header;
        private object _content;
        private bool _hasValidationErrors;

        public object Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;

                OnPropertyChanged(nameof(Content));
            }
        }

        public bool HasValidationErrors
        {
            get
            {
                return _hasValidationErrors;
            }

            set
            {
                _hasValidationErrors = value;

                OnPropertyChanged(nameof(HasValidationErrors));
            }
        }

        public object Header
        {
            get
            {
                return _header;
            }

            set
            {
                _header = value;

                OnPropertyChanged(nameof(Header));
            }
        }

        public Step()
        {
            _header = null;
            _content = null;
            _hasValidationErrors = false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged!=null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
