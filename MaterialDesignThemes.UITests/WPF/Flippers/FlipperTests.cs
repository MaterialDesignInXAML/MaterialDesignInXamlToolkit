namespace MaterialDesignThemes.UITests.WPF.Flippers;

public class FlipperTests : TestBase
{
    public FlipperTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
    public async Task Flipper_UniformCornerRadiusAndOutlinedCardStyleAttachedPropertiesApplied_AppliesCornerRadiusOnBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Flipper> flipper = await LoadXaml<Flipper>(
            @"<materialDesign:Flipper Style=""{StaticResource MaterialDesignCardFlipper}"" materialDesign:FlipperAssist.CardStyle=""{StaticResource MaterialDesignOutlinedCard}"" materialDesign:FlipperAssist.UniformCornerRadius=""5"" />");
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
    public async Task Flipper_UniformCornerRadiusAndElevatedCardStyleAttachedPropertiesApplied_AppliesCornerRadiusOnBorder()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Flipper> flipper = await LoadXaml<Flipper>(
            @"<materialDesign:Flipper Style=""{StaticResource MaterialDesignCardFlipper}"" materialDesign:FlipperAssist.CardStyle=""{StaticResource MaterialDesignElevatedCard}"" materialDesign:FlipperAssist.UniformCornerRadius=""5"" />");
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
    public async Task Flipper_ElevatedCardStyleApplied_AppliesDefaultElevation()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Flipper> flipper = await LoadXaml<Flipper>(
            @"<materialDesign:Flipper Style=""{StaticResource MaterialDesignCardFlipper}"" materialDesign:FlipperAssist.CardStyle=""{StaticResource MaterialDesignElevatedCard}"" />");
        IVisualElement<Card> internalCard = await flipper.GetElement<Card>();

        //Act
        // TODO: This throws an exception because it fails to load the 'MaterialDesignTheme.Shadows.xaml' resource dictionary in the ElevationAssist static ctor
        Elevation? defaultElevation = await internalCard.GetProperty<Elevation>(ElevationAssist.ElevationProperty);

        //Assert
        Assert.Equal(Elevation.Dp1, defaultElevation);

        recorder.Success();
    }
}