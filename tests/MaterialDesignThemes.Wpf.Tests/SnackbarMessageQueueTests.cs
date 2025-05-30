using System.ComponentModel;
using System.Windows.Threading;

using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public sealed class SnackbarMessageQueueTests : IDisposable
{
    private readonly SnackbarMessageQueue _snackbarMessageQueue;
    private readonly Dispatcher _dispatcher;
    private bool _isDisposed;

    public SnackbarMessageQueueTests()
    {
        _dispatcher = Dispatcher.CurrentDispatcher;
        _snackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3), _dispatcher);
    }

    [Test, STAThreadExecutor]
    [Description("Ensures that GetSnackbarMessage raises an exception on null values")]
    public async Task GetSnackbarMessageNullValues()
    {
        await _ = Assert.That(() => _snackbarMessageQueue.Enqueue(null!)).ThrowsExactly<ArgumentNullException>();
        await _ = Assert.That(() => _snackbarMessageQueue.Enqueue("", null, null)).ThrowsExactly<ArgumentNullException>();
        _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, "", null));
        _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, null, new Action(() => { })));
    }

    [Test, STAThreadExecutor]
    [Description("Ensures that GetSnackbarMessage behaves correctly if the queue should discard duplicate items")]
    public void GetSnackbarMessageDiscardDuplicatesQueue()
    {
        _snackbarMessageQueue.DiscardDuplicates = true;

        var firstItem = new object[] { "String & Action content", "Action content" };
        var secondItem = new object[] { "String & Action content", "Action content" };
        var thirdItem = new object[] { "Different String & Action content", "Action content" };

        _snackbarMessageQueue.Enqueue(firstItem[0], firstItem[1], new Action(() => { }));
        _snackbarMessageQueue.Enqueue(secondItem[0], secondItem[1], new Action(() => { }));
        _snackbarMessageQueue.Enqueue(thirdItem[0], thirdItem[1], new Action(() => { }));

        IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;

        await Assert.That(messages.Count).IsEqualTo(2);

        await Assert.That(messages[0].Content).IsEqualTo("String & Action content");
        await Assert.That(messages[0].ActionContent).IsEqualTo("Action content");
        await Assert.That(messages[1].Content).IsEqualTo("Different String & Action content");
        await Assert.That(messages[1].ActionContent).IsEqualTo("Action content");
    }

    [StaTheory]
    [Description("Ensures that GetSnackbarMessage behaves correctly if the queue simply outputs items")]
    [Arguments("String & Action content", "Action content")]
    [Arguments("Different String & Action content", "Action content")]
    [Arguments("", "")]
    public void GetSnackbarMessageSimpleQueue(object content, object actionContent)
    {
        _snackbarMessageQueue.DiscardDuplicates = false;

        _snackbarMessageQueue.Enqueue(content, actionContent, new Action(() => { }));

        IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;

        Assert.Single(messages);

        await Assert.That(messages[0].Content).IsEqualTo(content);
        await Assert.That(messages[0].ActionContent).IsEqualTo(actionContent);
    }

    [Test]
    [Description("Pull Request 2367")]
    public void Enqueue_ProperlySetsPromote()
    {
        _snackbarMessageQueue.Enqueue("Content", "Action Content", actionHandler: null, promote: true);

        IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;
        Assert.Single(messages);
        await Assert.That(messages[0].Content).IsEqualTo("Content");
        await Assert.That(messages[0].ActionContent).IsEqualTo("Action Content");
        Assert.True(messages[0].IsPromoted);
    }

    private void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _snackbarMessageQueue.Dispose();
            }

            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
