using System.Windows;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public abstract class TransitionEffectBase : FrameworkElement, ITransitionEffect
    {
        public abstract Timeline Build<TSubject>(TSubject effectSubject) where TSubject : FrameworkElement, ITransitionEffectSubject;        
    }
}