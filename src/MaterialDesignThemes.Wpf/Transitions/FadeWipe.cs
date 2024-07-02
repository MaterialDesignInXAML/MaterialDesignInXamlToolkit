using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions;

public class FadeWipe : ITransitionWipe
{
    private readonly SineEase sineEase = new();
    private KeyTime startKeyTime, endKeyTime;

    private TimeSpan _duration = TimeSpan.FromMilliseconds(500);

    public FadeWipe()
    {
        startKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
        CalculateEndKeyTime();
    }

    /// <summary>
    /// Duration of the animation
    /// </summary>
    public TimeSpan Duration
    {
        get => _duration;
        set
        {
            _duration = value;
            CalculateEndKeyTime();
        }
    }

    public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
    {
        if (fromSlide is null)
        {
            throw new ArgumentNullException(nameof(fromSlide));
        }
        if (toSlide is null)
        {
            throw new ArgumentNullException(nameof(toSlide));
        }
        if (zIndexController is null)
        {
            throw new ArgumentNullException(nameof(zIndexController));
        }

        // Remove current animations and reset to base value
        double currentFromOpacity = fromSlide.Opacity;
        fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
        fromSlide.Opacity = currentFromOpacity;

        double currentToOpacity = toSlide.Opacity;
        toSlide.BeginAnimation(UIElement.OpacityProperty, null);
        toSlide.Opacity = currentToOpacity != 1 ? currentToOpacity : 0;

        zIndexController.Stack(toSlide, fromSlide);
        DoubleAnimationUsingKeyFrames fromAnimation = SetupOpacityAnimation(currentFromOpacity, 0);
        DoubleAnimationUsingKeyFrames toAnimation = SetupOpacityAnimation(currentToOpacity, 1);

        fromAnimation.Completed += (sender, args) =>
        {
            fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
            toSlide.BeginAnimation(UIElement.OpacityProperty, toAnimation);
        };

        fromSlide.BeginAnimation(UIElement.OpacityProperty, fromAnimation);
    }

    private DoubleAnimationUsingKeyFrames SetupOpacityAnimation(double from, double to)
    {
        DoubleAnimationUsingKeyFrames animation = new();
        animation.KeyFrames.Add(new LinearDoubleKeyFrame(from, startKeyTime));
        animation.KeyFrames.Add(new EasingDoubleKeyFrame(to, endKeyTime, sineEase));
        return animation;
    }

    private void CalculateEndKeyTime()
    {
        endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(Duration.TotalSeconds / 2));
    }
}
