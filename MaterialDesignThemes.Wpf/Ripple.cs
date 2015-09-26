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
    [TemplatePart(Name = PartBubbleEllipse, Type = typeof(Ellipse))]
    public class Ripple : ContentControl
    {
        public const string PartBubbleEllipse = "PART_BubbleEllipse";

        private BubbleStoryboardController _bubbleStoryboardController;

        static Ripple()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Ripple), new FrameworkPropertyMetadata(typeof(Ripple)));
        }

        public Ripple()
        {
            MouseMove += OnMouseMove;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            MouseLeftButtonDownX = position.X;
            MouseLeftButtonDownY = position.Y;

            this.ReleaseMouseCapture();

            base.OnPreviewMouseLeftButtonDown(e);
        }

        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var position = mouseEventArgs.GetPosition(this);
            MouseX = position.X;
            MouseY = position.Y;
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(Ripple), new FrameworkPropertyMetadata(false, IsActivePropertyChangedCallback));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _bubbleStoryboardController = new BubbleStoryboardController(this);
        }

        private void StartBubbleAnimation()
        {
            _bubbleStoryboardController.Add();
        }

        private void StopBubbleAnimation()
        {
            _bubbleStoryboardController.Remove();
        }

        private static void IsActivePropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var box = dependencyObject as Ripple;
            if (box == null) return;

            bool isActive = (bool)dependencyPropertyChangedEventArgs.NewValue;

            if (isActive)
            {
                box.StartBubbleAnimation();
            }
            else
            {
                box.StopBubbleAnimation();
            }
        }

        public static readonly DependencyProperty FeedbackProperty = DependencyProperty.Register(
            "Feedback", typeof(Brush), typeof(Ripple), new PropertyMetadata(default(Brush)));

        public Brush Feedback
        {
            get { return (Brush)GetValue(FeedbackProperty); }
            set { SetValue(FeedbackProperty, value); }
        }

        private static readonly DependencyPropertyKey MouseXPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "MouseX", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MouseXProperty =
            MouseXPropertyKey.DependencyProperty;

        public double MouseX
        {
            get { return (double)GetValue(MouseXProperty); }
            private set { SetValue(MouseXPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey MouseYPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "MouseY", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MouseYProperty =
            MouseYPropertyKey.DependencyProperty;

        public double MouseY
        {
            get { return (double)GetValue(MouseYProperty); }
            private set { SetValue(MouseYPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey MouseLeftButtonDownXPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "MouseLeftButtonDownX", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MouseLeftButtonDownXProperty =
            MouseLeftButtonDownXPropertyKey.DependencyProperty;

        public double MouseLeftButtonDownX
        {
            get { return (double)GetValue(MouseLeftButtonDownXProperty); }
            private set { SetValue(MouseLeftButtonDownXPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey MouseLeftButtonDownYPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "MouseLeftButtonDownY", typeof(double), typeof(Ripple),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MouseLeftButtonDownYProperty =
            MouseLeftButtonDownYPropertyKey.DependencyProperty;

        public double MouseLeftButtonDownY
        {
            get { return (double)GetValue(MouseLeftButtonDownYProperty); }
            private set { SetValue(MouseLeftButtonDownYPropertyKey, value); }
        }


        private sealed class BubbleStoryboardController
        {
            private readonly Ripple _ripple;
            private readonly Ellipse _ellipse;
            private readonly TimeSpan _fadeOutDuration = TimeSpan.FromMilliseconds(200);
            private readonly TimeSpan _bubbleDuration = TimeSpan.FromMilliseconds(400);

            private volatile Storyboard _currentBubbleStoryboard;
            private volatile Storyboard _currentFadeOutStoryboard;

            public BubbleStoryboardController(Ripple ripple)
            {
                _ripple = ripple;
                _ellipse = _ripple.Template.FindName(PartBubbleEllipse, _ripple) as Ellipse;

                if (_ellipse == null) throw new InvalidOperationException();
            }
            public void Add()
            {
                _currentFadeOutStoryboard?.Remove();
                _currentBubbleStoryboard?.Remove();

                _currentBubbleStoryboard = CreateBubbleStoryboard();
                _currentBubbleStoryboard.Begin();
            }

            public void Remove()
            {
                _currentFadeOutStoryboard?.Remove();

                var fadeOutStoryboard = CreateFadeOutStoryboard();
                var bubbleStoryboardLocalCopy = _currentBubbleStoryboard;

                fadeOutStoryboard.Completed += delegate
                {
                    fadeOutStoryboard.Remove();
                    bubbleStoryboardLocalCopy?.Remove();
                };

                _currentFadeOutStoryboard = fadeOutStoryboard;
                _currentFadeOutStoryboard.Begin();
            }

            private Storyboard CreateFadeOutStoryboard()
            {
                Storyboard resultStorybaord = new Storyboard();

                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    To = 0
                };
                SetupAnimation(opacityAnimation, _fadeOutDuration, "(UIElement.Opacity)");

                resultStorybaord.Children.Add(opacityAnimation);

                return resultStorybaord;
            }

            private Storyboard CreateBubbleStoryboard()
            {
                Storyboard resultStorybaord = new Storyboard();

                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    To = 0.26,
                };
                SetupAnimation(opacityAnimation, _bubbleDuration, "(UIElement.Opacity)");

                double h = Math.Max(_ripple.MouseLeftButtonDownX, _ripple.ActualWidth - _ripple.MouseLeftButtonDownX);
                double w = Math.Max(_ripple.MouseLeftButtonDownY, _ripple.ActualHeight - _ripple.MouseLeftButtonDownY);
                double diameter = 2 * Math.Sqrt(h * h + w * w);

                DoubleAnimation widthAnimation = new DoubleAnimation
                {
                    To = diameter
                };
                SetupAnimation(widthAnimation, _bubbleDuration, "(FrameworkElement.Width)");

                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    To = diameter
                };
                SetupAnimation(heightAnimation, _bubbleDuration, "(FrameworkElement.Height)");

                DoubleAnimation translateXAnimation = new DoubleAnimation
                {
                    To = -diameter / 2
                };
                SetupAnimation(translateXAnimation, _bubbleDuration, "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)");

                DoubleAnimation translateYAnimation = new DoubleAnimation
                {
                    To = -diameter / 2
                };
                SetupAnimation(translateYAnimation, _bubbleDuration, "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)");

                resultStorybaord.Children.Add(opacityAnimation);
                resultStorybaord.Children.Add(widthAnimation);
                resultStorybaord.Children.Add(heightAnimation);
                resultStorybaord.Children.Add(translateXAnimation);
                resultStorybaord.Children.Add(translateYAnimation);

                return resultStorybaord;
            }

            private void SetupAnimation(AnimationTimeline animation, TimeSpan duration, string propertyPath)
            {
                animation.AccelerationRatio = 0.25;
                animation.Duration = new Duration(duration);
                Storyboard.SetTarget(animation, _ellipse);
                Storyboard.SetTargetProperty(animation, new PropertyPath(propertyPath));
            }
        }
    }
}
