using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private static readonly HashSet<DialogHost> LoadedInstances = new HashSet<DialogHost>();

        private readonly ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);
        private DialogClosingEventHandler _asyncShowClosingEventHandler = null;

        private Popup _popup;
        private Window _window;
        private DialogClosingEventHandler _attachedDialogClosingEventHandler;
        private bool _removeContentOnClose;
        private object _closeDialogExecutionParameter = null;

        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));            
        }

        public static async Task<object> Show(object content)
        {
            return await Show(content, null, null);
        }

        public static async Task<object> Show(object content, DialogClosingEventHandler closingEventHandler)
        {
            return await Show(content, null, closingEventHandler);
        }

        public static async Task<object> Show(object content, object dialogIndetifier)
        {
            return await Show(content, dialogIndetifier, null);
        }

        public static async Task<object> Show(object content, object dialogIndetifier, DialogClosingEventHandler closingEventHandler)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (LoadedInstances.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost instances.");
            LoadedInstances.First().Dispatcher.VerifyAccess();

            var targets = LoadedInstances.Where(dh => Equals(dh.Identifier, dialogIndetifier)).ToList();
            if (targets.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost matches identifier.");
            if (targets.Count > 1)
                throw new InvalidOperationException("Multiple viable DialogHosts.  Specify a unique identifier.");
            if (targets[0].IsOpen)
                throw new InvalidOperationException("DialogHost is already open.");

            targets[0].AssertTargetableContent();
            targets[0].DialogContent = content;
            targets[0].SetCurrentValue(IsOpenProperty, true);
            targets[0]._asyncShowClosingEventHandler = closingEventHandler;

            var task = new Task(() =>
            {
                targets[0]._asyncShowWaitHandle.WaitOne();
            });
            task.Start();

            await task;

            targets[0].DialogContent = null;
            targets[0]._asyncShowClosingEventHandler = null;

            return targets[0]._closeDialogExecutionParameter;
        }
        
        public DialogHost()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            SizeChanged += (sender, args) => TriggerPopupReposition();            

            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler));
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));
        }

        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(
            "Identifier", typeof (object), typeof (DialogHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Identifier which is used in conjunction with <see cref="Show"/> to determine where a dialog should be shown.
        /// </summary>
        public object Identifier
        {
            get { return GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof (bool), typeof (DialogHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsOpenPropertyChangedCallback));

        private static void IsOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var dialogHost = (DialogHost)dependencyObject;            

            VisualStateManager.GoToState(dialogHost, dialogHost.SelectState(), true);

            if (!dialogHost.IsOpen)
            {
                dialogHost._asyncShowWaitHandle.Set();
                dialogHost._attachedDialogClosingEventHandler = null;
                if (dialogHost._removeContentOnClose)
                {
                    dialogHost.DialogContent = null;
                    dialogHost._removeContentOnClose = false;
                }
                return;
            }

            dialogHost._asyncShowWaitHandle.Reset();

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
                AssertTargetableContent();
                DialogContent = executedRoutedEventArgs.Parameter;
                _removeContentOnClose = true;
            }

            SetCurrentValue(IsOpenProperty, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void AssertTargetableContent()
        {
            var existindBinding = BindingOperations.GetBindingExpression(this, DialogContentProperty);
            if (existindBinding != null || DialogContent != null)
                throw new InvalidOperationException(
                    "Content cannot be passed to a dialog via the OpenDialog of DialogContent is already set, or has a binding.");
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            var dialogClosingEventArgs = new DialogClosingEventArgs(executedRoutedEventArgs.Parameter, DialogContent);

            //multiple ways of calling back that the dialog is closing:
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            // * routed event
            _attachedDialogClosingEventHandler?.Invoke(this, dialogClosingEventArgs);
            DialogClosingCallback?.Invoke(this, dialogClosingEventArgs);
            _asyncShowClosingEventHandler?.Invoke(this, dialogClosingEventArgs);
            OnDialogClosing(dialogClosingEventArgs);

            if (!dialogClosingEventArgs.IsCancelled)
                SetCurrentValue(IsOpenProperty, false);

            _closeDialogExecutionParameter = executedRoutedEventArgs.Parameter;

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
            LoadedInstances.Remove(this);

            if (_window != null)
            {
                _window.LocationChanged -= WindowOnLocationChanged;
                _window.SizeChanged -= WindowOnSizeChanged;
            }
            SetCurrentValue(IsOpenProperty, false);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Add(this);

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
