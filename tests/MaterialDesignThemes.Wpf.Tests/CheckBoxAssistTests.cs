namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class CheckBoxAssistTests
{
    [Test, STAThreadExecutor]
    public async Task TestCheckBoxSizeProperty()
    {
        CheckBox testElement = new();

        // Assert defaults
        await Assert.That(CheckBoxAssist.CheckBoxSizeProperty.Name).IsEqualTo("CheckBoxSize");
        await Assert.That(CheckBoxAssist.GetCheckBoxSize(testElement)).IsEqualTo(18.0);

        // Assert setting works
        CheckBoxAssist.SetCheckBoxSize(testElement, 27.1);
        await Assert.That(CheckBoxAssist.GetCheckBoxSize(testElement)).IsEqualTo(27.1);
    }
}
