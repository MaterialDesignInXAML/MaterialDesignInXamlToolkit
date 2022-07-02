using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.Cards;

public class OutlinedCardTests : TestBase
{
    public OutlinedCardTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
    public async Task OutlinedCard_UsesThemeColorForBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Card> card = await LoadXaml<Card>(
            @"<materialDesign:Card Content=""Hello World"" Style=""{StaticResource MaterialDesignOutlinedCard}""/>");
        Color dividerColor = await GetThemeColor("MaterialDesignDivider");
        IVisualElement<Border> internalBorder = await card.GetElement<Border>();

        //Act
        Color? internalBorderColor = await internalBorder.GetBorderBrushColor();

        //Assert
        Assert.Equal(dividerColor, internalBorderColor);

        recorder.Success();
    }

    [Fact]
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
        Assert.Equal(5, internalBorderCornerRadius.Value.TopLeft);
        Assert.Equal(5, internalBorderCornerRadius.Value.TopRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomLeft);

        recorder.Success();
    }
}