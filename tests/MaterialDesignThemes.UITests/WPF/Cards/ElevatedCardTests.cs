

namespace MaterialDesignThemes.UITests.WPF.Cards;

public class ElevatedCardTests : TestBase
{

    [Test]
    public async Task ElevatedCard_UniformCornerRadiusApplied_AppliesCornerRadiusOnBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Card> card = await LoadXaml<Card>(
            @"<materialDesign:Card Content=""Hello World"" Style=""{StaticResource MaterialDesignElevatedCard}"" UniformCornerRadius=""5"" />");
        IVisualElement<Border> internalBorder = await card.GetElement<Border>();

        //Act
        CornerRadius? internalBorderCornerRadius = await internalBorder.GetCornerRadius();

        //Assert
        await Assert.That(internalBorderCornerRadius.Value.TopLeft).IsEqualTo(5);
        await Assert.That(internalBorderCornerRadius.Value.TopRight).IsEqualTo(5);
        await Assert.That(internalBorderCornerRadius.Value.BottomRight).IsEqualTo(5);
        await Assert.That(internalBorderCornerRadius.Value.BottomLeft).IsEqualTo(5);

        recorder.Success();
    }
}
