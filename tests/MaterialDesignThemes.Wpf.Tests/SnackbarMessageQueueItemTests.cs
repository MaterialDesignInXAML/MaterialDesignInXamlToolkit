
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class SnackbarMessageQueueItemTests
{
    [Test]
    public async Task IsDuplicate_ThrowsOnNullArgument()
    {
        SnackbarMessageQueueItem item = CreateItem();
        var ex = await Assert.That(() => item.IsDuplicate(null!)).ThrowsExactly<ArgumentNullException>();
        Assert.That(ex.ParamName).IsEqualTo("value");
    }

    [Test]
    public void IsDuplicate_WithDuplicateItems_ItReturnsTrue()
    {
        SnackbarMessageQueueItem item = CreateItem();
        SnackbarMessageQueueItem other = CreateItem();

        Assert.True(item.IsDuplicate(other));
    }

    [Test]
    public void IsDuplicate_AlwaysShowIsTrue_ItReturnsFalse()
    {
        SnackbarMessageQueueItem item = CreateItem(alwaysShow: true);
        SnackbarMessageQueueItem other = CreateItem();

        Assert.False(item.IsDuplicate(other));
    }

    [Test]
    public void IsDuplicate_WithDifferentContent_ItReturnsFalse()
    {
        SnackbarMessageQueueItem item = CreateItem();
        SnackbarMessageQueueItem other = CreateItem(content: Guid.NewGuid().ToString());

        Assert.False(item.IsDuplicate(other));
    }

    [Test]
    public void IsDuplicate_WithDifferentActionContent_ItReturnsFalse()
    {
        SnackbarMessageQueueItem item = CreateItem();
        SnackbarMessageQueueItem other = CreateItem(actionContent: Guid.NewGuid().ToString());

        Assert.False(item.IsDuplicate(other));
    }

    private static SnackbarMessageQueueItem CreateItem(
        string content = "Content",
        string? actionContent = null,
        bool alwaysShow = false)
        => new(content, TimeSpan.Zero, actionContent: actionContent, alwaysShow: alwaysShow);
}
