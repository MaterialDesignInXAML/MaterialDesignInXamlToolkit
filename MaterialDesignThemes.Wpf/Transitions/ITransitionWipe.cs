namespace MaterialDesignThemes.Wpf.Transitions
{
    public interface ITransitionWipe
    {
        bool IsDestinationTopmostForDuration { get; }

        void Wipe(TransitionerSlide from, TransitionerSlide to);
    }
}