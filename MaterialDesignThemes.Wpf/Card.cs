using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = ClipBorderPartName, Type = typeof(Border))]
    public class Card : ContentControl
    {
        private Border? _clipBorder;
        private const double DefaultUniformCornerRadius = 2.0;
        public const string ClipBorderPartName = "PART_ClipBorder";

        #region DependencyProperty : UniformCornerRadiusProperty
        public double UniformCornerRadius
        {
            get => (double)GetValue(UniformCornerRadiusProperty);
            set => SetValue(UniformCornerRadiusProperty, value);
        }
        public static readonly DependencyProperty UniformCornerRadiusProperty
            = DependencyProperty.Register(nameof(UniformCornerRadius), typeof(double), typeof(Card),
                new FrameworkPropertyMetadata(DefaultUniformCornerRadius, FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region DependencyProperty : ContentClipProperty
        private static readonly DependencyPropertyKey ContentClipPropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(ContentClip), typeof(Geometry), typeof(Card), new PropertyMetadata(default(Geometry)));

        public Geometry? ContentClip
        {
            get => (Geometry?)GetValue(ContentClipProperty);
            private set => SetValue(ContentClipPropertyKey, value);
        }

        public static readonly DependencyProperty ContentClipProperty
            = ContentClipPropertyKey.DependencyProperty;
        #endregion


        public static readonly DependencyProperty ClipContentProperty =
            DependencyProperty.Register("ClipContent", typeof(bool), typeof(Card), new PropertyMetadata(false));

        public bool ClipContent
        {
            get => (bool)GetValue(ClipContentProperty);
            set => SetValue(ClipContentProperty, value);
        }

        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _clipBorder = Template.FindName(ClipBorderPartName, this) as Border;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (_clipBorder is null)
            {
                return;
            }

            var farPointX = Math.Max(0, _clipBorder.ActualWidth);
            var farPointY = Math.Max(0, _clipBorder.ActualHeight);
            var farPoint = new Point(farPointX, farPointY);

            var clipRect = new Rect(new Point(0, 0), farPoint);
            ContentClip = new RectangleGeometry(clipRect, UniformCornerRadius, UniformCornerRadius);
        }
    }
}