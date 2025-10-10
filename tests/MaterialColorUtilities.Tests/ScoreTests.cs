namespace MaterialColorUtilities.Tests;

public sealed class ScoreTests
{
    private const int Red = unchecked((int)0xFFFF0000);
    private const int Green = unchecked((int)0xFF00FF00);
    private const int Blue = unchecked((int)0xFF0000FF);

    [Test]
    [DisplayName("Prioritizes chroma")]
    public async Task Prioritizes_Chroma()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF000000)] = 1,
            [unchecked((int)0xFFFFFFFF)] = 1,
            [Blue] = 1,
        };

        var ranked = Score.ScoreColors(colorsToPopulation, desired: 4).ToList();

        await Assert.That(ranked.Count).IsEqualTo(1);
        await Assert.That(ranked[0]).IsEqualTo(Blue);
    }

    [Test]
    [DisplayName("Prioritizes chroma when proportions equal")]
    public async Task Prioritizes_Chroma_When_Proportions_Equal()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [Red] = 1,
            [Green] = 1,
            [Blue] = 1,
        };

        var ranked = Score.ScoreColors(colorsToPopulation, desired: 4).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(Red);
        await Assert.That(ranked[1]).IsEqualTo(Green);
        await Assert.That(ranked[2]).IsEqualTo(Blue);
    }

    [Test]
    [DisplayName("Generates Google Blue when no colors available")]
    public async Task Generates_GoogleBlue_When_No_Colors_Available()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF000000)] = 1
        };

        var ranked = Score.ScoreColors(colorsToPopulation, desired: 4).ToList();

        await Assert.That(ranked.Count).IsEqualTo(1);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF4285F4));
    }

    [Test]
    [DisplayName("Dedupes nearby hues")]
    public async Task Dedupes_Nearby_Hues()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF008772)] = 1, // H 180 C 42 T 50
            [unchecked((int)0xFF318477)] = 1, // H 184 C 35 T 50
        };

        var ranked = Score.ScoreColors(colorsToPopulation, desired: 4).ToList();

        await Assert.That(ranked.Count).IsEqualTo(1);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF008772));
    }

    [Test]
    [DisplayName("Maximizes hue distance")]
    public async Task Maximizes_Hue_Distance()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF008772)] = 1, // H 180 C 42 T 50
            [unchecked((int)0xFF008587)] = 1, // H 198 C 50 T 50
            [unchecked((int)0xFF007EBC)] = 1, // H 245 C 50 T 50
        };

        var ranked = Score.ScoreColors(colorsToPopulation, desired: 2).ToList();

        await Assert.That(ranked.Count).IsEqualTo(2);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF007EBC));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFF008772));
    }

    [Test]
    [DisplayName("Generated scenario one")]
    public async Task Generated_Scenario_One()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF7EA16D)] = 67,
            [unchecked((int)0xFFD8CCAE)] = 67,
            [unchecked((int)0xFF835C0D)] = 49,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 3,
            fallbackColorArgb: unchecked((int)0xFF8D3819),
            filter: false).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF7EA16D));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFFD8CCAE));
        await Assert.That(ranked[2]).IsEqualTo(unchecked((int)0xFF835C0D));
    }

    [Test]
    [DisplayName("Generated scenario two")]
    public async Task Generated_Scenario_Two()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFFD33881)] = 14,
            [unchecked((int)0xFF3205CC)] = 77,
            [unchecked((int)0xFF0B48CF)] = 36,
            [unchecked((int)0xFFA08F5D)] = 81,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 4,
            fallbackColorArgb: unchecked((int)0xFF7D772B),
            filter: true).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF3205CC));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFFA08F5D));
        await Assert.That(ranked[2]).IsEqualTo(unchecked((int)0xFFD33881));
    }

    [Test]
    [DisplayName("Generated scenario three")]
    public async Task Generated_Scenario_Three()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFFBE94A6)] = 23,
            [unchecked((int)0xFFC33FD7)] = 42,
            [unchecked((int)0xFF899F36)] = 90,
            [unchecked((int)0xFF94C574)] = 82,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 3,
            fallbackColorArgb: unchecked((int)0xFFAA79A4),
            filter: true).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF94C574));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFFC33FD7));
        await Assert.That(ranked[2]).IsEqualTo(unchecked((int)0xFFBE94A6));
    }

    [Test]
    [DisplayName("Generated scenario four")]
    public async Task Generated_Scenario_Four()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFFDF241C)] = 85,
            [unchecked((int)0xFF685859)] = 44,
            [unchecked((int)0xFFD06D5F)] = 34,
            [unchecked((int)0xFF561C54)] = 27,
            [unchecked((int)0xFF713090)] = 88,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 5,
            fallbackColorArgb: unchecked((int)0xFF58C19C),
            filter: false).ToList();

        await Assert.That(ranked.Count).IsEqualTo(2);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFFDF241C));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFF561C54));
    }

    [Test]
    [DisplayName("Generated scenario five")]
    public async Task Generated_Scenario_Five()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFFBE66F8)] = 41,
            [unchecked((int)0xFF4BBDA9)] = 88,
            [unchecked((int)0xFF80F6F9)] = 44,
            [unchecked((int)0xFFAB8017)] = 43,
            [unchecked((int)0xFFE89307)] = 65,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 3,
            fallbackColorArgb: unchecked((int)0xFF916691),
            filter: false).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFFAB8017));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFF4BBDA9));
        await Assert.That(ranked[2]).IsEqualTo(unchecked((int)0xFFBE66F8));
    }

    [Test]
    [DisplayName("Generated scenario six")]
    public async Task Generated_Scenario_Six()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF18EA8F)] = 93,
            [unchecked((int)0xFF327593)] = 18,
            [unchecked((int)0xFF066A18)] = 53,
            [unchecked((int)0xFFFA8A23)] = 74,
            [unchecked((int)0xFF04CA1F)] = 62,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 2,
            fallbackColorArgb: unchecked((int)0xFF4C377A),
            filter: false).ToList();

        await Assert.That(ranked.Count).IsEqualTo(2);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF18EA8F));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFFFA8A23));
    }

    [Test]
    [DisplayName("Generated scenario seven")]
    public async Task Generated_Scenario_Seven()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF2E05ED)] = 23,
            [unchecked((int)0xFF153E55)] = 90,
            [unchecked((int)0xFF9AB220)] = 23,
            [unchecked((int)0xFF153379)] = 66,
            [unchecked((int)0xFF68BCC3)] = 81,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 2,
            fallbackColorArgb: unchecked((int)0xFFF588DC),
            filter: true).ToList();

        await Assert.That(ranked.Count).IsEqualTo(2);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF2E05ED));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFF9AB220));
    }

    [Test]
    [DisplayName("Generated scenario eight")]
    public async Task Generated_Scenario_Eight()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF816EC5)] = 24,
            [unchecked((int)0xFF6DCB94)] = 19,
            [unchecked((int)0xFF3CAE91)] = 98,
            [unchecked((int)0xFF5B542F)] = 25,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 1,
            fallbackColorArgb: unchecked((int)0xFF84B0FD),
            filter: false).ToList();

        await Assert.That(ranked.Count).IsEqualTo(1);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF3CAE91));
    }

    [Test]
    [DisplayName("Generated scenario nine")]
    public async Task Generated_Scenario_Nine()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF206F86)] = 52,
            [unchecked((int)0xFF4A620D)] = 96,
            [unchecked((int)0xFFF51401)] = 85,
            [unchecked((int)0xFF2B8EBF)] = 3,
            [unchecked((int)0xFF277766)] = 59,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 3,
            fallbackColorArgb: unchecked((int)0xFF02B415),
            filter: true).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFFF51401));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFF4A620D));
        await Assert.That(ranked[2]).IsEqualTo(unchecked((int)0xFF2B8EBF));
    }

    [Test]
    [DisplayName("Generated scenario ten")]
    public async Task Generated_Scenario_Ten()
    {
        var colorsToPopulation = new Dictionary<int, int>
        {
            [unchecked((int)0xFF8B1D99)] = 54,
            [unchecked((int)0xFF27EFFE)] = 43,
            [unchecked((int)0xFF6F558D)] = 2,
            [unchecked((int)0xFF77FDF2)] = 78,
        };

        var ranked = Score.ScoreColors(
            colorsToPopulation,
            desired: 4,
            fallbackColorArgb: unchecked((int)0xFF5E7A10),
            filter: true).ToList();

        await Assert.That(ranked.Count).IsEqualTo(3);
        await Assert.That(ranked[0]).IsEqualTo(unchecked((int)0xFF27EFFE));
        await Assert.That(ranked[1]).IsEqualTo(unchecked((int)0xFF8B1D99));
        await Assert.That(ranked[2]).IsEqualTo(unchecked((int)0xFF6F558D));
    }
}
