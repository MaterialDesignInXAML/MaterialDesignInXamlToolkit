
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class FlipperAssistTests
{
    private readonly FrameworkElement _testElement;

    public FlipperAssistTests()
    {
        _testElement = new FrameworkElement();
    }

    [Test, STAThreadExecutor]
    public async Task CardStyle_CardStyleNotSet_AttachedPropertyNotSet()
    {
        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(_testElement)).IsNull();
    }

    [Test, STAThreadExecutor]
    public async Task CardStyle_StyleWithWrongTargetType_AttachedPropertyNotSet()
    {
        // Arrange
        var style = new Style(typeof(Button));

        // Act
        FlipperAssist.SetCardStyle(_testElement, style);

        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(_testElement)).IsNull();
    }

    [Test, STAThreadExecutor]
    public void CardStyle_StyleWithCorrectTargetType_AttachedPropertySet()
    {
        // Arrange
        var style = new Style(typeof(Card));

        // Act
        FlipperAssist.SetCardStyle(_testElement, style);

        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(_testElement)).IsEqualTo(style);
    }

    [Test, STAThreadExecutor]
    public void CardStyle_StyleWithDerivedCardTargetType_AttachedPropertySet()
    {
        // Arrange
        var style = new Style(typeof(DerivedCard));

        // Act
        FlipperAssist.SetCardStyle(_testElement, style);

        // Assert
        await Assert.That(FlipperAssist.GetCardStyle(_testElement)).IsEqualTo(style);
    }

    internal class DerivedCard : Card { }
}
