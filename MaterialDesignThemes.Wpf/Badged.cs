using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;
using ControlzEx;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : BadgedEx
    {
        public static readonly DependencyProperty BadgeChangedStoryboardProperty = DependencyProperty.Register(
            "BadgeChangedStoryboard", typeof(Storyboard), typeof(Badged), new PropertyMetadata(default(Storyboard)));

        public Storyboard BadgeChangedStoryboard
        {
            get { return (Storyboard)this.GetValue(BadgeChangedStoryboardProperty); }
            set { this.SetValue(BadgeChangedStoryboardProperty, value); }
        }

        static Badged()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badged), new FrameworkPropertyMetadata(typeof(Badged)));
        }

        public static readonly DependencyProperty BadgeColorZoneModeProperty = DependencyProperty.Register(
            "BadgeColorZoneMode", typeof(ColorZoneMode), typeof(Badged), new PropertyMetadata(default(ColorZoneMode)));

        public ColorZoneMode BadgeColorZoneMode
        {
            get { return (ColorZoneMode) GetValue(BadgeColorZoneModeProperty); }
            set { SetValue(BadgeColorZoneModeProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Badged), new PropertyMetadata(new CornerRadius(9)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            this.BadgeChanged -= this.OnBadgeChanged;

            base.OnApplyTemplate();

            this.BadgeChanged += this.OnBadgeChanged;
        }

        private void OnBadgeChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var sb = this.BadgeChangedStoryboard;
            if (this._badgeContainer != null && sb != null)
            {
                try
                {
                    this._badgeContainer.BeginStoryboard(sb);
                }
                catch (Exception exc)
                {
                    Trace.WriteLine("Error during Storyboard execution, exception will be rethrown.");
                    Trace.WriteLine($"{exc.Message} ({exc.GetType().FullName})");
                    Trace.WriteLine(exc.StackTrace);

                    throw;
                }
            }
        }
    }
}
