using System.ComponentModel;

namespace MaterialDesignThemes.Wpf.Tests;

public class LabelTests
{
    [Test, STAThreadExecutor]
    [Description("Issue 1301")]
    public async Task DefaultVerticalAlignment_ShouldBeStretch()
    {
        var label = new Label();
        label.ApplyDefaultStyle();

        await Assert.That(label.VerticalAlignment).IsEqualTo(VerticalAlignment.Stretch);
    }

    [Test, STAThreadExecutor]
    [Description("Issue 1301")]
    public async Task DefaultVerticalContentAlignment_ShouldBeTop()
    {
        var label = new Label();
        label.ApplyDefaultStyle();

        await Assert.That(label.VerticalContentAlignment).IsEqualTo(VerticalAlignment.Top);
    }
}
