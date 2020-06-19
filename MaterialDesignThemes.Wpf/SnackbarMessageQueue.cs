using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public class SnackbarMessageQueue : ISnackbarMessageQueue, IDisposable
    {
        private readonly Dispatcher _dispatcher;
        private readonly TimeSpan _messageDuration;
        private readonly HashSet<Snackbar> _pairedSnackbars = new HashSet<Snackbar>();
        private readonly LinkedList<SnackbarMessageQueueItem> _snackbarMessages = new LinkedList<SnackbarMessageQueueItem>();
        private readonly object _snackbarMessagesLock = new object();
        private readonly ManualResetEvent _disposedEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _pausedEvent = new ManualResetEvent(false);
        private readonly SemaphoreSlim _showMessageSemaphore = new SemaphoreSlim(1, 1);
        private int _pauseCounter;
        private bool _isDisposed;

        /// <summary>
        /// Gets the <see cref="System.Windows.Threading.Dispatcher"/> this <see cref="SnackbarMessageQueue"/> is associated with.
        /// </summary>
        internal Dispatcher Dispatcher => _dispatcher;

        #region MouseNotOverManagedWaitHandle

        private class MouseNotOverManagedWaitHandle : IDisposable
        {
            private readonly UIElement _uiElement;
            private readonly ManualResetEvent _waitHandle;
            private readonly ManualResetEvent _disposedWaitHandle = new ManualResetEvent(false);
            private bool _isDisposed;
            private readonly object _waitHandleGate = new object();

            public MouseNotOverManagedWaitHandle(UIElement uiElement)
            {
                _uiElement = uiElement ?? throw new ArgumentNullException(nameof(uiElement));
                _waitHandle = new ManualResetEvent(!uiElement.IsMouseOver);
                uiElement.MouseEnter += UiElementOnMouseEnter;
                uiElement.MouseLeave += UiElementOnMouseLeave;
            }

            public EventWaitHandle WaitHandle => _waitHandle;

            private void UiElementOnMouseEnter(object sender, MouseEventArgs mouseEventArgs) => _waitHandle.Reset();

            private async void UiElementOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        _disposedWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
                    }
                    catch (ObjectDisposedException)
                    {
                        /* we are we suppressing this? 
                         * as we have switched out wait onto another thread, so we don't block the UI thread, the
                         * _cleanUp/Dispose() action might also happen, and the _disposedWaitHandle might get disposed
                         * just before we WaitOne. We won't add a lock in the _cleanUp because it might block for 2 seconds.
                         * We could use a Monitor.TryEnter in _cleanUp and run clean up after but oh my gosh it's just getting
                         * too complicated for this use case, so for the rare times this happens, we can swallow safely
                         */
                    }
                });
                if (((UIElement)sender).IsMouseOver) return;
                lock (_waitHandleGate)
                {
                    if (!_isDisposed)
                        _waitHandle.Set();
                }
            }

            public void Dispose()
            {
                if (_isDisposed)
                    return;

                _uiElement.MouseEnter -= UiElementOnMouseEnter;
                _uiElement.MouseLeave -= UiElementOnMouseLeave;
                lock (_waitHandleGate)
                {
                    _waitHandle.Dispose();
                    _isDisposed = true;
                }
                _disposedWaitHandle.Set();
                _disposedWaitHandle.Dispose();
            }
        }

        #endregion

        public SnackbarMessageQueue() 
            : this(TimeSpan.FromSeconds(3))
        {
        }

        public SnackbarMessageQueue(TimeSpan messageDuration)
            : this(messageDuration, Dispatcher.FromThread(Thread.CurrentThread)
                          ?? throw new InvalidOperationException("SnackbarMessageQueue must be created in a dispatcher thread"))
        { }

        public SnackbarMessageQueue(TimeSpan messageDuration, Dispatcher dispatcher)
        {
            _messageDuration = messageDuration;
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        //oh if only I had Disposable.Create in this lib :)  tempted to copy it in like dragablz, 
        //but this is an internal method so no one will know my dirty Action disposer...
        internal Action Pair(Snackbar snackbar)
        {
            if (snackbar is null) throw new ArgumentNullException(nameof(snackbar));

           _pairedSnackbars.Add(snackbar);

            return () => _pairedSnackbars.Remove(snackbar);
        }

        internal Action Pause()
        {
            if (_isDisposed) return () => { };

            if (Interlocked.Increment(ref _pauseCounter) == 1)
                _pausedEvent.Set();

            return () =>
            {
                if (Interlocked.Decrement(ref _pauseCounter) == 0)
                    _pausedEvent.Reset();
            };
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this message queue displays messages without discarding duplicates. 
        /// True to show every message even if there are duplicates.
        /// </summary>
        public bool IgnoreDuplicate { get; set; }

        public void Enqueue(object content) => Enqueue(content, false);

        public void Enqueue(object content, bool neverConsiderToBeDuplicate) 
            => Enqueue(content, null, null, null, false, neverConsiderToBeDuplicate);

        public void Enqueue(object content, object actionContent, Action actionHandler) 
            => Enqueue(content, actionContent, actionHandler, false);

        public void Enqueue(object content, object actionContent, Action actionHandler, bool promote)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));
            if (actionContent is null) throw new ArgumentNullException(nameof(actionContent));
            if (actionHandler is null) throw new ArgumentNullException(nameof(actionHandler));

            Enqueue(content, actionContent, _ => actionHandler(), promote, false, false);
        }

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument) 
            => Enqueue(content, actionContent, actionHandler, actionArgument, false, false);

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument, bool promote) =>
            Enqueue(content, actionContent, actionHandler, actionArgument, promote, promote);

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument, bool promote, bool neverConsiderToBeDuplicate, TimeSpan? durationOverride = null)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));

            if (actionContent is null ^ actionHandler is null)
            {
                throw new ArgumentException("All action arguments must be provided if any are provided.",
                    actionContent != null ? nameof(actionContent) : nameof(actionHandler));
            }

            Action<object> handler = actionHandler != null
                ? new Action<object>(argument => actionHandler((TArgument)argument))
                : null;
            Enqueue(content, actionContent, handler, actionArgument, promote, neverConsiderToBeDuplicate);
        }

        public void Enqueue(object content, object actionContent, Action<object> actionHandler,
            object actionArgument, bool promote, bool neverConsiderToBeDuplicate, TimeSpan? durationOverride = null)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));

            if (actionContent is null ^ actionHandler is null)
            {
                throw new ArgumentException("All action arguments must be provided if any are provided.",
                    actionContent != null ? nameof(actionContent) : nameof(actionHandler));
            }

            var snackbarMessageQueueItem = new SnackbarMessageQueueItem(content, durationOverride ?? _messageDuration,
                actionContent, actionHandler, actionArgument, promote, neverConsiderToBeDuplicate);
            InsertItem(snackbarMessageQueueItem);
        }

        private void InsertItem(SnackbarMessageQueueItem item)
        {
            lock (_snackbarMessagesLock)
            {
                var added = false;
                var node = _snackbarMessages.First;
                while (node != null)
                {
                    if (!IgnoreDuplicate && item.IsDuplicate(node.Value))
                        return;

                    if (item.IsPromoted && !node.Value.IsPromoted)
                    {
                        _snackbarMessages.AddBefore(node, item);
                        added = true;
                        break;
                    }
                    node = node.Next;
                }
                if (!added)
                    _snackbarMessages.AddLast(item);
                
            }

            _dispatcher.InvokeAsync(ShowNextAsync);
        }

        private void StartDuration(TimeSpan minimumDuration, EventWaitHandle durationPassedWaitHandle)
        {
            if (durationPassedWaitHandle is null) throw new ArgumentNullException(nameof(durationPassedWaitHandle));

            var completionTime = DateTime.Now.Add(minimumDuration);

            //this keeps the event waiting simpler, rather that actually watching play -> pause -> play -> pause etc
            var granularity = TimeSpan.FromMilliseconds(200);

            Task.Run(() =>
            {
                while (true)
                {
                    if (DateTime.Now >= completionTime) // time is over
                    {
                        durationPassedWaitHandle.Set();
                        break;
                    }

                    if (_disposedEvent.WaitOne(granularity)) // queue is disposed
                        break;

                    if (durationPassedWaitHandle.WaitOne(TimeSpan.Zero)) // manual exit (like message action click)
                        break;

                    if (_pausedEvent.WaitOne(TimeSpan.Zero)) // on pause completion time is extended
                        completionTime = completionTime.Add(granularity);
                }
            });
        }


        private async Task ShowNextAsync()
        {
            await _showMessageSemaphore.WaitAsync()
                .ConfigureAwait(true);
            try
            {
                Snackbar snackbar;
                while (true)
                {
                    if (_isDisposed || _dispatcher.HasShutdownStarted)
                        return;

                    snackbar = FindSnackbar();
                    if (snackbar != null)
                        break;

                    Trace.TraceWarning("A snackbar message as waiting, but no snackbar instances are assigned to the message queue.");
                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(true);
                }

                LinkedListNode<SnackbarMessageQueueItem> messageNode;
                lock (_snackbarMessagesLock)
                {
                    messageNode = _snackbarMessages.First;
                }
                if (messageNode is null)
                    return;
                
                await ShowAsync(snackbar, messageNode.Value)
                    .ConfigureAwait(false);

                lock (_snackbarMessagesLock)
                {
                    _snackbarMessages.Remove(messageNode);
                }
            }
            finally
            {
                _showMessageSemaphore.Release();
            }

            Snackbar FindSnackbar() => _pairedSnackbars.FirstOrDefault(sb =>
            {
                if (!sb.IsLoaded || sb.Visibility != Visibility.Visible) return false;
                var window = Window.GetWindow(sb);
                return window?.WindowState != WindowState.Minimized;
            });
        }

        private async Task ShowAsync(Snackbar snackbar, SnackbarMessageQueueItem messageQueueItem)
        {
            await Task.Run(async () =>
                {
                    //create and show the message, setting up all the handles we need to wait on
                    var actionClickWaitHandle = new ManualResetEvent(false);
                    var mouseNotOverManagedWaitHandle =
                        await snackbar.Dispatcher.InvokeAsync(
                                () => CreateAndShowMessage(snackbar, messageQueueItem, actionClickWaitHandle));
                    var durationPassedWaitHandle = new ManualResetEvent(false);
                    StartDuration(messageQueueItem.Duration.Add(snackbar.ActivateStoryboardDuration), durationPassedWaitHandle);

                    //wait until time span completed (including pauses and mouse overs), or the action is clicked
                    await WaitForCompletionAsync(mouseNotOverManagedWaitHandle, durationPassedWaitHandle, actionClickWaitHandle);

                    //close message on snackbar
                    await
                        snackbar.Dispatcher.InvokeAsync(
                            () => snackbar.SetCurrentValue(Snackbar.IsActiveProperty, false));

                    //we could wait for the animation event, but just doing 
                    //this for now...at least it is prevent extra call back hell
                    _disposedEvent.WaitOne(snackbar.DeactivateStoryboardDuration);

                    //remove message on snackbar
                    await snackbar.Dispatcher.InvokeAsync(
                        () => snackbar.SetCurrentValue(Snackbar.MessageProperty, null));

                    mouseNotOverManagedWaitHandle.Dispose();
                    durationPassedWaitHandle.Dispose();

                })
                .ContinueWith(t =>
                {
                    if (t.Exception == null) return;

                    var exc = t.Exception.InnerExceptions.FirstOrDefault() ?? t.Exception;
                    Trace.WriteLine("Error occured whilst showing Snackbar, exception will be rethrown.");
                    Trace.WriteLine($"{exc.Message} ({exc.GetType().FullName})");
                    Trace.WriteLine(exc.StackTrace);

                    throw t.Exception;
                });
        }

        private static MouseNotOverManagedWaitHandle CreateAndShowMessage(UIElement snackbar,
            SnackbarMessageQueueItem messageQueueItem, EventWaitHandle actionClickWaitHandle)
        {
            var clickCount = 0;
            var snackbarMessage = new SnackbarMessage
            {
                Content = messageQueueItem.Content,
                ActionContent = messageQueueItem.ActionContent
            };
            snackbarMessage.ActionClick += (sender, args) =>
            {
                if (++clickCount == 1)
                    DoActionCallback(messageQueueItem);
                actionClickWaitHandle.Set();
            };
            snackbar.SetCurrentValue(Snackbar.MessageProperty, snackbarMessage);
            snackbar.SetCurrentValue(Snackbar.IsActiveProperty, true);
            return new MouseNotOverManagedWaitHandle(snackbar);
        }

        private static async Task WaitForCompletionAsync(
            MouseNotOverManagedWaitHandle mouseNotOverManagedWaitHandle,
            EventWaitHandle durationPassedWaitHandle,
            EventWaitHandle actionClickWaitHandle)
        {
            var durationTask = Task.Run(() =>
            {
                WaitHandle.WaitAll(new WaitHandle[]
                {
                    mouseNotOverManagedWaitHandle.WaitHandle,
                    durationPassedWaitHandle
                });
            });
            var actionClickTask = Task.Run(actionClickWaitHandle.WaitOne);
            await Task.WhenAny(durationTask, actionClickTask);

            mouseNotOverManagedWaitHandle.WaitHandle.Set();
            durationPassedWaitHandle.Set();
            actionClickWaitHandle.Set();

            await Task.WhenAll(durationTask, actionClickTask);
        }

        private static void DoActionCallback(SnackbarMessageQueueItem messageQueueItem)
        {
            try
            {
                messageQueueItem.ActionHandler(messageQueueItem.ActionArgument);

            }
            catch (Exception exc)
            {
                Trace.WriteLine("Error during SnackbarMessageQueue message action callback, exception will be rethrown.");
                Trace.WriteLine($"{exc.Message} ({exc.GetType().FullName})");
                Trace.WriteLine(exc.StackTrace);

                throw;
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
            _disposedEvent.Set();
            _disposedEvent.Dispose();
            _pausedEvent.Dispose();
        }
    }
}