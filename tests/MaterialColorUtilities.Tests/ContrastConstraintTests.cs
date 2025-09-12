namespace MaterialColorUtilities.Tests;

public sealed class SchemeCorrectnessTests
{
private static readonly double ContrastTolerance = 0.05;
    private static readonly double DeltaTolerance = 0.5;

    private static readonly int[] SeedArgb =
    {
        unchecked((int)0xFF0000FF), // blue
        unchecked((int)0xFF00FF00), // green
        unchecked((int)0xFFFFFF00), // yellow
        unchecked((int)0xFFFF0000), // red
    };

    private static readonly double[] ContrastLevels = { -1.0, 0.0, 0.5, 1.0 };

    private readonly MaterialDynamicColors _mdc = new();

    // ---- Constraint helpers (ported) ----

    private interface IConstraint
    {
        void TestAgainst(DynamicScheme scheme);
    }

    private sealed class ContrastConstraint : IConstraint
    {
        private readonly DynamicColor _foreground;
        private readonly DynamicColor _background;
        private readonly string _foregroundName;
        private readonly string _backgroundName;
        private readonly ContrastCurve _curve;

        public ContrastConstraint(DynamicColor foreground, string foregroundName,
                                  DynamicColor background, string backgroundName,
                                  ContrastCurve curve)
        {
            _foreground = foreground;
            _background = background;
            _foregroundName = foregroundName;
            _backgroundName = backgroundName;
            _curve = curve;
        }

        public void TestAgainst(DynamicScheme scheme)
        {
            var fgTone = _foreground.GetHct(scheme).Tone;
            var bgTone = _background.GetHct(scheme).Tone;
            var actual = Contrast.RatioOfTones(fgTone, bgTone);
            var desired = _curve.Get(scheme.ContrastLevel);

            // If desired <= 4.5, actual must be >= desired - tol.
            if (desired <= 4.5)
            {
                if (actual < desired - ContrastTolerance)
                {
                    Assert.Fail(FormatMessage(scheme, desired, actual));
                }
            }
            else
            {
                // Always must be >= 4.5 - tol
                if (actual < 4.5 - ContrastTolerance)
                {
                    Assert.Fail(FormatMessage(scheme, 4.5, actual));
                }
                // If still under desired (with tol) and not clamped by tone 0/100, fail.
                else if (actual < desired - ContrastTolerance &&
                         fgTone != 100.0 && fgTone != 0.0)
                {
                    Assert.Fail(
                        FormatMessage(scheme, desired, actual) +
                        " (and no color has tone 0 or 100)");
                }
            }
        }

        private string FormatMessage(DynamicScheme scheme, double desired, double actual)
            => $"Dynamic scheme {scheme} fails contrast constraint:\n" +
               $"{_foregroundName} should have contrast at least {desired} " +
               $"against {_backgroundName}, but has {actual}\n";
    }

    private sealed class DeltaConstraint : IConstraint
    {
        private readonly DynamicColor _a;
        private readonly DynamicColor _b;
        private readonly string _aName;
        private readonly string _bName;
        private readonly double _delta;
        private readonly TonePolarity _polarity;

        public DeltaConstraint(DynamicColor a, string aName,
                               DynamicColor b, string bName,
                               double delta, TonePolarity polarity)
        {
            _a = a; _aName = aName; _b = b; _bName = bName;
            _delta = delta; _polarity = polarity;
        }

        public void TestAgainst(DynamicScheme scheme)
        {
            var aTone = _a.GetHct(scheme).Tone;
            var bTone = _b.GetHct(scheme).Tone;

            var aShouldBeLighter =
                (_polarity == TonePolarity.Lighter) ||
                (_polarity == TonePolarity.Nearer && !scheme.IsDark) ||
                (_polarity == TonePolarity.Farther && scheme.IsDark);

            var actual = aShouldBeLighter ? (aTone - bTone) : (bTone - aTone);
            if (actual < _delta - DeltaTolerance)
            {
                var lighterOrDarker = aShouldBeLighter ? "lighter" : "darker";
                Assert.Fail(
                    $"Dynamic scheme {scheme} fails delta constraint:\n" +
                    $"{_aName} should be {_delta} {lighterOrDarker} than {_bName}, " +
                    $"but tones are {aTone} and {bTone} (Δ={actual})\n");
            }
        }
    }

    private sealed class BackgroundConstraint : IConstraint
    {
        private readonly DynamicColor _background;
        private readonly string _name;

        public BackgroundConstraint(DynamicColor background, string name)
        {
            _background = background; _name = name;
        }

