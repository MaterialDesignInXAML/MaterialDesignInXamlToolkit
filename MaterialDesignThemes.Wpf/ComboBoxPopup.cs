using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal enum ComboBoxPopupPlacement
    {
        Undefined,
        Down,
        Up, 
        Classic
    }

    internal class ComboBoxPopup : Popup
    {
        #region UpContentTemplate property

        public static readonly DependencyProperty UpContentTemplateProperty
            = DependencyProperty.Register(nameof(UpContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null));

        public ControlTemplate UpContentTemplate
        {
            get { return (ControlTemplate)GetValue(UpContentTemplateProperty); }
            set { SetValue(UpContentTemplateProperty, value); }
        }

        #endregion

        #region DownContentTemplate region

        public static readonly DependencyProperty DownContentTemplateProperty
            = DependencyProperty.Register(nameof(DownContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null));

        public ControlTemplate DownContentTemplate
        {
            get { return (ControlTemplate)GetValue(DownContentTemplateProperty); }
            set { SetValue(DownContentTemplateProperty, value); }
        }

        #endregion

        #region ClassicContentTemplate property

        public static readonly DependencyProperty ClassicContentTemplateProperty
            = DependencyProperty.Register(nameof(ClassicContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null));

        public ControlTemplate ClassicContentTemplate
        {
            get { return (ControlTemplate)GetValue(ClassicContentTemplateProperty); }
            set { SetValue(ClassicContentTemplateProperty, value); }
        }

        #endregion

        #region UpVerticalOffset property

        public static readonly DependencyProperty UpVerticalOffsetProperty
            = DependencyProperty.Register(nameof(UpVerticalOffset),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double UpVerticalOffset
        {
            get { return (double)GetValue(UpVerticalOffsetProperty); }
            set { SetValue(UpVerticalOffsetProperty, value); }
        }

        #endregion

        #region DownVerticalOffset property

        public static readonly DependencyProperty DownVerticalOffsetProperty
            = DependencyProperty.Register(nameof(DownVerticalOffset),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double DownVerticalOffset
        {
            get { return (double)GetValue(DownVerticalOffsetProperty); }
            set { SetValue(DownVerticalOffsetProperty, value); }
        }

        #endregion

        #region PopupPlacement property

        public static readonly DependencyProperty PopupPlacementProperty
            = DependencyProperty.Register(nameof(PopupPlacement),
                typeof(ComboBoxPopupPlacement),
                typeof(ComboBoxPopup),
                new PropertyMetadata(ComboBoxPopupPlacement.Undefined, PopupPlacementPropertyChangedCallback));

        public ComboBoxPopupPlacement PopupPlacement
        {
            get { return (ComboBoxPopupPlacement)GetValue(PopupPlacementProperty); }
            set { SetValue(PopupPlacementProperty, value); }
        }

        #endregion
        
        #region Background property

        private static readonly DependencyPropertyKey BackgroundPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Background", typeof(Brush), typeof(ComboBoxPopup),
                new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty BackgroundProperty =
            BackgroundPropertyKey.DependencyProperty;

        public Brush Background
        {
            get { return (Brush) GetValue(BackgroundProperty); }
            private set { SetValue(BackgroundPropertyKey, value); }
        }

        #endregion

        #region DefaultVerticalOffset

        public static readonly DependencyProperty DefaultVerticalOffsetProperty
            = DependencyProperty.Register(nameof(DefaultVerticalOffset),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        public double DefaultVerticalOffset
        {
            get { return (double)GetValue(DefaultVerticalOffsetProperty); }
            set { SetValue(DefaultVerticalOffsetProperty, value); }
        }

        #endregion

        #region VisiblePlacementWidth

        public double VisiblePlacementWidth
        {
            get { return (double)GetValue(VisiblePlacementWidthProperty); }
            set { SetValue(VisiblePlacementWidthProperty, value); }
        }

        public static readonly DependencyProperty VisiblePlacementWidthProperty
            = DependencyProperty.Register(nameof(VisiblePlacementWidth),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        #endregion

        public ComboBoxPopup()
        {
            this.CustomPopupPlacementCallback = ComboBoxCustomPopupPlacementCallback;
        }

        private void SetupBackground(IEnumerable<DependencyObject> visualAncestry)
        {
            var background = visualAncestry
                .Select(v => (v as Control)?.Background ?? (v as Panel)?.Background ?? (v as Border)?.Background)
                .FirstOrDefault(v => v != null && !Equals(v, Brushes.Transparent) && v is SolidColorBrush);

            if (background != null)
            {
                Background = background;
            }
        }

        private void SetupVisiblePlacementWidth(IEnumerable<DependencyObject> visualAncestry)
        {
            var parent = visualAncestry.OfType<Panel>().ElementAt(1);
            VisiblePlacementWidth = TreeHelper.GetVisibleWidth((FrameworkElement)PlacementTarget, parent);
        }

        private CustomPopupPlacement[] ComboBoxCustomPopupPlacementCallback(
            Size popupSize, Size targetSize, Point offset)
        {
            var visualAncestry = PlacementTarget.GetVisualAncestry().ToList();

            SetupBackground(visualAncestry);

            SetupVisiblePlacementWidth(visualAncestry);

            var locationFromScreen = PlacementTarget.PointToScreen(new Point(0, 0));

            var mainVisual = visualAncestry.OfType<Visual>().LastOrDefault();
            if (mainVisual == null) return new CustomPopupPlacement[0];

            var screenWidth = (int) DpiHelper.TransformToDeviceX(mainVisual, SystemParameters.PrimaryScreenWidth);
            var screenHeight = (int) DpiHelper.TransformToDeviceY(mainVisual, SystemParameters.PrimaryScreenHeight);

            var locationX = (int)locationFromScreen.X % screenWidth;
            var locationY = (int)locationFromScreen.Y % screenHeight;

            var realOffsetX = (popupSize.Width - targetSize.Width) / 2.0;

            double offsetX;
            const int rtlHorizontalOffset = 20;

            if (FlowDirection == FlowDirection.LeftToRight)
                offsetX = DpiHelper.TransformToDeviceX(mainVisual, offset.X);
            else
                offsetX = DpiHelper.TransformToDeviceX(mainVisual,
                    offset.X - targetSize.Width - rtlHorizontalOffset);


            if (locationX + popupSize.Width - realOffsetX > screenWidth
                || locationX - realOffsetX < 0)
            {
                PopupPlacement = ComboBoxPopupPlacement.Classic;

                var defaultVerticalOffsetIndepent = DpiHelper.TransformToDeviceY(mainVisual, DefaultVerticalOffset);
                var newY = locationY + popupSize.Height > screenHeight
                    ? -(defaultVerticalOffsetIndepent + popupSize.Height)
                    : defaultVerticalOffsetIndepent + targetSize.Height;

                return new[] { new CustomPopupPlacement(new Point(offsetX, newY), PopupPrimaryAxis.Horizontal) };
            }

            if (locationY + popupSize.Height > screenHeight)
            {
                PopupPlacement = ComboBoxPopupPlacement.Up;

                var upVerticalOffsetIndepent = DpiHelper.TransformToDeviceY(mainVisual, UpVerticalOffset);
                var newY = upVerticalOffsetIndepent - popupSize.Height + targetSize.Height;

                return new[] { new CustomPopupPlacement(new Point(offsetX, newY), PopupPrimaryAxis.None) };
            }
            else
            {
                PopupPlacement = ComboBoxPopupPlacement.Down;

                var downVerticalOffsetIndepent = DpiHelper.TransformToDeviceY(mainVisual, DownVerticalOffset);
                var newY = downVerticalOffsetIndepent;

                return new[] { new CustomPopupPlacement(new Point(offsetX, newY), PopupPrimaryAxis.None) };
            }
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

        private void SetChildTemplate(ComboBoxPopupPlacement placement)
        {
            switch (placement)
            {
                case ComboBoxPopupPlacement.Classic:
                    SetChildTemplateIfNeed(ClassicContentTemplate);
                    return;
                case ComboBoxPopupPlacement.Down:
                    SetChildTemplateIfNeed(DownContentTemplate);
                    return;
                case ComboBoxPopupPlacement.Up:
                    SetChildTemplateIfNeed(UpContentTemplate);
                    return;
                default:
                    throw new NotImplementedException($"Unexpected value {placement} of the {nameof(PopupPlacement)} property inside the {nameof(ComboBoxPopup)} control.");
            }
        }

        private static void PopupPlacementPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var popup = d as ComboBoxPopup;
            if (popup == null) return;

            if (!(e.NewValue is ComboBoxPopupPlacement)) return;
            var placement = (ComboBoxPopupPlacement)e.NewValue;

            popup.SetChildTemplate(placement);
        }
    }
}
