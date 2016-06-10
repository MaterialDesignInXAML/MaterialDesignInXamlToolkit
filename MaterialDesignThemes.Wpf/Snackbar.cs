using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Implements a <see cref="Snackbar"/> according to the Material Design specs. Instances are considered for a single-use Snackbar.
    /// </summary>
    [TemplateVisualState(GroupName = VisibilityStatesGroupName, Name = HiddenStateName)]
    [TemplateVisualState(GroupName = VisibilityStatesGroupName, Name = VisibleStateName)]
    public class Snackbar : Control
    {
        public const string PartActionButtonName = "PART_actionButton";

        public const string VisibilityStatesGroupName = "VisibilityStates";
        public const string HiddenStateName = "Hidden";
        public const string VisibleStateName = "Visible";

        public static readonly DependencyProperty ActionLabelProperty = DependencyProperty.Register(nameof(ActionLabel), typeof(object), typeof(Snackbar), new PropertyMetadata(null));

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

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(object), typeof(Snackbar), new PropertyMetadata(null));

        public object Message
        {
            get
            {
                return GetValue(MessageProperty);
            }

            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public SnackbarActionEventHandler ActionHandler { get; internal set; }

        private SnackbarState _state;

        private SnackbarState State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;

                // set the according visual state to trigger the animations
                if (_state == SnackbarState.Initialized)
                {
                    VisualStateManager.GoToState(this, HiddenStateName, false);
                }
                else if (_state == SnackbarState.Visible)
                {
                    VisualStateManager.GoToState(this, VisibleStateName, true);
                }
                else if (_state == SnackbarState.Hidden)
                {
                    VisualStateManager.GoToState(this, HiddenStateName, true);
                }
            }
        }

        private DispatcherTimer _timer;

        static Snackbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(typeof(Snackbar)));
        }

        public Snackbar() { }

        public override void OnApplyTemplate()
        {
            Button actionButton = (Button)GetTemplateChild(PartActionButtonName);
            actionButton.Click += ActionButtonClickHandler;

            State = SnackbarState.Initialized;

            base.OnApplyTemplate();
        }

        /// <summary>
        /// Shows this <see cref="Snackbar"/> inside its parent <see cref="SnackbarHost"/>.
        /// </summary>
        /// <returns></returns>
        public async Task Show()
        {
            if (State != SnackbarState.Initialized)
            {
                // only a fresh initialized Snackbar can be shown
                return;
            }

            // set the state, trigger the animation and wait for it
            State = SnackbarState.Visible;

            await Task.Delay(300);

            // start timer which will hide the Snackbar
            _timer = new DispatcherTimer();
            _timer.Tick += async (object sender, EventArgs args) =>
            {
                await Hide();
            };
            _timer.Interval = new TimeSpan(0, 0, 0, 3, 0);
            _timer.Start();
        }

        /// <summary>
        /// Hides this <see cref="Snackbar"/>.
        /// </summary>
        /// <returns></returns>
        public async Task Hide()
        {
            if (State != SnackbarState.Visible)
            {
                // only a visible Snackbar can be hidden
                return;
            }

            // stop the timer
            _timer.Stop();

            // set the state, trigger the animation and wait for it
            State = SnackbarState.Hidden;

            await Task.Delay(300);

            // finally remove the Snackbar from the UI
            FindSnackbarHost()?.RemoveSnackbar(this);
        }

        private async void ActionButtonClickHandler(object sender, RoutedEventArgs args)
        {
            await Hide();

            // call the optional action handler
            ActionHandler?.Invoke(this, new RoutedEventArgs(args.RoutedEvent, this));
        }

        private SnackbarHost FindSnackbarHost()
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);

            while (parent != null)
            {
                if (parent is SnackbarHost)
                {
                    return (SnackbarHost)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        private enum SnackbarState : byte
        {
            Initialized,
            Visible,
            Hidden
        }
    }
}
