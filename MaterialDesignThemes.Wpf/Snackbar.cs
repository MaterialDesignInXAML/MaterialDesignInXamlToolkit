using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Implements a <see cref="Snackbar"/> inspired by the Material Design specs (https://material.google.com/components/snackbars-toasts.html).
    /// </summary>
    public class Snackbar : ContentControl
    {
        private const string PartActionButtonName = "PART_actionButton";
        private const string PartContentGridName = "PART_contentGrid";
        private const string PartContentPanelName = "PART_contentPanel";

        /// <summary>
        /// The duration of the open and close animations in milliseconds.
        /// </summary>
        public const int AnimationDuration = 300;

        private const int OpacityAnimationHintOffset = 50;

        /// <summary>
        /// The minimum timeout for a visible <see cref="Snackbar" /> in milliseconds.
        /// </summary>
        public const int MinimumVisibilityTimeout = 3000;

        public static readonly RoutedEvent ActionClickEvent = EventManager.RegisterRoutedEvent(nameof(ActionClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Snackbar));

        /// <summary>
        /// An event raised by clicking on the action button.
        /// </summary>
        public event RoutedEventHandler ActionClick
        {
            add
            {
                AddHandler(ActionClickEvent, value);
            }

            remove
            {
                RemoveHandler(ActionClickEvent, value);
            }
        }

        public static readonly DependencyProperty ActionCommandProperty = DependencyProperty.Register(
            nameof(ActionCommand), typeof(ICommand), typeof(Snackbar), new PropertyMetadata(null));

        /// <summary>
        /// A command by clicking on the action button.
        /// </summary>
        public ICommand ActionCommand
        {
            get
            {
                return (ICommand)GetValue(ActionCommandProperty);
            }

            set
            {
                SetValue(ActionCommandProperty, value);
            }
        }

        public static readonly DependencyProperty ActionCommandParameterProperty = DependencyProperty.Register(
            nameof(ActionCommandParameter), typeof(object), typeof(Snackbar), new PropertyMetadata(null));

        /// <summary>
        /// A parameter for the <see cref="ActionCommand"/>.
        /// </summary>
        public object ActionCommandParameter
        {
            get
            {
                return GetValue(ActionCommandParameterProperty);
            }

            set
            {
                SetValue(ActionCommandParameterProperty, value);
            }
        }

        public static readonly DependencyProperty ActionLabelProperty = DependencyProperty.Register(nameof(ActionLabel), typeof(object), typeof(Snackbar), new PropertyMetadata(null));

        /// <summary>
        /// The label for the action button. A null value will completely hide the action button.
        /// </summary>
        public object ActionLabel
        {
            get
            {
                return GetValue(ActionLabelProperty);
            }

            set
            {
                SetValue(ActionLabelProperty, value);
            }
        }

        private static async void ContentChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            // call the controller method which sets the content and triggers the animations according to the mode
            await ((Snackbar)sender).ShowContentAsync(args.NewValue);
        }

        private static readonly DependencyProperty InternalContentProperty = DependencyProperty.Register(
                nameof(InternalContent), typeof(object), typeof(Snackbar), new PropertyMetadata(null));

        /// <summary>
        /// The <see cref="ContentControl"/> in the template binds to this property.
        /// This property is needed for the automatic behaviour (<see cref="Mode"/> == <see cref="SnackbarMode.HalfAutomatic"/>).
        /// If new content should be shown, the previous content should first hide with the animation, if it is still visible.
        /// Therefore the content cannot switch immediatelly to keep the behaviour in line with the specs.
        /// Have a look at the example videos in the specs (https://material.google.com/components/snackbars-toasts.html).
        /// </summary>
        private object InternalContent
        {
            get
            {
                return GetValue(InternalContentProperty);
            }

            set
            {
                SetValue(InternalContentProperty, value);
            }
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen), typeof(bool), typeof(Snackbar), new PropertyMetadata(false, IsOpenChangedHandler));

        /// <summary>
        /// Returns true if the <see cref="Snackbar"/> is visible or false otherwise. Setting this property shows or hides the <see cref="Snackbar"/>.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }

            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        private static void IsOpenChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            // trigger the animations
            Snackbar snackbar = (Snackbar)sender;

            if ((bool)args.NewValue)
            {
                if (snackbar._openStoryboard != null)
                {
                    // set the duration of the dummy visibility timeout animation as it may has changed since the last call
                    if (snackbar._dummyVisibilityAnimation != null)
                    {
                        int timeout = snackbar.VisibilityTimeout;

                        if (timeout < MinimumVisibilityTimeout)
                        {
                            timeout = MinimumVisibilityTimeout;
                        }

                        snackbar._dummyVisibilityAnimation.Duration = TimeSpan.FromMilliseconds(timeout);
                    }

                    // start the open animation
                    snackbar._openStoryboard.Begin(snackbar, true);
                }
            }
            else
            {
                // stop the open animation if the Snackbar should close before the visibility timeout is reached
                if (snackbar._openStoryboard != null)
                {
                    snackbar._openStoryboard.Stop(snackbar);
                }

                // start the close animation
                if (snackbar._closeStoryboard != null)
                {
                    snackbar._closeStoryboard.Begin(snackbar, true);
                }
            }
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
                nameof(Mode), typeof(SnackbarMode), typeof(Snackbar), new PropertyMetadata(SnackbarMode.HalfAutomatic));

        /// <summary>
        /// The mode of the <see cref="Snackbar"/> which defines its behaviour.
        /// <see cref="SnackbarMode.HalfAutomatic"/> and <see cref="SnackbarMode.Manual"/> are possible values.
        /// </summary>
        public SnackbarMode Mode
        {
            get
            {
                return (SnackbarMode)GetValue(ModeProperty);
            }

            set
            {
                SetValue(ModeProperty, value);
            }
        }

        public static readonly DependencyProperty VisibilityTimeoutProperty = DependencyProperty.Register(
                nameof(VisibilityTimeout), typeof(int), typeof(Snackbar), new PropertyMetadata(MinimumVisibilityTimeout));

        /// <summary>
        /// The timeout for a visible HalfAutomatic mode <see cref="Snackbar"/> in milliseconds. It will hide at the end of the timeout.
        /// Values less than 3000 milliseconds will be overruled by 3000 milliseconds in the automation logic.
        /// </summary>
        public int VisibilityTimeout
        {
            get
            {
                return (int)GetValue(VisibilityTimeoutProperty);
            }

            set
            {
                SetValue(VisibilityTimeoutProperty, value);
            }
        }

        private Storyboard _openStoryboard;
        private Storyboard _closeStoryboard;
        private DoubleAnimation _dummyVisibilityAnimation;

        private Button _actionButton;

        static Snackbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(typeof(Snackbar)));

            ContentProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(ContentChangedHandler));
            TagProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(0.0));
        }

        public Snackbar() : base() { }

        public override void OnApplyTemplate()
        {
            // set the event handler for the action button from the template
            if (_actionButton != null)
            {
                _actionButton.Click -= ActionButtonClickHandler;
            }

            _actionButton = GetTemplateChild(PartActionButtonName) as Button;

            if (_actionButton != null)
            {
                _actionButton.Click += ActionButtonClickHandler;
            }

            // Storyboard for the open animation
            _openStoryboard = new Storyboard();

            // height
            DoubleAnimation contentPanelTagAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromMilliseconds(AnimationDuration));
            contentPanelTagAnimation.BeginTime = TimeSpan.FromMilliseconds(0.0);
            contentPanelTagAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(contentPanelTagAnimation, GetTemplateChild(PartContentPanelName));
            Storyboard.SetTargetProperty(contentPanelTagAnimation, new PropertyPath(StackPanel.TagProperty));
            _openStoryboard.Children.Add(contentPanelTagAnimation);

            // opacity of the content
            DoubleAnimation contentGridOpacityAnimation = new DoubleAnimation(0.0, TimeSpan.FromMilliseconds(0.0));
            contentGridOpacityAnimation.BeginTime = TimeSpan.FromMilliseconds(0.0);
            Storyboard.SetTarget(contentGridOpacityAnimation, GetTemplateChild(PartContentPanelName));
            Storyboard.SetTargetProperty(contentGridOpacityAnimation, new PropertyPath(Grid.OpacityProperty));
            _openStoryboard.Children.Add(contentGridOpacityAnimation);

            contentGridOpacityAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromMilliseconds(AnimationDuration - OpacityAnimationHintOffset));
            contentGridOpacityAnimation.BeginTime = TimeSpan.FromMilliseconds(OpacityAnimationHintOffset);
            contentGridOpacityAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(contentGridOpacityAnimation, GetTemplateChild(PartContentPanelName));
            Storyboard.SetTargetProperty(contentGridOpacityAnimation, new PropertyPath(Grid.OpacityProperty));
            _openStoryboard.Children.Add(contentGridOpacityAnimation);

            // dummy animation to keep the HalfAutomatic mode Snackbar open during the visibility timeout
            _dummyVisibilityAnimation = new DoubleAnimation(1.0, 1.0, TimeSpan.FromMilliseconds(VisibilityTimeout));
            _dummyVisibilityAnimation.BeginTime = TimeSpan.FromMilliseconds(AnimationDuration);
            Storyboard.SetTarget(_dummyVisibilityAnimation, GetTemplateChild(PartContentPanelName));
            Storyboard.SetTargetProperty(_dummyVisibilityAnimation, new PropertyPath(StackPanel.TagProperty));
            _openStoryboard.Children.Add(_dummyVisibilityAnimation);

            _openStoryboard.Completed += (object sender, EventArgs args) =>
            {
                // close the Snackbar after the animation in the HalfAutomatic mode
                if (Mode == SnackbarMode.HalfAutomatic)
                {
                    IsOpen = false;
                }
            };

            // Storyboard for the close animation
            _closeStoryboard = new Storyboard();

            // height
            contentPanelTagAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromMilliseconds(AnimationDuration));
            contentPanelTagAnimation.BeginTime = TimeSpan.FromMilliseconds(0.0);
            contentPanelTagAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(contentPanelTagAnimation, GetTemplateChild(PartContentPanelName));
            Storyboard.SetTargetProperty(contentPanelTagAnimation, new PropertyPath(StackPanel.TagProperty));
            _closeStoryboard.Children.Add(contentPanelTagAnimation);

            base.OnApplyTemplate();
        }

        private async Task ShowContentAsync(object content)
        {
            if (Mode == SnackbarMode.HalfAutomatic)
            {
                // first close the Snackbar if it is already visible
                if (IsOpen)
                {
                    IsOpen = false;

                    // wait for the animation, otherwise the new content will already be shown in the close animation
                    await Task.Delay(AnimationDuration);
                }

                // now set the new content and open the Snackbar
                InternalContent = content;

                if (content != null)
                {
                    // only open it with content
                    IsOpen = true;

                    await Task.Delay(AnimationDuration);
                }
            }
            else
            {
                // switch the content immediatelly
                InternalContent = content;
            }
        }

        private async void ActionButtonClickHandler(object sender, RoutedEventArgs args)
        {
            // do not you raise the event if the Snackbar is not fully visible
            if (!IsOpen)
            {
                return;
            }

            Task task = null;

            // close the Snackbar in HalfAutomatic mode
            if (Mode == SnackbarMode.HalfAutomatic)
            {
                IsOpen = false;

                task = Task.Delay(AnimationDuration);
            }

            // raise the event and call the command
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(ActionClickEvent, this);
            RaiseEvent(routedEventArgs);

            if (ActionCommand != null && ActionCommand.CanExecute(ActionCommandParameter))
            {
                ActionCommand.Execute(ActionCommandParameter);
            }

            if (task != null)
            {
                await task;
            }

            args.Handled = true;
        }

        /// <summary>
        /// Defines the behaviour of a <see cref="Snackbar"/>.
        /// </summary>
        public enum SnackbarMode : byte
        {
            /// <summary>
            /// Enables the behaviour defined in the Material Design specs. The <see cref="Snackbar"/> shows itself on setting the
            /// <see cref="ContentControl.Content"/> property to a not null value and hides again after <see cref="VisibilityTimeout"/>
            /// milliseconds. A click on the action button will immediatelly hide the <see cref="Snackbar"/>. This bevahiour can be
            /// partially overruled by manually setting the <see cref="IsOpen"/> property.
            /// </summary>
            HalfAutomatic,

            /// <summary>
            /// The user fully controls the visibility of the <see cref="Snackbar"/> by setting the <see cref="IsOpen"/> property.
            /// </summary>
            Manual
        }
    }
}
