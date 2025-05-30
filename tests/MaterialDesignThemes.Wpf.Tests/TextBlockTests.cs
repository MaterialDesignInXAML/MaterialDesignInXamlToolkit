using System.ComponentModel;

namespace MaterialDesignThemes.Wpf.Tests;

public class TextBlockTests
{
    [Test, STAThreadExecutor]
    [Description("Issue 1301")]
    public async Task DefaultVerticalAlignment_ShouldBeStretch()
    {
        foreach (var styleKey in MdixHelper.GetStyleKeysFor<TextBlock>())
        {
            var textBlock = new TextBlock();
            await textBlock.ApplyStyle(styleKey, false);

            await Assert.That(textBlock.VerticalAlignment).IsEqualTo(VerticalAlignment.Stretch);
        }
    }
}
