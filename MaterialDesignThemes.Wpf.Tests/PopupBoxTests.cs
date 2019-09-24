using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class PopupBoxTests
    {
        [StaFact]
        [Description("Issue 1091")]
        public void ToggleButtonInheritsTabIndex()
        {
            var popupBox = new PopupBox { TabIndex = 3 };
            popupBox.ApplyDefaultStyle();

            ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);
            
            Assert.Equal(3, togglePart.TabIndex);
        }

        [StaFact]
        [Description("Issue 1231")]
        public void ToggleButtonInheritsIsTabStopWhenFalse()
        {
            var popupBox = new PopupBox { IsTabStop = false };
            popupBox.ApplyDefaultStyle();

            ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

            Assert.False(togglePart.IsTabStop);
        }

        [StaFact]
        [Description("Issue 1231")]
        public void ToggleButtonInheritsIsTabStopWhenTrue()
        {
            var popupBox = new PopupBox { IsTabStop = true };
            popupBox.ApplyDefaultStyle();

            ToggleButton togglePart = popupBox.FindVisualChild<ToggleButton>(PopupBox.TogglePartName);

            Assert.True(togglePart.IsTabStop);
        }
    }
}