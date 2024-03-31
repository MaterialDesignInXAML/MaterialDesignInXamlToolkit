namespace MaterialDesignThemes.UITests.WPF.Flippers;

public class ClassicFlipperTests : TestBase
{
    public ClassicFlipperTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
    public async Task UniformCornerRadiusAndOutlinedCardStyleAttachedPropertiesApplied_AppliesCornerRadiusOnBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<FlipperClassic> flipper = await LoadXaml<FlipperClassic>(
            """
            <materialDesign:FlipperClassic Style="{StaticResource MaterialDesignCardFlipperClassic}" materialDesign:FlipperAssist.CardStyle="{StaticResource MaterialDesignOutlinedCard}" materialDesign:FlipperAssist.UniformCornerRadius="5" />
            """);
        IVisualElement<Card> internalCard = await flipper.GetElement<Card>();
        IVisualElement<Border> internalBorder = await internalCard.GetElement<Border>();

        //Act
        CornerRadius? internalBorderCornerRadius = await internalBorder.GetCornerRadius();

        //Assert
        Assert.Equal(5, internalBorderCornerRadius.Value.TopLeft);
        Assert.Equal(5, internalBorderCornerRadius.Value.TopRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomLeft);

        recorder.Success();
    }

    [Fact]
    public async Task UniformCornerRadiusAndElevatedCardStyleAttachedPropertiesApplied_AppliesCornerRadiusOnBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<FlipperClassic> flipper = await LoadXaml<FlipperClassic>(
            """
            <materialDesign:FlipperClassic Style="{StaticResource MaterialDesignCardFlipperClassic}"
                            materialDesign:FlipperAssist.CardStyle="{StaticResource MaterialDesignElevatedCard}"
                            materialDesign:FlipperAssist.UniformCornerRadius="5" />
            """);
        IVisualElement<Card> internalCard = await flipper.GetElement<Card>();
        IVisualElement<Border> internalBorder = await internalCard.GetElement<Border>();

        //Act
        CornerRadius? internalBorderCornerRadius = await internalBorder.GetCornerRadius();

        //Assert
        Assert.Equal(5, internalBorderCornerRadius.Value.TopLeft);
        Assert.Equal(5, internalBorderCornerRadius.Value.TopRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomRight);
        Assert.Equal(5, internalBorderCornerRadius.Value.BottomLeft);

        recorder.Success();
    }

    [Fact]
    public async Task ElevatedCardStyleApplied_AppliesDefaultElevation()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<FlipperClassic> flipper = await LoadXaml<FlipperClassic>(
            """
            <materialDesign:FlipperClassic Style="{StaticResource MaterialDesignCardFlipperClassic}"
                materialDesign:FlipperAssist.CardStyle="{StaticResource MaterialDesignElevatedCard}" />
            """
            );
        IVisualElement <Card> internalCard = await flipper.GetElement<Card>();

        //Act
        Elevation? defaultElevation = await internalCard.GetProperty<Elevation>(ElevationAssist.ElevationProperty);

        //Assert
        Assert.Equal(Elevation.Dp1, defaultElevation);

        recorder.Success();
    }
}
