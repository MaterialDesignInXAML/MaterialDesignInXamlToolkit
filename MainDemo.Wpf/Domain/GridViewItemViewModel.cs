using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class GridViewItemViewModel : INotifyPropertyChanged
    {
        private string _pathData;
        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;

                OnPropertyChanged();
            }
        }

        public string PathData
        {
            get
            {
                return _pathData;
            }

            set
            {
                _pathData = value;

                OnPropertyChanged();
            }
        }

        public GridViewItemViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
