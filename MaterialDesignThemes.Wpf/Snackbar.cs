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

            VisualStateManager.GoToState(this, HiddenStateName, false);

            base.OnApplyTemplate();
        }

        public async Task Show()
        {
            // trigger animation and wait for it
            VisualStateManager.GoToState(this, VisibleStateName, true);

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

        public async Task Hide()
        {
            // stop the timer
            _timer.Stop();

            // trigger animation and wait for it
            VisualStateManager.GoToState(this, HiddenStateName, true);

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
    }
}
