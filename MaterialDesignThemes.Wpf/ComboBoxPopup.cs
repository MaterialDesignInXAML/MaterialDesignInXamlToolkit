using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public class ComboBoxPopup : Popup
    {
        public static readonly DependencyProperty UpContentTemplateProperty
            = DependencyProperty.Register(nameof(UpContentTemplateProperty),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null));

        public ControlTemplate UpContentTemplate
        {
            get { return (ControlTemplate)GetValue(UpContentTemplateProperty); }
            set { SetValue(UpContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty DownContentTemplateProperty
            = DependencyProperty.Register(nameof(DownContentTemplateProperty),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null));

        public ControlTemplate DownContentTemplate
        {
            get { return (ControlTemplate)GetValue(DownContentTemplateProperty); }
            set { SetValue(DownContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultContentTemplateProperty
            = DependencyProperty.Register(nameof(DefaultContentTemplateProperty),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null));

        public ControlTemplate DefaultContentTemplate
        {
            get { return (ControlTemplate)GetValue(DefaultContentTemplateProperty); }
            set { SetValue(DefaultContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty UpVerticalOffsetProperty
            = DependencyProperty.Register(nameof(UpVerticalOffsetProperty),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double UpVerticalOffset
        {
            get { return (double)GetValue(UpVerticalOffsetProperty); }
            set { SetValue(UpVerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty DownVerticalOffsetProperty
            = DependencyProperty.Register(nameof(DownVerticalOffsetProperty),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double DownVerticalOffset
        {
            get { return (double)GetValue(DownVerticalOffsetProperty); }
            set { SetValue(DownVerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty DefaultVerticalOffsetProperty
            = DependencyProperty.Register(nameof(DefaultVerticalOffsetProperty),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double DefaultVerticalOffset
        {
            get { return (double)GetValue(DefaultVerticalOffsetProperty); }
            set { SetValue(DefaultVerticalOffsetProperty, value); }
        }

        public ComboBoxPopup()
        {
            this.CustomPopupPlacementCallback = ComboBoxCustomPopupPlacementCallback;
        }

        private void SetChildTemplateIfNeed(ControlTemplate template)
        {
            var contentControl = Child as ContentControl;
            if (contentControl == null) throw new InvalidOperationException("Child must be ContentControl");

            if (!ReferenceEquals(contentControl.Template, template))
            {
                contentControl.Template = template;
            }
        }

        private CustomPopupPlacement[] ComboBoxCustomPopupPlacementCallback(Size popupSize, Size targetSize,
            Point offset)
        {
            var locationFromScreen = this.PlacementTarget.PointToScreen(new Point(0, 0));

            int locationX = (int) locationFromScreen.X%(int) SystemParameters.PrimaryScreenWidth;
            int locationY = (int) locationFromScreen.Y%(int) SystemParameters.PrimaryScreenHeight;

            if (locationX + popupSize.Width > SystemParameters.PrimaryScreenWidth)
            {
                SetChildTemplateIfNeed(DefaultContentTemplate);

                double newY = locationY + popupSize.Height > SystemParameters.PrimaryScreenHeight
                    ? -(DefaultVerticalOffset + popupSize.Height)
                    : DefaultVerticalOffset + targetSize.Height;

                return new[] { new CustomPopupPlacement(new Point(offset.X, newY), PopupPrimaryAxis.Horizontal) };
            }
            if (locationY + popupSize.Height > SystemParameters.PrimaryScreenHeight)
            {
                SetChildTemplateIfNeed(UpContentTemplate);

                double newY = UpVerticalOffset - popupSize.Height + targetSize.Height;

                return new[] { new CustomPopupPlacement(new Point(offset.X, newY), PopupPrimaryAxis.None) };
            }
            else
            {
                SetChildTemplateIfNeed(DownContentTemplate);

                double newY = DownVerticalOffset;

                return new[] { new CustomPopupPlacement(new Point(offset.X, newY), PopupPrimaryAxis.None) };
            }
        }
    }
}
