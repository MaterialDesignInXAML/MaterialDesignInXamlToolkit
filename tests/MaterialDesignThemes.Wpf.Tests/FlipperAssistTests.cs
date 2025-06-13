namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class FlipperAssistTests
{
    [Test]
    public async Task CardStyle_CardStyleNotSet_AttachedPropertyNotSet()
    {
        FrameworkElement testElement = new();
        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(testElement)).IsNull();
    }

    [Test]
    public async Task CardStyle_StyleWithWrongTargetType_AttachedPropertyNotSet()
    {
        // Arrange
        FrameworkElement testElement = new();

        var style = new Style(typeof(Button));

        // Act
        FlipperAssist.SetCardStyle(testElement, style);

        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(testElement)).IsNull();
    }

    [Test]
    public async Task CardStyle_StyleWithCorrectTargetType_AttachedPropertySet()
    {
        // Arrange
        FrameworkElement testElement = new();

        var style = new Style(typeof(Card));

        // Act
        FlipperAssist.SetCardStyle(testElement, style);

        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(testElement)).IsEqualTo(style);
    }

    [Test]
    public async Task CardStyle_StyleWithDerivedCardTargetType_AttachedPropertySet()
    {
        // Arrange
        FrameworkElement testElement = new();

        var style = new Style(typeof(DerivedCard));

        // Act
        FlipperAssist.SetCardStyle(testElement, style);

        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(testElement)).IsEqualTo(style);
    }

    internal class DerivedCard : Card { }
}
