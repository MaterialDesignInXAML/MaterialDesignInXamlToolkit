using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class SnackbarMessageQueueTests
    {
        #region Fields & Constructor
        private readonly SnackbarMessageQueue snackbarMessageQueue;

        public SnackbarMessageQueueTests()
        {
             snackbarMessageQueue = new SnackbarMessageQueue();
        }
        #endregion

        #region StaFacts
        [StaFact]
        [Description("Ensures that GetSnackbarMessage behaves correctly if the queue is empty")]
        public void GetSnackbarMessageEmptyQueue()
        {
            var getMessage = snackbarMessageQueue.GetType().GetMethod("GetSnackbarMessage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var ret = getMessage.Invoke(snackbarMessageQueue, null);

            Assert.Null(ret);
        }

        [StaFact]
        [Description("Ensures that GetSnackbarMessage raises an exception on null values")]
        public void GetSnackbarMessageNullValues()
        {
            Assert.Throws<ArgumentNullException>(() => snackbarMessageQueue.Enqueue(null));
            Assert.Throws<ArgumentNullException>(() => snackbarMessageQueue.Enqueue("", null, null));
            Assert.Throws<ArgumentNullException>(() => snackbarMessageQueue.Enqueue(null, "", null));
            Assert.Throws<ArgumentNullException>(() => snackbarMessageQueue.Enqueue(null, null, new Action(() => { })));
        }

        [StaFact]
        [Description("Ensures that GetSnackbaMessage behaves correctly if the queue should discard duplicate items")]
        public void GetSnackbarMessageDiscardDuplicatesQueue()
        {
            snackbarMessageQueue.DiscardDuplicates = true;

            var firstItem = new object[] { "String & Action content", "Action content" };
            var secondItem = new object[] { "String & Action content", "Action content" };
            var thirdItem = new object[] { "Different String & Action content", "Action content" };

            snackbarMessageQueue.Enqueue(firstItem[0], firstItem[1], new Action(() => { }));
            snackbarMessageQueue.Enqueue(secondItem[0], secondItem[1], new Action(() => { }));
            snackbarMessageQueue.Enqueue(thirdItem[0], thirdItem[1], new Action(() => { }));

            var getMessage = snackbarMessageQueue.GetType().GetMethod("GetSnackbarMessage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var ret = getMessage.Invoke(snackbarMessageQueue, null) as SnackbarMessageQueueItem;

            ret.LastShownAt = DateTime.Now;

            var lastMessage = snackbarMessageQueue.GetType().GetField("_latestShownMessage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            lastMessage.SetValue(snackbarMessageQueue, ret);

            Assert.Equal(firstItem, new object[] { ret.Content, ret.ActionContent });

            ret = getMessage.Invoke(snackbarMessageQueue, null) as SnackbarMessageQueueItem;

            Assert.Null(ret);

            ret = getMessage.Invoke(snackbarMessageQueue, null) as SnackbarMessageQueueItem;

            Assert.Equal(thirdItem, new object[] { ret.Content, ret.ActionContent });
        }
        #endregion

        #region StaTheories
        private class SnackbarMessageQueueSimpleTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "String & Action content", "Action content", new object[] { "String & Action content", "Action content" } };
                yield return new object[] { "Different String & Action content", "Action content", new object[] { "Different String & Action content", "Action content" } };
                yield return new object[] { "Different String & Action content", "Action content", new object[] { "Different String & Action content", "Action content" } };
                yield return new object[] { "", "", new object[] { "", "" } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [StaTheory]
        [ClassData(typeof(SnackbarMessageQueueSimpleTestData))]
        [Description("Ensures that GetSnackbaMessage behaves correctly if the queue simply outputs items")]
        public void GetSnackbarMessageSimpleQueue(object content, object actionContent, object[] expected)
        {
            snackbarMessageQueue.Enqueue(content, actionContent, new Action(() => { }));
            var getMessage = snackbarMessageQueue.GetType().GetMethod("GetSnackbarMessage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var ret = getMessage.Invoke(snackbarMessageQueue, null) as SnackbarMessageQueueItem;

            var res = new object[] { ret.Content, ret.ActionContent };

            Assert.Equal(res, expected);
        }
        #endregion
    }
}