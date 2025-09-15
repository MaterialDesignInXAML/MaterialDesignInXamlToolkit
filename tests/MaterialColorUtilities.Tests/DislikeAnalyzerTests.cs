namespace MaterialColorUtilities.Tests;

public class DislikeAnalyzerTests
{
    [Test]
    [DisplayName("Monk Skin Tone Scale colors liked")]
    public async Task MonkSkinToneScale_Colors_NotDisliked()
    {
        // From https://skintone.google#/get-started (ported)
        var monkSkinToneScaleColors = new[]
        {
            unchecked((int)0xFFF6EDE4),
            unchecked((int)0xFFF3E7DB),
            unchecked((int)0xFFF7EAD0),
            unchecked((int)0xFFEADABA),
            unchecked((int)0xFFD7BD96),
            unchecked((int)0xFFA07E56),
            unchecked((int)0xFF825C43),
            unchecked((int)0xFF604134),
            unchecked((int)0xFF3A312A),
            unchecked((int)0xFF292420),
        };

        foreach (var color in monkSkinToneScaleColors)
        {
            var hct = Hct.FromInt(color);
            await Assert.That(DislikeAnalyzer.IsDisliked(hct)).IsFalse();
        }
    }

    [Test]
    [DisplayName("bile colors disliked")]
    public async Task Bile_Colors_AreDisliked()
    {
        var unlikable = new[]
        {
            unchecked((int)0xFF95884B),
            unchecked((int)0xFF716B40),
            unchecked((int)0xFFB08E00),
            unchecked((int)0xFF4C4308),
            unchecked((int)0xFF464521),
        };

        foreach (var color in unlikable)
        {
            var hct = Hct.FromInt(color);
            await Assert.That(DislikeAnalyzer.IsDisliked(hct)).IsTrue();
        }
    }

    [Test]
    [DisplayName("bile colors became likable after fix")]
    public async Task Bile_Colors_BecomeLikable_AfterFix()
    {
        var unlikable = new[]
        {
            unchecked((int)0xFF95884B),
            unchecked((int)0xFF716B40),
            unchecked((int)0xFFB08E00),
            unchecked((int)0xFF4C4308),
            unchecked((int)0xFF464521),
        };

        foreach (var color in unlikable)
        {
            var hct = Hct.FromInt(color);
            await Assert.That(DislikeAnalyzer.IsDisliked(hct)).IsTrue();

            var likable = DislikeAnalyzer.FixIfDisliked(hct);
            await Assert.That(DislikeAnalyzer.IsDisliked(likable)).IsFalse();
        }
    }

    [Test]
    [DisplayName("tone 67 not disliked and remains unchanged by fix")]
    public async Task Tone67_NotDisliked_And_Unchanged()
    {
        var color = Hct.From(100.0, 50.0, 67.0);

        await Assert.That(DislikeAnalyzer.IsDisliked(color)).IsFalse();

        var fixedColor = DislikeAnalyzer.FixIfDisliked(color);
        // In the C# implementation, Hct exposes ARGB via `.Argb`
        await Assert.That(fixedColor.Argb).IsEqualTo(color.Argb);
    }
}
