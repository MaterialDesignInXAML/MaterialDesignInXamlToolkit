using System.Windows.Controls;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class ButtonProgressAssistTests
    {
        private readonly Button testElement;

        public ButtonProgressAssistTests()
        {
            testElement = new Button();
        }

        [StaFact]
        public void TestMinimumProperty()
        {
            // Assert defaults
            Assert.Equal("Minimum", ButtonProgressAssist.MinimumProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetMinimum(testElement));
            
            // Assert setting works
            ButtonProgressAssist.SetMinimum(testElement, 133.14);
            Assert.Equal(133.14, ButtonProgressAssist.GetMinimum(testElement));
        }

        [StaFact]
        public void TestMaximumProperty()
        {
            // Assert defaults
            Assert.Equal("Maximum", ButtonProgressAssist.MaximumProperty.Name);
            Assert.Equal(100.0, ButtonProgressAssist.GetMaximum(testElement));

            // Assert setting works
            ButtonProgressAssist.SetMaximum(testElement, 39.56);
            Assert.Equal(39.56, ButtonProgressAssist.GetMaximum(testElement));
        }

        [StaFact]
        public void TestValueProperty()
        {
            // Assert defaults
            Assert.Equal("Value", ButtonProgressAssist.ValueProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetValue(testElement));

            // Assert setting works
            ButtonProgressAssist.SetValue(testElement, 99.1);
            Assert.Equal(99.1, ButtonProgressAssist.GetValue(testElement));
        }

        [StaFact]
        public void TestIsIndeterminateProperty()
        {
            // Assert defaults
            Assert.Equal("IsIndeterminate", ButtonProgressAssist.IsIndeterminateProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIsIndeterminate(testElement));

            // Assert setting works
            ButtonProgressAssist.SetIsIndeterminate(testElement, false);
            Assert.False(ButtonProgressAssist.GetIsIndeterminate(testElement));
        }

        [StaFact]
        public void TestIndicatorForegroundProperty()
        {
            // Assert defaults
            Assert.Equal("IndicatorForeground", ButtonProgressAssist.IndicatorForegroundProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIndicatorForeground(testElement));

            // Assert setting works
            ButtonProgressAssist.SetIndicatorForeground(testElement, Brushes.LightBlue);
            Assert.Equal(Brushes.LightBlue, ButtonProgressAssist.GetIndicatorForeground(testElement));
        }

        [StaFact]
        public void TestIndicatorBackgroundProperty()
        {
            // Assert defaults
            Assert.Equal("IndicatorBackground", ButtonProgressAssist.IndicatorBackgroundProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIndicatorBackground(testElement));

            // Assert setting works
            ButtonProgressAssist.SetIndicatorBackground(testElement, Brushes.DarkGoldenrod);
            Assert.Equal(Brushes.DarkGoldenrod, ButtonProgressAssist.GetIndicatorBackground(testElement));
        }

        [StaFact]
        public void TestIsIndicatorVisibleProperty()
        {
            // Assert defaults
            Assert.Equal("IsIndicatorVisible", ButtonProgressAssist.IsIndicatorVisibleProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIsIndicatorVisible(testElement));

            // Assert setting works
            ButtonProgressAssist.SetIsIndicatorVisible(testElement, true);
            Assert.True(ButtonProgressAssist.GetIsIndicatorVisible(testElement));
        }

        [StaFact]
        public void TestOpacityProperty()
        {
            // Assert defaults
            Assert.Equal("Opacity", ButtonProgressAssist.OpacityProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetOpacity(testElement));

            // Assert setting works
            ButtonProgressAssist.SetOpacity(testElement, 311);
            Assert.Equal(311, ButtonProgressAssist.GetOpacity(testElement));
        }

    }
}