using System.Windows.Controls;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class ButtonProgressAssistTests
    {
        private readonly Button _testElement;

        public ButtonProgressAssistTests()
        {
            _testElement = new Button();
        }

        [StaFact]
        public void TestMinimumProperty()
        {
            // Assert defaults
            Assert.Equal("Minimum", ButtonProgressAssist.MinimumProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetMinimum(_testElement));
            
            // Assert setting works
            ButtonProgressAssist.SetMinimum(_testElement, 133.14);
            Assert.Equal(133.14, ButtonProgressAssist.GetMinimum(_testElement));
        }

        [StaFact]
        public void TestMaximumProperty()
        {
            // Assert defaults
            Assert.Equal("Maximum", ButtonProgressAssist.MaximumProperty.Name);
            Assert.Equal(100.0, ButtonProgressAssist.GetMaximum(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetMaximum(_testElement, 39.56);
            Assert.Equal(39.56, ButtonProgressAssist.GetMaximum(_testElement));
        }

        [StaFact]
        public void TestValueProperty()
        {
            // Assert defaults
            Assert.Equal("Value", ButtonProgressAssist.ValueProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetValue(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetValue(_testElement, 99.1);
            Assert.Equal(99.1, ButtonProgressAssist.GetValue(_testElement));
        }

        [StaFact]
        public void TestIsIndeterminateProperty()
        {
            // Assert defaults
            Assert.Equal("IsIndeterminate", ButtonProgressAssist.IsIndeterminateProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIsIndeterminate(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetIsIndeterminate(_testElement, false);
            Assert.False(ButtonProgressAssist.GetIsIndeterminate(_testElement));
        }

        [StaFact]
        public void TestIndicatorForegroundProperty()
        {
            // Assert defaults
            Assert.Equal("IndicatorForeground", ButtonProgressAssist.IndicatorForegroundProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIndicatorForeground(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetIndicatorForeground(_testElement, Brushes.LightBlue);
            Assert.Equal(Brushes.LightBlue, ButtonProgressAssist.GetIndicatorForeground(_testElement));
        }

        [StaFact]
        public void TestIndicatorBackgroundProperty()
        {
            // Assert defaults
            Assert.Equal("IndicatorBackground", ButtonProgressAssist.IndicatorBackgroundProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIndicatorBackground(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetIndicatorBackground(_testElement, Brushes.DarkGoldenrod);
            Assert.Equal(Brushes.DarkGoldenrod, ButtonProgressAssist.GetIndicatorBackground(_testElement));
        }

        [StaFact]
        public void TestIsIndicatorVisibleProperty()
        {
            // Assert defaults
            Assert.Equal("IsIndicatorVisible", ButtonProgressAssist.IsIndicatorVisibleProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetIsIndicatorVisible(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetIsIndicatorVisible(_testElement, true);
            Assert.True(ButtonProgressAssist.GetIsIndicatorVisible(_testElement));
        }

        [StaFact]
        public void TestOpacityProperty()
        {
            // Assert defaults
            Assert.Equal("Opacity", ButtonProgressAssist.OpacityProperty.Name);
            Assert.Equal(default, ButtonProgressAssist.GetOpacity(_testElement));

            // Assert setting works
            ButtonProgressAssist.SetOpacity(_testElement, 311);
            Assert.Equal(311, ButtonProgressAssist.GetOpacity(_testElement));
        }

    }
}