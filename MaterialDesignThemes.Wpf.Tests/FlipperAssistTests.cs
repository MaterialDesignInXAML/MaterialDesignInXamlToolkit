using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class FlipperAssistTests
{
    private readonly FrameworkElement _testElement;

    public FlipperAssistTests()
    {
        _testElement = new FrameworkElement();
    }

    [StaFact]
    public void CardStyle_CardStyleNotSet_AttachedPropertyNotSet()
    {
        // Assert
        Assert.Null(FlipperAssist.GetCardStyle(_testElement));
    }

    [StaFact]
    public void CardStyle_StyleWithWrongTargetType_AttachedPropertyNotSet()
    {
        // Arrange
        var style = new Style(typeof(Button));

        // Act
        FlipperAssist.SetCardStyle(_testElement, style);

        // Assert
        Assert.Null(FlipperAssist.GetCardStyle(_testElement));
    }

    [StaFact]
    public void CardStyle_StyleWithCorrectTargetType_AttachedPropertySet()
    {
        // Arrange
        var style = new Style(typeof(Card));

        // Act
        FlipperAssist.SetCardStyle(_testElement, style);

        // Assert
        Assert.Equal(style, FlipperAssist.GetCardStyle(_testElement));
    }

    [StaFact]
    public void CardStyle_StyleWithDerivedCardTargetType_AttachedPropertySet()
    {
        // Arrange
        var style = new Style(typeof(DerivedCard));

        // Act
        FlipperAssist.SetCardStyle(_testElement, style);

        // Assert
        Assert.Equal(style, FlipperAssist.GetCardStyle(_testElement));
    }

    internal class DerivedCard : Card { }
}