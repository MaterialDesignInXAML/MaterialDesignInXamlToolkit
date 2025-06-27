namespace MaterialDesignThemes.Wpf.Tests;

public class SnackbarMessageQueueItemTests
{
    [Test]
    public async Task IsDuplicate_ThrowsOnNullArgument()
    {
        SnackbarMessageQueueItem item = CreateItem();
        var ex = await Assert.That(() => item.IsDuplicate(null!)).ThrowsExactly<ArgumentNullException>();
        await Assert.That(ex?.ParamName).IsEqualTo("value");
    }

    [Test]
    public async Task IsDuplicate_WithDuplicateItems_ItReturnsTrue()
    {
        SnackbarMessageQueueItem item = CreateItem();
        SnackbarMessageQueueItem other = CreateItem();

        await Assert.That(item.IsDuplicate(other)).IsTrue();
    }

    [Test]
    public async Task IsDuplicate_AlwaysShowIsTrue_ItReturnsFalse()
    {
        SnackbarMessageQueueItem item = CreateItem(alwaysShow: true);
        SnackbarMessageQueueItem other = CreateItem();

        await Assert.That(item.IsDuplicate(other)).IsFalse();
    }

    [Test]
    public async Task IsDuplicate_WithDifferentContent_ItReturnsFalse()
    {
        SnackbarMessageQueueItem item = CreateItem();
        SnackbarMessageQueueItem other = CreateItem(content: Guid.NewGuid().ToString());

        await Assert.That(item.IsDuplicate(other)).IsFalse();
    }

    [Test]
    public async Task IsDuplicate_WithDifferentActionContent_ItReturnsFalse()
    {
        SnackbarMessageQueueItem item = CreateItem();
        SnackbarMessageQueueItem other = CreateItem(actionContent: Guid.NewGuid().ToString());

        await Assert.That(item.IsDuplicate(other)).IsFalse();
    }

    private static SnackbarMessageQueueItem CreateItem(
        string content = "Content",
        string? actionContent = null,
        bool alwaysShow = false)
        => new(content, TimeSpan.Zero, actionContent: actionContent, alwaysShow: alwaysShow);
}
