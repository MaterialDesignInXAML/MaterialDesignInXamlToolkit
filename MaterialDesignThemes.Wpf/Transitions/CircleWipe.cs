using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class CircleWipe : ITransitionWipe
    {
        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {
            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));

            var horizontalProportion = Math.Max(1.0 - origin.X, 1.0 * origin.X);
            var verticalProportion = Math.Max(1.0 - origin.Y, 1.0 * origin.Y);
            var radius = Math.Sqrt(Math.Pow(toSlide.ActualWidth * horizontalProportion, 2) + Math.Pow(toSlide.ActualHeight * verticalProportion, 2));

            var scaleTransform = new ScaleTransform(0, 0);
            var translateTransform = new TranslateTransform(toSlide.ActualWidth * origin.X, toSlide.ActualHeight * origin.Y);
            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            var ellipseGeomotry = new EllipseGeometry()
            {
                RadiusX = radius,
                RadiusY = radius,
                Transform = transformGroup
            };
            
            toSlide.SetCurrentValue(UIElement.ClipProperty, ellipseGeomotry);            
            zIndexController.Stack(toSlide, fromSlide);

            var zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
            var endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300));

            var scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.Completed  += (sender, args) =>
            {
                toSlide.SetCurrentValue(UIElement.ClipProperty, null);
            };
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, zeroKeyTime));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, endKeyTime));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }
    }

    public class SlideOutWipe : ITransitionWipe
    {
        private readonly SineEase _sineEase = new SineEase();

        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {
            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));

            var zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
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

            //slide in new slide setup
            var translateTransform = new TranslateTransform(0, toSlide.ActualHeight);
            toSlide.RenderTransform = translateTransform;            
            var slideAnimation = new DoubleAnimationUsingKeyFrames();
            slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(toSlide.ActualHeight, zeroKeyTime) { EasingFunction = _sineEase});
            slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime) { EasingFunction = _sineEase });

            //kick off!
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);            
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            zIndexController.Stack(toSlide, fromSlide);
        }
    }    
}