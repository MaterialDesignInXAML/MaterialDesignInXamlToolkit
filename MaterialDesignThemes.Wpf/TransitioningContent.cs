using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Content control to enable easier transitions.
    /// </summary>
    public class TransitioningContent : TransitioningContentBase
    {
        static TransitioningContent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitioningContent), new FrameworkPropertyMetadata(typeof(TransitioningContent)));
        }

        public TransitioningContent()
        {
            Loaded += (sender, args) => RunOpeningEffects();
            IsVisibleChanged += (sender, args) => RunOpeningEffects();
        }
    }
}