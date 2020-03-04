using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Converters;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters
{
    public class FloatingHintTransformConverterTests
    {
        public static IEnumerable<object[]> InvalidParameters =>
            new[] 
            {
                new object[] {null, null},
                new object[] {1.0, null},
                new object[] {null, new Point()},
                new object[] {DependencyProperty.UnsetValue, new Point()},
                new object[] {1.0, DependencyProperty.UnsetValue},
            };

        [StaTheory]
        [MemberData(nameof(InvalidParameters))]
        public void WhenParametersAreNotSetItReturnsIdentity(object scale, object offset)
        {
            var converter = new FloatingHintTransformConverter();

            var result = converter.Convert(new[] { scale, offset }, typeof(Transform), null, CultureInfo.CurrentUICulture);

            Assert.Equal(Transform.Identity, result);
        }

        [StaTheory]
        [InlineData(2.0, 3.0, 4.0)]
        [InlineData(1.5, 2.0, 3.0)]
        public void WhenParametersAreSpecifiedItReturnsScaleTransform(double scale, double x, double y)
        {
            var converter = new FloatingHintTransformConverter();

            var result = (TranslateTransform)converter.Convert(new object[] { scale, new Point(x, y) }, typeof(Transform), null, CultureInfo.CurrentUICulture);

            Assert.Equal(scale * x, result.X);
            Assert.Equal(scale * y, result.Y);
        }
    }

    
}
