﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

public class RatingBarTests
{
    [Test, TestExecutor<STAThreadExecutor>]
    public async Task SetMin_ShouldCoerceToMax_WhenMinIsGreaterThanMax()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10 };

        // Act
        ratingBar.Min = 15;

        // Assert
        await Assert.That(ratingBar.Min).IsEqualTo(10);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    public async Task SetMin_ShouldNotCoerceValue_WhenFractionalValuesAreDisabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5 };

        // Act
        ratingBar.Min = 7;

        // Assert
        await Assert.That(ratingBar.Value).IsEqualTo(5);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    public async Task SetMin_ShouldCoerceValue_WhenFractionalValuesAreEnabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5, ValueIncrements = 0.5 };

        // Act
        ratingBar.Min = 7;

        // Assert
        await Assert.That(ratingBar.Value).IsEqualTo(7);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    public async Task SetMax_ShouldNotCoerceValue_WhenFractionalValuesAreDisabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5 };

        // Act
        ratingBar.Max = 3;

        // Assert
        await Assert.That(ratingBar.Value).IsEqualTo(5);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    public async Task SetMax_ShouldCoerceValue_WhenFractionalValuesAreEnabled()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, Value = 5, ValueIncrements = 0.5 };

        // Act
        ratingBar.Max = 3;

        // Assert
        await Assert.That(ratingBar.Value).IsEqualTo(3);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    public async Task SetMax_ShouldCoerceToMin_WhenMaxIsLessThanMin()
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10 };

        // Act
        ratingBar.Max = -5;

        // Assert
        await Assert.That(ratingBar.Max).IsEqualTo(1);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    [Arguments(-5, 1.0)]
    [Arguments(5, 5.0)]
    [Arguments(15, 10.0)]
    [Arguments(1.2, 1.0)]
    [Arguments(1.3, 1.5)]
    [Arguments(1.7, 1.5)]
    [Arguments(1.8, 2.0)]
    [Arguments(2.2, 2.0)]
    [Arguments(2.3, 2.5)]
    public async Task SetValue_ShouldCoerceToCorrectMultipleAndStaysWithinBounds_WhenFractionalValuesAreEnabled(double valueToSet, double expectedValue)
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10, ValueIncrements = 0.5 };

        // Act
        ratingBar.Value = valueToSet;

        // Assert
        await Assert.That(ratingBar.Value).IsEqualTo(expectedValue);
    }

    [Test, TestExecutor<STAThreadExecutor>]
    [Arguments(-5, -5.0)]
    [Arguments(5, 5.0)]
    [Arguments(15, 15.0)]
    [Arguments(1.2, 1.2)]
    [Arguments(2.3, 2.3)]
    public async Task SetValue_ShouldNotCoerceValue_WhenFractionalValuesAreDisabled(double valueToSet, double expectedValue)
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 1, Max = 10 };

        // Act
        ratingBar.Value = valueToSet;

        // Assert
        await Assert.That(ratingBar.Value).IsEqualTo(expectedValue);
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnOriginalBrush_WhenValueIsEqualToButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1, buttonValue: 1);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsEqualTo(brush);
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnOriginalBrush_WhenValueIsGreaterThanButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 2, buttonValue: 1);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsEqualTo(brush);
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnSemiTransparentBrush_WhenValueIsLessThanButtonValueMinusOne()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 0.5, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<SolidColorBrush>();
        SolidColorBrush resultBrush = (SolidColorBrush)result!;
        await Assert.That(resultBrush.Color.A).IsEqualTo(RatingBar.TextBlockForegroundConverter.SemiTransparent);
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnHorizontalLinearGradientBrush_WhenValueIsBetweenButtonValueAndButtonValueMinusOne()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.5, buttonValue: 2, orientation: Orientation.Horizontal);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<LinearGradientBrush>();
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        await Assert.That(resultBrush.StartPoint).IsEqualTo(new Point(0, 0.5));
        await Assert.That(resultBrush.EndPoint).IsEqualTo(new Point(1, 0.5));
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnVerticalLinearGradientBrush_WhenValueIsBetweenButtonValueAndButtonValueMinusOne()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.5, buttonValue: 2, orientation: Orientation.Vertical);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<LinearGradientBrush>();
        var resultBrush = (LinearGradientBrush)result!;
        await Assert.That(resultBrush.StartPoint).IsEqualTo(new Point(0.5, 0));
        await Assert.That(resultBrush.EndPoint).IsEqualTo(new Point(0.5, 1));
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers10PercentOfButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.1, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<LinearGradientBrush>();
        var resultBrush = (LinearGradientBrush)result!;
        await Assert.That(resultBrush.GradientStops.Count).IsEqualTo(2);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        await Assert.That(stop1.Offset).IsCloseTo(0.1, 0.0001);
        await Assert.That(stop1.Color).IsEqualTo(brush.Color);
        await Assert.That(stop2.Offset).IsCloseTo(0.1, 0.0001);
        await Assert.That(stop2.Color).IsEqualTo(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent));
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers10PercentOfButtonValueAndDirectionIsInverted()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.1, buttonValue: 2, invertDirection: true);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<LinearGradientBrush>();
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        await Assert.That(resultBrush.GradientStops.Count).IsEqualTo(2);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        await Assert.That(stop1.Offset).IsCloseTo(0.9, 0.0001);
        await Assert.That(stop1.Color).IsEqualTo(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent));
        await Assert.That(stop2.Offset).IsCloseTo(0.9, 0.0001);
        await Assert.That(stop2.Color).IsEqualTo(brush.Color);
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers42PercentOfButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.42, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<LinearGradientBrush>();
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        await Assert.That(resultBrush.GradientStops.Count).IsEqualTo(2);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        await Assert.That(stop1.Offset).IsCloseTo(0.42, 0.0001);
        await Assert.That(stop1.Color).IsEqualTo(brush.Color);
        await Assert.That(stop2.Offset).IsCloseTo(0.42, 0.0001);
        await Assert.That(stop2.Color).IsEqualTo(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent));
    }

    [Test]
    public async Task TextBlockForegroundConverter_ShouldReturnFractionalGradientStops_WhenValueCovers87PercentOfButtonValue()
    {
        // Arrange
        SolidColorBrush brush = Brushes.Red;
        var converter = RatingBar.TextBlockForegroundConverter.Instance;
        object[] values = Arrange_TextBlockForegroundConverterValues(brush, value: 1.87, buttonValue: 2);

        // Act
        var result = converter.Convert(values, typeof(Brush), null, CultureInfo.CurrentCulture) as Brush;

        // Assert
        await Assert.That(result).IsAssignableTo<LinearGradientBrush>();
        LinearGradientBrush resultBrush = (LinearGradientBrush)result!;
        await Assert.That(resultBrush.GradientStops.Count).IsEqualTo(2);
        GradientStop stop1 = resultBrush.GradientStops[0];
        GradientStop stop2 = resultBrush.GradientStops[1];
        await Assert.That(stop1.Offset).IsCloseTo(0.87, 0.0001);
        await Assert.That(stop1.Color).IsEqualTo(brush.Color);
        await Assert.That(stop2.Offset).IsCloseTo(0.87, 0.0001);
        await Assert.That(stop2.Color).IsEqualTo(brush.Color.WithAlphaChannel(RatingBar.TextBlockForegroundConverter.SemiTransparent));
    }

    private static object[] Arrange_TextBlockForegroundConverterValues(SolidColorBrush brush, double value, int buttonValue, Orientation orientation = Orientation.Horizontal, bool invertDirection = false) =>
        [brush, orientation, invertDirection, value, buttonValue];

    [Test]
    public async Task PreviewIndicatorTransformXConverter_ShouldCenterPreviewIndicator_WhenFractionalValuesAreDisabledAndOrientationIsHorizontal()
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Horizontal, false, false, 1, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(40.0); // 50% of 100 minus 20/2
    }

    [Test]
    [Arguments(false, 15.0)] // 25% of 100 minus 20/2
    [Arguments(true, 65.0)]  // 75% of 100 minus 20/2
    public async Task PreviewIndicatorTransformXConverter_ShouldOffsetPreviewIndicatorByPercentage_WhenFractionalValuesAreEnabledAndOrientationIsHorizontal(bool invertDirection, double expectedValue)
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Horizontal, invertDirection, true, 1.25, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(expectedValue); 
    }

    [Test]
    public async Task PreviewIndicatorTransformXConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreDisabledAndOrientationIsVertical()
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Vertical, false, false, 1, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformXConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(expectedValue); // 100% of 20 minus fixed margin
    }

    [Test]
    public async Task PreviewIndicatorTransformXConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreEnabledAndOrientationIsVertical()
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformXConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformXConverterValues(100, 20, Orientation.Vertical, false, true, 1.25, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformXConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(expectedValue); // 100% of 20 minus fixed margin
    }



    private static object[] Arrange_PreviewIndicatorTransformXConverterValues(double ratingBarButtonActualWidth, double previewValueActualWidth, Orientation orientation, bool invertDirection, bool isFractionalValueEnabled, double previewValue, int buttonValue) =>
        [ratingBarButtonActualWidth, previewValueActualWidth, orientation, invertDirection, isFractionalValueEnabled, previewValue, buttonValue];

    [Test]
    public async Task PreviewIndicatorTransformYConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreDisabledAndOrientationIsHorizontal()
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Horizontal, false, false, 1, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformYConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(expectedValue); // 100% of 20 minus fixed margin
    }

    [Test]
    public async Task PreviewIndicatorTransformYConverter_ShouldPlacePreviewIndicatorWithSmallMargin_WhenFractionalValuesAreEnabledAndOrientationIsHorizontal()
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Horizontal, false, true, 1.25, 1);
        double expectedValue = -20 - RatingBar.PreviewIndicatorTransformYConverter.Margin;

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(expectedValue); // 100% of 20 minus fixed margin
    }

    [Test]
    public async Task PreviewIndicatorTransformYConverter_ShouldCenterPreviewIndicator_WhenFractionalValuesAreDisabledAndOrientationIsVertical()
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Vertical, false, false, 1, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(40.0); // 50% of 100 minus 20/2
    }

    [Test]
    [Arguments(false, 15.0)] // 25% of 100 minus 20/2
    [Arguments(true, 65.0)]  // 75% of 100 minus 20/2
    public async Task PreviewIndicatorTransformYConverter_ShouldPreviewIndicatorByPercentage_WhenFractionalValuesAreEnabledAndOrientationIsVertical(bool invertDirection, double expectedValue)
    {
        // Arrange
        var converter = RatingBar.PreviewIndicatorTransformYConverter.Instance;
        object[] values = Arrange_PreviewIndicatorTransformYConverterValues(100, 20, Orientation.Vertical, invertDirection, true, 1.25, 1);

        // Act
        double? result = converter.Convert(values, typeof(double), null, CultureInfo.CurrentCulture) as double?;

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result).IsEqualTo(expectedValue);
    }

    private static object[] Arrange_PreviewIndicatorTransformYConverterValues(double ratingBarButtonActualHeight, double previewValueActualHeight, Orientation orientation, bool invertDirection, bool isFractionalValueEnabled, double previewValue, int buttonValue) =>
        [ratingBarButtonActualHeight, previewValueActualHeight, orientation, invertDirection, isFractionalValueEnabled, previewValue, buttonValue];
}

internal static class ColorExtensions
{
    public static Color WithAlphaChannel(this Color color, byte alphaChannel)
        => Color.FromArgb(alphaChannel, color.R, color.G, color.B);
}
