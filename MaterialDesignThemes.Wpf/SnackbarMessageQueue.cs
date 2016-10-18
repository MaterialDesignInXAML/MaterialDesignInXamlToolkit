using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class SnackbarMessageQueue : ISnackbarMessageQueue, IDisposable
    {
        private readonly TimeSpan _messageDuration;
        private readonly HashSet<Snackbar2> _pairedSnackbars = new HashSet<Snackbar2>();
        private readonly Queue<SnackbarMessageQueueItem> _snackbarMessages = new Queue<SnackbarMessageQueueItem>();        
        private readonly ManualResetEvent _disposedEvent = new ManualResetEvent(false);
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
            //TODO...dialogs, for example, can pause the "countdown" of an active message as they are hiding it


            if (Interlocked.Increment(ref _pauseCounter) == 1)
                //TODO cease counting down
                Console.WriteLine("Paused");

            return () =>
            {
                if (Interlocked.Decrement(ref _pauseCounter) == 0)
                    //TODO resume counting down
                    Console.WriteLine("Paus realesed");
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

            _snackbarMessages.Enqueue(new SnackbarMessageQueueItem(content, actionContent, actionHandler, actionArgument, argumentType));
            _messageWaitingEvent.Set();
        }
        
        private readonly ManualResetEvent _messageWaitingEvent = new ManualResetEvent(false);

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
                    await Show(snackbar, message);
                }

                if (_snackbarMessages.Count > 0)
                    _messageWaitingEvent.Set();
                else
                    _messageWaitingEvent.Reset();
            }         
            
        }

        private async Task Show(Snackbar2 snackbar, SnackbarMessageQueueItem messageQueueItem)
        {
            await Task.Run(() =>
            {
                var completedHandle = new ManualResetEvent(false);
                //TODO set message on snackbar
                snackbar.Dispatcher.InvokeAsync(() =>
                {
                    snackbar.SetCurrentValue(Snackbar2.MessageProperty, Create(messageQueueItem));
                    snackbar.SetCurrentValue(Snackbar2.IsActiveProperty, true);
                });


                

                //TODO wait until 3 things:
                // (
                // * NOT PAUSED
                // * NOT MOUSE OVER
                // * TIME SPAN COMPLETED -- PAUSE MUST KEEP UPPING TIMESPAN                
                // )
                // OR
                //  * ACTION CLICK

                //quick hack implementation
                _disposedEvent.WaitOne(snackbar.ActivateStoryboardDuration.Add(_messageDuration));
                

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
        }
    }
}