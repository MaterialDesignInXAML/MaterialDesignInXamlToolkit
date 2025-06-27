using System.ComponentModel;

namespace MaterialDesignThemes.Wpf.Tests;

public sealed class ControlHost<T>(Action<T> cleanup) : IDisposable
        where T : Control, new()
{
    private bool disposedValue;

    public T Content { get; } = new T();
    private Action<T> Cleanup { get; } = cleanup;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects)
                Cleanup?.Invoke(Content);
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public static implicit operator T(ControlHost<T> host)
    {
        return host.Content;
    }
}

[NotInParallel(nameof(DialogHost))]
[TestExecutor<STAThreadExecutor>]
public class DialogHostTests
{
    private static ControlHost<DialogHost> CreateElement()
    {
        ControlHost<DialogHost> host = new(x => x.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent)));
        DialogHost dialogHost = host;
        dialogHost.ApplyDefaultStyle();
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
        return host;
    }

    [Test]
    public async Task CanOpenAndCloseDialogWithIsOpen()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;

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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
        var id = Guid.NewGuid();
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        await Assert.That(ex?.Message).IsEqualTo("No loaded DialogHost instances.");
    }

    [Test]
    public async Task WhenNoDialogsMatchIdentifierItThrows()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
        var id = Guid.NewGuid();

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => DialogHost.Show("Content", id));

        await Assert.That(ex?.Message).IsEqualTo($"No loaded DialogHost have an {nameof(DialogHost.Identifier)} property matching dialogIdentifier ('{id}') argument.");
    }

    [Test]
    public async Task WhenMultipleDialogHostsHaveTheSameIdentifierItThrows()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement(); // ensure at least one DialogHost exists
        DialogHost dialogHost = host.Content;
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => DialogHost.Show(null!));

        await Assert.That(ex?.ParamName).IsEqualTo("content");
    }

    [Test]
    [Description("Issue 1212")]
    public async Task WhenContentIsUpdatedClosingEventHandlerIsInvoked()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
        dialogHost.IsOpen = true;
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        await Assert.That(dialogHost.IsOpen).IsTrue();
    }

    [Test]
    [Description("Issue 1750")]
    public async Task WhenSettingIsOpenToFalseItReturnsClosingParameterToShow()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;

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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;
        dialogHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
        await Assert.That(ex.Message).IsEqualTo("No loaded DialogHost instances.");
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogWithInvalidIdentifierItThrowsWhenNoMatchingInstances()
    {
        using var host = CreateElement(); // ensure at least one DialogHost exists
        DialogHost dialogHost = host.Content;
        object id = Guid.NewGuid();
        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(id));
        await Assert.That(ex.Message).IsEqualTo($"No loaded DialogHost have an Identifier property matching dialogIdentifier ('{id}') argument.");
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogWithMultipleDialogHostsItThrowsTooManyMatchingInstances()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;

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
        using var host = CreateElement(); // ensure at least one DialogHost exists
        var ex = Assert.Throws<InvalidOperationException>(() => DialogHost.Close(null!));
        await Assert.That(ex.Message).IsEqualTo("DialogHost is not open.");
    }

    [Test]
    [Description("Pull Request 2029")]
    public async Task WhenClosingDialogWithParameterItPassesParameterToHandlers()
    {
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;

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
        using var host = CreateElement();
        DialogHost dialogHost = host.Content;

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
        using var _ = CreateElement();

        using var host = CreateElement();
        DialogHost dialogHost2 = host.Content;
        dialogHost2.Identifier = Guid.NewGuid();

        Task showTask = DialogHost.Show("Content");
        await Assert.That(DialogHost.IsDialogOpen(null)).IsTrue();
        await Assert.That(DialogHost.IsDialogOpen(dialogHost2.Identifier)).IsFalse();
        DialogHost.Close(null);
        await showTask;
    }

    //[Test]
    //[Skip("This has not been working since moving to TUnit. There is a deadlock")]
    //public async Task GetDialogSession_ShouldAllowAccessFromMultipleUIThreads()
    //{
    //    Dispatcher? otherUiThreadDispatcher = null;
    //    try
    //    {
    //        // Arrange
    //        DialogHost? dialogHostOnOtherUiThread = null;
    //        Guid dialogHostIdentifier = Guid.NewGuid();
    //        Guid dialogHostOnOtherUiThreadIdentifier = Guid.NewGuid();
    //        ManualResetEventSlim sync1 = new();

    //        // Load dialogHost on current UI thread
    //        DialogHost dialogHost = CreateElement();
    //        dialogHost.Identifier = dialogHostIdentifier;

    //        // Load dialogHostOnOtherUiThread on different UI thread
    //        TaskCompletionSource<object?> tcs = new();
    //        var thread = new Thread(() =>
    //        {
    //            try
    //            {
    //                dialogHostOnOtherUiThread = CreateElement();
    //                dialogHostOnOtherUiThread.Identifier = dialogHostOnOtherUiThreadIdentifier;
    //                otherUiThreadDispatcher = Dispatcher.CurrentDispatcher;
    //                sync1.Set();
    //                tcs.SetResult(null);
    //                Dispatcher.Run();
    //            }
    //            catch (Exception ex)
    //            {
    //                tcs.SetException(ex);
    //            }

    //        });
    //        thread.SetApartmentState(ApartmentState.STA);
    //        thread.Start();
    //        sync1.Wait();

    //        await tcs.Task;
    //        // Act & Assert

    //        await Assert.That(DialogHost.GetDialogSession(dialogHostIdentifier)).IsNull();
    //        await Assert.That(DialogHost.GetDialogSession(dialogHostOnOtherUiThreadIdentifier)).IsNull();
    //    }
    //    finally
    //    {
    //        // Cleanup 
    //        otherUiThreadDispatcher?.InvokeShutdown();
    //    }
    //}
}
