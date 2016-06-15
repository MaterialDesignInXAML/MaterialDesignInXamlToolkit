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

        private string _firstLevelLabel;
        private string _secondLevelLabel;
        private object _content;

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

        public string FirstLevelLabel
        {
            get
            {
                return _firstLevelLabel;
            }

            set
            {
                _firstLevelLabel = value;

                OnPropertyChanged(nameof(FirstLevelLabel));
            }
        }

        public string SecondLevelLabel
        {
            get
            {
                return _secondLevelLabel;
            }

            set
            {
                _secondLevelLabel = value;

                OnPropertyChanged(nameof(SecondLevelLabel));
            }
        }

        public Step()
        {
            _firstLevelLabel = null;
            _content = null;
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
