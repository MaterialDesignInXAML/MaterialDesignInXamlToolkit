using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    public class SnackbarMessageQueue : ISnackbarMessageQueue, IDisposable
    {
        private readonly TimeSpan _messageDuration;
        private readonly HashSet<Snackbar2> _pairedSnackbars = new HashSet<Snackbar2>();
        private readonly Queue<SnackbarMessageQueueItem> _snackbarMessages = new Queue<SnackbarMessageQueueItem>();        
        private readonly ManualResetEvent _disposedEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _notPausedEvent = new ManualResetEvent(true);
        private int _pauseCounter = 0;
        private bool _isDisposed;

        public SnackbarMessageQueue() : this(TimeSpan.FromSeconds(3))
        { }

        public SnackbarMessageQueue(TimeSpan messageDuration)
        {            
            _messageDuration = messageDuration;
            Task.Factory.StartNew(Pump);
        }

        //oh if only I had Disposable.Create in this lib :)  tempted to copy it in like dragabalz, 
        //but this is an internal method so no one will know my direty Action disposer...
        internal Action Pair(Snackbar2 snackbar)
        {
            if (snackbar == null) throw new ArgumentNullException(nameof(snackbar));

            _pairedSnackbars.Add(snackbar);

            return () => _pairedSnackbars.Remove(snackbar);            
        }

        internal Action Pause()
        {
            if (_isDisposed) return () => { };            

            if (Interlocked.Increment(ref _pauseCounter) == 1)
                _notPausedEvent.Set();

            return () =>
            {
                if (Interlocked.Decrement(ref _pauseCounter) == 0)
                    _notPausedEvent.Reset();
            };
        }

        public void Enqueue(object content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            _snackbarMessages.Enqueue(new SnackbarMessageQueueItem(content));
            _messageWaitingEvent.Set();
        }

        public void Enqueue(object content, object actionContent, Action actionHandler)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            _snackbarMessages.Enqueue(new SnackbarMessageQueueItem(content, actionContent, actionHandler));
            _messageWaitingEvent.Set();
        }

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (actionContent != null ^ actionHandler != null ^ actionArgument != null)
            {
                throw new ArgumentException("All action arguments must be provided if any are provided.", nameof(actionContent));
            }

            var argumentType = actionArgument != null ? typeof(TArgument) : null;
            _snackbarMessages.Enqueue(new SnackbarMessageQueueItem(content, actionContent, actionHandler,
                actionArgument, argumentType));
            _messageWaitingEvent.Set();
        }
        
        private readonly ManualResetEvent _messageWaitingEvent = new ManualResetEvent(false);
        private Tuple<SnackbarMessageQueueItem, DateTime> _latestShownItem;

        private async void Pump()
        {
            while (!_isDisposed)
            {
                var eventId = WaitHandle.WaitAny(new WaitHandle[] {_disposedEvent, _messageWaitingEvent});
                if (eventId == 0) continue;
                var exemplar = _pairedSnackbars.FirstOrDefault();
                if (exemplar == null)
                {
                    //TODO this is a pretty bad scenario, a message waiting, but no snack bar
                    _disposedEvent.WaitOne(TimeSpan.FromSeconds(1));
                    continue;
                }

                //find a target
                var snackbar = await exemplar.Dispatcher.InvokeAsync(() =>
                {
                    return _pairedSnackbars.FirstOrDefault(sb =>
                    {
                        if (!sb.IsLoaded || sb.Visibility != Visibility.Visible) return false;
                        var window = Window.GetWindow(sb);
                        return window != null && window.WindowState != WindowState.Minimized;
                    });
                });

                if (snackbar != null)
                {
                    var message = _snackbarMessages.Dequeue();
                    if (_latestShownItem == null || !Equals(_latestShownItem.Item1.Content, message.Content) ||
                        !Equals(_latestShownItem.Item1.ActionContent, message.ActionContent) ||
                        _latestShownItem.Item2 <= DateTime.Now.Subtract(_messageDuration))
                    {
                        await Show(snackbar, message);
                        _latestShownItem = new Tuple<SnackbarMessageQueueItem, DateTime>(message, DateTime.Now);
                    }
                }

                if (_snackbarMessages.Count > 0)
                    _messageWaitingEvent.Set();
                else
                    _messageWaitingEvent.Reset();
            }                     
        }

        private class MouseNotOverManagedWaitHandle : IDisposable
        {
            private readonly ManualResetEvent _waitHandle;
            private Action _cleanUp;

            public MouseNotOverManagedWaitHandle(UIElement uiElement)
            {
                if (uiElement == null) throw new ArgumentNullException(nameof(uiElement));

                uiElement.MouseEnter += UiElementOnMouseEnter;
                uiElement.MouseLeave += UiElementOnMouseLeave;
                _waitHandle = new ManualResetEvent(!uiElement.IsMouseOver);

                _cleanUp = () =>
                {
                    uiElement.MouseEnter -= UiElementOnMouseEnter;
                    uiElement.MouseLeave -= UiElementOnMouseLeave;
                    _waitHandle.Dispose();
                    _cleanUp = () => { };
                };
            }

            public WaitHandle WaitHandle => _waitHandle;

            private void UiElementOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
            {
                _waitHandle.Set();
            }

            private void UiElementOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
            {
                _waitHandle.Reset();
            }

            public void Dispose()
            {
                _cleanUp();
            }
        }

        private class TimeSpanSpinManagedHandle : IDisposable
        {
            public TimeSpanSpinManagedHandle(WaitHandle pausedWaitHandle)
            {
                //new System.Threading.Timer()



            }



            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        private async Task Show(Snackbar2 snackbar, SnackbarMessageQueueItem messageQueueItem)
        {
            await Task.Run(async () =>
            {
                var completedHandle = new ManualResetEvent(false);
                //TODO set message on snackbar

                var mouseNotOverManagedWaitHandle = await snackbar.Dispatcher.InvokeAsync(() =>
                {                    
                    snackbar.SetCurrentValue(Snackbar2.MessageProperty, Create(messageQueueItem));
                    snackbar.SetCurrentValue(Snackbar2.IsActiveProperty, true);                    
                    return new MouseNotOverManagedWaitHandle(snackbar);
                });

                
                WaitHandle.WaitAll(new WaitHandle[]
                {
                    mouseNotOverManagedWaitHandle.WaitHandle,
                });

                //TODO wait until 3 things:
                // (                
                // * NOT MOUSE OVER
                // * TIME SPAN COMPLETED -- PAUSE MUST KEEP UPPING TIMESPAN                
                // )
                // OR
                //  * ACTION CLICK

                //quick hack implementation
                _disposedEvent.WaitOne(_messageDuration);
                

                //remove message on snackbar
                snackbar.Dispatcher.InvokeAsync(() => snackbar.SetCurrentValue(Snackbar2.IsActiveProperty, false));

                //we could wait for the animation event, but just doing 
                //this for now...at least it is prevent extra call back hell
                _disposedEvent.WaitOne(snackbar.DeactivateStoryboardDuration);

                snackbar.Dispatcher.InvokeAsync(
                    () => snackbar.SetCurrentValue(Snackbar2.MessageProperty, Create(messageQueueItem)));



            });
        }        

        private static SnackbarMessage Create(SnackbarMessageQueueItem messageQueueItem)
        {
            return new SnackbarMessage
            {
                Content = messageQueueItem.Content,
                ActionContent = messageQueueItem.ActionContent
            };
        }

        public void Dispose()
        {
            _isDisposed = true;                  
            _disposedEvent.Set();
            _disposedEvent.Dispose();
            _notPausedEvent.Dispose();
        }
    }
}