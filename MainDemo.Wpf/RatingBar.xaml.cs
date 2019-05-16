using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for RatingBar.xaml
    /// </summary>
    public partial class RatingBar : UserControl
    {
        public RatingBar()
        {
            InitializeComponent();
        }

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            Debug.WriteLine($"BasicRatingBar value changed from {e.OldValue} to {e.NewValue}.");
        }
    }
}
