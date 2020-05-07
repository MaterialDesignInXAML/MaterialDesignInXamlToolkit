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
        private readonly TimeSpan _messageDuration;
        private readonly HashSet<Snackbar> _pairedSnackbars = new HashSet<Snackbar>();
        private readonly LinkedList<SnackbarMessageQueueItem> _snackbarMessages = new LinkedList<SnackbarMessageQueueItem>();
        private readonly ManualResetEvent _disposedEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _pausedEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _messageWaitingEvent = new ManualResetEvent(false);
        private int _pauseCounter;
        private bool _isDisposed;

        #region private class MouseNotOverManagedWaitHandle : IDisposable

        private class MouseNotOverManagedWaitHandle : IDisposable
        {
            private readonly ManualResetEvent _waitHandle;
            private readonly ManualResetEvent _disposedWaitHandle = new ManualResetEvent(false);
            private Action _cleanUp;
            private bool _isWaitHandleDisposed;
            private readonly object _waitHandleGate = new object();

            public MouseNotOverManagedWaitHandle(UIElement uiElement)
            {
                if (uiElement == null) throw new ArgumentNullException(nameof(uiElement));

                _waitHandle = new ManualResetEvent(!uiElement.IsMouseOver);
                uiElement.MouseEnter += UiElementOnMouseEnter;
                uiElement.MouseLeave += UiElementOnMouseLeave;

                _cleanUp = () =>
                {
                    uiElement.MouseEnter -= UiElementOnMouseEnter;
                    uiElement.MouseLeave -= UiElementOnMouseLeave;
                    lock (_waitHandleGate)
                    {
                        _waitHandle.Dispose();
                        _isWaitHandleDisposed = true;
                    }
                    _disposedWaitHandle.Set();
                    _disposedWaitHandle.Dispose();
                    _cleanUp = () => { };
                };
            }

            public WaitHandle WaitHandle => _waitHandle;

            private void UiElementOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _disposedWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
                    }
                    catch (ObjectDisposedException)
                    {
                        /* we are we suppresing this? 
                         * as we have switched out wait onto another thread, so we don't block the UI thread, the
                         * _cleanUp/Dispose() action might also happen, and the _disposedWaitHandle might get disposed
                         * just before we WaitOne. We wond add a lock in the _cleanUp because it might block for 2 seconds.
                         * We could use a Monitor.TryEnter in _cleanUp and run clean up after but oh my gosh it's just getting
                         * too complicated for this use case, so for the rare times this happens, we can swallow safely                         
                         */
                    }

                }).ContinueWith(t =>
                {
                    if (((UIElement)sender).IsMouseOver) return;
                    lock (_waitHandleGate)
                    {
                        if (!_isWaitHandleDisposed)
                            _waitHandle.Set();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
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

        #endregion

        #region private class DurationMonitor

        private class DurationMonitor
        {
            private DateTime _completionTime;

            private DurationMonitor(
                TimeSpan minimumDuration,
                WaitHandle pausedWaitHandle,
                EventWaitHandle signalWhenDurationPassedWaitHandle,
                WaitHandle ceaseWaitHandle)
            {
                if (pausedWaitHandle == null) throw new ArgumentNullException(nameof(pausedWaitHandle));
                if (signalWhenDurationPassedWaitHandle == null)
                    throw new ArgumentNullException(nameof(signalWhenDurationPassedWaitHandle));
                if (ceaseWaitHandle == null) throw new ArgumentNullException(nameof(ceaseWaitHandle));

                _completionTime = DateTime.Now.Add(minimumDuration);

                //this keeps the event waiting simpler, rather that actually watching play -> pause -> play -> pause etc
                var granularity = TimeSpan.FromMilliseconds(200);

                Task.Factory.StartNew(() =>
                {
                    //keep upping the completion time in case it's paused...
                    while (DateTime.Now < _completionTime && !ceaseWaitHandle.WaitOne(granularity))
                    {
                        if (pausedWaitHandle.WaitOne(TimeSpan.Zero))
                        {
                            _completionTime = _completionTime.Add(granularity);
                        }
                    }

                    if (DateTime.Now >= _completionTime)
                        signalWhenDurationPassedWaitHandle.Set();
                });
            }

            public static DurationMonitor Start(TimeSpan minimumDuration,
                WaitHandle pausedWaitHandle,
                EventWaitHandle signalWhenDurationPassedWaitHandle,
                WaitHandle ceaseWaitHandle)
            {
                return new DurationMonitor(minimumDuration, pausedWaitHandle, signalWhenDurationPassedWaitHandle,
                    ceaseWaitHandle);
            }
        }

        #endregion

        public SnackbarMessageQueue() : this(TimeSpan.FromSeconds(3))
        {
        }

        public SnackbarMessageQueue(TimeSpan messageDuration)
        {
            _messageDuration = messageDuration;
            Task.Factory.StartNew(PumpAsync);
        }

        //oh if only I had Disposable.Create in this lib :)  tempted to copy it in like dragabalz, 
        //but this is an internal method so no one will know my direty Action disposer...
        internal Action Pair(Snackbar snackbar)
        {
            if (snackbar == null) throw new ArgumentNullException(nameof(snackbar));

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

        public void Enqueue(object content)
        {
            Enqueue(content, false);
        }

        public void Enqueue(object content, bool neverConsiderToBeDuplicate)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            Enqueue(content, null, null, null, false, neverConsiderToBeDuplicate);
        }

        public void Enqueue(object content, object actionContent, Action actionHandler)
        {
            Enqueue(content, actionContent, actionHandler, false);
        }

        public void Enqueue(object content, object actionContent, Action actionHandler, bool promote)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (actionContent == null) throw new ArgumentNullException(nameof(actionContent));
            if (actionHandler == null) throw new ArgumentNullException(nameof(actionHandler));

            Enqueue(content, actionContent, _ => actionHandler(), promote, false, false);
        }

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument)
        {
            Enqueue(content, actionContent, actionHandler, actionArgument, false, false);
        }

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument, bool promote) =>
            Enqueue(content, actionContent, actionHandler, actionArgument, promote, promote);

        public void Enqueue<TArgument>(object content, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument, bool promote, bool neverConsiderToBeDuplicate, TimeSpan? durationOverride = null)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (actionContent == null ^ actionHandler == null)
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
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (actionContent == null ^ actionHandler == null)
            {
                throw new ArgumentException("All action arguments must be provided if any are provided.",
                    actionContent != null ? nameof(actionContent) : nameof(actionHandler));
            }

            var snackbarMessageQueueItem = new SnackbarMessageQueueItem(content, durationOverride ?? _messageDuration,
                actionContent, actionHandler, actionArgument, promote, neverConsiderToBeDuplicate);
            InsertItem(snackbarMessageQueueItem);

            _messageWaitingEvent.Set();
        }

        private void InsertItem(SnackbarMessageQueueItem item)
        {
            var node = _snackbarMessages.First;
            while (node != null)
            {
                if (!IgnoreDuplicate && item.IsDuplicate(node.Value))
                    return;

                if (item.IsPromoted && !node.Value.IsPromoted)
                {
                    _snackbarMessages.AddBefore(node, item);
                    return;
                }
                node = node.Next;
            }
            _snackbarMessages.AddLast(item);
        }

        private async void PumpAsync()
        {
            while (!_isDisposed)
            {
                var eventId = WaitHandle.WaitAny(new WaitHandle[] { _disposedEvent, _messageWaitingEvent });
                if (eventId == 0) continue;
                var exemplar = _pairedSnackbars.FirstOrDefault();
                if (exemplar == null)
                {
                    Trace.TraceWarning(
                        "A snackbar message as waiting, but no Snackbar instances are assigned to the message queue.");
                    _disposedEvent.WaitOne(TimeSpan.FromSeconds(1));
                    continue;
                }

                //find a target
                var snackbar = await FindSnackbar(exemplar.Dispatcher);

                //show message
                if (snackbar != null)
                {
                    var message = _snackbarMessages.First!.Value;
                    await ShowAsync(snackbar, message);
                    _snackbarMessages.RemoveFirst();
                }
                else
                {
                    //no snackbar could be found, take a break
                    _disposedEvent.WaitOne(TimeSpan.FromSeconds(1));
                }

                if (_snackbarMessages.Count > 0)
                    _messageWaitingEvent.Set();
                else
                    _messageWaitingEvent.Reset();
            }
        }

        private DispatcherOperation<Snackbar> FindSnackbar(Dispatcher dispatcher)
        {
            return dispatcher.InvokeAsync(() =>
            {
                return _pairedSnackbars.FirstOrDefault(sb =>
                {
                    if (!sb.IsLoaded || sb.Visibility != Visibility.Visible) return false;
                    var window = Window.GetWindow(sb);
                    return window?.WindowState != WindowState.Minimized;
                });
            });
        }

        private async Task ShowAsync(Snackbar snackbar, SnackbarMessageQueueItem messageQueueItem)
        {
            await Task.Run(async () =>
                {
                    //create and show the message, setting up all the handles we need to wait on
                    var actionClickWaitHandle = new ManualResetEvent(false);
                    var mouseNotOverManagedWaitHandle =
                        await
                            snackbar.Dispatcher.InvokeAsync(
                                () => CreateAndShowMessage(snackbar, messageQueueItem, actionClickWaitHandle));
                    var durationPassedWaitHandle = new ManualResetEvent(false);
                    DurationMonitor.Start(messageQueueItem.Duration.Add(snackbar.ActivateStoryboardDuration),
                        _pausedEvent, durationPassedWaitHandle, _disposedEvent);

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
            var snackbarMessage = Create(messageQueueItem);
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
            WaitHandle durationPassedWaitHandle, WaitHandle actionClickWaitHandle)
        {
            await Task.WhenAny(
                Task.Factory.StartNew(() =>
                {
                    WaitHandle.WaitAll(new[]
                    {
                        mouseNotOverManagedWaitHandle.WaitHandle,
                        durationPassedWaitHandle
                    });
                }),
                Task.Factory.StartNew(actionClickWaitHandle.WaitOne));
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
            _pausedEvent.Dispose();
        }
    }
}