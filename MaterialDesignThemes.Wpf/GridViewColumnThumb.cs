﻿namespace MaterialDesignThemes.Wpf
{
    internal class GridViewColumnThumb : Thumb
    {
        public GridViewColumnThumb() : base() { }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeWE;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Mouse.OverrideCursor = null;
        }
    }
}
