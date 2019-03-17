using System.ComponentModel;
using System.Windows;
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
    }
}