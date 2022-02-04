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
        [Description("Issue 2556")]
        public void DefaultVerticalContentAlignment_ShouldBeStretch()
        {
            //The default was initially set to Top from issue 1301
            //However because TextBox contains a ScrollViewer this pushes
            //the horizontal scroll bar up by default, which is different
            //than the default WPF behavior.
            var textBox = new TextBox();
            textBox.ApplyDefaultStyle();

            Assert.Equal(VerticalAlignment.Stretch, textBox.VerticalContentAlignment);
        }
    }
}