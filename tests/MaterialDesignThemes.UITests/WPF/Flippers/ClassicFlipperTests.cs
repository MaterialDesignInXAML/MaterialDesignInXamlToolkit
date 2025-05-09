

namespace MaterialDesignThemes.UITests.WPF.Flippers;

public class ClassicFlipperTests : TestBase
{
    [Test]
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
        await Assert.That(internalBorderCornerRadius).IsEqualTo(new CornerRadius(5));

        recorder.Success();
    }

    [Test]
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
        await Assert.That(internalBorderCornerRadius).IsEqualTo(new CornerRadius(5));

        recorder.Success();
    }

    [Test]
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
        await Assert.That(defaultElevation).IsEqualTo(Elevation.Dp1);

        recorder.Success();
    }
}
