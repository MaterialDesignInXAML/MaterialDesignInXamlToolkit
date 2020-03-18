using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialDesignThemes.Wpf.Converters;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters
{

    public class ExpanderDirectinConverterTests
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, typeof(int), "1,2,3,4")]
        [InlineData(ExpandDirection.Left, null, "1,2,3,4")]
        [InlineData(ExpandDirection.Left, typeof(int), null)]
        [InlineData(ExpandDirection.Left, typeof(int), "")]
        [InlineData(ExpandDirection.Left, typeof(int), "1,2")]
        public void WhenValuesInvalid_ItDoesNothing(object value, Type targetType, object parameter)
        {
            var converter = new ExpanderDirectionConverter();

            Assert.Equal(Binding.DoNothing, converter.Convert(value, targetType, parameter, CultureInfo.CurrentUICulture));
        }

        [Fact]
        public void WhenExpandDirectionInvalid_ItThrowsInvalidOperationException()
        {
            var converter = new ExpanderDirectionConverter();

            var invalid = (ExpandDirection)(Enum.GetValues(typeof(ExpandDirection)).Cast<int>().Max() + 1);

            Assert.ThrowsAny<InvalidOperationException>(
                () => converter.Convert(invalid, typeof(int), "1,2,3,4", CultureInfo.CurrentUICulture));
        }

        [Theory]
        [InlineData(ExpandDirection.Left, "1,2,3,4", 1)]
        [InlineData(ExpandDirection.Up, "1,2,3,4", 2)]
        [InlineData(ExpandDirection.Right, "1,2,3,4", 3)]
        [InlineData(ExpandDirection.Down, "1,2,3,4", 4)]
        public void WhenValuesAreValid_ItParsesExpected(ExpandDirection direction, string parameter, int expected)
        {
            var converter = new ExpanderDirectionConverter();

            Assert.Equal(expected, converter.Convert(direction, typeof(int), parameter, CultureInfo.CurrentUICulture));
        }

        [Theory]
        [InlineData(ExpandDirection.Left, "1,2,3,4", "1")]
        [InlineData(ExpandDirection.Up, "1,2,3,4", "2")]
        [InlineData(ExpandDirection.Right, "1,2,3,4", "3")]
        [InlineData(ExpandDirection.Down, "1,2,3,4", "4")]
        public void WhenTargetTypeIsObject_ItReturnsStringValue(ExpandDirection direction, string parameter, string expected)
        {
            var converter = new ExpanderDirectionConverter();

            Assert.Equal(expected, converter.Convert(direction, typeof(object), parameter, CultureInfo.CurrentUICulture));
        }
    }
}
