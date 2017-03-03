using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class SlideRightWipe : ITransitionWipe
    {
        private readonly SineEase _sineEase = new SineEase();

        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {
            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));

            var zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
            var midishKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));
            var endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400));

            // Old
            fromSlide.RenderTransform = new TranslateTransform(0, 0);
            var fromSlideAnimation = new DoubleAnimationUsingKeyFrames();
            fromSlideAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, zeroKeyTime));
            fromSlideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, midishKeyTime) { EasingFunction = _sineEase });
            fromSlideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(fromSlide.ActualWidth, endKeyTime) { EasingFunction = _sineEase });

            // New
            toSlide.RenderTransform = new TranslateTransform(-toSlide.ActualWidth, 0);
            var toSlideAnimation = new DoubleAnimationUsingKeyFrames();
            toSlideAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(-toSlide.ActualWidth, zeroKeyTime));
            toSlideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(-toSlide.ActualWidth, midishKeyTime) { EasingFunction = _sineEase });
            toSlideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime) { EasingFunction = _sineEase });

            // Start
            fromSlide.RenderTransform.BeginAnimation(TranslateTransform.XProperty, fromSlideAnimation);
            toSlide.RenderTransform.BeginAnimation(TranslateTransform.XProperty, toSlideAnimation);

            zIndexController.Stack(toSlide, fromSlide);
        }
    }
}