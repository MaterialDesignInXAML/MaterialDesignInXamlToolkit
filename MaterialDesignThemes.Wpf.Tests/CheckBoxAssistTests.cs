using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class CheckBoxAssistTests
    {
        private readonly CheckBox _testElement;

        public CheckBoxAssistTests()
        {
            _testElement = new CheckBox();
        }

        [StaFact]
        public void TestCheckBoxSizeProperty()
        {
            // Assert defaults
            Assert.Equal("CheckBoxSize", CheckBoxAssist.CheckBoxSizeProperty.Name);
            Assert.Equal(18.0, CheckBoxAssist.GetCheckBoxSize(_testElement));

            // Assert setting works
            CheckBoxAssist.SetCheckBoxSize(_testElement, 27.1);
            Assert.Equal(27.1, CheckBoxAssist.GetCheckBoxSize(_testElement));
        }
    }
}