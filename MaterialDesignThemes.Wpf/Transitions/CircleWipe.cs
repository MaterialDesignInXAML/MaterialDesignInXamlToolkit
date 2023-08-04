using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions;

public class CircleWipe : ITransitionWipe
{
    private KeyTime startKeyTime, midKeyTime, endKeyTime;

    public CircleWipe()
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

        double horizontalProportion = Math.Max(1.0 - origin.X, 1.0 * origin.X);
        double verticalProportion = Math.Max(1.0 - origin.Y, 1.0 * origin.Y);
        double radius = Math.Sqrt(Math.Pow(toSlide.ActualWidth * horizontalProportion, 2) + Math.Pow(toSlide.ActualHeight * verticalProportion, 2));

        var scaleTransform = new ScaleTransform(0, 0);
        var translateTransform = new TranslateTransform(toSlide.ActualWidth * origin.X, toSlide.ActualHeight * origin.Y);
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(scaleTransform);
        transformGroup.Children.Add(translateTransform);
        var ellipseGeometry = new EllipseGeometry()
        {
            RadiusX = radius,
            RadiusY = radius,
            Transform = transformGroup
        };

        toSlide.SetCurrentValue(UIElement.ClipProperty, ellipseGeometry);
        zIndexController.Stack(toSlide, fromSlide);

        var opacityAnimation = SetupOpacityAnimation(fromSlide);
        fromSlide.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

        var scaleAnimation = SetupScaleAnimation(fromSlide, toSlide);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
    }

    private DoubleAnimationUsingKeyFrames SetupScaleAnimation(TransitionerSlide fromSlide, TransitionerSlide toSlide)
    {
        var scaleAnimation = new DoubleAnimationUsingKeyFrames();
        scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, startKeyTime));
        scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, endKeyTime));
        scaleAnimation.Completed += (sender, args) =>
        {
            toSlide.SetCurrentValue(UIElement.ClipProperty, null);
            fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
        };
        return scaleAnimation;
    }
    private DoubleAnimationUsingKeyFrames SetupOpacityAnimation(TransitionerSlide fromSlide)
    {
        var opacityAnimation = GenerateThreeStepAnimation();
        opacityAnimation.Completed += (sender, args) =>
        {
            fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
        };
        return opacityAnimation;
    }
    private DoubleAnimationUsingKeyFrames GenerateThreeStepAnimation()
    {
        var animation = new DoubleAnimationUsingKeyFrames();
        animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, startKeyTime));
        animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, midKeyTime));
        animation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime));
        return animation;
    }
}
