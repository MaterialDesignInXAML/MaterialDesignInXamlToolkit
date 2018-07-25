using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf.Transitions
{
    [Flags]
    public enum TransitioningContentRunHint
    {
        Loaded = 1,
        IsVisibleChanged = 2,
        All = Loaded | IsVisibleChanged
    }


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
            Loaded += (sender, args) => Run(TransitioningContentRunHint.Loaded);
            IsVisibleChanged += (sender, args) => Run(TransitioningContentRunHint.IsVisibleChanged);
            
        }

        public static readonly DependencyProperty RunHintProperty = DependencyProperty.Register(
            nameof(RunHint), typeof(TransitioningContentRunHint), typeof(TransitioningContent), new PropertyMetadata(TransitioningContentRunHint.All));

        public TransitioningContentRunHint RunHint
        {
            get => (TransitioningContentRunHint)GetValue(RunHintProperty);
            set => SetValue(RunHintProperty, value);
        }

        private void Run(TransitioningContentRunHint requiredHint)
        {
            if ((RunHint & requiredHint) == requiredHint)
                RunOpeningEffects();
        }
    }
}