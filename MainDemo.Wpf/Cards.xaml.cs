using System.Windows;

namespace MaterialDesignDemo
{
    public partial class Cards
    {
        public Cards() => InitializeComponent();

        private void Flipper_OnIsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
            => System.Diagnostics.Debug.WriteLine($"Card is flipped = {e.NewValue}");
    }
}
