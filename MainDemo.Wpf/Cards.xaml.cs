using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Cards.xaml
    /// </summary>
    public partial class Cards : UserControl
    {
        public Cards()
        {
            InitializeComponent();
        }

        private void Flipper_OnIsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            System.Diagnostics.Debug.WriteLine("Card is flipped = " + e.NewValue);
        }
    }
}
