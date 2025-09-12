namespace MaterialColorUtilities.Tests;

public sealed class MathUtilsTests
{
    private static double ReferenceRotationDirection(double from, double to)
    {
        var a = to - from;
        var b = to - from + 360.0;
        var c = to - from - 360.0;
        var aAbs = Math.Abs(a);
        var bAbs = Math.Abs(b);
        var cAbs = Math.Abs(c);

        if (aAbs <= bAbs && aAbs <= cAbs)
        {
            return a >= 0.0 ? 1.0 : -1.0;
        }

        if (bAbs <= aAbs && bAbs <= cAbs)
        {
            return b >= 0.0 ? 1.0 : -1.0;
        }

        return c >= 0.0 ? 1.0 : -1.0;
    }

    [Test]
    [DisplayName("rotationDirection behaves correctly")]
    public async Task RotationDirection_Behaves_Correctly()
    {
        for (double from = 0.0; from < 360.0; from += 15.0)
        {
            for (double to = 7.5; to < 360.0; to += 15.0)
            {
                var expected = ReferenceRotationDirection(from, to);
                var actual = MathUtils.RotationDirection(from, to);

                await Assert.That(actual)
                    .IsEqualTo(expected)
                    .Because($"should be {expected} from {from} to {to}");

                await Assert.That(Math.Abs(actual))
                    .IsEqualTo(1.0)
                    .Because($"should be either +1.0 or -1.0 from {from} to {to} (got {actual})");
            }
        }
    }
}