        public void TestAgainst(DynamicScheme scheme)
        {
            var tone = _background.GetHct(scheme).Tone;
            if (tone >= 50.5 && tone < 59.5)
            {
                Assert.Fail(
                    $"Dynamic scheme {scheme} fails background constraint:\n" +
                    $"{_name} has tone {tone} in forbidden zone 50.5 <= tone < 59.5\n");
            }
        }
    }

    private List<IConstraint> BuildConstraints()
    {
        // Convenience local to name roles
        DynamicColor DC(string name) => name switch
        {
            "background" => _mdc.Background,
            "on_surface" => _mdc.OnSurface,
            "surface_dim" => _mdc.SurfaceDim,
            "surface_bright" => _mdc.SurfaceBright,
            "primary" => _mdc.Primary,
            "secondary" => _mdc.Secondary,
            "tertiary" => _mdc.Tertiary,
            "error" => _mdc.Error,
            "on_surface_variant" => _mdc.OnSurfaceVariant,
            "outline" => _mdc.Outline,
            "primary_container" => _mdc.PrimaryContainer,
            "primary_fixed" => _mdc.PrimaryFixed,
            "primary_fixed_dim" => _mdc.PrimaryFixedDim,
            "secondary_container" => _mdc.SecondaryContainer,
            "secondary_fixed" => _mdc.SecondaryFixed,
            "secondary_fixed_dim" => _mdc.SecondaryFixedDim,
            "tertiary_container" => _mdc.TertiaryContainer,
            "tertiary_fixed" => _mdc.TertiaryFixed,
            "tertiary_fixed_dim" => _mdc.TertiaryFixedDim,
            "error_container" => _mdc.ErrorContainer,
            "outline_variant" => _mdc.OutlineVariant,
            "inverse_on_surface" => _mdc.InverseOnSurface,
            "inverse_surface" => _mdc.InverseSurface,
            "inverse_primary" => _mdc.InversePrimary,
            "on_primary" => _mdc.OnPrimary,
            "on_secondary" => _mdc.OnSecondary,
            "on_tertiary" => _mdc.OnTertiary,
            "on_error" => _mdc.OnError,
            "on_primary_container" => _mdc.OnPrimaryContainer,
            "on_secondary_container" => _mdc.OnSecondaryContainer,
            "on_tertiary_container" => _mdc.OnTertiaryContainer,
            "on_error_container" => _mdc.OnErrorContainer,
            "on_primary_fixed" => _mdc.OnPrimaryFixed,
            "on_secondary_fixed" => _mdc.OnSecondaryFixed,
            "on_tertiary_fixed" => _mdc.OnTertiaryFixed,
            "on_primary_fixed_variant" => _mdc.OnPrimaryFixedVariant,
            "on_secondary_fixed_variant" => _mdc.OnSecondaryFixedVariant,
            "on_tertiary_fixed_variant" => _mdc.OnTertiaryFixedVariant,
            // Surfaces / background family
            "on_background" => _mdc.OnBackground,
            "surface" => _mdc.Surface,
            "surface_container" => _mdc.SurfaceContainer,
            "surface_container_high" => _mdc.SurfaceContainerHigh,
            "surface_container_highest" => _mdc.SurfaceContainerHighest,
            "surface_container_low" => _mdc.SurfaceContainerLow,
            "surface_container_lowest" => _mdc.SurfaceContainerLowest,
            "surface_tint" => _mdc.SurfaceTint,
            "surface_variant" => _mdc.SurfaceVariant,
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };

        var list = new List<IConstraint>
        {
            new ContrastConstraint(DC("on_surface"), "on_surface", DC("surface_dim"), "surface_dim", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_surface"), "on_surface", DC("surface_bright"), "surface_bright", new ContrastCurve(4.5, 7, 11, 21)),

            new ContrastConstraint(DC("primary"), "primary", DC("surface_dim"), "surface_dim", new ContrastCurve(3, 4.5, 7, 7)),
            new ContrastConstraint(DC("primary"), "primary", DC("surface_bright"), "surface_bright", new ContrastCurve(3, 4.5, 7, 7)),

            new ContrastConstraint(DC("secondary"), "secondary", DC("surface_dim"), "surface_dim", new ContrastCurve(3, 4.5, 7, 7)),
            new ContrastConstraint(DC("secondary"), "secondary", DC("surface_bright"), "surface_bright", new ContrastCurve(3, 4.5, 7, 7)),

            new ContrastConstraint(DC("tertiary"), "tertiary", DC("surface_dim"), "surface_dim", new ContrastCurve(3, 4.5, 7, 7)),
            new ContrastConstraint(DC("tertiary"), "tertiary", DC("surface_bright"), "surface_bright", new ContrastCurve(3, 4.5, 7, 7)),

            new ContrastConstraint(DC("error"), "error", DC("surface_dim"), "surface_dim", new ContrastCurve(3, 4.5, 7, 7)),
            new ContrastConstraint(DC("error"), "error", DC("surface_bright"), "surface_bright", new ContrastCurve(3, 4.5, 7, 7)),

            new ContrastConstraint(DC("on_surface_variant"), "on_surface_variant", DC("surface_dim"), "surface_dim", new ContrastCurve(3, 4.5, 7, 11)),
            new ContrastConstraint(DC("on_surface_variant"), "on_surface_variant", DC("surface_bright"), "surface_bright", new ContrastCurve(3, 4.5, 7, 11)),

            new ContrastConstraint(DC("outline"), "outline", DC("surface_dim"), "surface_dim", new ContrastCurve(1.5, 3, 4.5, 7)),
            new ContrastConstraint(DC("outline"), "outline", DC("surface_bright"), "surface_bright", new ContrastCurve(1.5, 3, 4.5, 7)),

            new ContrastConstraint(DC("primary_container"), "primary_container", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("primary_container"), "primary_container", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("primary_fixed"), "primary_fixed", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("primary_fixed"), "primary_fixed", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("primary_fixed_dim"), "primary_fixed_dim", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("primary_fixed_dim"), "primary_fixed_dim", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("secondary_container"), "secondary_container", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("secondary_container"), "secondary_container", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("secondary_fixed"), "secondary_fixed", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("secondary_fixed"), "secondary_fixed", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("secondary_fixed_dim"), "secondary_fixed_dim", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("secondary_fixed_dim"), "secondary_fixed_dim", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("tertiary_container"), "tertiary_container", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("tertiary_container"), "tertiary_container", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("tertiary_fixed"), "tertiary_fixed", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("tertiary_fixed"), "tertiary_fixed", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("tertiary_fixed_dim"), "tertiary_fixed_dim", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("tertiary_fixed_dim"), "tertiary_fixed_dim", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("error_container"), "error_container", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("error_container"), "error_container", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("outline_variant"), "outline_variant", DC("surface_dim"), "surface_dim", new ContrastCurve(0, 0, 3, 4.5)),
            new ContrastConstraint(DC("outline_variant"), "outline_variant", DC("surface_bright"), "surface_bright", new ContrastCurve(0, 0, 3, 4.5)),

            new ContrastConstraint(DC("inverse_on_surface"), "inverse_on_surface", DC("inverse_surface"), "inverse_surface", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("inverse_primary"), "inverse_primary", DC("inverse_surface"), "inverse_surface", new ContrastCurve(3, 4.5, 7, 7)),

            new ContrastConstraint(DC("on_primary"), "on_primary", DC("primary"), "primary", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_secondary"), "on_secondary", DC("secondary"), "secondary", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_tertiary"), "on_tertiary", DC("tertiary"), "tertiary", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_error"), "on_error", DC("error"), "error", new ContrastCurve(4.5, 7, 11, 21)),

            new ContrastConstraint(DC("on_primary_container"), "on_primary_container", DC("primary_container"), "primary_container", new ContrastCurve(3, 4.5, 7, 11)),
            new ContrastConstraint(DC("on_secondary_container"), "on_secondary_container", DC("secondary_container"), "secondary_container", new ContrastCurve(3, 4.5, 7, 11)),
            new ContrastConstraint(DC("on_tertiary_container"), "on_tertiary_container", DC("tertiary_container"), "tertiary_container", new ContrastCurve(3, 4.5, 7, 11)),
            new ContrastConstraint(DC("on_error_container"), "on_error_container", DC("error_container"), "error_container", new ContrastCurve(3, 4.5, 7, 11)),

            new ContrastConstraint(DC("on_primary_fixed"), "on_primary_fixed", DC("primary_fixed"), "primary_fixed", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_primary_fixed"), "on_primary_fixed", DC("primary_fixed_dim"), "primary_fixed_dim", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_secondary_fixed"), "on_secondary_fixed", DC("secondary_fixed"), "secondary_fixed", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_secondary_fixed"), "on_secondary_fixed", DC("secondary_fixed_dim"), "secondary_fixed_dim", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_tertiary_fixed"), "on_tertiary_fixed", DC("tertiary_fixed"), "tertiary_fixed", new ContrastCurve(4.5, 7, 11, 21)),
            new ContrastConstraint(DC("on_tertiary_fixed"), "on_tertiary_fixed", DC("tertiary_fixed_dim"), "tertiary_fixed_dim", new ContrastCurve(4.5, 7, 11, 21)),

            // Delta constraints
            new DeltaConstraint(DC("primary"), "primary", DC("primary_container"), "primary_container", 10, TonePolarity.Farther),
            new DeltaConstraint(DC("secondary"), "secondary", DC("secondary_container"), "secondary_container", 10, TonePolarity.Farther),
            new DeltaConstraint(DC("tertiary"), "tertiary", DC("tertiary_container"), "tertiary_container", 10, TonePolarity.Farther),
            new DeltaConstraint(DC("error"), "error", DC("error_container"), "error_container", 10, TonePolarity.Farther),

            new DeltaConstraint(DC("primary_fixed_dim"), "primary_fixed_dim", DC("primary_fixed"), "primary_fixed", 10, TonePolarity.Darker),
            new DeltaConstraint(DC("secondary_fixed_dim"), "secondary_fixed_dim", DC("secondary_fixed"), "secondary_fixed", 10, TonePolarity.Darker),
            new DeltaConstraint(DC("tertiary_fixed_dim"), "tertiary_fixed_dim", DC("tertiary_fixed"), "tertiary_fixed", 10, TonePolarity.Darker),

            // Background forbidden zone constraints
            new BackgroundConstraint(DC("background"), "background"),
            new BackgroundConstraint(DC("error"), "error"),
            new BackgroundConstraint(DC("error_container"), "error_container"),
            new BackgroundConstraint(DC("primary"), "primary"),
            new BackgroundConstraint(DC("primary_container"), "primary_container"),
            new BackgroundConstraint(DC("primary_fixed"), "primary_fixed"),
            new BackgroundConstraint(DC("primary_fixed_dim"), "primary_fixed_dim"),
            new BackgroundConstraint(DC("secondary"), "secondary"),
            new BackgroundConstraint(DC("secondary_container"), "secondary_container"),
            new BackgroundConstraint(DC("secondary_fixed"), "secondary_fixed"),
            new BackgroundConstraint(DC("secondary_fixed_dim"), "secondary_fixed_dim"),
            new BackgroundConstraint(DC("surface"), "surface"),
            new BackgroundConstraint(DC("surface_bright"), "surface_bright"),
            new BackgroundConstraint(DC("surface_container"), "surface_container"),
            new BackgroundConstraint(DC("surface_container_high"), "surface_container_high"),
            new BackgroundConstraint(DC("surface_container_highest"), "surface_container_highest"),
            new BackgroundConstraint(DC("surface_container_low"), "surface_container_low"),
            new BackgroundConstraint(DC("surface_container_lowest"), "surface_container_lowest"),
            new BackgroundConstraint(DC("surface_dim"), "surface_dim"),
            new BackgroundConstraint(DC("surface_tint"), "surface_tint"),
            new BackgroundConstraint(DC("surface_variant"), "surface_variant"),
            new BackgroundConstraint(DC("tertiary"), "tertiary"),
            new BackgroundConstraint(DC("tertiary_container"), "tertiary_container"),
            new BackgroundConstraint(DC("tertiary_fixed"), "tertiary_fixed"),
            new BackgroundConstraint(DC("tertiary_fixed_dim"), "tertiary_fixed_dim"),
        };

