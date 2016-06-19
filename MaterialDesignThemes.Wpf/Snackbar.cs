using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Implements a <see cref="Snackbar"/> inspired by the Material Design specs (https://material.google.com/components/snackbars-toasts.html).
    /// </summary>
    public class Snackbar : ContentControl
    {
        public const string PartActionButtonName = "PART_actionButton";

        /// <summary>
        /// The duration of the animation in milliseconds.
        /// </summary>
        public const int AnimationDuration = 300;

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
        public ICommand ActionCommandParameter
        {
            get
            {
                return (ICommand)GetValue(ActionCommandParameterProperty);
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

        private async static void IsOpenChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            // the animations are triggered by the value of the IsOpen property

            Snackbar snackbar = (Snackbar)sender;

            // stop the timer because it may mess up the behaviour of the Snackbar
            snackbar._timer?.Stop();

            if ((bool)args.NewValue)
            {
                await snackbar.ShowAsync();
            }
            else
            {
                await snackbar.HideAsync();
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

        // used to implement the behaviour of the HalfAutomatic mode defined in the Material Design specs
        private DispatcherTimer _timer;

        private Button _actionButton;

        static Snackbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(typeof(Snackbar)));

            ContentProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(ContentChangedHandler));
            TagProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata("0"));
        }

        public Snackbar() : base() { }

        public override void OnApplyTemplate()
        {
            if (_actionButton != null)
            {
                _actionButton.Click -= ActionButtonClickHandler;
            }

            _actionButton = GetTemplateChild(PartActionButtonName) as Button;

            if (_actionButton != null)
            {
                _actionButton.Click += ActionButtonClickHandler;
            }

            base.OnApplyTemplate();
        }

        private async Task ShowContentAsync(object content)
        {
            if (Mode == SnackbarMode.HalfAutomatic)
            {
                // first hide the Snackbar if its already visible
                if (IsOpen)
                {
                    IsOpen = false;

                    // wait for the animation, otherwise the new content will already be shown in the hide animation
                    await Task.Delay(AnimationDuration);
                }

                // now set the new content and show the Snackbar
                InternalContent = content;

                if (content != null)
                {
                    // only show it with content
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

        private async Task ShowAsync()
        {
            // wait for the animation
            await Task.Delay(AnimationDuration);

            // start the timeout in HalfAutomatic mode
            if (Mode == SnackbarMode.HalfAutomatic)
            {
                // start the timer which will hide the Snackbar
                int timeout = VisibilityTimeout;

                if (timeout < MinimumVisibilityTimeout)
                {
                    timeout = MinimumVisibilityTimeout;
                }

                if (_timer == null)
                {
                    _timer = new DispatcherTimer();
                }

                _timer.Tick += async (object sender, EventArgs args) =>
                {
                    IsOpen = false;

                    await Task.Delay(AnimationDuration);
                };
                _timer.Interval = new TimeSpan(0, 0, 0, 0, timeout);
                _timer.Start();
            }
        }

        private async Task HideAsync()
        {
            // wait for the animation
            await Task.Delay(AnimationDuration);
        }

        private async void ActionButtonClickHandler(object sender, RoutedEventArgs args)
        {
            // do not you raise the event if the Snackbar is not fully visible
            if (!IsOpen)
            {
                return;
            }

            Task task = null;

            // hide the Snackbar in HalfAutomatic mode
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
