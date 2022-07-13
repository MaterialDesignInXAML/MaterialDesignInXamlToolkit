using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class RatingBarTests
{
    [StaFact]
    public void SetMin_ShouldCoerceToMax_WhenMinIsGreaterThanMax()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10 };

        // Act
        ratingBar.Min = 15;

        // Assert
        Assert.Equal(10, ratingBar.Min);
    }

    [StaFact]
    public void SetMin_ShouldNotCoerceValue_WhenFractionalValuesAreDisabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5};

        // Act
        ratingBar.Min = 7;

        // Assert
        Assert.Equal(5, ratingBar.Value);
    }

    [StaFact]
    public void SetMin_ShouldCoerceValue_WhenFractionalValuesAreEnabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5, ValueIncrements = 0.5 };

        // Act
        ratingBar.Min = 7;

        // Assert
        Assert.Equal(7, ratingBar.Value);
    }

    [StaFact]
    public void SetMax_ShouldNotCoerceValue_WhenFractionalValuesAreDisabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5 };

        // Act
        ratingBar.Max = 3;

        // Assert
        Assert.Equal(5, ratingBar.Value);
    }

    [StaFact]
    public void SetMax_ShouldCoerceValue_WhenFractionalValuesAreEnabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5, ValueIncrements = 0.5 };

        // Act
        ratingBar.Max = 3;

        // Assert
        Assert.Equal(3, ratingBar.Value);
    }

    [StaFact]
    public void SetMax_ShouldCoerceToMin_WhenMaxIsLessThanMin()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10 };

        // Act
        ratingBar.Max = -5;

        // Assert
        Assert.Equal(1, ratingBar.Max);
    }

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
    public void SetValue_ShouldCoerceToCorrectMultipleAndStaysWithinBounds_WhenFractionalValuesAreEnabled(double valueToSet, double expectedValue)
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
    public void SetValue_ShouldNotCoerceValue_WhenFractionalValuesAreDisabled(double valueToSet, double expectedValue)
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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1, buttonValue: 1);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 2, buttonValue: 1);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 0.5, buttonValue: 2);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.5, buttonValue: 2, orientation: Orientation.Horizontal);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.5, buttonValue: 2, orientation: Orientation.Vertical);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.1, buttonValue: 2);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.42, buttonValue: 2);

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
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.87, buttonValue: 2);

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

    private static object[] Arrange_TextBlockForegroundConverterValues(SolidColorBrush brush, double value, int buttonValue, Orientation orientation = Orientation.Horizontal) =>
        new object[] { brush, orientation, value, buttonValue };

    [Fact]
    public void PreviewIndicatorTransformXConverter_ShouldCenterPreviewIndicator_WhenFractionalValuesAreDisabledAndOrientationIsHorizontal()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Horizontal, false, 1, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(40.0, result); // 50% of 100 minus 20/2
    }

    [Fact]
    public void PreviewIndicatorTransformXConverter_ShouldOffsetPreviewIndicatorByPercentage_WhenFractionalValuesAreEnabledAndOrientationIsHorizontal()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Horizontal, true, 1.25, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(15.0, result); // 25% of 100 minus 20/2
    }

    [Fact]
    public void PreviewIndicatorTransformXConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreDisabledAndOrientationIsVertical()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Vertical, false, 1, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformXConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedValue, result); // 100% of 20 minus fixed margin
    }

    [Fact]
    public void PreviewIndicatorTransformXConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreEnabledAndOrientationIsVertical()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Vertical, true, 1.25, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformXConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedValue, result); // 100% of 20 minus fixed margin
    }

    

    private static object[] Arrange_PreviewIndicatorTransformXConverterValues(double ratingBarButtonActualWidth, double previewValueActualWidth, Orientation orientation, bool isFractionalValueEnabled, double previewValue, int buttonValue) =>
        new object[] { ratingBarButtonActualWidth, previewValueActualWidth, orientation, isFractionalValueEnabled, previewValue, buttonValue };

    [Fact]
    public void PreviewIndicatorTransformYConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreDisabledAndOrientationIsHorizontal()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Horizontal, false, 1, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformYConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedValue, result); // 100% of 20 minus fixed margin
    }

    [Fact]
    public void PreviewIndicatorTransformYConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreEnabledAndOrientationIsHorizontal()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Horizontal, true, 1.25, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformYConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedValue, result); // 100% of 20 minus fixed margin
    }

    [Fact]
    public void PreviewIndicatorTransformYConverter_ShouldCenterPreviewIndicator_WhenFractionalValuesAreDisabledAndOrientationIsVertical()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Vertical, false, 1, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(40.0, result); // 50% of 100 minus 20/2
    }

    [Fact]
    public void PreviewIndicatorTransformYConverter_ShouldPreviewIndicatorByPercentage_WhenFractionalValuesAreEnabledAndOrientationIsVertical()
    {
        // Arrange
        IMultiValueConverter converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Vertical, true, 1.25, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(15.0, result); // 25% of 100 minus 20/2
    }

    private static object[] Arrange_PreviewIndicatorTransformYConverterValues(double ratingBarButtonActualHeight, double previewValueActualHeight, Orientation orientation, bool isFractionalValueEnabled, double previewValue, int buttonValue) =>
        new object[] { ratingBarButtonActualHeight, previewValueActualHeight, orientation, isFractionalValueEnabled, previewValue, buttonValue };
}

internal static class ColorExtensions
{
    public static Color WithAlphaChannel(this Color color, byte alphaChannel)
        => Color.FromArgb(alphaChannel, color.R, color.G, color.B);
}