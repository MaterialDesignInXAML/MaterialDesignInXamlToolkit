using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    internal class DrawersViewModel : INotifyPropertyChanged
    {
        private Brush _overlayBackground;

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        public Brush OverlayBackground
        {
            get { return _overlayBackground; }
            set
            {
                _overlayBackground = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToggleOverlayBackground { get; }

        private void ChangeOverlayBackground(bool primary)
        {
            if (primary)
            {
                var theme = _paletteHelper.GetTheme();
                OverlayBackground = new SolidColorBrush(theme.PrimaryMid.Color);
            }
            else
            {
                OverlayBackground = new SolidColorBrush(Colors.Black);
            }
        }

        public DrawersViewModel()
        {
            ToggleOverlayBackground = new AnotherCommandImplementation(o => ChangeOverlayBackground((bool)o));
            ChangeOverlayBackground(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
