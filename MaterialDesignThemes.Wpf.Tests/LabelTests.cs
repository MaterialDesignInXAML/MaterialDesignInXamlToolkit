using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class LabelTests
    {
        [StaFact]
        [Description("Issue 1301")]
        public void DefaultVerticalAlignment_ShouldBeStretch()
        {
            var label = new Label();
            label.ApplyDefaultStyle();

            Assert.Equal(VerticalAlignment.Stretch, label.VerticalAlignment);
        }

        [StaFact]
        [Description("Issue 1301")]
        public void DefaultVerticalContentAlignment_ShouldBeTop()
        {
            var label = new Label();
            label.ApplyDefaultStyle();

            Assert.Equal(VerticalAlignment.Top, label.VerticalContentAlignment);
        }
    }
}