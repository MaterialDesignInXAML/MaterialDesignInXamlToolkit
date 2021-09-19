using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public enum PopupDirection
    {
        None,
        Up,
        Down
    }

    public class ComboBoxPopup : Popup
    {
        #region UpContentTemplate property
        [Obsolete("UpContentTemplate is now obsolete and has no affect.")]
        public static readonly DependencyProperty UpContentTemplateProperty
            = DependencyProperty.Register(nameof(UpContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new PropertyMetadata(null));

        [Obsolete("UpContentTemplate is now obsolete and has no affect.")]
        public ControlTemplate? UpContentTemplate
        {
            get => (ControlTemplate?)GetValue(UpContentTemplateProperty);
            set => SetValue(UpContentTemplateProperty, value);
        }

        #endregion

        #region DownContentTemplate region
        [Obsolete("DownContentTemplate is now obsolete and has no affect.")]
        public static readonly DependencyProperty DownContentTemplateProperty
            = DependencyProperty.Register(nameof(DownContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new PropertyMetadata(null));

        [Obsolete("DownContentTemplate is now obsolete and has no affect.")]
        public ControlTemplate? DownContentTemplate
        {
            get => (ControlTemplate?)GetValue(DownContentTemplateProperty);
            set => SetValue(DownContentTemplateProperty, value);
        }

        #endregion

        #region ClassicContentTemplate property

        public static readonly DependencyProperty ClassicContentTemplateProperty
            = DependencyProperty.Register(nameof(ClassicContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new PropertyMetadata(null));

        public ControlTemplate? ClassicContentTemplate
        {
            get => (ControlTemplate?)GetValue(ClassicContentTemplateProperty);
            set => SetValue(ClassicContentTemplateProperty, value);
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
            get => (double)GetValue(UpVerticalOffsetProperty);
            set => SetValue(UpVerticalOffsetProperty, value);
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
            get => (double)GetValue(DownVerticalOffsetProperty);
            set => SetValue(DownVerticalOffsetProperty, value);
        }

        #endregion

        #region PopupPlacement property

        [Obsolete("PopupPlacement is now obsolete and has no affect.")]
        public static readonly DependencyProperty PopupPlacementProperty
            = DependencyProperty.Register(nameof(PopupPlacement),
                typeof(ComboBoxPopupPlacement),
                typeof(ComboBoxPopup),
                new PropertyMetadata(ComboBoxPopupPlacement.Undefined));

        [Obsolete("PopupPlacement is now obsolete and has no affect.")]
        public ComboBoxPopupPlacement PopupPlacement
        {
            get => (ComboBoxPopupPlacement)GetValue(PopupPlacementProperty);
            set => SetValue(PopupPlacementProperty, value);
        }

        #endregion

        #region Background property

        private static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background", typeof(Brush), typeof(ComboBoxPopup),
                new PropertyMetadata(default(Brush)));

        public Brush? Background
        {
            get => (Brush?)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
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
            get => (double)GetValue(DefaultVerticalOffsetProperty);
            set => SetValue(DefaultVerticalOffsetProperty, value);
        }

        #endregion

        #region VisiblePlacementWidth

        public double VisiblePlacementWidth
        {
            get => (double)GetValue(VisiblePlacementWidthProperty);
            set => SetValue(VisiblePlacementWidthProperty, value);
        }

        public static readonly DependencyProperty VisiblePlacementWidthProperty
            = DependencyProperty.Register(nameof(VisiblePlacementWidth),
                typeof(double),
                typeof(ComboBoxPopup),
                new PropertyMetadata(0.0));

        #endregion

        #region ClassicMode property

        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static readonly DependencyProperty ClassicModeProperty
            = DependencyProperty.Register(
                nameof(ClassicMode),
                typeof(bool),
                typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(true));

        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public bool ClassicMode
        {
            get => (bool)GetValue(ClassicModeProperty);
            set => SetValue(ClassicModeProperty, value);
        }

        #endregion

        #region CornerRadius
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.Register(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion CornerRadius

        #region ContentMargin

        public static readonly DependencyProperty ContentMarginProperty
            = DependencyProperty.Register(
                nameof(ContentMargin),
                typeof(Thickness),
                typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(default(Thickness)));

        public Thickness ContentMargin
        {
            get => (Thickness)GetValue(ContentMarginProperty);
            set => SetValue(ContentMarginProperty, value);
        }

        #endregion ContentMargin

        #region ContentMinWidth
        public static readonly DependencyProperty ContentMinWidthProperty
            = DependencyProperty.Register(
                nameof(ContentMinWidth),
                typeof(double),
                typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(default(double)));

        public double ContentMinWidth
        {
            get => (double)GetValue(ContentMinWidthProperty);
            set => SetValue(ContentMinWidthProperty, value);
        }
        #endregion ContentMinWidth

        #region RelativeHorizontalOffset

        public static readonly DependencyProperty RelativeHorizontalOffsetProperty
            = DependencyProperty.Register(
                nameof(RelativeHorizontalOffset), typeof(double), typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(default(double)));

        public double RelativeHorizontalOffset
        {
            get => (double)GetValue(RelativeHorizontalOffsetProperty);
            set => SetValue(RelativeHorizontalOffsetProperty, value);
        }
        #endregion RelativeHorizontalOffset

        public PopupDirection OpenDirection
        {
            get => (PopupDirection)GetValue(OpenDirectionProperty);
            set => SetValue(OpenDirectionProperty, value);
        }

        // Using a DependencyProperty as the backing store for OpenDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenDirectionProperty =
            DependencyProperty.Register(nameof(OpenDirection), typeof(PopupDirection),
                typeof(ComboBoxPopup), new PropertyMetadata(PopupDirection.None));

        public ComboBoxPopup()
            => CustomPopupPlacementCallback = ComboBoxCustomPopupPlacementCallback;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ChildProperty &&
                Child is ContentControl contentControl &&
                !ReferenceEquals(contentControl.Template, ClassicContentTemplate))
            {
                contentControl.Template = ClassicContentTemplate;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            SetCurrentValue(OpenDirectionProperty, PopupDirection.None);
            base.OnClosed(e);
        }

        private CustomPopupPlacement[] ComboBoxCustomPopupPlacementCallback(
            Size popupSize, Size targetSize, Point offset)
        {
            var visualAncestry = PlacementTarget.GetVisualAncestry().ToList();

            var parent = visualAncestry.OfType<Panel>().ElementAt(1);
            VisiblePlacementWidth = TreeHelper.GetVisibleWidth((FrameworkElement)PlacementTarget, parent, FlowDirection);

            var data = GetPositioningData(visualAncestry, popupSize, targetSize);

            var defaultVerticalOffsetIndependent = DpiHelper.TransformToDeviceY(data.MainVisual, DefaultVerticalOffset);

            double newY;
            PopupDirection direction;
            if (data.LocationY + data.PopupSize.Height > data.ScreenHeight)
            {
                newY = -(defaultVerticalOffsetIndependent + data.PopupSize.Height);
                direction = PopupDirection.Up;
            }
            else
            {
                newY = defaultVerticalOffsetIndependent + data.TargetSize.Height;
                direction = PopupDirection.Down;
            }

            SetCurrentValue(OpenDirectionProperty, direction);
            return new[] { new CustomPopupPlacement(new Point(data.OffsetX, newY), PopupPrimaryAxis.Horizontal) };

            PositioningData GetPositioningData(IEnumerable<DependencyObject?> visualAncestry, Size popupSize, Size targetSize)
            {
                var locationFromScreen = PlacementTarget.PointToScreen(new Point(0, 0));

                var mainVisual = visualAncestry.OfType<Visual>().LastOrDefault();
                if (mainVisual is null) throw new ArgumentException($"{nameof(visualAncestry)} must contains unless one {nameof(Visual)} control inside.");

                var controlVisual = visualAncestry.OfType<Visual>().FirstOrDefault();
                if (controlVisual == null) throw new ArgumentException($"{nameof(visualAncestry)} must contains unless one {nameof(Visual)} control inside.");

                var screen = Screen.FromPoint(locationFromScreen);
                var screenWidth = (int)screen.Bounds.Width;
                var screenHeight = (int)screen.Bounds.Height;

                //Adjust the location to be in terms of the current screen
                var locationX = (int)(locationFromScreen.X - screen.Bounds.X) % screenWidth;
                var locationY = (int)(locationFromScreen.Y - screen.Bounds.Y) % screenHeight;

                var upVerticalOffsetIndependent = DpiHelper.TransformToDeviceY(mainVisual, UpVerticalOffset) * ScaleHelper.GetTotalTransformScaleY(controlVisual);
                var newUpY = upVerticalOffsetIndependent - popupSize.Height + targetSize.Height;
                var newDownY = DpiHelper.TransformToDeviceY(mainVisual, DownVerticalOffset) * ScaleHelper.GetTotalTransformScaleY(controlVisual);
                var offsetX = DpiHelper.TransformToDeviceX(mainVisual, RelativeHorizontalOffset) * ScaleHelper.GetTotalTransformScaleX(controlVisual);
                offsetX = FlowDirection == FlowDirection.LeftToRight
                    ? Round(offsetX)
                    : Math.Truncate(offsetX - targetSize.Width);

                return new PositioningData(
                    mainVisual, offsetX,
                    newUpY, newDownY,
                    popupSize, targetSize,
                    locationX, locationY,
                    screenHeight, screenWidth);
            }
        }

        private static double Round(double val) => val < 0 ? (int)(val - 0.5) : (int)(val + 0.5);

        private struct PositioningData
        {
            public Visual MainVisual { get; }
            public double OffsetX { get; }
            public double NewUpY { get; }
            public double NewDownY { get; }
            public double PopupLocationX => LocationX + OffsetX;
            public Size PopupSize { get; }
            public Size TargetSize { get; }
            public double LocationX { get; }
            public double LocationY { get; }
            public double ScreenHeight { get; }
            public double ScreenWidth { get; }

            public PositioningData(Visual mainVisual, double offsetX, double newUpY, double newDownY, Size popupSize, Size targetSize, double locationX, double locationY, double screenHeight, double screenWidth)
            {
                MainVisual = mainVisual;
                OffsetX = Round(offsetX);
                NewUpY = Round(newUpY);
                NewDownY = Round(newDownY);
                PopupSize = popupSize;
                TargetSize = targetSize;
                LocationX = locationX;
                LocationY = locationY;
                ScreenWidth = screenWidth;
                ScreenHeight = screenHeight;
            }
        }
    }
}
