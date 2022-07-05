namespace MaterialDesignThemes.UITests.WPF.Cards;

public class ElevatedCardTests : TestBase
{
    public ElevatedCardTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
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
        Assert.Equal(5, internalBorderCornerRadius.Value.TopLeft);
        Assert.Equal(5, internalBorderCornerRadius.Value.TopRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomLeft);

        recorder.Success();
    }
}