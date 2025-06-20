using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf.Tests;

[NotInParallel(nameof(DialogHost))]
[TestExecutor<STAThreadExecutor>]
public class DialogHostTests
{
    private static DialogHost CreateElement()
    {
        DialogHost dialogHost = new();
        dialogHost.ApplyDefaultStyle();
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
        return dialogHost;
    }

    [Test]
    public async Task CanOpenAndCloseDialogWithIsOpen()
    {
        var dialogHost = CreateElement();
        dialogHost.IsOpen = true;
        DialogSession? session = dialogHost.CurrentSession;
        await Assert.That(session?.IsEnded).IsFalse();
        dialogHost.IsOpen = false;

        await Assert.That(dialogHost.IsOpen).IsFalse();
        await Assert.That(dialogHost.CurrentSession).IsNull();
        await Assert.That(session?.IsEnded).IsTrue();
    }

    [Test]
    public async Task CanOpenAndCloseDialogWithShowMethod()
    {
        var dialogHost = CreateElement();
        var id = Guid.NewGuid();
        dialogHost.Identifier = id;

        object? result = await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler(((sender, args) => { args.Session.Close(42); })));
        await Assert.That(result).IsEqualTo(42);
        await Assert.That(dialogHost.IsOpen).IsFalse();
    }

    [Test]
    public async Task CanOpenDialogWithShowMethodAndCloseWithIsOpen()
    {
        var dialogHost = CreateElement();
        var id = Guid.NewGuid();
        dialogHost.Identifier = id;

        object? result = await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler(((sender, args) => { dialogHost.IsOpen = false; })));
        await Assert.That(result).IsNull();
        await Assert.That(dialogHost.IsOpen).IsFalse();
    }

    [Test]
    public async Task CanCloseDialogWithRoutedEvent()
    {
        var dialogHost = CreateElement();
        Guid closeParameter = Guid.NewGuid();
        Task<object?> showTask = dialogHost.ShowDialog("Content");
        DialogSession? session = dialogHost.CurrentSession;
        await Assert.That(session?.IsEnded).IsFalse();

        DialogHost.CloseDialogCommand.Execute(closeParameter, dialogHost);

        await Assert.That(dialogHost.IsOpen).IsFalse();
        await Assert.That(dialogHost.CurrentSession).IsNull();
        await Assert.That(session?.IsEnded).IsTrue();
        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test]
    public async Task DialogHostExposesSessionAsProperty()
    {
        var dialogHost = CreateElement();
        var id = Guid.NewGuid();
        dialogHost.Identifier = id;

        await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler(async (sender, args) =>
            {
                await Assert.That(ReferenceEquals(args.Session, dialogHost.CurrentSession)).IsTrue();
                args.Session.Close();
            }));
    }

    [Test]
    public async Task CannotShowDialogWhileItIsAlreadyOpen()
    {
        var dialogHost = CreateElement();
        var id = Guid.NewGuid();
        dialogHost.Identifier = id;

        await DialogHost.Show("Content", id,
            new DialogOpenedEventHandler((async (sender, args) =>
            {
                var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));
                args.Session.Close();
                await Assert.That(ex?.Message).IsEqualTo("DialogHost is already open.");
            })));
    }

    [Test]
    public async Task WhenNoDialogsAreOpenItThrows()
    {
        var dialogHost = CreateElement();
        var id = Guid.NewGuid();
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        await Assert.That(ex?.Message).IsEqualTo("No loaded DialogHost instances.");
    }

    [Test]
    public async Task WhenNoDialogsMatchIdentifierItThrows()
    {
        var _ = CreateElement();
        var id = Guid.NewGuid();

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        await Assert.That(ex?.Message).IsEqualTo($"No loaded DialogHost have an {nameof(DialogHost.Identifier)} property matching dialogIdentifier ('{id}') argument.");
    }

    [Test]
    public async Task WhenMultipleDialogHostsHaveTheSameIdentifierItThrows()
    {
        var dialogHost = CreateElement();
        var id = Guid.NewGuid();
        dialogHost.Identifier = id;
        var otherDialogHost = new DialogHost { Identifier = id };
        otherDialogHost.ApplyDefaultStyle();
        otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        otherDialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        await Assert.That(ex?.Message).IsEqualTo("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.");
    }

    [Test]
    public async Task WhenNoIdentifierIsSpecifiedItUsesSingleDialogHost()
    {
        var dialogHost = CreateElement();
        bool isOpen = false;
        await DialogHost.Show("Content", new DialogOpenedEventHandler(((sender, args) =>
        {
            isOpen = dialogHost.IsOpen;
            args.Session.Close();
        })));

        await Assert.That(isOpen).IsTrue();
    }

    [Test]
    public async Task WhenContentIsNullItThrows()
    {
        CreateElement(); // ensure at least one DialogHost exists
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => DialogHost.Show(null!));

        await Assert.That(ex?.ParamName).IsEqualTo("content");
    }

    [Test]
    [Description("Issue 1212")]
    public async Task WhenContentIsUpdatedClosingEventHandlerIsInvoked()
    {
        var dialogHost = CreateElement();
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
        dialogHost.CurrentSession?.Close("FirstResult");
        dialogHost.CurrentSession?.Close("SecondResult");
        object? result = await dialogTask;

        await Assert.That(result).IsEqualTo("SecondResult");
        await Assert.That(closeInvokeCount).IsEqualTo(2);
    }

    [Test]
    public async Task WhenCancellingClosingEventClosedEventHandlerIsNotInvoked()
    {
        var dialogHost = CreateElement();
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
        dialogHost.CurrentSession?.Close("FirstResult");
        dialogHost.CurrentSession?.Close("SecondResult");
        object? result = await dialogTask;

        await Assert.That(result).IsEqualTo("SecondResult");
        await Assert.That(closingInvokeCount).IsEqualTo(2);
        await Assert.That(closedInvokeCount).IsEqualTo(1);
    }

    [Test]
    [Description("Issue 1328")]
    public async Task WhenDoubleClickAwayDialogCloses()
    {
        var dialogHost = CreateElement();
        dialogHost.CloseOnClickAway = true;
        Grid contentCover = dialogHost.FindVisualChild<Grid>(DialogHost.ContentCoverGridName);

        int closingCount = 0;
        Task shownDialog = dialogHost.ShowDialog("Content", new DialogClosingEventHandler((sender, args) =>
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

    [Test]
    [Description("Issue 1618")]
    public async Task WhenDialogHostIsUnloadedIsOpenRemainsTrue()
    {
        var dialogHost = CreateElement();
        dialogHost.IsOpen = true;
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        await Assert.That(dialogHost.IsOpen).IsTrue();
    }

    [Test]
    [Description("Issue 1750")]
    public async Task WhenSettingIsOpenToFalseItReturnsClosingParameterToShow()
    {
        var dialogHost = CreateElement();
        Guid closeParameter = Guid.NewGuid();

        Task<object?> showTask = dialogHost.ShowDialog("Content");
        dialogHost.CurrentSession!.CloseParameter = closeParameter;

        dialogHost.IsOpen = false;

        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test]
    [Description("Issue 1750")]
    public async Task WhenClosingDialogReturnValueCanBeSpecifiedInClosingEventHandler()
    {
        var dialogHost = CreateElement();
        Guid closeParameter = Guid.NewGuid();

        Task<object?> showTask = dialogHost.ShowDialog("Content", (object sender, DialogClosingEventArgs args) =>
        {
            args.Session.CloseParameter = closeParameter;
        });

        DialogHost.CloseDialogCommand.Execute(null, dialogHost);

        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test]
    public async Task WhenClosingDialogReturnValueCanBeSpecifiedInClosedEventHandler()
    {
        var dialogHost = CreateElement();
        Guid closeParameter = Guid.NewGuid();

        Task<object?> showTask = dialogHost.ShowDialog("Content", (sender, args) => { }, (sender, args) => { }, (object sender, DialogClosedEventArgs args) =>
        {
            args.Session.CloseParameter = closeParameter;
        });

        DialogHost.CloseDialogCommand.Execute(null, dialogHost);

        await Assert.That(await showTask).IsEqualTo(closeParameter);
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogItThrowsWhenNoInstancesLoaded()
    {
        var dialogHost = CreateElement();
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
        await Assert.That(ex.Message).IsEqualTo("No loaded DialogHost instances.");
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogWithInvalidIdentifierItThrowsWhenNoMatchingInstances()
    {
        CreateElement(); // ensure at least one DialogHost exists
        object id = Guid.NewGuid();
        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(id));
        await Assert.That(ex.Message).IsEqualTo($"No loaded DialogHost have an Identifier property matching dialogIdentifier ('{id}') argument.");
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogWithMultipleDialogHostsItThrowsTooManyMatchingInstances()
    {
        var dialogHost = CreateElement();
        var secondInstance = new DialogHost();
        try
        {
            secondInstance.ApplyDefaultStyle();
            secondInstance.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
            var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
            await Assert.That(ex.Message).IsEqualTo("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.");
        }
        finally
        {
            secondInstance.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
        }
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogThatIsNotOpenItThrowsDialogNotOpen()
    {
        CreateElement(); // ensure at least one DialogHost exists
        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
        await Assert.That(ex.Message).IsEqualTo("DialogHost is not open.");
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogWithParameterItPassesParameterToHandlers()
    {
        var dialogHost = CreateElement();
        object parameter = Guid.NewGuid();
        object? closingParameter = null;
        object? closedParameter = null;
        dialogHost.DialogClosing += DialogClosing;
        dialogHost.DialogClosed += DialogClosed;
        dialogHost.IsOpen = true;

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

    [Test]
    public async Task WhenOpenDialogsAreOpenIsExist()
    {
        var dialogHost = CreateElement();
        object id = Guid.NewGuid();
        dialogHost.Identifier = id;
        bool isOpen = false;
        _ = dialogHost.ShowDialog("Content", new DialogOpenedEventHandler((sender, arg) =>
        {
            isOpen = DialogHost.IsDialogOpen(id);
        }));
        await Assert.That(isOpen).IsTrue();
        DialogHost.Close(id);
        await Assert.That(DialogHost.IsDialogOpen(id)).IsFalse();
    }

    [Test]
    [Description("Issue 2262")]
    public async Task WhenOnlySingleDialogHostIdentifierIsNullItShowsDialog()
    {
        var _ = CreateElement();

        var dialogHost2 = CreateElement();
        dialogHost2.Identifier = Guid.NewGuid();

        Task showTask = DialogHost.Show("Content");
        await Assert.That(DialogHost.IsDialogOpen(null)).IsTrue();
        await Assert.That(DialogHost.IsDialogOpen(dialogHost2.Identifier)).IsFalse();
        DialogHost.Close(null);
        await showTask;
    }

    [Test]
    public async Task GetDialogSession_ShouldAllowAccessFromMultipleUIThreads()
    {
        Dispatcher? otherUiThreadDispatcher = null;
        try
        {
            // Arrange
            DialogHost? dialogHostOnOtherUiThread = null;
            Guid dialogHostIdentifier = Guid.NewGuid();
            Guid dialogHostOnOtherUiThreadIdentifier = Guid.NewGuid();
            ManualResetEventSlim sync1 = new();

            // Load dialogHost on current UI thread
            DialogHost dialogHost = CreateElement();
            dialogHost.Identifier = dialogHostIdentifier;

            // Load dialogHostOnOtherUiThread on different UI thread
            TaskCompletionSource<object?> tcs = new();
            var thread = new Thread(() =>
            {
                try
                {
                    dialogHostOnOtherUiThread = CreateElement();
                    dialogHostOnOtherUiThread.Identifier = dialogHostOnOtherUiThreadIdentifier;
                    otherUiThreadDispatcher = Dispatcher.CurrentDispatcher;
                    sync1.Set();
                    Dispatcher.Run();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }

            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            sync1.Wait();

            await tcs.Task;
            // Act & Assert
            //await Assert.That(DialogHost.GetDialogSession(dialogHostIdentifier)).IsNull();
            await Assert.That(DialogHost.GetDialogSession(dialogHostOnOtherUiThreadIdentifier)).IsNull();
        }
        finally
        {
            // Cleanup 
            otherUiThreadDispatcher?.InvokeShutdown();
        }
    }
}
