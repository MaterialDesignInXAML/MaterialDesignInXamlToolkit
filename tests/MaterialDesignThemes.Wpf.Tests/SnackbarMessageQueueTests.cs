using System.ComponentModel;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf.Tests;

public sealed class SnackbarMessageQueueTests
{
    private readonly SnackbarMessageQueue _snackbarMessageQueue;
    private readonly Dispatcher _dispatcher;

    public SnackbarMessageQueueTests()
    {
        _dispatcher = Dispatcher.CurrentDispatcher;
        _snackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3), _dispatcher);
    }

    [After(Test)]
    public void Cleanup()
    {
        _snackbarMessageQueue.Dispose();
    }

    [Test, STAThreadExecutor]
    [Description("Ensures that GetSnackbarMessage raises an exception on null values")]
    public async Task GetSnackbarMessageNullValues()
    {
        await Assert.That(() => _snackbarMessageQueue.Enqueue(null!)).ThrowsExactly<ArgumentNullException>();
        await Assert.That(() => _snackbarMessageQueue.Enqueue("", null, null)).ThrowsExactly<ArgumentNullException>();
        _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, "", null));
        _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, null, new Action(() => { })));
    }

    [Test, STAThreadExecutor]
    [Description("Ensures that GetSnackbarMessage behaves correctly if the queue should discard duplicate items")]
    public async Task GetSnackbarMessageDiscardDuplicatesQueue()
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

    [Test, STAThreadExecutor]
    [Description("Ensures that GetSnackbarMessage behaves correctly if the queue simply outputs items")]
    [Arguments("String & Action content", "Action content")]
    [Arguments("Different String & Action content", "Action content")]
    [Arguments("", "")]
    public async Task GetSnackbarMessageSimpleQueue(object content, object actionContent)
    {
        _snackbarMessageQueue.DiscardDuplicates = false;

        _snackbarMessageQueue.Enqueue(content, actionContent, new Action(() => { }));

        IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;

        await Assert.That(messages.Count).IsEqualTo(1);

        await Assert.That(messages[0].Content).IsEqualTo(content);
        await Assert.That(messages[0].ActionContent).IsEqualTo(actionContent);
    }

    [Test]
    [Description("Pull Request 2367")]
    public async Task Enqueue_ProperlySetsPromote()
    {
        _snackbarMessageQueue.Enqueue("Content", "Action Content", actionHandler: null, promote: true);

        IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;
        await Assert.That(messages.Count).IsEqualTo(1);

        await Assert.That(messages[0].Content).IsEqualTo("Content");
        await Assert.That(messages[0].ActionContent).IsEqualTo("Action Content");
        await Assert.That(messages[0].IsPromoted).IsTrue();
    }

}
