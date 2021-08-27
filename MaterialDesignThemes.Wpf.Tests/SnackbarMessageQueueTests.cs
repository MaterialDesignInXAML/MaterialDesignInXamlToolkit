using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
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

        [StaFact]
        [Description("Ensures that GetSnackbarMessage raises an exception on null values")]
        public void GetSnackbarMessageNullValues()
        {
            _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!));
            _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue("", null, null));
            _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, "", null));
            _ = Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, null, new Action(() => { })));
        }

        [StaFact]
        [Description("Ensures that GetSnackbaMessage behaves correctly if the queue should discard duplicate items")]
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

            Assert.Equal(2, messages.Count);

            Assert.Equal("String & Action content", messages[0].Content);
            Assert.Equal("Action content", messages[0].ActionContent);
            Assert.Equal("Different String & Action content", messages[1].Content);
            Assert.Equal("Action content", messages[1].ActionContent);
        }

        [StaTheory]
        [Description("Ensures that GetSnackbaMessage behaves correctly if the queue simply outputs items")]
        [InlineData("String & Action content", "Action content")]
        [InlineData("Different String & Action content", "Action content")]
        [InlineData("", "")]
        public void GetSnackbarMessageSimpleQueue(object content, object actionContent)
        {
            _snackbarMessageQueue.DiscardDuplicates = false;

            _snackbarMessageQueue.Enqueue(content, actionContent, new Action(() => { }));

            IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;

            Assert.Equal(1, messages.Count);

            Assert.Equal(content, messages[0].Content);
            Assert.Equal(actionContent, messages[0].ActionContent);
        }

        [Fact]
        [Description("Pull Request 2367")]
        public void Enqueue_ProperlySetsPromote()
        {
            _snackbarMessageQueue.Enqueue("Content", "Action Content", actionHandler: null, promote: true);

            IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;
            Assert.Equal(1, messages.Count);
            Assert.Equal("Content", messages[0].Content);
            Assert.Equal("Action Content", messages[0].ActionContent);
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
}