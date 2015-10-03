using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplateVisualState(GroupName = "PopupStates", Name = OpenStateName)]
    [TemplateVisualState(GroupName = "PopupStates", Name = ClosedStateName)]
    public class DialogHost : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        public const string OpenStateName = "Open";
        public const string ClosedStateName = "Closed";

        public static RoutedCommand OpenDialogCommand = new RoutedCommand();
        public static RoutedCommand CloseDialogCommand = new RoutedCommand();

        private Popup _popup;
        private Window _window;
        private DialogClosingEventHandler _attachedDialogClosingEventHandler;
        private bool _removeContentOnClose;

        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));            
        }

        public DialogHost()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            SizeChanged += (sender, args) => TriggerPopupReposition();            

            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler));
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof (bool), typeof (DialogHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsOpenPropertyChangedCallback));

        private static void IsOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var dialogHost = (DialogHost)dependencyObject;            

            VisualStateManager.GoToState(dialogHost, dialogHost.SelectState(), true);

            if (!dialogHost.IsOpen)
            {
                dialogHost._attachedDialogClosingEventHandler = null;
                if (dialogHost._removeContentOnClose)
                {
                    dialogHost.DialogContent = null;
                    dialogHost._removeContentOnClose = false;
                }
                return;
            }

            dialogHost.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                dialogHost._popup?.Child?.Focus();
                dialogHost._popup?.Child?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }));
        }

        public bool IsOpen
        {
            get { return (bool) GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register(
            "DialogContent", typeof (object), typeof (DialogHost), new PropertyMetadata(default(object)));

        public object DialogContent
        {
            get { return (object) GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }

        public static readonly DependencyProperty DialogContentTemplateProperty = DependencyProperty.Register(
            "DialogContentTemplate", typeof (DataTemplate), typeof (DialogHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate DialogContentTemplate
        {
            get { return (DataTemplate) GetValue(DialogContentTemplateProperty); }
            set { SetValue(DialogContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty DialogContentTemplateSelectorProperty = DependencyProperty.Register(
            "DialogContentTemplateSelector", typeof (DataTemplateSelector), typeof (DialogHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector DialogContentTemplateSelector
        {
            get { return (DataTemplateSelector) GetValue(DialogContentTemplateSelectorProperty); }
            set { SetValue(DialogContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty DialogContentStringFormatProperty = DependencyProperty.Register(
            "DialogContentStringFormat", typeof (string), typeof (DialogHost), new PropertyMetadata(default(string)));

        public string DialogContentStringFormat
        {
            get { return (string) GetValue(DialogContentStringFormatProperty); }
            set { SetValue(DialogContentStringFormatProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            _popup = GetTemplateChild(PopupPartName) as Popup;            

            VisualStateManager.GoToState(this, SelectState(), false);

            base.OnApplyTemplate();            
        }

        public static readonly RoutedEvent DialogClosingEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosing",
                RoutingStrategy.Bubble,
                typeof (DialogClosingEventHandler),
                typeof (DialogHost));

        /// <summary>
        /// Raised just beforee a dialog is closed.
        /// </summary>
        public event DialogClosingEventHandler DialogClosing
        {
            add { AddHandler(DialogClosingEvent, value); }
            remove { RemoveHandler(DialogClosingEvent, value); }
        }

        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the closing event.
        /// </summary>
        public static readonly DependencyProperty DialogClosingProperty = DependencyProperty.RegisterAttached(
            "DialogClosing", typeof (DialogClosingEventHandler), typeof (DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        public static void SetDialogClosing(DependencyObject element, DialogClosingEventHandler value)
        {
            element.SetValue(DialogClosingProperty, value);
        }

        public static DialogClosingEventHandler GetDialogClosing(DependencyObject element)
        {
            return (DialogClosingEventHandler) element.GetValue(DialogClosingProperty);
        }

        public static readonly DependencyProperty DialogClosingCallbackProperty = DependencyProperty.Register(
            "DialogClosingCallback", typeof (DialogClosingEventHandler), typeof (DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        /// <summary>
        /// Callback fired when the <see cref="DialogClosing"/> event is fired, allowing the event to be processed from a binding/view model.
        /// </summary>
        public DialogClosingEventHandler DialogClosingCallback
        {
            get { return (DialogClosingEventHandler) GetValue(DialogClosingCallbackProperty); }
            set { SetValue(DialogClosingCallbackProperty, value); }
        }

        protected void OnDialogClosing(DialogClosingEventArgs eventArgs)
        {
            eventArgs.RoutedEvent = DialogClosingEvent; 
            eventArgs.Source = eventArgs.Source ?? this;
            RaiseEvent(eventArgs);
        }

        private void OpenDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            var dependencyObject = executedRoutedEventArgs.OriginalSource as DependencyObject;
            if (dependencyObject != null)
            {
                _attachedDialogClosingEventHandler = GetDialogClosing(dependencyObject);

            }

            if (executedRoutedEventArgs.Parameter != null)
            {
                var existindBinding = BindingOperations.GetBindingExpression(this, DialogContentProperty);
                if (existindBinding != null || DialogContent != null)
                    throw new InvalidOperationException("Content cannot be passed to a dialog via the OpenDialog of DialogContent is already set, or has a binding.");
                DialogContent = executedRoutedEventArgs.Parameter;
                _removeContentOnClose = true;
            }

            SetCurrentValue(IsOpenProperty, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            var dialogClosingEventArgs = new DialogClosingEventArgs(executedRoutedEventArgs.Parameter, DialogContent);

            //multiple ways of calling back that the dialog is closing:
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * routed event
            _attachedDialogClosingEventHandler?.Invoke(this, dialogClosingEventArgs);
            DialogClosingCallback?.Invoke(this, dialogClosingEventArgs);
            OnDialogClosing(dialogClosingEventArgs);

            if (!dialogClosingEventArgs.IsCancelled)
                SetCurrentValue(IsOpenProperty, false);

            executedRoutedEventArgs.Handled = true;
        }

        private string SelectState()
        {
            return IsOpen ? OpenStateName : ClosedStateName;
        }

        private void TriggerPopupReposition()
        {
            if (_popup == null) return;

            _popup.HorizontalOffset++;
            _popup.HorizontalOffset--;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_window != null)
            {
                _window.LocationChanged -= WindowOnLocationChanged;
                _window.SizeChanged -= WindowOnSizeChanged;
            }
            SetCurrentValue(IsOpenProperty, false);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _window = Window.GetWindow(this);
            if (_window == null) return;

            _window.LocationChanged += WindowOnLocationChanged;
            _window.SizeChanged += WindowOnSizeChanged;
        }

        private void WindowOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            TriggerPopupReposition();
        }

        private void WindowOnLocationChanged(object sender, EventArgs eventArgs)
        {
            TriggerPopupReposition();
        }
    }
}
