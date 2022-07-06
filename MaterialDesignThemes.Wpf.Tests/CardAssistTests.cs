using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class CardAssistTests
{
    private readonly FrameworkElement _testElement;

    public CardAssistTests()
    {
        _testElement = new FrameworkElement();
    }

    [StaFact]
    public void CardStyle_CardStyleNotSet_AttachedPropertyNotSet()
    {
        // Assert
        Assert.Null(CardAssist.GetCardStyle(_testElement));
    }

    [StaFact]
    public void CardStyle_StyleWithWrongTargetType_AttachedPropertyNotSet()
    {
        // Arrange
        var style = new Style(typeof(Button));

        // Act
        CardAssist.SetCardStyle(_testElement, style);

        // Assert
        Assert.Null(CardAssist.GetCardStyle(_testElement));
    }

    [StaFact]
    public void CardStyle_StyleWithCorrectTargetType_AttachedPropertySet()
    {
        // Arrange
        var style = new Style(typeof(Card));

        // Act
        CardAssist.SetCardStyle(_testElement, style);

        // Assert
        Assert.Equal(style, CardAssist.GetCardStyle(_testElement));
    }
}