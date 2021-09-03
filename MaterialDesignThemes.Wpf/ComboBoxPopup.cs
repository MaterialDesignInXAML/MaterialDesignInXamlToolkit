using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class ComboBoxPopup : Popup
    {
        public TextBox ComboBoxEditableTextBox
        {
            get { return (TextBox)GetValue(ComboBoxEditableTextBoxProperty); }
            set { SetValue(ComboBoxEditableTextBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComboBoxEditableTextBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxEditableTextBoxProperty =
            DependencyProperty.Register("ComboBoxEditableTextBox", typeof(TextBox), typeof(ComboBoxPopup), new PropertyMetadata(null));



        public ComboBox ComboBox
        {
            get => (ComboBox)GetValue(ComboBoxProperty);
            set => SetValue(ComboBoxProperty, value);
        }

        public static readonly DependencyProperty ComboBoxProperty =
            DependencyProperty.Register("ComboBox", typeof(ComboBox), typeof(ComboBoxPopup), new PropertyMetadata(null));



        #region UpContentTemplate property

        public static readonly DependencyProperty UpContentTemplateProperty
            = DependencyProperty.Register(nameof(UpContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null, CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement.Classic)));

        public ControlTemplate? UpContentTemplate
        {
            get => (ControlTemplate?)GetValue(UpContentTemplateProperty);
            set => SetValue(UpContentTemplateProperty, value);
        }

        #endregion

        #region DownContentTemplate region

        public static readonly DependencyProperty DownContentTemplateProperty
            = DependencyProperty.Register(nameof(DownContentTemplate),
                typeof(ControlTemplate),
                typeof(ComboBoxPopup),
                new UIPropertyMetadata(null, CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement.Down)));

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
                new UIPropertyMetadata(null, CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement.Up)));

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

        public static readonly DependencyProperty PopupPlacementProperty
            = DependencyProperty.Register(nameof(PopupPlacement),
                typeof(ComboBoxPopupPlacement),
                typeof(ComboBoxPopup),
                new PropertyMetadata(ComboBoxPopupPlacement.Undefined, PopupPlacementPropertyChangedCallback));

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

        public static readonly DependencyProperty ClassicModeProperty
            = DependencyProperty.Register(
                nameof(ClassicMode),
                typeof(bool),
                typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(true));

        public bool ClassicMode
        {
            get => (bool)GetValue(ClassicModeProperty);
            set => SetValue(ClassicModeProperty, value);
        }

        #endregion

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

        public static readonly DependencyProperty RelativeHorizontalOffsetProperty
            = DependencyProperty.Register(
                nameof(RelativeHorizontalOffset), typeof(double), typeof(ComboBoxPopup),
                new FrameworkPropertyMetadata(default(double)));

        public double RelativeHorizontalOffset
        {
            get => (double)GetValue(RelativeHorizontalOffsetProperty);
            set => SetValue(RelativeHorizontalOffsetProperty, value);
        }

        public ComboBoxPopup()
            => CustomPopupPlacementCallback = ComboBoxCustomPopupPlacementCallback;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ChildProperty)
            {
                if (PopupPlacement != ComboBoxPopupPlacement.Undefined)
                {
                    UpdateChildTemplate(PopupPlacement);
                }
            }
        }

        private Grid? _parent;
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            if (this.Child.VisualBreadthFirstTraversal()
                .OfType<ContentControl>().FirstOrDefault(x => x.Name == "TextBoxHolder") is ContentControl cc &&
                ComboBoxEditableTextBox is { } textBox &&
                VisualTreeHelper.GetParent(textBox) is Grid parent)
            {
                _parent = parent;
                parent.Children.Remove(textBox);
                cc.Content = textBox;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (GetTemplateChild("TextBoxHolder") is ContentControl cc &&
                ComboBoxEditableTextBox is { } textBox &&
                _parent is { } parent)
            {
                cc.Content = null;
                parent.Children.Add(textBox);
            }
        }

        private void SetupVisiblePlacementWidth(IEnumerable<DependencyObject?> visualAncestry)
        {
            var parent = visualAncestry.OfType<Panel>().ElementAt(1);
            VisiblePlacementWidth = TreeHelper.GetVisibleWidth((FrameworkElement)PlacementTarget, parent, FlowDirection);
        }

        private CustomPopupPlacement[] ComboBoxCustomPopupPlacementCallback(
            Size popupSize, Size targetSize, Point offset)
        {
            var visualAncestry = PlacementTarget.GetVisualAncestry().ToList();

            SetupVisiblePlacementWidth(visualAncestry);

            var data = GetPositioningData(visualAncestry, popupSize, targetSize);
            var preferUpIfSafe = data.LocationY + data.PopupSize.Height > data.ScreenHeight;

            if (ClassicMode
                || data.PopupLocationX + data.PopupSize.Width > data.ScreenWidth
                || data.PopupLocationX < 0
                || !preferUpIfSafe && data.LocationY + data.NewDownY < 0)
            {
                SetCurrentValue(PopupPlacementProperty, ComboBoxPopupPlacement.Classic);
                return new[] { GetClassicPopupPlacement(this, data) };
            }
            if (preferUpIfSafe)
            {
                SetCurrentValue(PopupPlacementProperty, ComboBoxPopupPlacement.Up);
                return new[] { GetUpPopupPlacement(data) };
            }
            SetCurrentValue(PopupPlacementProperty, ComboBoxPopupPlacement.Down);
            return new[] { GetDownPopupPlacement(data) };
        }

        private void SetChildTemplateIfNeed(ControlTemplate? template)
        {
            var contentControl = Child as ContentControl;
            if (contentControl is null) return;
            //throw new InvalidOperationException($"The type of {nameof(Child)} must be {nameof(ContentControl)}");

            if (!ReferenceEquals(contentControl.Template, template))
            {
                contentControl.Template = template;
            }
        }

        private PositioningData GetPositioningData(IEnumerable<DependencyObject?> visualAncestry, Size popupSize, Size targetSize)
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
            if (FlowDirection == FlowDirection.LeftToRight)
                offsetX = Round(offsetX);
            else
                offsetX = Math.Truncate(offsetX - targetSize.Width);

            return new PositioningData(
                mainVisual, offsetX,
                newUpY, newDownY,
                popupSize, targetSize,
                locationX, locationY,
                screenHeight, screenWidth);
        }

        private static double Round(double val) => val < 0 ? (int)(val - 0.5) : (int)(val + 0.5);

        private static PropertyChangedCallback CreateTemplatePropertyChangedCallback(ComboBoxPopupPlacement popupPlacement)
        {
            return delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var popup = d as ComboBoxPopup;
                if (popup is null) return;

                var template = e.NewValue as ControlTemplate;
                if (template is null) return;

                if (popup.PopupPlacement == popupPlacement)
                {
                    popup.SetChildTemplateIfNeed(template);
                }
            };
        }

        private void UpdateChildTemplate(ComboBoxPopupPlacement placement)
        {
            switch (placement)
            {
                case ComboBoxPopupPlacement.Classic:
                    SetChildTemplateIfNeed(ClassicContentTemplate);
                    break;
                case ComboBoxPopupPlacement.Down:
                    SetChildTemplateIfNeed(DownContentTemplate);
                    break;
                case ComboBoxPopupPlacement.Up:
                    SetChildTemplateIfNeed(UpContentTemplate);
                    break;
                    //                default:
                    //                    throw new NotImplementedException($"Unexpected value {placement} of the {nameof(PopupPlacement)} property inside the {nameof(ComboBoxPopup)} control.");
            }
        }

        private static void PopupPlacementPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var popup = d as ComboBoxPopup;
            if (popup is null) return;

            if (!(e.NewValue is ComboBoxPopupPlacement)) return;
            var placement = (ComboBoxPopupPlacement)e.NewValue;
            popup.UpdateChildTemplate(placement);
        }

        private static CustomPopupPlacement GetClassicPopupPlacement(ComboBoxPopup popup, PositioningData data)
        {
            var defaultVerticalOffsetIndependent = DpiHelper.TransformToDeviceY(data.MainVisual, popup.DefaultVerticalOffset);
            var newY = data.LocationY + data.PopupSize.Height > data.ScreenHeight
                ? -(defaultVerticalOffsetIndependent + data.PopupSize.Height)
                : defaultVerticalOffsetIndependent + data.TargetSize.Height;

            return new CustomPopupPlacement(new Point(data.OffsetX, newY), PopupPrimaryAxis.Horizontal);
        }

        private static CustomPopupPlacement GetDownPopupPlacement(PositioningData data)
            => new CustomPopupPlacement(new Point(data.OffsetX, data.NewDownY), PopupPrimaryAxis.None);

        private static CustomPopupPlacement GetUpPopupPlacement(PositioningData data)
            => new CustomPopupPlacement(new Point(data.OffsetX, data.NewUpY), PopupPrimaryAxis.None);

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
                PopupSize = popupSize; TargetSize = targetSize;
                LocationX = locationX; LocationY = locationY;
                ScreenWidth = screenWidth; ScreenHeight = screenHeight;
            }
        }
    }
}
