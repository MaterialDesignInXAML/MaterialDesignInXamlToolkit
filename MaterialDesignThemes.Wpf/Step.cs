using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    public interface IStep : INotifyPropertyChanged
    {
        object Content { get; set; }

        bool HasValidationErrors { get; set; }

        object Header { get; set; }

        void Validate();
    }

    public class Step : IStep
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected object _header;
        protected object _content;
        protected bool _hasValidationErrors;

        public virtual object Content
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

        public virtual bool HasValidationErrors
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

        public virtual object Header
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

        public virtual void Validate() { }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
