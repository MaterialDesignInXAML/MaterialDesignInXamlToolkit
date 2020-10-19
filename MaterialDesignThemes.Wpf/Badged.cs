using System.Windows;
using System.Windows.Media.Animation;
using ControlzEx;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : BadgedEx
    {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(9);

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
            BadgeChanged += OnBadgeChanged;
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