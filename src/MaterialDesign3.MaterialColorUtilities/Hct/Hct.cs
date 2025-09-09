using static System.Math;

namespace MaterialColorUtilities;

/// <summary>
/// HCT, hue, chroma, and tone. A color system that provides a perceptually accurate color
/// measurement system that can also accurately render what colors will appear as in different
/// lighting environments.
/// </summary>
public sealed class Hct
{
    public int Argb { get; set; }
    public double Hue { get; set; }
    public double Chroma { get; set; }
    public double Tone { get; set; }

    /// <summary>
    /// Create an HCT color from hue, chroma, and tone.
    /// </summary>
    public static Hct From(double hue, double chroma, double tone)
    {
        int argb = HctSolver.SolveToInt(hue, chroma, tone);
        return new Hct(argb);
    }

    /// <summary>
    /// Create an HCT color from an ARGB color.
    /// </summary>
    public static Hct FromInt(int argb)
    {
        return new Hct(argb);
    }

    private Hct(int argb)
    {
        SetInternalState(argb);
    }

    /// <summary>
    /// Set the hue of this color. Chroma may decrease because chroma has a different maximum for any
    /// given hue and tone.
    /// </summary>
    public void SetHue(double newHue)
    {
        SetInternalState(HctSolver.SolveToInt(newHue, Chroma, Tone));
    }

    /// <summary>
    /// Set the chroma of this color. Chroma may decrease because chroma has a different maximum for
    /// any given hue and tone.
    /// </summary>
    public void SetChroma(double newChroma)
    {
        SetInternalState(HctSolver.SolveToInt(Hue, newChroma, Tone));
    }

    /// <summary>
    /// Set the tone of this color. Chroma may decrease because chroma has a different maximum for any
    /// given hue and tone.
    /// </summary>
    public void SetTone(double newTone)
    {
        SetInternalState(HctSolver.SolveToInt(Hue, Chroma, newTone));
    }

    public override string ToString()
    {
        return $"HCT({(int)Round(Hue)}, {(int)Round(Chroma)}, {(int)Round(Tone)})";
    }

    public static bool IsBlue(double hue)
    {
        return hue is >= 250 and < 270;
    }

    public static bool IsYellow(double hue)
    {
        return hue is >= 105 and < 125;
    }

    public static bool IsCyan(double hue)
    {
        return hue is >= 170 and < 207;
    }

    /// <summary>
    /// Translate a color into different ViewingConditions.
    /// </summary>
    public Hct InViewingConditions(ViewingConditions vc)
    {
        // 1. Use CAM16 to find XYZ coordinates of color in specified VC.
        Cam16 cam16 = Cam16.FromInt(Argb);
        double[] viewedInVc = cam16.XyzInViewingConditions(vc, null);

        // 2. Create CAM16 of those XYZ coordinates in default VC.
        Cam16 recastInVc =
            Cam16.FromXyzInViewingConditions(
                viewedInVc[0], viewedInVc[1], viewedInVc[2], ViewingConditions.DEFAULT);

        // 3. Create HCT from CAM16 (default VC) with XYZ from specified VC and L* from Y in that VC.
        return Hct.From(
            recastInVc.GetHue(), recastInVc.GetChroma(), ColorUtils.LstarFromY(viewedInVc[1]));
    }

    private void SetInternalState(int argb)
    {
        Argb = argb;
        Cam16 cam = Cam16.FromInt(argb);
        Hue = cam.GetHue();
        Chroma = cam.GetChroma();
        Tone = ColorUtils.LstarFromArgb(argb);
    }
}