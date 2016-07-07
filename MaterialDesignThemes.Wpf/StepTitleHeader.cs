using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    public class StepTitleHeader : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstLevelTitle;
        private string _secondLevelTitle;

        public string FirstLevelTitle
        {
            get
            {
                return _firstLevelTitle;
            }

            set
            {
                _firstLevelTitle = value;

                OnPropertyChanged(nameof(FirstLevelTitle));
            }
        }

        public string SecondLevelTitle
        {
            get
            {
                return _secondLevelTitle;
            }

            set
            {
                _secondLevelTitle = value;

                OnPropertyChanged(nameof(SecondLevelTitle));
            }
        }

        public StepTitleHeader()
        {
            _firstLevelTitle = null;
            _secondLevelTitle = null;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
