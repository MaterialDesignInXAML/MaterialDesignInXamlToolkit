using System.Windows.Media;


namespace MaterialDesignThemes.UITests.WPF.Cards;

public class OutlinedCardTests : TestBase
{
    public OutlinedCardTests(ITestOutputHelper output)
        : base(output)
    { }

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
        await Assert.Equal(dividerColor, internalBorderColor);

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
        await Assert.Equal(5, internalBorderCornerRadius.Value.TopLeft);
        await Assert.Equal(5, internalBorderCornerRadius.Value.TopRight);
        await Assert.Equal(5, internalBorderCornerRadius.Value.BottomRight);
        await Assert.Equal(5, internalBorderCornerRadius.Value.BottomLeft);

        recorder.Success();
    }
}
