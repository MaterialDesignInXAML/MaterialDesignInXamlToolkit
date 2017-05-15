using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A card is a content control, styled according to Material Design guidelines.
    /// </summary>
    [TemplatePart(Name = ClipBorderPartName, Type = typeof(Border))]
    public class Card : ContentControl
    {
        public const string ClipBorderPartName = "PART_ClipBorder";

        private Border _clipBorder;

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

            if (_clipBorder == null) return;

            var farPoint = new Point(
                Math.Max(0, _clipBorder.ActualWidth),
                Math.Max(0, _clipBorder.ActualHeight));
            
            var clipRect = new Rect(
                new Point(),
                new Point(farPoint.X, farPoint.Y));

            ContentClip = new RectangleGeometry(clipRect, UniformCornerRadius, UniformCornerRadius);
        }

        public static readonly DependencyProperty UniformCornerRadiusProperty = DependencyProperty.Register(
            nameof(UniformCornerRadius), typeof (double), typeof (Card), new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double UniformCornerRadius
        {
            get { return (double) GetValue(UniformCornerRadiusProperty); }
            set { SetValue(UniformCornerRadiusProperty, value); }
        }

        private static readonly DependencyPropertyKey ContentClipPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "ContentClip", typeof (Geometry), typeof (Card),
                new PropertyMetadata(default(Geometry)));

        public static readonly DependencyProperty ContentClipProperty =
            ContentClipPropertyKey.DependencyProperty;        

        public Geometry ContentClip
        {
            get { return (Geometry) GetValue(ContentClipProperty); }
            private set { SetValue(ContentClipPropertyKey, value); }
        }
    }
}
