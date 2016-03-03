using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignThemes.Wpf.Transitions
{
    /// <summary>
    /// Content control to host the content of an individual page within a <see cref="Transitioner"/>.
    /// </summary>
    public class TransitionerSlide : TransitioningContentBase
    {
        private Point? _overrideTransitionOrigin;

        static TransitionerSlide()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitionerSlide), new FrameworkPropertyMetadata(typeof(TransitionerSlide)));
        }

        public static RoutedEvent InTransitionFinished =
            EventManager.RegisterRoutedEvent("InTransitionFinished", RoutingStrategy.Bubble, typeof (RoutedEventHandler),
                typeof (TransitionerSlide));

        protected void OnInTransitionFinished(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof (TransitionerSlideState), typeof (TransitionerSlide), new PropertyMetadata(default(TransitionerSlideState), new PropertyChangedCallback(StatePropertyChangedCallback)));

        private static void StatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TransitionerSlide) d).AnimateToState();
        }

        public TransitionerSlideState State
        {
            get { return (TransitionerSlideState) GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }        

        //TODO validate inputs
        public static readonly DependencyProperty TransitionOriginProperty = DependencyProperty.Register("TransitionOrigin", typeof (Point), typeof (TransitionerSlide), new PropertyMetadata(new Point(.5, .5)));

        public Point TransitionOrigin
        {
            get { return (Point) GetValue(TransitionOriginProperty); }
            set { SetValue(TransitionOriginProperty, value); }
        }

        internal void OverrideOnce(Point transitionOrigin)
        {
            _overrideTransitionOrigin = transitionOrigin;
        }

        private void AnimateToState()
        {
            if (State != TransitionerSlideState.Current) return;                        
            
            RunInTransitionIn();

            RunOpeningEffects();
        }

        protected virtual void RunInTransitionIn()
        {
            var transitionOrigin = _overrideTransitionOrigin ?? TransitionOrigin;
            _overrideTransitionOrigin = null;

            var horizontalProportion = Math.Max(1.0 - transitionOrigin.X, 1.0*transitionOrigin.X);
            var verticalProportion = Math.Max(1.0 - transitionOrigin.Y, 1.0*transitionOrigin.Y);
            var radius = Math.Sqrt(Math.Pow(ActualWidth*horizontalProportion, 2) + Math.Pow(ActualHeight*verticalProportion, 2));

            var scaleTransform = new ScaleTransform(0, 0);
            var translateTransform = new TranslateTransform(ActualWidth*transitionOrigin.X, ActualHeight*transitionOrigin.Y);
            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            var ellipseGeomotry = new EllipseGeometry()
            {
                RadiusX = radius,
                RadiusY = radius,
                Transform = transformGroup
            };
            Clip = ellipseGeomotry;

            var zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
            var endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400));

            var scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.Completed += ScaleAnimationOnCompleted;
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, zeroKeyTime));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, endKeyTime));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        private void ScaleAnimationOnCompleted(object sender, EventArgs eventArgs)
        {
            Clip = null;
            OnInTransitionFinished(new RoutedEventArgs(InTransitionFinished) { Source = this });
        }
    }
}
