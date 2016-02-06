using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// The transitioner provides an easy way to move between content with a default in-place circular transition.
    /// </summary>
    public class Transitioner : Selector
    {
        private Point? _nextTransitionOrigin;

        static Transitioner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Transitioner), new FrameworkPropertyMetadata(typeof(Transitioner)));
        }

        /// <summary>
        /// Causes the the next slide to be displayed (affectively increments <see cref="SelectedIndex"/>).
        /// </summary>
        public static RoutedCommand MoveNextCommand = new RoutedCommand();

        /// <summary>
        /// Causes the the previous slide to be displayed (affectively decrements <see cref="SelectedIndex"/>).
        /// </summary>
        public static RoutedCommand MovePreviousCommand = new RoutedCommand();

        /// <summary>
        /// Moves to the first slide.
        /// </summary>
        public static RoutedCommand MoveFirstCommand = new RoutedCommand();

        /// <summary>
        /// Moves to the last slide.
        /// </summary>
        public static RoutedCommand MoveLastCommand = new RoutedCommand();

        public Transitioner()
        {
            CommandBindings.Add(new CommandBinding(MoveNextCommand, MoveNextHandler));
            CommandBindings.Add(new CommandBinding(MovePreviousCommand, MovePreviousHandler));
            CommandBindings.Add(new CommandBinding(MoveFirstCommand, MoveFirstHandler));
            CommandBindings.Add(new CommandBinding(MoveLastCommand, MoveLastHandler));
            AddHandler(TransitionerSlide.InTransitionFinished, new RoutedEventHandler(IsTransitionFinishedHandler));
            Loaded += (sender, args) =>
            {
                if (SelectedIndex != -1)
                    ActivateFrame(SelectedIndex, -1);
            };
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TransitionerSlide;
        }        

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TransitionerSlide();            
        }        

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            var unselectedIndex = -1;
            if (e.RemovedItems.Count == 1)
            {
                unselectedIndex = Items.IndexOf(e.RemovedItems[0]);
            }
            var selectedIndex = 1;
            if (e.AddedItems.Count == 1)
            {
                selectedIndex = Items.IndexOf(e.AddedItems[0]);
            }

            ActivateFrame(selectedIndex, unselectedIndex);

            base.OnSelectionChanged(e);
        }

        private void IsTransitionFinishedHandler(object sender, RoutedEventArgs routedEventArgs)
        {
            foreach (var slide in Items.OfType<object>().Select(GetSlide).Where(s => s.State == TransitionerSlideState.Previous))
            {
                slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.None);
            }
        }

        private void MoveNextHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            _nextTransitionOrigin = GetNavigationSourcePoint(executedRoutedEventArgs);
            SetCurrentValue(SelectedIndexProperty, Math.Min(Items.Count - 1, SelectedIndex + 1));
        }

        private void MovePreviousHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            _nextTransitionOrigin = GetNavigationSourcePoint(executedRoutedEventArgs);
            SetCurrentValue(SelectedIndexProperty, Math.Max(0, SelectedIndex - 1));
        }

        private void MoveFirstHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            _nextTransitionOrigin = GetNavigationSourcePoint(executedRoutedEventArgs);
            SetCurrentValue(SelectedIndexProperty, 0);
        }

        private void MoveLastHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            _nextTransitionOrigin = GetNavigationSourcePoint(executedRoutedEventArgs);
            SetCurrentValue(SelectedIndexProperty, Items.Count - 1);
        }

        private Point? GetNavigationSourcePoint(ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var sourceElement = executedRoutedEventArgs.OriginalSource as FrameworkElement;
            if (sourceElement == null || !IsAncestorOf(sourceElement) || !IsSafePositive(ActualWidth) ||
                !IsSafePositive(ActualHeight) || !IsSafePositive(sourceElement.ActualWidth) ||
                !IsSafePositive(sourceElement.ActualHeight)) return null;

            var transitionOrigin = sourceElement.TranslatePoint(new Point(sourceElement.ActualWidth / 2, sourceElement.ActualHeight), this);
            transitionOrigin = new Point(transitionOrigin.X / ActualWidth, transitionOrigin.Y / ActualHeight);
            return transitionOrigin;
        }

        private static bool IsSafePositive(double dubz)
        {
            return !double.IsNaN(dubz) && !double.IsInfinity(dubz) && dubz > 0.0;
        }

        private TransitionerSlide GetSlide(object item)
        {
            if (IsItemItsOwnContainer(item))
                return (TransitionerSlide)item;

            return (TransitionerSlide)ItemContainerGenerator.ContainerFromItem(item);
        }

        private void ActivateFrame(int selectedIndex, int unselectedIndex)
        {
            if (!IsLoaded) return;            

            for (var index = 0; index < Items.Count; index++)
            {
                var slide = GetSlide(Items[index]);
                if (index == selectedIndex)
                {
                    Panel.SetZIndex(slide, 2);
                    if (_nextTransitionOrigin != null)
                        slide.OverrideOnce(_nextTransitionOrigin.Value);
                    slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.Current);
                }
                else if (index == unselectedIndex)
                {
                    Panel.SetZIndex(slide, 1);
                    slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.Previous);
                }
                else
                {
                    Panel.SetZIndex(slide, 0);
                    slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.None);
                }
            }

            _nextTransitionOrigin = null;
        }        
    }
}