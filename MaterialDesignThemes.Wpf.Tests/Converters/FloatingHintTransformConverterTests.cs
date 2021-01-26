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
        public static IEnumerable<object?[]> InvalidParameters =>
            new[]
            {
                new object?[] {null, null, null, null},
                new object?[] {1.0, null, null, null},
                new object?[] {null, 1.0, null, null},
                new object?[] {null, null, 1.0, null},
                new object?[] {null, null, null, new Point()},
                new object?[] {1.0, DependencyProperty.UnsetValue, DependencyProperty.UnsetValue, DependencyProperty.UnsetValue},
                new object?[] {DependencyProperty.UnsetValue, 1.0, DependencyProperty.UnsetValue, DependencyProperty.UnsetValue},
                new object?[] {DependencyProperty.UnsetValue, DependencyProperty.UnsetValue, 1.0, DependencyProperty.UnsetValue},
                new object?[] {DependencyProperty.UnsetValue, DependencyProperty.UnsetValue, DependencyProperty.UnsetValue, new Point() },
            };

        [StaTheory]
        [MemberData(nameof(InvalidParameters))]
        public void WhenParametersAreNotSetItReturnsIdentity(object? scale, object? lower, object? upper, object? offset)
        {
            var converter = new FloatingHintTransformConverter();

            var result = converter.Convert(new[] { scale, lower, upper, offset }, 
                typeof(Transform), null, CultureInfo.CurrentUICulture);

            Assert.Equal(Transform.Identity, result);
        }

        [StaTheory]
        [InlineData(2.0, 1.5, 3.0, 3.0, 4.0)]
        [InlineData(1.5, 2.0, 3.0, 2.0, 3.0)]
        public void WhenParametersAreSpecifiedItReturnsTransforms(double scale, double lower, double upper, double x, double y)
        {
            var converter = new FloatingHintTransformConverter();

            var result = (TransformGroup?)converter.Convert(new object?[] { scale, lower, upper, new Point(x, y) }, typeof(Transform), null, CultureInfo.CurrentUICulture);

            Assert.NotNull(result);
            var scaleTransform = (ScaleTransform)result!.Children[0];
            var translateTransform = (TranslateTransform)result.Children[1];

            Assert.Equal(upper + (lower - upper) * scale, scaleTransform.ScaleX);
            Assert.Equal(upper + (lower - upper) * scale, scaleTransform.ScaleY);

            Assert.Equal(scale * x, translateTransform.X);
            Assert.Equal(scale * y, translateTransform.Y);
        }
    }


}
