using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Defines how a data context is sourced for a dialog if a <see cref="FrameworkElement"/>
    /// is passed as the command parameter when using <see cref="DialogHost.OpenDialogCommand"/>.
    /// </summary>
    public enum DialogHostOpenDialogCommandDataContextSource
    {
        /// <summary>
        /// The data context from the sender element (typically a <see cref="Button"/>) 
        /// is applied to the content.
        /// </summary>
        SenderElement,
        /// <summary>
        /// The data context from the <see cref="DialogHost"/> is applied to the content.
        /// </summary>
        DialogHostInstance,
        /// <summary>
        /// The data context is explicitly set to <c>null</c>.
        /// </summary>
        None
    }

    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplatePart(Name = PopupPartName, Type = typeof(ContentControl))]
    [TemplateVisualState(GroupName = "PopupStates", Name = OpenStateName)]
    [TemplateVisualState(GroupName = "PopupStates", Name = ClosedStateName)]
    public class DialogHost : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        public const string PopupContentPartName = "PART_PopupContentElement";
        public const string OpenStateName = "Open";
        public const string ClosedStateName = "Closed";

        /// <summary>
        /// Routed command to be used somewhere inside an instance to trigger showing of the dialog. Content can be passed to the dialog via a <see cref="Button.CommandParameter"/>.
        /// </summary>
        public static RoutedCommand OpenDialogCommand = new RoutedCommand();
        /// <summary>
        /// Routed command to be used inside dialog content to close a dialog. Use a <see cref="Button.CommandParameter"/> to indicate the result of the parameter.
        /// </summary>
        public static RoutedCommand CloseDialogCommand = new RoutedCommand();

        private static readonly HashSet<DialogHost> LoadedInstances = new HashSet<DialogHost>();

        private readonly ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);
        private DialogOpenedEventHandler _asyncShowOpenedEventHandler;
        private DialogClosingEventHandler _asyncShowClosingEventHandler;

        private Popup _popup;
        private ContentControl _popupContentControl;
        private DialogSession _session;
        private DialogOpenedEventHandler _attachedDialogOpenedEventHandler;
        private DialogClosingEventHandler _attachedDialogClosingEventHandler;        
        private object _closeDialogExecutionParameter;
        private IInputElement _restoreFocus;
        private Action _closeCleanUp = () => { };

        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));            
        }

        #region .Show overloads

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content)
        {
            return await Show(content, null, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>        
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>        
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, DialogOpenedEventHandler openedEventHandler)
        {
            return await Show(content, null, openedEventHandler, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, DialogClosingEventHandler closingEventHandler)
        {
            return await Show(content, null, null, closingEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>        
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            return await Show(content, null, openedEventHandler, closingEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIndetifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, object dialogIndetifier)
        {
            return await Show(content, dialogIndetifier, null, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIndetifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static Task<object> Show(object content, object dialogIndetifier, DialogOpenedEventHandler openedEventHandler)
        {
            return Show(content, dialogIndetifier, openedEventHandler, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIndetifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>        
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static Task<object> Show(object content, object dialogIndetifier, DialogClosingEventHandler closingEventHandler)
        {
            return Show(content, dialogIndetifier, null, closingEventHandler);
        }

        #endregion

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIndetifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, object dialogIndetifier, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (LoadedInstances.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost instances.");
            LoadedInstances.First().Dispatcher.VerifyAccess();

            var targets = LoadedInstances.Where(dh => Equals(dh.Identifier, dialogIndetifier)).ToList();
            if (targets.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost have an Identifier property matching dialogIndetifier argument.");
            if (targets.Count > 1)
                throw new InvalidOperationException("Multiple viable DialogHosts.  Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.");
            if (targets[0].IsOpen)
                throw new InvalidOperationException("DialogHost is already open.");

            targets[0].AssertTargetableContent();
            targets[0].DialogContent = content;
            targets[0]._asyncShowOpenedEventHandler = openedEventHandler;
            targets[0]._asyncShowClosingEventHandler = closingEventHandler;
            targets[0].SetCurrentValue(IsOpenProperty, true);

            var task = new Task(() =>
            {
                targets[0]._asyncShowWaitHandle.WaitOne();
            });
            task.Start();

            await task;

            targets[0]._asyncShowOpenedEventHandler = null;
            targets[0]._asyncShowClosingEventHandler = null;

            return targets[0]._closeDialogExecutionParameter;
        }

        public DialogHost()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler, CloseDialogCanExecute));
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));            
        }

        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(
            "Identifier", typeof (object), typeof (DialogHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Identifier which is used in conjunction with <see cref="Show(object)"/> to determine where a dialog should be shown.
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

            ValidationAssist.SetSuppress(dialogHost._popupContentControl, !dialogHost.IsOpen);
            VisualStateManager.GoToState(dialogHost, dialogHost.SelectState(), !TransitionAssist.GetDisableTransitions(dialogHost));

            if (dialogHost.IsOpen)
            {
                WatchWindowActivation(dialogHost);
            }
            else
            {
                dialogHost._asyncShowWaitHandle.Set();
                dialogHost._attachedDialogClosingEventHandler = null;
                dialogHost._session.IsEnded = true;
                dialogHost._session = null;
                dialogHost._closeCleanUp();
                
                return;
            }

            dialogHost._asyncShowWaitHandle.Reset();
            dialogHost._session = new DialogSession(dialogHost);

            //multiple ways of calling back that the dialog has opened:
            // * routed event
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            var dialogOpenedEventArgs = new DialogOpenedEventArgs(dialogHost._session, DialogOpenedEvent);
            dialogHost.OnDialogOpened(dialogOpenedEventArgs);
            dialogHost._attachedDialogOpenedEventHandler?.Invoke(dialogHost, dialogOpenedEventArgs);
            dialogHost.DialogOpenedCallback?.Invoke(dialogHost, dialogOpenedEventArgs);
            dialogHost._asyncShowOpenedEventHandler?.Invoke(dialogHost, dialogOpenedEventArgs);

            dialogHost.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var child = dialogHost._popup?.Child;
                if (child == null) return;

                child.Focus();
                child.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                //https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/issues/187
                //totally not happy about this, but on immediate validation we can get some wierd looking stuff...give WPF a kick to refresh...
                Task.Delay(300).ContinueWith(t => child.Dispatcher.BeginInvoke(new Action(() => child.InvalidateVisual())));                
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

        public static readonly DependencyProperty OpenDialogCommandDataContextSourceProperty = DependencyProperty.Register(
            "OpenDialogCommandDataContextSource", typeof (DialogHostOpenDialogCommandDataContextSource), typeof (DialogHost), new PropertyMetadata(default(DialogHostOpenDialogCommandDataContextSource)));

        /// <summary>
        /// Defines how a data context is sourced for a dialog if a <see cref="FrameworkElement"/>
        /// is passed as the command parameter when using <see cref="DialogHost.OpenDialogCommand"/>.
        /// </summary>
        public DialogHostOpenDialogCommandDataContextSource OpenDialogCommandDataContextSource
        {
            get { return (DialogHostOpenDialogCommandDataContextSource) GetValue(OpenDialogCommandDataContextSourceProperty); }
            set { SetValue(OpenDialogCommandDataContextSourceProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            _popup = GetTemplateChild(PopupPartName) as Popup;
            _popupContentControl = GetTemplateChild(PopupContentPartName) as ContentControl;

            VisualStateManager.GoToState(this, SelectState(), false);

            base.OnApplyTemplate();            
        }

        #region open dialog events/callbacks

        public static readonly RoutedEvent DialogOpenedEvent =
            EventManager.RegisterRoutedEvent(
                "DialogOpened",
                RoutingStrategy.Bubble,
                typeof (DialogOpenedEventHandler),
                typeof (DialogHost));

        /// <summary>
        /// Raised when a dialog is opened.
        /// </summary>
        public event DialogOpenedEventHandler DialogOpened
        {
            add { AddHandler(DialogOpenedEvent, value); }
            remove { RemoveHandler(DialogOpenedEvent, value); }
        }

        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the event.
        /// </summary>
        public static readonly DependencyProperty DialogOpenedAttachedProperty = DependencyProperty.RegisterAttached(
            "DialogOpenedAttached", typeof(DialogOpenedEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogOpenedEventHandler)));

        public static void SetDialogOpenedAttached(DependencyObject element, DialogOpenedEventHandler value)
        {
            element.SetValue(DialogOpenedAttachedProperty, value);
        }

        public static DialogOpenedEventHandler GetDialogOpenedAttached(DependencyObject element)
        {
            return (DialogOpenedEventHandler)element.GetValue(DialogOpenedAttachedProperty);
        }

        public static readonly DependencyProperty DialogOpenedCallbackProperty = DependencyProperty.Register(
            "DialogOpenedCallback", typeof(DialogOpenedEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogOpenedEventHandler)));

        /// <summary>
        /// Callback fired when the <see cref="DialogOpened"/> event is fired, allowing the event to be processed from a binding/view model.
        /// </summary>
        public DialogOpenedEventHandler DialogOpenedCallback
        {
            get { return (DialogOpenedEventHandler)GetValue(DialogOpenedCallbackProperty); }
            set { SetValue(DialogOpenedCallbackProperty, value); }
        }

        protected void OnDialogOpened(DialogOpenedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }

        #endregion

        #region close dialog events/callbacks

        public static readonly RoutedEvent DialogClosingEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosing",
                RoutingStrategy.Bubble,
                typeof (DialogClosingEventHandler),
                typeof (DialogHost));

        /// <summary>
        /// Raised just before a dialog is closed.
        /// </summary>
        public event DialogClosingEventHandler DialogClosing
        {
            add { AddHandler(DialogClosingEvent, value); }
            remove { RemoveHandler(DialogClosingEvent, value); }
        }
        
        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the closing event.
        /// </summary>
        public static readonly DependencyProperty DialogClosingAttachedProperty = DependencyProperty.RegisterAttached(
            "DialogClosingAttached", typeof (DialogClosingEventHandler), typeof (DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        public static void SetDialogClosingAttached(DependencyObject element, DialogClosingEventHandler value)
        {
            element.SetValue(DialogClosingAttachedProperty, value);
        }

        public static DialogClosingEventHandler GetDialogClosingAttached(DependencyObject element)
        {
            return (DialogClosingEventHandler) element.GetValue(DialogClosingAttachedProperty);
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
            RaiseEvent(eventArgs);
        }

        #endregion

        internal void AssertTargetableContent()
        {
            var existindBinding = BindingOperations.GetBindingExpression(this, DialogContentProperty);
            if (existindBinding != null)
                throw new InvalidOperationException(
                    "Content cannot be passed to a dialog via the OpenDialog if DialogContent already has a binding.");
        }

        internal void Close(object parameter)
        {
            var dialogClosingEventArgs = new DialogClosingEventArgs(_session, parameter, DialogClosingEvent);

            _session.IsEnded = true;            

            //multiple ways of calling back that the dialog is closing:
            // * routed event
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            OnDialogClosing(dialogClosingEventArgs);
            _attachedDialogClosingEventHandler?.Invoke(this, dialogClosingEventArgs);
            DialogClosingCallback?.Invoke(this, dialogClosingEventArgs);
            _asyncShowClosingEventHandler?.Invoke(this, dialogClosingEventArgs);

            if (!dialogClosingEventArgs.IsCancelled)
                SetCurrentValue(IsOpenProperty, false);
            else
                _session.IsEnded = false;            

            _closeDialogExecutionParameter = parameter;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null && !window.IsActive)
                window.Activate();
            base.OnPreviewMouseDown(e);
        }

        private void OpenDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            var dependencyObject = executedRoutedEventArgs.OriginalSource as DependencyObject;
            if (dependencyObject != null)
            {
                _attachedDialogOpenedEventHandler = GetDialogOpenedAttached(dependencyObject);
                _attachedDialogClosingEventHandler = GetDialogClosingAttached(dependencyObject);
            }

            if (executedRoutedEventArgs.Parameter != null)
            {
                AssertTargetableContent();

                if (_popupContentControl != null)
                {
                    switch (OpenDialogCommandDataContextSource)
                    {
                        case DialogHostOpenDialogCommandDataContextSource.SenderElement:
                            _popupContentControl.DataContext =
                                (executedRoutedEventArgs.Parameter as FrameworkElement)?.DataContext;
                            break;
                        case DialogHostOpenDialogCommandDataContextSource.DialogHostInstance:
                            _popupContentControl.DataContext = DataContext;
                            break;
                        case DialogHostOpenDialogCommandDataContextSource.None:
                            _popupContentControl.DataContext = null;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                DialogContent = executedRoutedEventArgs.Parameter;
            }

            SetCurrentValue(IsOpenProperty, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void CloseDialogCanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = _session != null;
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            Close(executedRoutedEventArgs.Parameter);

            executedRoutedEventArgs.Handled = true;
        }

        private string SelectState()
        {
            return IsOpen ? OpenStateName : ClosedStateName;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Remove(this);
            SetCurrentValue(IsOpenProperty, false);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Add(this);
        }

        private static void WatchWindowActivation(DialogHost dialogHost)
        {
            var window = Window.GetWindow(dialogHost);
            if (window != null)
            {
                window.Activated += dialogHost.WindowOnActivated;
                window.Deactivated += dialogHost.WindowOnDeactivated;
                dialogHost._closeCleanUp = () =>
                {
                    window.Activated -= dialogHost.WindowOnActivated;
                    window.Deactivated -= dialogHost.WindowOnDeactivated;
                };
            }
            else
            {
                dialogHost._closeCleanUp = () => { };
            }
        }

        private void WindowOnDeactivated(object sender, EventArgs eventArgs)
        {            
            _restoreFocus = _popup != null ? FocusManager.GetFocusedElement((Window)sender) : null;
        }

        private void WindowOnActivated(object sender, EventArgs eventArgs)
        {
            if (_restoreFocus != null)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {                    
                    Keyboard.Focus(_restoreFocus);
                }));
            }
        }
    }
}