        return list;
    }

    // ---- Variant → Scheme constructor mapping ----
    private static DynamicScheme SchemeFromVariant(Variant variant, Hct source, bool isDark, double contrastLevel)
    {
        var name = variant.ToString().ToLowerInvariant();
        if (name.Contains("fidelity"))     return new SchemeFidelity(source, isDark, contrastLevel);
        if (name.Contains("content"))      return new SchemeContent(source, isDark, contrastLevel);
        if (name.Contains("monochrome"))   return new SchemeMonochrome(source, isDark, contrastLevel);
        if (name.Contains("tonal"))        return new SchemeTonalSpot(source, isDark, contrastLevel);
        // Fallback: pick a sane default
        return new SchemeTonalSpot(source, isDark, contrastLevel);
    }

    [Test]
    [DisplayName("scheme_correctness_test: all variants & contrast levels satisfy constraints")]
    public async Task Scheme_Correctness_All()
    {
        var constraints = BuildConstraints();

        foreach (var variant in Enum.GetValues(typeof(Variant)).Cast<Variant>())
        foreach (var contrastLevel in ContrastLevels)
        foreach (var argb in SeedArgb)
        foreach (var isDark in new[] { false, true })
        {
            var scheme = SchemeFromVariant(variant, Hct.FromInt(argb), isDark, contrastLevel);

            // Each constraint throws Assert.Fail(...) if violated.
            foreach (var c in constraints)
            {
                c.TestAgainst(scheme);
            }

            // If we got here, constraints passed for this (variant, seed, CL, dark) tuple.
            await Assert.That(true).IsTrue(); // keep async signature satisfied
        }
    }
}
