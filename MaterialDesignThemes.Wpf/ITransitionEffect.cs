using System.Windows;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf
{
    public interface ITransitionEffect
    {
        Timeline Build<TSubject>(TSubject effectSubject) where TSubject : FrameworkElement, ITransitionEffectSubject;
    }
}