using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace MaterialDesignColors.WpfExample.Domain
{
    internal class ThemeEditorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BrushColor> _brushes = BrushColor.FromLight();
        public ObservableCollection<BrushColor> Brushes
        {
            get { return _brushes; }
            set
            {
                _brushes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Brushes"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
