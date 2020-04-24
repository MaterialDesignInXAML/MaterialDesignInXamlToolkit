using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public enum SnackbarActionButtonPlacementMode
    {
        Auto,
        Inline,
        SeparateLine
    }

    /// <summary>
    /// Implements a <see cref="Snackbar"/> inspired by the Material Design specs (https://material.google.com/components/snackbars-toasts.html).
    /// </summary>
    [ContentProperty(nameof(Message))]
    public class Snackbar : Control
    {
        private const string ActivateStoryboardName = "ActivateStoryboard";
        private const string DeactivateStoryboardName = "DeactivateStoryboard";

        private Action _messageQueueRegistrationCleanUp;

        static Snackbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Snackbar), new FrameworkPropertyMetadata(typeof(Snackbar)));
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message), typeof(SnackbarMessage), typeof(Snackbar), new PropertyMetadata(default(SnackbarMessage)));

        public SnackbarMessage Message
        {
            get => (SnackbarMessage) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty MessageQueueProperty = DependencyProperty.Register(
            nameof(MessageQueue), typeof(SnackbarMessageQueue), typeof(Snackbar), new PropertyMetadata(default(SnackbarMessageQueue), MessageQueuePropertyChangedCallback));

        private static void MessageQueuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var snackbar = (Snackbar) dependencyObject;
            (snackbar._messageQueueRegistrationCleanUp ?? (() => { }))();
            var messageQueue = dependencyPropertyChangedEventArgs.NewValue as SnackbarMessageQueue;
            snackbar._messageQueueRegistrationCleanUp = messageQueue?.Pair(snackbar);
        }

        public SnackbarMessageQueue MessageQueue
        {
            get => (SnackbarMessageQueue) GetValue(MessageQueueProperty);
            set => SetValue(MessageQueueProperty, value);
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive), typeof(bool), typeof(Snackbar), new PropertyMetadata(default(bool), IsActivePropertyChangedCallback));

        public bool IsActive
        {
            get => (bool) GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public event RoutedPropertyChangedEventHandler<bool> IsActiveChanged
        {
            add => AddHandler(IsActiveChangedEvent, value);
            remove => RemoveHandler(IsActiveChangedEvent, value);
        }

        public static readonly RoutedEvent IsActiveChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(IsActiveChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(Snackbar));

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as Snackbar;
            var args = new RoutedPropertyChangedEventArgs<bool>((bool) e.OldValue, (bool) e.NewValue)
                { RoutedEvent = IsActiveChangedEvent };
            instance?.RaiseEvent(args);
        }

        public static readonly RoutedEvent DeactivateStoryboardCompletedEvent = EventManager.RegisterRoutedEvent(
            nameof(DeactivateStoryboardCompleted), RoutingStrategy.Bubble, typeof(SnackbarMessageEventArgs), typeof(Snackbar));

        public event RoutedPropertyChangedEventHandler<SnackbarMessage> DeactivateStoryboardCompleted
        {
            add => AddHandler(DeactivateStoryboardCompletedEvent, value);
            remove => RemoveHandler(DeactivateStoryboardCompletedEvent, value);
        }

        private static void OnDeactivateStoryboardCompleted(IInputElement snackbar, SnackbarMessage message)
        {
            var args = new SnackbarMessageEventArgs(DeactivateStoryboardCompletedEvent, message);
            snackbar.RaiseEvent(args);
        }

        public TimeSpan ActivateStoryboardDuration { get; private set; }

        public TimeSpan DeactivateStoryboardDuration { get; private set; }

        public static readonly DependencyProperty ActionButtonStyleProperty = DependencyProperty.Register(
            nameof(ActionButtonStyle), typeof(Style), typeof(Snackbar), new PropertyMetadata(default(Style)));

        public Style ActionButtonStyle
        {
            get => (Style) GetValue(ActionButtonStyleProperty);
            set => SetValue(ActionButtonStyleProperty, value);
        }

        public override void OnApplyTemplate()
        {
            //we regards to notification of deactivate storyboard finishing,
            //we either build a storyboard in code and subscribe to completed event, 
            //or take the not 100% proof of the storyboard duration from the storyboard itself
            //...HOWEVER...we can both methods result can work under the same public API so 
            //we can flip the implementation if this version does not pan out

            //(currently we have no even on the activate animation; don't 
            // need it just now, but it would mirror the deactivate)

            ActivateStoryboardDuration = GetStoryboardResourceDuration(ActivateStoryboardName);
            DeactivateStoryboardDuration = GetStoryboardResourceDuration(DeactivateStoryboardName);

            base.OnApplyTemplate();
        }

        private TimeSpan GetStoryboardResourceDuration(string resourceName)
        {
            var storyboard = Template.Resources.Contains(resourceName)
                ? (Storyboard) Template.Resources[resourceName]
                : null;

            return storyboard != null && storyboard.Duration.HasTimeSpan
                ? storyboard.Duration.TimeSpan
                : new Func<TimeSpan>(() =>
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Warning, no Duration was specified at root of storyboard '{resourceName}'.");
                    return TimeSpan.Zero;
                })();
        }

        private static void IsActivePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            OnIsActiveChanged(dependencyObject, dependencyPropertyChangedEventArgs);

            if ((bool) dependencyPropertyChangedEventArgs.NewValue) return;

            var snackbar = (Snackbar) dependencyObject;
            if (snackbar.Message == null) return;

            var dispatcherTimer = new DispatcherTimer
            {
                Tag = new Tuple<Snackbar, SnackbarMessage>(snackbar, snackbar.Message),
                Interval = snackbar.DeactivateStoryboardDuration
            };
            dispatcherTimer.Tick += DeactivateStoryboardDispatcherTimerOnTick;
            dispatcherTimer.Start();
        }

        private static void DeactivateStoryboardDispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            var dispatcherTimer = (DispatcherTimer) sender;
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= DeactivateStoryboardDispatcherTimerOnTick;
            var source = (Tuple<Snackbar, SnackbarMessage>) dispatcherTimer.Tag;
            OnDeactivateStoryboardCompleted(source.Item1, source.Item2);
        }

        public static readonly DependencyProperty ActionButtonPlacementProperty = DependencyProperty.Register(
            nameof(ActionButtonPlacement), typeof(SnackbarActionButtonPlacementMode), typeof(Snackbar), new PropertyMetadata(SnackbarActionButtonPlacementMode.Auto));

        public SnackbarActionButtonPlacementMode ActionButtonPlacement
        {
            get => (SnackbarActionButtonPlacementMode) GetValue(ActionButtonPlacementProperty);
            set => SetValue(ActionButtonPlacementProperty, value);
        }
    }
}