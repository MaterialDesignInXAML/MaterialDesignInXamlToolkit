using System.ComponentModel;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class PopupBoxTests
{
    [Test]
    [Description("Issue 1091")]
    public async Task ToggleButtonInheritsTabIndex()
    {
        var popupBox = new PopupBox { TabIndex = 3 };
        popupBox.ApplyDefaultStyle();

        ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

        await Assert.That(togglePart.TabIndex).IsEqualTo(3);
    }

    [Test]
    [Description("Issue 1231")]
    public async Task ToggleButtonInheritsIsTabStopWhenFalse()
    {
        var popupBox = new PopupBox { IsTabStop = false };
        popupBox.ApplyDefaultStyle();

        ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

        await Assert.That(togglePart.IsTabStop).IsFalse();
    }

    [Test]
    [Description("Issue 1231")]
    public async Task ToggleButtonInheritsIsTabStopWhenTrue()
    {
        var popupBox = new PopupBox { IsTabStop = true };
        popupBox.ApplyDefaultStyle();

        ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

        await Assert.That(togglePart.IsTabStop).IsTrue();
    }
}
