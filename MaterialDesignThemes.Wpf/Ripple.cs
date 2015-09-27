using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignThemes.Wpf
{
    [TemplateVisualState(GroupName = "CommonStates", Name = "Normal")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "Pressed")]
    public class Ripple : ContentControl
    {
        static Ripple()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Ripple), new FrameworkPropertyMetadata(typeof(Ripple)));
        }

        public Ripple()
        {               
            SizeChanged += OnSizeChanged;          
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            RippleSize = Math.Max(sizeChangedEventArgs.NewSize.Width, sizeChangedEventArgs.NewSize.Height) * RippleSizeMultiplier;
        }

        public static readonly DependencyProperty FeedbackProperty = DependencyProperty.Register(
            "Feedback", typeof(Brush), typeof(Ripple), new PropertyMetadata(default(Brush)));

        public Brush Feedback
        {
            get { return (Brush)GetValue(FeedbackProperty); }
            set { SetValue(FeedbackProperty, value); }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var point = e.GetPosition(this);

            RippleX = point.X - RippleSize / 2;
            RippleY = point.Y - RippleSize / 2;

            VisualStateManager.GoToState(this, "Normal", true);
            VisualStateManager.GoToState(this, "MousePressed", true);            

            base.OnPreviewMouseLeftButtonDown(e);
        }

        public static readonly DependencyProperty RippleSizeMultiplierProperty = DependencyProperty.Register(
            "RippleSizeMultiplier", typeof(double), typeof(Ripple), new PropertyMetadata(1.75));

        public double RippleSizeMultiplier
        {
            get { return (double)GetValue(RippleSizeMultiplierProperty); }
            set { SetValue(RippleSizeMultiplierProperty, value); }
        }

        private static readonly DependencyPropertyKey RippleSizePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "RippleSize", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty RippleSizeProperty =
            RippleSizePropertyKey.DependencyProperty;

        public double RippleSize
        {
            get { return (double)GetValue(RippleSizeProperty); }
            private set { SetValue(RippleSizePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey RippleXPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "RippleX", typeof (double), typeof (Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty RippleXProperty =
            RippleXPropertyKey.DependencyProperty;

        public double RippleX
        {
            get { return (double) GetValue(RippleXProperty); }
            private set { SetValue(RippleXPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey RippleYPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "RippleY", typeof (double), typeof (Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty RippleYProperty =
            RippleYPropertyKey.DependencyProperty;

        public double RippleY
        {
            get { return (double) GetValue(RippleYProperty); }
            private set { SetValue(RippleYPropertyKey, value); }
        }                                
    }
}
