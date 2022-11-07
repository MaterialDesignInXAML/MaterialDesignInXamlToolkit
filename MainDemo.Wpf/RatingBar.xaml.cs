﻿using System.Diagnostics;

namespace MaterialDesignDemo
{
    public partial class RatingBar
    {
        public RatingBar() => InitializeComponent();

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            => Debug.WriteLine($"BasicRatingBar value changed from {e.OldValue} to {e.NewValue}.");

        private void BasicRatingBarFractional_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            => Debug.WriteLine($"BasicRatingBarFractional value changed from {e.OldValue} to {e.NewValue}.");
    }
}
