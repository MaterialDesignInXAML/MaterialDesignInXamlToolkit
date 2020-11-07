using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf
{
    public enum BadgePlacementMode
    {
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left
    }

    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : ContentControl
    {
        public const string BadgeContainerPartName = "PART_BadgeContainer";
        protected FrameworkElement? _badgeContainer;

        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(9);

        public static readonly DependencyProperty BadgeProperty = DependencyProperty.Register(
            "Badge", typeof(object), typeof(Badged), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.AffectsArrange, OnBadgeChanged));

        public object? Badge
        {
            get => GetValue(BadgeProperty);
            set => SetValue(BadgeProperty, value);
        }

        public static readonly DependencyProperty BadgeBackgroundProperty = DependencyProperty.Register(
            "BadgeBackground", typeof(Brush), typeof(Badged), new PropertyMetadata(default(Brush)));

        public Brush? BadgeBackground
        {
            get => (Brush?)GetValue(BadgeBackgroundProperty);
            set => SetValue(BadgeBackgroundProperty, value);
        }

        public static readonly DependencyProperty BadgeForegroundProperty = DependencyProperty.Register(
            "BadgeForeground", typeof(Brush), typeof(Badged), new PropertyMetadata(default(Brush)));

        public Brush? BadgeForeground
        {
            get => (Brush?)GetValue(BadgeForegroundProperty);
            set => SetValue(BadgeForegroundProperty, value);
        }

        public static readonly DependencyProperty BadgePlacementModeProperty = DependencyProperty.Register(
            "BadgePlacementMode", typeof(BadgePlacementMode), typeof(Badged), new PropertyMetadata(default(BadgePlacementMode)));

        public BadgePlacementMode BadgePlacementMode
        {
            get => (BadgePlacementMode)GetValue(BadgePlacementModeProperty);
            set => SetValue(BadgePlacementModeProperty, value);
        }

        public static readonly RoutedEvent BadgeChangedEvent =
            EventManager.RegisterRoutedEvent(
                "BadgeChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<object>),
                typeof(Badged));

        public event RoutedPropertyChangedEventHandler<object> BadgeChanged
        {
            add { AddHandler(BadgeChangedEvent, value); }
            remove { RemoveHandler(BadgeChangedEvent, value); }
        }

        private static readonly DependencyPropertyKey IsBadgeSetPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsBadgeSet", typeof(bool), typeof(Badged),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsBadgeSetProperty =
            IsBadgeSetPropertyKey.DependencyProperty;

        public bool IsBadgeSet
        {
            get => (bool)GetValue(IsBadgeSetProperty);
            private set => SetValue(IsBadgeSetPropertyKey, value);
        }

        private static void OnBadgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Badged)d;

            instance.IsBadgeSet = !string.IsNullOrWhiteSpace(e.NewValue as string) || (e.NewValue != null && !(e.NewValue is string));

            var args = new RoutedPropertyChangedEventArgs<object?>(
                e.OldValue,
                e.NewValue)
            {
                RoutedEvent = BadgeChangedEvent
            };
            instance.RaiseEvent(args);
        }

        #region DependencyProperty : BadgeChangedStoryboardProperty
        public Storyboard BadgeChangedStoryboard
        {
            get { return (Storyboard)GetValue(BadgeChangedStoryboardProperty); }
            set { SetValue(BadgeChangedStoryboardProperty, value); }
        }
        public static readonly DependencyProperty BadgeChangedStoryboardProperty
            = DependencyProperty.Register(nameof(BadgeChangedStoryboard), typeof(Storyboard), typeof(Badged), new PropertyMetadata(default(Storyboard)));
        #endregion

        #region DependencyProperty : BadgeColorZoneModeProperty
        public ColorZoneMode BadgeColorZoneMode
        {
            get { return (ColorZoneMode)GetValue(BadgeColorZoneModeProperty); }
            set { SetValue(BadgeColorZoneModeProperty, value); }
        }
        public static readonly DependencyProperty BadgeColorZoneModeProperty
            = DependencyProperty.Register(nameof(BadgeColorZoneMode), typeof(ColorZoneMode), typeof(Badged), new PropertyMetadata(default(ColorZoneMode)));
        #endregion

        #region DependencyProperty : CornerRadiusProperty
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Badged), new PropertyMetadata(DefaultCornerRadius));
        #endregion
        
        static Badged()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badged), new FrameworkPropertyMetadata(typeof(Badged)));
        }

        public override void OnApplyTemplate()
        {
            BadgeChanged -= OnBadgeChanged;
            base.OnApplyTemplate();
            _badgeContainer = GetTemplateChild(BadgeContainerPartName) as FrameworkElement;

            BadgeChanged += OnBadgeChanged;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);

            if (_badgeContainer is null) return result;

            var containerDesiredSize = _badgeContainer.DesiredSize;
            if ((containerDesiredSize.Width <= 0.0 || containerDesiredSize.Height <= 0.0)
                && !double.IsNaN(_badgeContainer.ActualWidth) && !double.IsInfinity(_badgeContainer.ActualWidth)
                && !double.IsNaN(_badgeContainer.ActualHeight) && !double.IsInfinity(_badgeContainer.ActualHeight))
            {
                containerDesiredSize = new Size(_badgeContainer.ActualWidth, _badgeContainer.ActualHeight);
            }

            var h = 0 - containerDesiredSize.Width / 2;
            var v = 0 - containerDesiredSize.Height / 2;
            _badgeContainer.Margin = new Thickness(0);
            _badgeContainer.Margin = new Thickness(h, v, h, v);

            return result;
        }

        private void OnBadgeChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_badgeContainer is null || BadgeChangedStoryboard is null)
            {
                return;
            }
            
            _badgeContainer.BeginStoryboard(BadgeChangedStoryboard);
        }
    }
}