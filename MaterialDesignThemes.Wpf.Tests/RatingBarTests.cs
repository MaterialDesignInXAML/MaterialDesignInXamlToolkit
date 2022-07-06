using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class RatingBarTests
{
    [Theory]
    [InlineData(-5, 0.0)]
    [InlineData(5, 5.0)]
    [InlineData(15, 10.0)]
    [InlineData(0.2, 0.0)]
    [InlineData(0.3, 0.5)]
    [InlineData(0.7, 0.5)]
    [InlineData(0.8, 1.0)]
    public void SetValue_CoercesToCorrectMultipleAndStaysWithinBounds(double valueToSet, double expectedValue)
    {
        // Arrange
        RatingBar ratingBar = new() { Min = 0, Max = 10};

        // Act
        ratingBar.Value = valueToSet;

        // Assert
        Assert.Equal(expectedValue, ratingBar.Value);
    }
}