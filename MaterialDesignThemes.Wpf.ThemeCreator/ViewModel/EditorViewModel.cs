using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.ThemeCreator.ViewModel
{
    class EditorViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Model.BrushColor> _brushes = Model.BrushColor.FromLight();
        public ObservableCollection<Model.BrushColor> Brushes
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
