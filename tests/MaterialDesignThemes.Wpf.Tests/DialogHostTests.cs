using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf.Tests;

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

    [Test, STAThreadExecutor]
    public async Task CanOpenAndCloseDialogWithIsOpen()
    {
        _dialogHost.IsOpen = true;
        DialogSession? session = _dialogHost.CurrentSession;
        await Assert.That(session?.IsEnded).IsFalse();
        _dialogHost.IsOpen = false;

        await Assert.That(_dialogHost.IsOpen).IsFalse();
        await Assert.That(_dialogHost.CurrentSession).IsNull();
        await Assert.That(session?.IsEnded).IsTrue();
    }

    [Test, STAThreadExecutor]
    public async Task CanOpenAndCloseDialogWithShowMethod()
    {
        var id = Guid.NewGuid();
        _dialogHost.Identifier = id;

        object? result = await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler(((sender, args) => { args.Session.Close(42); })));

        await Assert.That(result).IsEqualTo(42);
        Assert.False(_dialogHost.IsOpen);
    }

    [Test, STAThreadExecutor]
    public async Task CanOpenDialogWithShowMethodAndCloseWithIsOpen()
    {
        var id = Guid.NewGuid();
        _dialogHost.Identifier = id;

        object? result = await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler(((sender, args) => { _dialogHost.IsOpen = false; })));

        Assert.Null(result);
        Assert.False(_dialogHost.IsOpen);
    }

    [Test, STAThreadExecutor]
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
        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test, STAThreadExecutor]
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

    [Test, STAThreadExecutor]
    public async Task CannotShowDialogWhileItIsAlreadyOpen()
    {
        var id = Guid.NewGuid();
        _dialogHost.Identifier = id;

        await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler((async (sender, args) =>
            {
                var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));
                args.Session.Close();
                await Assert.That(ex.Message).IsEqualTo("DialogHost is already open.");
            })));
    }

    [Test, STAThreadExecutor]
    public async Task WhenNoDialogsAreOpenItThrows()
    {
        var id = Guid.NewGuid();
        _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        await Assert.That(ex.Message).IsEqualTo("No loaded DialogHost instances.");
    }

    [Test, STAThreadExecutor]
    public async Task WhenNoDialogsMatchIdentifierItThrows()
    {
        var id = Guid.NewGuid();

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        await Assert.That(ex.Message).IsEqualTo($"No loaded DialogHost have an {nameof(DialogHost.Identifier)} property matching dialogIdentifier ('{id}') argument.");
    }

    [Test, STAThreadExecutor]
    public async Task WhenMultipleDialogHostsHaveTheSameIdentifierItThrows()
    {
        var id = Guid.NewGuid();
        _dialogHost.Identifier = id;
        var otherDialogHost = new DialogHost { Identifier = id };
        otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));


        await Assert.That(ex.Message).IsEqualTo("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost especially where multiple Windows are a concern.");
    }

    [Test, STAThreadExecutor]
    public async Task WhenNoIdentifierIsSpecifiedItUsesSingleDialogHost()
    {
        bool isOpen = false;
        await DialogHost.Show("Content", new DialogOpenedEventHandler(((sender, args) =>
        {
            isOpen = _dialogHost.IsOpen;
            args.Session.Close();
        })));

        await Assert.That(_dialogHost.IsOpen).IsTrue();
    }

    [Test, STAThreadExecutor]
    public async Task WhenContentIsNullItThrows()
    {
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => DialogHost.Show(null!));

        await Assert.That(ex.ParamName).IsEqualTo("content");
    }

    [Test, STAThreadExecutor]
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

        await Assert.That(result).IsEqualTo("SecondResult");
        await Assert.That(closeInvokeCount).IsEqualTo(2);
    }

    [Test, STAThreadExecutor]
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

        await Assert.That(result).IsEqualTo("SecondResult");
        await Assert.That(closingInvokeCount).IsEqualTo(2);
        await Assert.That(closedInvokeCount).IsEqualTo(1);
    }

    [Test, STAThreadExecutor]
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

        await Assert.That(closingCount).IsEqualTo(1);
    }

    [Test, STAThreadExecutor]
    [Description("Issue 1618")]
    public void WhenDialogHostIsUnloadedIsOpenRemainsTrue()
    {
        _dialogHost.IsOpen = true;
        _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        Assert.True(_dialogHost.IsOpen);
    }

    [Test, STAThreadExecutor]
    [Description("Issue 1750")]
    public async Task WhenSettingIsOpenToFalseItReturnsClosingParameterToShow()
    {
        Guid closeParameter = Guid.NewGuid();

        Task<object?> showTask = _dialogHost.ShowDialog("Content");
        _dialogHost.CurrentSession!.CloseParameter = closeParameter;

        _dialogHost.IsOpen = false;

        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test, STAThreadExecutor]
    [Description("Issue 1750")]
    public async Task WhenClosingDialogReturnValueCanBeSpecifiedInClosingEventHandler()
    {
        Guid closeParameter = Guid.NewGuid();

        Task<object?> showTask = _dialogHost.ShowDialog("Content", (object sender, DialogClosingEventArgs args) =>
        {
            args.Session.CloseParameter = closeParameter;
        });

        DialogHost.CloseDialogCommand.Execute(null, _dialogHost);

        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test, STAThreadExecutor]
    public async Task WhenClosingDialogReturnValueCanBeSpecifiedInClosedEventHandler()
    {
        Guid closeParameter = Guid.NewGuid();

        Task<object?> showTask = _dialogHost.ShowDialog("Content", (sender, args) => { }, (sender, args) => { }, (object sender, DialogClosedEventArgs args) =>
        {
            args.Session.CloseParameter = closeParameter;
        });

        DialogHost.CloseDialogCommand.Execute(null, _dialogHost);

        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test, STAThreadExecutor]
    [Description("Pull Request 2029")]
    public void WhenClosingDialogItThrowsWhenNoInstancesLoaded()
    {
        _dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
        await Assert.That(ex.Message).IsEqualTo("No loaded DialogHost instances.");
    }

    [Test, STAThreadExecutor]
    [Description("Pull Request 2029")]
    public void WhenClosingDialogWithInvalidIdentifierItThrowsWhenNoMatchingInstances()
    {
        object id = Guid.NewGuid();
        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(id));
        await Assert.That(ex.Message).IsEqualTo($"No loaded DialogHost have an Identifier property matching dialogIdentifier ('{id}') argument.");
    }

    [Test, STAThreadExecutor]
    [Description("Pull Request 2029")]
    public void WhenClosingDialogWithMultipleDialogHostsItThrowsTooManyMatchingInstances()
    {
        var secondInstance = new DialogHost();
        try
        {
            secondInstance.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
            var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
            await Assert.That(especially where multiple Windows are a concern.", ex.Message).IsEqualTo("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost);
        }
        finally
        {
            secondInstance.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
        }
    }

    [Test, STAThreadExecutor]
    [Description("Pull Request 2029")]
    public void WhenClosingDialogThatIsNotOpenItThrowsDialogNotOpen()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
        await Assert.That(ex.Message).IsEqualTo("DialogHost is not open.");
    }

    [Test, STAThreadExecutor]
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

        await Assert.That(closingParameter).IsEqualTo(parameter);
        await Assert.That(closedParameter).IsEqualTo(parameter);

        void DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            closingParameter = eventArgs.Parameter;
        }

        void DialogClosed(object sender, DialogClosedEventArgs eventArgs)
        {
            closedParameter = eventArgs.Parameter;
        }
    }

    [Test, STAThreadExecutor]
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

    [Test, STAThreadExecutor]
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

    [Test, STAThreadExecutor]
    [Description("Issue 2844")]
    public void GetDialogSession_ShouldAllowAccessFromMultipleUIThreads()
    {
        DialogHost? dialogHost = null;
        DialogHost? dialogHostOnOtherUiThread = null;
        Dispatcher? otherUiThreadDispatcher = null;
        try
        {
            // Arrange
            Guid dialogHostIdentifier = Guid.NewGuid();
            Guid dialogHostOnOtherUiThreadIdentifier = Guid.NewGuid();
            dialogHost = new DialogHost();
            ManualResetEventSlim sync1 = new();

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
        }
        finally
        {
            // Cleanup 
            otherUiThreadDispatcher?.InvokeShutdown();

            dialogHost?.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
            dialogHostOnOtherUiThread?.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
        }
    }
}
