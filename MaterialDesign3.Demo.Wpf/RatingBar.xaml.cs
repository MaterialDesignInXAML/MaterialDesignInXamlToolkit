using System.Diagnostics;
using System.Windows;

namespace MaterialDesign3Demo
{
    public partial class RatingBar
    {
        public RatingBar() => InitializeComponent();

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
            => Debug.WriteLine($"BasicRatingBar value changed from {e.OldValue} to {e.NewValue}.");
    }
}
