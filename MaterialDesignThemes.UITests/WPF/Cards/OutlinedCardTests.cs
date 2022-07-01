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
}
