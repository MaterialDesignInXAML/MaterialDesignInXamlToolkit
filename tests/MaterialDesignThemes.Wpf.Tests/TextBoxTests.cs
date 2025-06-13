using System.ComponentModel;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class TextBoxTests
{
    [Test]
    [Description("Issue 1301")]
    public async Task DefaultVerticalAlignment_ShouldBeStretch()
    {
        var testBox = new TextBox();
        testBox.ApplyDefaultStyle();

        await Assert.That(testBox.VerticalAlignment).IsEqualTo(VerticalAlignment.Stretch);
    }

    [Test]
    [Description("Issue 2556")]
    public async Task DefaultVerticalContentAlignment_ShouldBeStretch()
    {
        //The default was initially set to Top from issue 1301
        //However because TextBox contains a ScrollViewer this pushes
        //the horizontal scroll bar up by default, which is different
        //than the default WPF behavior.
        var textBox = new TextBox();
        textBox.ApplyDefaultStyle();

        await Assert.That(textBox.VerticalContentAlignment).IsEqualTo(VerticalAlignment.Stretch);
    }
}
