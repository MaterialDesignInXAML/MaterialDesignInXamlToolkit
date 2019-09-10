using System;
using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class CustomColorThemeTests
    {
        [Theory]
        [MemberData(nameof(GetThemeValues))]
        public void WhenValueIsMissingThemeIsNotSet(BaseTheme? baseTheme, Color? primaryColor, Color? secondaryColor)
        {
            //Arrange
            var bundledTheme = new CustomColorTheme();

            //Act
            bundledTheme.BaseTheme = baseTheme;
            bundledTheme.PrimaryColor = primaryColor;
            bundledTheme.SecondaryColor = secondaryColor;

            //Assert
            Assert.Throws<InvalidOperationException>(() => bundledTheme.GetTheme());
        }

        public static IEnumerable<object[]> GetThemeValues()
        {
            yield return new object[] { null, null, null };
            yield return new object[] { BaseTheme.Light, null, null };
            yield return new object[] { BaseTheme.Inherit, null, null };
            yield return new object[] { null, Colors.Blue, null };
            yield return new object[] { BaseTheme.Light, Colors.Blue, null };
            yield return new object[] { BaseTheme.Inherit, Colors.Blue, null };
            yield return new object[] { null, null, Colors.Blue };
            yield return new object[] { BaseTheme.Light, null, Colors.Blue };
            yield return new object[] { BaseTheme.Inherit, null, Colors.Blue };
        }

        [Fact]
        public void WhenAllValuesAreSetThemeIsSet()
        {
            //Arrange
            var bundledTheme = new CustomColorTheme();

            //Act
            bundledTheme.BaseTheme = BaseTheme.Light;
            bundledTheme.PrimaryColor = Colors.Fuchsia;
            bundledTheme.SecondaryColor = Colors.Lime;

            //Assert
            ITheme theme = bundledTheme.GetTheme();
            Assert.Equal(Colors.Fuchsia, theme.PrimaryMid.Color);
            Assert.Equal(Colors.Lime, theme.SecondaryMid.Color);
            Assert.Equal(Theme.Light.MaterialDesignBody, theme.Body);
        }
    }
}