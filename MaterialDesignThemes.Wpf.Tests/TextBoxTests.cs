using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class TextBoxTests
    {
        [StaFact]
        [Description("Issue 1301")]
        public void DefaultVerticalAlignment_ShouldBeStretch()
        {
            var testBox = new TextBox();
            testBox.ApplyDefaultStyle();

            Assert.Equal(VerticalAlignment.Stretch, testBox.VerticalAlignment);
        }

        [StaFact]
        [Description("Issue 1301")]
        public void DefaultVerticalContentAlignment_ShouldBeTop()
        {
            var textBox = new TextBox();
            textBox.ApplyDefaultStyle();

            Assert.Equal(VerticalAlignment.Top, textBox.VerticalContentAlignment);
        }
    }
}