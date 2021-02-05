using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class SnackbarMessageQueueTests
    {
        private readonly SnackbarMessageQueue _snackbarMessageQueue;
        private readonly Dispatcher _dispatcher;

        public SnackbarMessageQueueTests()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _snackbarMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3), _dispatcher);
        }

        [StaFact]
        [Description("Ensures that GetSnackbarMessage raises an exception on null values")]
        public void GetSnackbarMessageNullValues()
        {
            Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!));
            Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue("", null, null));
            Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, "", null));
            Assert.Throws<ArgumentNullException>(() => _snackbarMessageQueue.Enqueue(null!, null, new Action(() => { })));
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

        private class SnackbarMessageQueueSimpleTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "String & Action content", "Action content" };
                yield return new object[] { "Different String & Action content", "Action content" };
                yield return new object[] { "Different String & Action content", "Action content" };
                yield return new object[] { "", "" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [StaTheory]
        [ClassData(typeof(SnackbarMessageQueueSimpleTestData))]
        [Description("Ensures that GetSnackbaMessage behaves correctly if the queue simply outputs items")]
        public void GetSnackbarMessageSimpleQueue(object content, object actionContent)
        {
            _snackbarMessageQueue.DiscardDuplicates = false;

            _snackbarMessageQueue.Enqueue(content, actionContent, new Action(() => { }));

            IReadOnlyList<SnackbarMessageQueueItem> messages = _snackbarMessageQueue.QueuedMessages;

            Assert.Equal(1, messages.Count);

            Assert.Equal(content, messages[0].Content);
            Assert.Equal(actionContent, messages[0].ActionContent);
        }
    }
}