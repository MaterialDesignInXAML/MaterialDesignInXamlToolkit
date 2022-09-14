using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class DialogHostTests : IDisposable
    {
        private readonly DialogHost _dialogHost;

        public DialogHostTests()
        {
            _dialogHost = new DialogHost();
            _dialogHost.ApplyDefaultStyle();
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
        }

        public void Dispose()
        {
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
        }

        [StaFact]
        public void CanOpenAndCloseDialogWithIsOpen()
        {
            _dialogHost.IsOpen = true;
            DialogSession? session = _dialogHost.CurrentSession;
            Assert.False(session?.IsEnded);
            _dialogHost.IsOpen = false;

            Assert.False(_dialogHost.IsOpen);
            Assert.Null(_dialogHost.CurrentSession);
            Assert.True(session?.IsEnded);
        }

        [StaFact]
        public async Task CanOpenAndCloseDialogWithShowMethod()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            object? result = await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler(((sender, args) => { args.Session.Close(42); })));

            Assert.Equal(42, result);
            Assert.False(_dialogHost.IsOpen);
        }

        [StaFact]
        public async Task CanOpenDialogWithShowMethodAndCloseWithIsOpen()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            object? result = await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler(((sender, args) => { _dialogHost.IsOpen = false; })));

            Assert.Null(result);
            Assert.False(_dialogHost.IsOpen);
        }

        [StaFact]
        public async Task CanCloseDialogWithRoutedEvent()
        {
            Guid closeParameter = Guid.NewGuid();
            Task<object?> showTask = _dialogHost.ShowDialog("Content");
            DialogSession? session = _dialogHost.CurrentSession;
            Assert.False(session?.IsEnded);

            DialogHost.CloseDialogCommand.Execute(closeParameter, _dialogHost);

            Assert.False(_dialogHost.IsOpen);
            Assert.Null(_dialogHost.CurrentSession);
            Assert.True(session?.IsEnded);
            Assert.Equal(closeParameter, await showTask);
        }

        [StaFact]
        public async Task DialogHostExposesSessionAsProperty()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler(((sender, args) =>
                {
                    Assert.True(ReferenceEquals(args.Session, _dialogHost.CurrentSession));
                    args.Session.Close();
                })));
        }

        [StaFact]
        public async Task CannotShowDialogWhileItIsAlreadyOpen()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;

            await DialogHost.Show("Content", id,
                new DialogOpenedEventHandler((async (sender, args) =>
                {
                    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));
                    args.Session.Close();
                    Assert.Equal("DialogHost is already open.", ex.Message);
                })));
        }

        [StaFact]
        public async Task WhenNoDialogsAreOpenItThrows()
        {
            var id = Guid.NewGuid();
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

            Assert.Equal("No loaded DialogHost instances.", ex.Message);
        }

        [StaFact]
        public async Task WhenNoDialogsMatchIdentifierItThrows()
        {
            var id = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

            Assert.Equal($"No loaded DialogHost have an {nameof(DialogHost.Identifier)} property matching dialogIdentifier ('{id}') argument.", ex.Message);
        }

        [StaFact]
        public async Task WhenMultipleDialogHostsHaveTheSameIdentifierItThrows()
        {
            var id = Guid.NewGuid();
            _dialogHost.Identifier = id;
            var otherDialogHost = new DialogHost { Identifier = id };
            otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

            otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));


            Assert.Equal("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.", ex.Message);
        }

        [StaFact]
        public async Task WhenNoIdentifierIsSpecifiedItUsesSingleDialogHost()
        {
            bool isOpen = false;
            await DialogHost.Show("Content", new DialogOpenedEventHandler(((sender, args) =>
            {
                isOpen = _dialogHost.IsOpen;
                args.Session.Close();
            })));

            Assert.True(isOpen);
        }

        [StaFact]
        public async Task WhenContentIsNullItThrows()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => DialogHost.Show(null!));

            Assert.Equal("content", ex.ParamName);
        }

        [StaFact]
        [Description("Issue 1212")]
        public async Task WhenContentIsUpdatedClosingEventHandlerIsInvoked()
        {
            int closeInvokeCount = 0;
            void ClosingHandler(object s, DialogClosingEventArgs e)
            {
                closeInvokeCount++;
                if (closeInvokeCount == 1)
                {
                    e.Cancel();
                }
            }

            var dialogTask = DialogHost.Show("Content", ClosingHandler);
            _dialogHost.CurrentSession?.Close("FirstResult");
            _dialogHost.CurrentSession?.Close("SecondResult");
            object? result = await dialogTask;

            Assert.Equal("SecondResult", result);
            Assert.Equal(2, closeInvokeCount);
        }

        [StaFact]
        public async Task WhenCancellingClosingEventClosedEventHandlerIsNotInvoked()
        {
            int closingInvokeCount = 0;
            void ClosingHandler(object s, DialogClosingEventArgs e)
            {
                closingInvokeCount++;
                if (closingInvokeCount == 1)
                {
                    e.Cancel();
                }
            }
            int closedInvokeCount = 0;
            void ClosedHandler(object s, DialogClosedEventArgs e)
            {
                closedInvokeCount++;
            }

            var dialogTask = DialogHost.Show("Content", null, ClosingHandler, ClosedHandler);
            _dialogHost.CurrentSession?.Close("FirstResult");
            _dialogHost.CurrentSession?.Close("SecondResult");
            object? result = await dialogTask;

            Assert.Equal("SecondResult", result);
            Assert.Equal(2, closingInvokeCount);
            Assert.Equal(1, closedInvokeCount);
        }

        [StaFact]
        [Description("Issue 1328")]
        public async Task WhenDoubleClickAwayDialogCloses()
        {
            _dialogHost.CloseOnClickAway = true;
            Grid contentCover = _dialogHost.FindVisualChild<Grid>(DialogHost.ContentCoverGridName);

            int closingCount = 0;
            Task shownDialog = _dialogHost.ShowDialog("Content", new DialogClosingEventHandler((sender, args) =>
            {
                closingCount++;
            }));

            contentCover.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 1, MouseButton.Left)
            {
                RoutedEvent = UIElement.MouseLeftButtonUpEvent
            });
            contentCover.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 1, MouseButton.Left)
            {
                RoutedEvent = UIElement.MouseLeftButtonUpEvent
            });

            await shownDialog;

            Assert.Equal(1, closingCount);
        }

        [StaFact]
        [Description("Issue 1618")]
        public void WhenDialogHostIsUnloadedIsOpenRemainsTrue()
        {
            _dialogHost.IsOpen = true;
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

            Assert.True(_dialogHost.IsOpen);
        }

        [StaFact]
        [Description("Issue 1750")]
        public async Task WhenSettingIsOpenToFalseItReturnsClosingParameterToShow()
        {
            Guid closeParameter = Guid.NewGuid();

            Task<object?> showTask = _dialogHost.ShowDialog("Content");
            _dialogHost.CurrentSession!.CloseParameter = closeParameter;

            _dialogHost.IsOpen = false;

            Assert.Equal(closeParameter, await showTask);
        }

        [StaFact]
        [Description("Issue 1750")]
        public async Task WhenClosingDialogReturnValueCanBeSpecifiedInClosingEventHandler()
        {
            Guid closeParameter = Guid.NewGuid();

            Task<object?> showTask = _dialogHost.ShowDialog("Content", (object sender, DialogClosingEventArgs args) =>
            {
                args.Session.CloseParameter = closeParameter;
            });

            DialogHost.CloseDialogCommand.Execute(null, _dialogHost);

            Assert.Equal(closeParameter, await showTask);
        }

        [StaFact]
        public async Task WhenClosingDialogReturnValueCanBeSpecifiedInClosedEventHandler()
        {
            Guid closeParameter = Guid.NewGuid();

            Task<object?> showTask = _dialogHost.ShowDialog("Content", (sender, args) => { }, (sender, args) => { }, (object sender, DialogClosedEventArgs args) =>
            {
                args.Session.CloseParameter = closeParameter;
            });

            DialogHost.CloseDialogCommand.Execute(null, _dialogHost);

            Assert.Equal(closeParameter, await showTask);
        }

        [StaFact]
        [Description("Pull Request 2029")]
        public void WhenClosingDialogItThrowsWhenNoInstancesLoaded()
        {
            _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

            var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
            Assert.Equal("No loaded DialogHost instances.", ex.Message);
        }

        [StaFact]
        [Description("Pull Request 2029")]
        public void WhenClosingDialogWithInvalidIdentifierItThrowsWhenNoMatchingInstances()
        {
            object id = Guid.NewGuid();
            var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(id));
            Assert.Equal($"No loaded DialogHost have an Identifier property matching dialogIdentifier ('{id}') argument.", ex.Message);
        }

        [StaFact]
        [Description("Pull Request 2029")]
        public void WhenClosingDialogWithMultipleDialogHostsItThrowsTooManyMatchingInstances()
        {
            var secondInstance = new DialogHost();
            try
            {
                secondInstance.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
                var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
                Assert.Equal("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.", ex.Message);
            }
            finally
            {
                secondInstance.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
            }
        }

        [StaFact]
        [Description("Pull Request 2029")]
        public void WhenClosingDialogThatIsNotOpenItThrowsDialogNotOpen()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
            Assert.Equal("DialogHost is not open.", ex.Message);
        }

        [StaFact]
        [Description("Pull Request 2029")]
        public void WhenClosingDialogWithParameterItPassesParameterToHandlers()
        {
            object parameter = Guid.NewGuid();
            object? closingParameter = null;
            object? closedParameter = null;
            _dialogHost.DialogClosing += DialogClosing;
            _dialogHost.DialogClosed += DialogClosed;
            _dialogHost.IsOpen = true;

            DialogHost.Close(null, parameter);

            Assert.Equal(parameter, closingParameter);
            Assert.Equal(parameter, closedParameter);

            void DialogClosing(object sender, DialogClosingEventArgs eventArgs)
            {
                closingParameter = eventArgs.Parameter;
            }

            void DialogClosed(object sender, DialogClosedEventArgs eventArgs)
            {
                closedParameter = eventArgs.Parameter;
            }
        }

        [StaFact]
        public void WhenOpenDialogsAreOpenIsExist()
        {
            object id = Guid.NewGuid();
            _dialogHost.Identifier = id;
            bool isExist = false;
            _ = _dialogHost.ShowDialog("Content", new DialogOpenedEventHandler((sender, arg) =>
            {
                isExist = DialogHost.IsDialogOpen(id);
            }));
            Assert.True(isExist);
            DialogHost.Close(id);
            Assert.False(DialogHost.IsDialogOpen(id));
        }

        [StaFact]
        [Description("Issue 2262")]
        public async Task WhenOnlySingleDialogHostIdentifierIsNullItShowsDialog()
        {
            DialogHost dialogHost2 = new();
            dialogHost2.ApplyDefaultStyle();
            dialogHost2.Identifier = Guid.NewGuid();

            try
            {
                dialogHost2.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
                Task showTask = DialogHost.Show("Content");
                Assert.True(DialogHost.IsDialogOpen(null));
                Assert.False(DialogHost.IsDialogOpen(dialogHost2.Identifier));
                DialogHost.Close(null);
                await showTask;
            }
            finally
            {
                dialogHost2.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
            }
        }

        [StaFact]
        [Description("Issue 2844")]
        public void GetDialogSession_ShouldAllowAccessFromMultipleUIThreads()
        {
            // Arrange
            Guid dialogHostIdentifier = Guid.NewGuid();
            Guid dialogHostOnOtherUiThreadIdentifier = Guid.NewGuid();
            DialogHost dialogHost = new();
            DialogHost dialogHostOnOtherUiThread;
            ManualResetEventSlim sync1 = new ManualResetEventSlim();
            Dispatcher? otherUiThreadDispatcher = null;

            // Load dialogHost on current UI thread
            dialogHost.ApplyDefaultStyle();
            dialogHost.Identifier = dialogHostIdentifier;
            dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
            // Load dialogHostOnOtherUiThread on different UI thread
            var thread = new Thread(() =>
            {
                dialogHostOnOtherUiThread = new();
                dialogHostOnOtherUiThread.ApplyDefaultStyle();
                dialogHostOnOtherUiThread.Identifier = dialogHostOnOtherUiThreadIdentifier;
                dialogHostOnOtherUiThread.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
                otherUiThreadDispatcher = Dispatcher.CurrentDispatcher;
                sync1.Set();
                Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            sync1.Wait();

            // Act & Assert
            DialogHost.GetDialogSession(dialogHostIdentifier);
            DialogHost.GetDialogSession(dialogHostOnOtherUiThreadIdentifier);

            // Cleanup 
            otherUiThreadDispatcher?.InvokeShutdown();
        }
    }
}