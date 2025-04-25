
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class CheckBoxAssistTests
{
    private readonly CheckBox _testElement;

    public CheckBoxAssistTests()
    {
        _testElement = new CheckBox();
    }

    [Test, STAThreadExecutor]
    public async Task TestCheckBoxSizeProperty()
    {
        // Assert defaults
        await Assert.That(CheckBoxAssist.CheckBoxSizeProperty.Name).IsEqualTo("CheckBoxSize");
        await Assert.That(CheckBoxAssist.GetCheckBoxSize(_testElement)).IsEqualTo(18.0);

        // Assert setting works
        CheckBoxAssist.SetCheckBoxSize(_testElement, 27.1);
        await Assert.That(CheckBoxAssist.GetCheckBoxSize(_testElement)).IsEqualTo(27.1);
    }
}
