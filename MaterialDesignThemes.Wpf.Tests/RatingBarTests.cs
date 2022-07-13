using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class RatingBarTests
{
    [StaTheory]
    [InlineData(-5, 1.0)]
    [InlineData(5, 5.0)]
    [InlineData(15, 10.0)]
    [InlineData(1.2, 1.0)]
    [InlineData(1.3, 1.5)]
    [InlineData(1.7, 1.5)]
    [InlineData(1.8, 2.0)]
    [InlineData(2.2, 2.0)]
    [InlineData(2.3, 2.5)]
    public void SetValue_FractionalValuesEnabled_CoercesToCorrectMultipleAndStaysWithinBounds(double valueToSet, double expectedValue)
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, ValueIncrements = 0.5 };

        // Act
        ratingBar.Value = valueToSet;

        // Assert
        Assert.Equal(expectedValue, ratingBar.Value);
    }

    [StaTheory]
    [InlineData(-5, -5.0)]
    [InlineData(5, 5.0)]
    [InlineData(15, 15.0)]
    [InlineData(1.2, 1.2)]
    [InlineData(2.3, 2.3)]
    public void SetValue_FractionalValuesDisabled_DoesNotCoerceValue(double valueToSet, double expectedValue)
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10 };

        // Act
        ratingBar.Value = valueToSet;

        // Assert
        Assert.Equal(expectedValue, ratingBar.Value);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnOriginalBrush_WhenValueIsEqualToButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 1, buttonValue: 1);

        // Act
        var result =  converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.Equal(brush, result);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnOriginalBrush_WhenValueIsGreaterThanButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 2, buttonValue: 1);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.Equal(brush, result);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnSemiTransparentBrush_WhenValueIsLessThanButtonValueMinusOne()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 0.5, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.IsAssignableFrom<SolidColorBrush>(result);
        SolidColorBrush resultBrush = (SolidColorBrush)result!;
        Assert.Equal(RatingBar.TextBlockForegroundConverter.SemiTransparent, resultBrush.Color.A);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnHorizontalLinearGradientBrush_WhenValueIsBetweenButtonValueAndButtonValueMinusOne()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 1.5, buttonValue: 2, orientation: Orientation.Horizontal);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.IsAssignableFrom<LinearGradientBrush>(result);
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        Assert.Equal(new Point(0, 0.5), resultBrush.StartPoint);
        Assert.Equal(new Point(1, 0.5), resultBrush.EndPoint);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnVerticalLinearGradientBrush_WhenValueIsBetweenButtonValueAndButtonValueMinusOne()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 1.5, buttonValue: 2, orientation: Orientation.Vertical);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.IsAssignableFrom<LinearGradientBrush>(result);
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        Assert.Equal(new Point(0.5, 0), resultBrush.StartPoint);
        Assert.Equal(new Point(0.5, 1), resultBrush.EndPoint);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers10PercentOfButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 1.1, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.IsAssignableFrom<LinearGradientBrush>(result);
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        Assert.Equal(2, resultBrush.GradientStops.Count);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        Assert.Equal(0.1, stop1.Offset, 10);
        Assert.Equal(brush.Color, stop1.Color);
        Assert.Equal(0.1, stop2.Offset, 10);
        Assert.Equal(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent), stop2.Color);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers42PercentOfButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 1.42, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.IsAssignableFrom<LinearGradientBrush>(result);
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        Assert.Equal(2, resultBrush.GradientStops.Count);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        Assert.Equal(0.42, stop1.Offset, 10);
        Assert.Equal(brush.Color, stop1.Color);
        Assert.Equal(0.42, stop2.Offset, 10);
        Assert.Equal(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent), stop2.Color);
    }

    [Fact]
    public void TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers87PercentOfButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        IMultiValueConverter converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_Values(brush, value: 1.87, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        Assert.IsAssignableFrom<LinearGradientBrush>(result);
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        Assert.Equal(2, resultBrush.GradientStops.Count);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        Assert.Equal(0.87, stop1.Offset, 10);
        Assert.Equal(brush.Color, stop1.Color);
        Assert.Equal(0.87, stop2.Offset, 10);
        Assert.Equal(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent), stop2.Color);
    }

    private static object[] Arrange_Values(SolidColorBrush brush, double value, int buttonValue, Orientation orientation = Orientation.Horizontal) =>
        new object[] { brush, orientation, value, buttonValue };
}

internal static class ColorExtensions
{
    public static Color WithAlphaChannel(this Color color, byte alphaChannel)
        => Color.FromArgb(alphaChannel, color.R, color.G, color.B);
}