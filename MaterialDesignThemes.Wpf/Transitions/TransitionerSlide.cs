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
        static TransitionerSlide()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitionerSlide), new FrameworkPropertyMetadata(typeof(TransitionerSlide)));
        }

        public static readonly DependencyProperty TransitionOriginProperty = DependencyProperty.Register(
            "TransitionOrigin", typeof(Point), typeof(Transitioner), new PropertyMetadata(new Point(0.5, 0.5)));

        public Point TransitionOrigin
        {
            get => (Point)GetValue(TransitionOriginProperty);
            set => SetValue(TransitionOriginProperty, value);
        }

        public static RoutedEvent InTransitionFinished =
            EventManager.RegisterRoutedEvent("InTransitionFinished", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(TransitionerSlide));

        protected void OnInTransitionFinished(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(TransitionerSlideState), typeof(TransitionerSlide), new PropertyMetadata(default(TransitionerSlideState), new PropertyChangedCallback(StatePropertyChangedCallback)));

        private static void StatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TransitionerSlide)d).AnimateToState();
        }

        public TransitionerSlideState State
        {
            get => (TransitionerSlideState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public static readonly DependencyProperty ForwardWipeProperty = DependencyProperty.Register(
            "ForwardWipe", typeof(ITransitionWipe), typeof(TransitionerSlide), new PropertyMetadata(new CircleWipe()));

        public ITransitionWipe ForwardWipe
        {
            get => (ITransitionWipe)GetValue(ForwardWipeProperty);
            set => SetValue(ForwardWipeProperty, value);
        }

        public static readonly DependencyProperty BackwardWipeProperty = DependencyProperty.Register(
            "BackwardWipe", typeof(ITransitionWipe), typeof(TransitionerSlide), new PropertyMetadata(new SlideOutWipe()));

        public ITransitionWipe BackwardWipe
        {
            get => (ITransitionWipe)GetValue(BackwardWipeProperty);
            set => SetValue(BackwardWipeProperty, value);
        }

        private void AnimateToState()
        {
            if (State != TransitionerSlideState.Current) return;

            RunOpeningEffects();
        }
    }
}
