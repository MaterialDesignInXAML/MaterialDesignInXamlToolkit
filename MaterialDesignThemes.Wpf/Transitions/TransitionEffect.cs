using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class TransitionEffect : TransitionEffectBase
    {
        public TransitionEffect()
        {
        }

        public TransitionEffect(TransitionEffectKind kind)
        {
            Kind = kind;
        }

        public TransitionEffect(TransitionEffectKind kind, TimeSpan duration)
        {
            Kind = kind;
            Duration = duration;
        }

        public TransitionEffectKind Kind { get; set; }

        public TimeSpan OffsetTime { get; set; } = TimeSpan.Zero;

        public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(400);        

        public override Timeline Build<TSubject>(TSubject effectSubject)
        {
            if (effectSubject == null) throw new ArgumentNullException(nameof(effectSubject));

            Timeline timeline = null;
            DependencyProperty property = null;
            DependencyObject target = null;
            string targetName = null;
            switch (Kind)
            {
                //we need these long winded property paths as combined storyboards wont play directly on transforms
                case TransitionEffectKind.None:
                    break;
                case TransitionEffectKind.ExpandIn:
                    return CreateExpandIn(effectSubject);
                case TransitionEffectKind.SlideInFromLeft:
                    timeline = CreateSlide(-300, 0);
                    property = TranslateTransform.XProperty;
                    targetName = effectSubject.TranslateTransformName;
                    break;
                case TransitionEffectKind.SlideInFromTop:
                    timeline = CreateSlide(-300, 0);
                    property = TranslateTransform.YProperty;
                    targetName = effectSubject.TranslateTransformName;
                    break;
                case TransitionEffectKind.SlideInFromRight:
                    timeline = CreateSlide(300, 0);
                    property = TranslateTransform.XProperty;
                    targetName = effectSubject.TranslateTransformName;
                    break;
                case TransitionEffectKind.SlideInFromBottom:
                    timeline = CreateSlide(300, 0);
                    property = TranslateTransform.YProperty;
                    targetName = effectSubject.TranslateTransformName;
                    break;
                case TransitionEffectKind.FadeIn:
                    timeline = new DoubleAnimation { EasingFunction = new SineEase(), From = 0, To = 1};
                    property = OpacityProperty;
                    target = effectSubject;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (timeline == null || (target == null && targetName == null)) return null;            
            timeline.Duration = Duration;
            if (target != null)
                Storyboard.SetTarget(timeline, target);
            if (targetName != null)
                Storyboard.SetTargetName(timeline, targetName);            
            
            Storyboard.SetTargetProperty(timeline, new PropertyPath(property));

            return timeline;
        }

        private Timeline CreateSlide(double from, double to)
        {
            var zeroFrame = new DiscreteDoubleKeyFrame(from);
            var startFrame = new DiscreteDoubleKeyFrame(from, OffsetTime);
            var endFrame = new EasingDoubleKeyFrame(to, OffsetTime + Duration) { EasingFunction = new SineEase() };
            var slideAnimation = new DoubleAnimationUsingKeyFrames();
            slideAnimation.KeyFrames.Add(zeroFrame);
            slideAnimation.KeyFrames.Add(startFrame);
            slideAnimation.KeyFrames.Add(endFrame);

            return slideAnimation;
        }

        private Timeline CreateExpandIn(ITransitionEffectSubject effectSubject)
        {            
            var scaleXAnimation = new DoubleAnimationUsingKeyFrames();
            var zeroFrame = new DiscreteDoubleKeyFrame(0.0);
            var startFrame = new DiscreteDoubleKeyFrame(.5, OffsetTime);
            var endFrame = new EasingDoubleKeyFrame(1, OffsetTime + Duration) { EasingFunction = new SineEase() };
            scaleXAnimation.KeyFrames.Add(zeroFrame);
            scaleXAnimation.KeyFrames.Add(startFrame);
            scaleXAnimation.KeyFrames.Add(endFrame);

            Storyboard.SetTargetName(scaleXAnimation, effectSubject.ScaleTransformName);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath(ScaleTransform.ScaleXProperty));

            var scaleYAnimation = scaleXAnimation.Clone();

            Storyboard.SetTargetName(scaleYAnimation, effectSubject.ScaleTransformName);
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath(ScaleTransform.ScaleYProperty));

            var parallelTimeline = new ParallelTimeline();
            parallelTimeline.Children.Add(scaleXAnimation);
            parallelTimeline.Children.Add(scaleYAnimation);

            return parallelTimeline;

            /*
            var scaleXAnimation = new DoubleAnimation { EasingFunction = new SineEase(), From = .5, To = 1 };
            scaleXAnimation.BeginTime = BeginTime;
            scaleXAnimation.Duration = Duration;
            

            Storyboard.SetTargetName(scaleXAnimation, effectSubject.ScaleTransformName);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath(ScaleTransform.ScaleXProperty));

            var scaleYAnimation = new DoubleAnimation { EasingFunction = new SineEase(), From = .5, To = 1 };
            scaleYAnimation.BeginTime = BeginTime;
            scaleYAnimation.Duration = Duration;
            Storyboard.SetTargetName(scaleYAnimation, effectSubject.ScaleTransformName);
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath(ScaleTransform.ScaleYProperty.Name));
            
            var parallelTimeline = new ParallelTimeline();
            parallelTimeline.Children.Add(scaleXAnimation);
            parallelTimeline.Children.Add(scaleYAnimation);

            return parallelTimeline;

    */
        }
    }
}