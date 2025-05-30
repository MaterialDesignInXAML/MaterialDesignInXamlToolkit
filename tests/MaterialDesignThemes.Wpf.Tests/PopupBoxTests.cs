using System.ComponentModel;

using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class PopupBoxTests
{
    [Test, STAThreadExecutor]
    [Description("Issue 1091")]
    public async Task ToggleButtonInheritsTabIndex()
    {
        var popupBox = new PopupBox { TabIndex = 3 };
        popupBox.ApplyDefaultStyle();

        ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

        await Assert.That(togglePart.TabIndex).IsEqualTo(3);
    }

    [Test, STAThreadExecutor]
    [Description("Issue 1231")]
    public async Task ToggleButtonInheritsIsTabStopWhenFalse()
    {
        var popupBox = new PopupBox { IsTabStop = false };
        popupBox.ApplyDefaultStyle();

        ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

        await Assert.That(togglePart.IsTabStop).IsFalse();
    }

    [Test, STAThreadExecutor]
    [Description("Issue 1231")]
    public async Task ToggleButtonInheritsIsTabStopWhenTrue()
    {
        var popupBox = new PopupBox { IsTabStop = true };
        popupBox.ApplyDefaultStyle();

        ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

        await Assert.That(togglePart.IsTabStop).IsTrue();
    }
}
