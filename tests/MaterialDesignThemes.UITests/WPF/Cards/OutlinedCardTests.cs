using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.Cards;

public class OutlinedCardTests : TestBase
{
    [Test]
    public async Task OutlinedCard_UsesThemeColorForBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Card> card = await LoadXaml<Card>(
            @"<materialDesign:Card Content=""Hello World"" Style=""{StaticResource MaterialDesignOutlinedCard}""/>");
        Color dividerColor = await GetThemeColor("MaterialDesign.Brush.Card.Border");
        IVisualElement<Border> internalBorder = await card.GetElement<Border>();

        //Act
        Color? internalBorderColor = await internalBorder.GetBorderBrushColor();

        //Assert
        await Assert.That(internalBorderColor).IsEqualTo(dividerColor);

        recorder.Success();
    }

    [Test]
    public async Task OutlinedCard_UniformCornerRadiusApplied_AppliesCornerRadiusOnBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Card> card = await LoadXaml<Card>(
            @"<materialDesign:Card Content=""Hello World"" Style=""{StaticResource MaterialDesignOutlinedCard}"" UniformCornerRadius=""5"" />");
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
