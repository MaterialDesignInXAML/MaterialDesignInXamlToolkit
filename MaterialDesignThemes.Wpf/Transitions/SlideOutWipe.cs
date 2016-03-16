using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class SlideOutWipe : ITransitionWipe
    {
        private readonly SineEase _sineEase = new SineEase();

        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {
            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));

            var zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
            var midishKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100));
            var endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300));

            //back out old slide setup
            var scaleTransform = new ScaleTransform(1, 1);
            fromSlide.RenderTransform = scaleTransform;
            var scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, zeroKeyTime));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(.8, endKeyTime));
            scaleAnimation.Completed += (sender, args) =>
            {
                fromSlide.RenderTransform = null;                             
            };
            var opacityAnimation = new DoubleAnimationUsingKeyFrames();
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, zeroKeyTime));
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime));
            opacityAnimation.Completed += (sender, args) =>
            {
                fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
                fromSlide.Opacity = 0;
            };

            //slide in new slide setup
            var translateTransform = new TranslateTransform(0, toSlide.ActualHeight);
            toSlide.RenderTransform = translateTransform;            
            var slideAnimation = new DoubleAnimationUsingKeyFrames();
            slideAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(toSlide.ActualHeight, zeroKeyTime));
            slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(toSlide.ActualHeight, midishKeyTime) { EasingFunction = _sineEase});
            slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime) { EasingFunction = _sineEase });

            //kick off!
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);            
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            fromSlide.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            zIndexController.Stack(toSlide, fromSlide);
        }
    }
}