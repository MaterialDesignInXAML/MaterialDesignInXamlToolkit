using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions;

public class SlideOutWipe : ITransitionWipe
{
    private readonly SineEase _sineEase = new SineEase();
    private KeyTime startKeyTime, midKeyTime, endKeyTime;

    public SlideOutWipe()
    {
        startKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
        midKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));
        endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400));
    }

    public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
    {
        if (fromSlide == null)
        {
            throw new ArgumentNullException(nameof(fromSlide));
        }
        if (toSlide == null)
        {
            throw new ArgumentNullException(nameof(toSlide));
        }
        if (zIndexController == null)
        {
            throw new ArgumentNullException(nameof(zIndexController));
        }

        // Back out old slide setup
        var scaleTransform = new ScaleTransform(1, 1);
        fromSlide.RenderTransform = scaleTransform;

        // Setup scale animation
        var scaleAnimation = new DoubleAnimationUsingKeyFrames();
        scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, startKeyTime));
        scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(.8, endKeyTime));
        scaleAnimation.Completed += (sender, args) =>
        {
            fromSlide.RenderTransform = null;
        };

        // Setup opacity animation
        var opacityAnimation = new DoubleAnimationUsingKeyFrames();
        opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, startKeyTime));
        opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime));
        opacityAnimation.Completed += (sender, args) =>
        {
            fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
        };

        // Slide in new slide setup
        var translateTransform = new TranslateTransform(0, toSlide.ActualHeight);
        toSlide.RenderTransform = translateTransform;

        // Setup slide animation
        var slideAnimation = new DoubleAnimationUsingKeyFrames();
        slideAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(toSlide.ActualHeight, startKeyTime));
        slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(toSlide.ActualHeight, midKeyTime) { EasingFunction = _sineEase });
        slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime) { EasingFunction = _sineEase });

        // Start animations
        translateTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        fromSlide.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

        zIndexController.Stack(toSlide, fromSlide);
    }
}
