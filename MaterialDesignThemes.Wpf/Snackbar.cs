using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Implements a <see cref="Snackbar"/> inspired by the Material Design specs (https://material.google.com/components/snackbars-toasts.html).
    /// </summary>
    [ContentProperty("Message")]
    public class Snackbar2 : Control
    {
        private Action _messageQueueRegistrationCleanUp = null;

        static Snackbar2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Snackbar2), new FrameworkPropertyMetadata(typeof(Snackbar2)));
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(SnackbarMessage), typeof(Snackbar2), new PropertyMetadata(default(SnackbarMessage)));

        public SnackbarMessage Message
        {
            get { return (SnackbarMessage) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageQueueProperty = DependencyProperty.Register(
            "MessageQueue", typeof(SnackbarMessageQueue), typeof(Snackbar2), new PropertyMetadata(default(SnackbarMessageQueue), MessageQueuePropertyChangedCallback));

        private static void MessageQueuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var snackbar = (Snackbar2) dependencyObject;
            (snackbar._messageQueueRegistrationCleanUp ?? (() => { }))();            
            var messageQueue = dependencyPropertyChangedEventArgs.NewValue as SnackbarMessageQueue;
            snackbar._messageQueueRegistrationCleanUp = messageQueue?.Pair(snackbar);
        }

        public SnackbarMessageQueue MessageQueue
        {
            get { return (SnackbarMessageQueue) GetValue(MessageQueueProperty); }
            set { SetValue(MessageQueueProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(Snackbar2), new PropertyMetadata(default(bool), IsActivePropertyChangedCallback));

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public event RoutedPropertyChangedEventHandler<bool> IsActiveChanged
        {
            add { AddHandler(IsActiveChangedEvent, value); }
            remove { RemoveHandler(IsActiveChangedEvent, value); }
        }

        public static readonly RoutedEvent IsActiveChangedEvent =
            EventManager.RegisterRoutedEvent(
                "IsActiveChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>),
                typeof(Snackbar2));

        private static void OnIsActiveChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as Snackbar2;
            var args = new RoutedPropertyChangedEventArgs<bool>(
                (bool) e.OldValue,
                (bool) e.NewValue) {RoutedEvent = IsActiveChangedEvent };
            instance?.RaiseEvent(args);
        }        

        public static readonly RoutedEvent DeactivateStoryboardCompletedEvent =
            EventManager.RegisterRoutedEvent(
                "DeactivateStoryboardCompleted",
                RoutingStrategy.Bubble,
                typeof(SnackbarMessageEventArgs),
                typeof(Snackbar2));

        public event RoutedPropertyChangedEventHandler<SnackbarMessage> DeactivateStoryboardCompleted
        {
            add { AddHandler(DeactivateStoryboardCompletedEvent, value); }
            remove { RemoveHandler(DeactivateStoryboardCompletedEvent, value); }
        }

        private static void OnDeactivateStoryboardCompleted(
            IInputElement snackbar, SnackbarMessage message)
        {
            var args = new SnackbarMessageEventArgs(DeactivateStoryboardCompletedEvent, message);
            snackbar.RaiseEvent(args);
        }

        public TimeSpan ActivateStoryboardDuration { get; private set; }

        public TimeSpan DeactivateStoryboardDuration { get; private set; }

        public static readonly DependencyProperty ActionButtonStyleProperty = DependencyProperty.Register(
            "ActionButtonStyle", typeof(Style), typeof(Snackbar2), new PropertyMetadata(default(Style)));

        public Style ActionButtonStyle
        {
            get { return (Style) GetValue(ActionButtonStyleProperty); }
            set { SetValue(ActionButtonStyleProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            //we regards to notification of deactivate storyboard finishing,
            //we either build a storyboard in code and subscribe to completed event, 
            //or take the not 100% proof of the storyboard duration from the storyboard itself
            //...HOWEVER...we can both methods result can work under the same public API so 
            //we can flip the implementation if this version doesnt pan out

            //(currently we have no even on the activate animation; don't 
            // need it just now, but it would mirror the deactivate)

            ActivateStoryboardDuration = GetStoryboardResourceDuration("ActivateStoryboard");
            DeactivateStoryboardDuration = GetStoryboardResourceDuration("DeactivateStoryboard");
            
            base.OnApplyTemplate();
        }

        private TimeSpan GetStoryboardResourceDuration(string resourceName)
        {
            var storyboard = Template.Resources.Contains(resourceName)
                ? (Storyboard)Template.Resources[resourceName]
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

            if ((bool)dependencyPropertyChangedEventArgs.NewValue) return;

            var snackbar = (Snackbar2)dependencyObject;
            if (snackbar.Message == null) return;

            var dispatcherTimer = new DispatcherTimer
            {
                Tag = new Tuple<Snackbar2, SnackbarMessage>(snackbar, snackbar.Message),
                Interval = snackbar.DeactivateStoryboardDuration
            };
            dispatcherTimer.Tick += DeactivateStoryboardDispatcherTimerOnTick;
            dispatcherTimer.Start();            
        }

        private static void DeactivateStoryboardDispatcherTimerOnTick(object sender, EventArgs eventArgs)
        {
            var dispatcherTimer = (DispatcherTimer)sender;
            dispatcherTimer.Stop(); 
            dispatcherTimer.Tick -= DeactivateStoryboardDispatcherTimerOnTick;
            var source = (Tuple<Snackbar2, SnackbarMessage>)dispatcherTimer.Tag;
            OnDeactivateStoryboardCompleted(source.Item1, source.Item2);
        }
    }    
}