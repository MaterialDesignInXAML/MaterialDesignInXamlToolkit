using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Convenience class for building a text header with a first and a second level header.
    /// The corresponding data template is already implemented and will be automatically applied.
    /// </summary>
    public class StepTitleHeader : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstLevelTitle;
        private string _secondLevelTitle;

        /// <summary>
        /// The text of the first level title.
        /// A value of null will hide this title.
        /// </summary>
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

        /// <summary>
        /// The text of the second level title beneath the first level title.
        /// A value of null will hide the this title.
        /// It uses a smaller font size.
        /// </summary>
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
