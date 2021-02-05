using System;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class SnackbarMessageQueueItemTests
    {
        [Fact]
        public void IsDuplicate_ThrowsOnNullArgument()
        {
            SnackbarMessageQueueItem item = CreateItem();
            var ex = Assert.Throws<ArgumentNullException>(() => item.IsDuplicate(null!));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void IsDuplicate_WithDuplicateItems_ItReturnsTrue()
        {
            SnackbarMessageQueueItem item = CreateItem();
            SnackbarMessageQueueItem other = CreateItem();

            Assert.True(item.IsDuplicate(other));
        }

        [Fact]
        public void IsDuplicate_AlwaysShowIsTrue_ItReturnsFalse()
        {
            SnackbarMessageQueueItem item = CreateItem(alwaysShow:true);
            SnackbarMessageQueueItem other = CreateItem();

            Assert.False(item.IsDuplicate(other));
        }

        [Fact]
        public void IsDuplicate_WithDifferentContent_ItReturnsFalse()
        {
            SnackbarMessageQueueItem item = CreateItem();
            SnackbarMessageQueueItem other = CreateItem(content:Guid.NewGuid().ToString());

            Assert.False(item.IsDuplicate(other));
        }

        [Fact]
        public void IsDuplicate_WithDifferentActionContent_ItReturnsFalse()
        {
            SnackbarMessageQueueItem item = CreateItem();
            SnackbarMessageQueueItem other = CreateItem(actionContent:Guid.NewGuid().ToString());

            Assert.False(item.IsDuplicate(other));
        }

        private static SnackbarMessageQueueItem CreateItem(
            string content = "Content",
            string? actionContent = null,
            bool alwaysShow = false)
            => new(content, TimeSpan.Zero, actionContent: actionContent, alwaysShow: alwaysShow);
    }
}
