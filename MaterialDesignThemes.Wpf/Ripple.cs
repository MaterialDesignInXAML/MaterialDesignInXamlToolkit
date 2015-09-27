using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignThemes.Wpf
{
    [TemplateVisualState(GroupName = "CommonStates", Name = TemplateStateNormal)]
    [TemplateVisualState(GroupName = "CommonStates", Name = TemplateStateMousePressed)]
    public class Ripple : ContentControl
    {
        public const string TemplateStateNormal = "Normal";
        public const string TemplateStateMousePressed = "MousePressed";

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
            double radius = Math.Sqrt(Math.Pow(sizeChangedEventArgs.NewSize.Width, 2) + Math.Pow(sizeChangedEventArgs.NewSize.Height, 2));
            RippleSize = 2 * radius * RippleSizeMultiplier;
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

            base.OnPreviewMouseLeftButtonDown(e);
        }

        public static readonly DependencyProperty RippleSizeMultiplierProperty = DependencyProperty.Register(
            "RippleSizeMultiplier", typeof(double), typeof(Ripple), new PropertyMetadata(1.0));

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
                "RippleX", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty RippleXProperty =
            RippleXPropertyKey.DependencyProperty;

        public double RippleX
        {
            get { return (double)GetValue(RippleXProperty); }
            private set { SetValue(RippleXPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey RippleYPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "RippleY", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty RippleYProperty =
            RippleYPropertyKey.DependencyProperty;

        public double RippleY
        {
            get { return (double)GetValue(RippleYProperty); }
            private set { SetValue(RippleYPropertyKey, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(Ripple), new FrameworkPropertyMetadata(false, IsActivePropertyChangedCallback));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void IsActivePropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var box = dependencyObject as Ripple;
            if (box == null) return;

            bool isActive = (bool)dependencyPropertyChangedEventArgs.NewValue;

            VisualStateManager.GoToState(box, isActive ? TemplateStateMousePressed : TemplateStateNormal, true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            VisualStateManager.GoToState(this, TemplateStateNormal, false);
        }
    }
}
