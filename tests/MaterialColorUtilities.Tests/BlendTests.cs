namespace MaterialColorUtilities.Tests;

public sealed class BlendTests
{
    // Same ARGB constants as in the Dart tests (note the unchecked casts for >0x7FFFFFFF).
    private const int Red = unchecked((int)0xFFFF0000);
    private const int Blue = unchecked((int)0xFF0000FF);
    private const int Green = unchecked((int)0xFF00FF00);
    private const int Yellow = unchecked((int)0xFFFFFF00);

    [Test]
    [DisplayName("Blend.Harmonize matches MaterialColorUtilities vectors")]
    [Arguments(Red, Blue, unchecked((int)0xFFFB0057))] // redToBlue
    [Arguments(Red, Green, unchecked((int)0xFFD85600))] // redToGreen
    [Arguments(Red, Yellow, unchecked((int)0xFFD85600))] // redToYellow
    [Arguments(Blue, Green, unchecked((int)0xFF0047A3))] // blueToGreen
    [Arguments(Blue, Red, unchecked((int)0xFF5700DC))] // blueToRed
    [Arguments(Blue, Yellow, unchecked((int)0xFF0047A3))] // blueToYellow
    [Arguments(Green, Blue, unchecked((int)0xFF00FC94))] // greenToBlue
    [Arguments(Green, Red, unchecked((int)0xFFB1F000))] // greenToRed
    [Arguments(Green, Yellow, unchecked((int)0xFFB1F000))] // greenToYellow
    [Arguments(Yellow,Blue, unchecked((int)0xFFEBFFBA))] // yellowToBlue
    [Arguments(Yellow,Green, unchecked((int)0xFFEBFFBA))] // yellowToGreen
    [Arguments(Yellow,Red, unchecked((int)0xFFFFF6E3))] // yellowToRed
    public async Task Harmonize_Matches_MaterialColorUtilities_Vectors(int designColor, int sourceColor, int expectedArgb)
    {
        // act
        var actual = Blend.Harmonize(designColor, sourceColor);

        // assert (TUnit uses async fluent assertions)
        await Assert.That(actual).IsEqualTo(expectedArgb);
    }

    [Test]
    [DisplayName("Blend.HctHue blends hue correctly")]
    [Arguments(Red, Blue, 0.5, unchecked((int)0xffe700c9))]
    [Arguments(Green, Yellow, 1.0, unchecked((int)0xffe3e300))]
    public async Task HctHue_BlendsCorrectly(int from, int to, double amount, int expectedArgb)
    {
        var actual = Blend.HctHue(from, to, amount);
        await Assert.That(actual).IsEqualTo(expectedArgb);
    }
}
