namespace MaterialDesignThemes.Motion;

internal static class SpringEstimation
{
    public static TimeSpan EstimateAnimationDuration(float stiffness, float dampingRatio, float initialVelocity, float initialDisplacement, float delta)
    {
        if (Math.Abs(dampingRatio) < float.Epsilon)
        {
            return TimeSpan.MaxValue;
        }

        return EstimateAnimationDuration(
            stiffness: (double)stiffness,
            dampingRatio: (double)dampingRatio,
            initialVelocity: (double)initialVelocity,
            initialDisplacement: (double)initialDisplacement,
            delta: (double)delta);
    }

    public static TimeSpan EstimateAnimationDuration(double stiffness, double dampingRatio, double initialVelocity, double initialDisplacement, double delta)
    {
        var dampingCoefficient = 2.0 * dampingRatio * Math.Sqrt(stiffness);

        var partialRoot = dampingCoefficient * dampingCoefficient - 4.0 * stiffness;
        var partialRootReal = partialRoot < 0.0 ? 0.0 : Math.Sqrt(partialRoot);
        var partialRootImaginary = partialRoot < 0.0 ? Math.Sqrt(Math.Abs(partialRoot)) : 0.0;

        var firstRootReal = (-dampingCoefficient + partialRootReal) * 0.5;
        var firstRootImaginary = partialRootImaginary * 0.5;
        var secondRootReal = (-dampingCoefficient - partialRootReal) * 0.5;

        return EstimateDurationInternal(
            firstRootReal,
            firstRootImaginary,
            secondRootReal,
            dampingRatio,
            initialVelocity,
            initialDisplacement,
            delta);
    }

    public static TimeSpan EstimateAnimationDuration(double springConstant, double dampingCoefficient, double mass, double initialVelocity, double initialDisplacement, double delta)
    {
        var criticalDamping = 2.0 * Math.Sqrt(springConstant * mass);
        var dampingRatio = dampingCoefficient / criticalDamping;

        var partialRoot = dampingCoefficient * dampingCoefficient - 4.0 * mass * springConstant;
        var divisor = 1.0 / (2.0 * mass);
        var partialRootReal = partialRoot < 0.0 ? 0.0 : Math.Sqrt(partialRoot);
        var partialRootImaginary = partialRoot < 0.0 ? Math.Sqrt(Math.Abs(partialRoot)) : 0.0;

        var firstRootReal = (-dampingCoefficient + partialRootReal) * divisor;
        var firstRootImaginary = partialRootImaginary * divisor;
        var secondRootReal = (-dampingCoefficient - partialRootReal) * divisor;

        return EstimateDurationInternal(
            firstRootReal,
            firstRootImaginary,
            secondRootReal,
            dampingRatio,
            initialVelocity,
            initialDisplacement,
            delta);
    }

    private static double EstimateUnderDamped(double firstRootReal, double firstRootImaginary, double p0, double v0, double delta)
    {
        var r = firstRootReal;
        var c1 = p0;
        var c2 = (v0 - r * c1) / firstRootImaginary;
        var c = Math.Sqrt(c1 * c1 + c2 * c2);

        return Math.Log(delta / c) / r;
    }

    private static double EstimateCriticallyDamped(double firstRootReal, double p0, double v0, double delta)
    {
        var r = firstRootReal;
        var c1 = p0;
        var c2 = v0 - r * c1;

        var t1 = Math.Log(Math.Abs(delta / c1)) / r;
        double t2;
        if (Math.Abs(c2) < double.Epsilon)
        {
            t2 = double.NaN;
        }
        else
        {
            var guess = Math.Log(Math.Abs(delta / c2));
            var t = guess;
            for (var i = 0; i <= 5; i++)
            {
                t = guess - Math.Log(Math.Abs(t / r));
            }

            t2 = t / r;
        }

        var tCurr = t1.IsNotFinite()
            ? t2
            : t2.IsNotFinite()
                ? t1
                : Math.Max(t1, t2);

        var tInflection = -(r * c1 + c2) / (r * c2);
        var xInflection = c1 * Math.Exp(r * tInflection) + c2 * tInflection * Math.Exp(r * tInflection);

        double signedDelta;
        if (double.IsNaN(tInflection) || tInflection <= 0.0)
        {
            signedDelta = -delta;
        }
        else if (tInflection > 0.0 && -xInflection < delta)
        {
            if (c2 < 0.0 && c1 > 0.0)
            {
                tCurr = 0.0;
            }

            signedDelta = -delta;
        }
        else
        {
            tCurr = -(2.0 / r) - (c1 / c2);
            signedDelta = delta;
        }

        var tDelta = double.MaxValue;
        var iterations = 0;
        while (tDelta > 0.001 && iterations < 100)
        {
            iterations++;
            var tLast = tCurr;
            tCurr = IterateNewtonsMethod(
                tCurr,
                t => (c1 + c2 * t) * Math.Exp(r * t) + signedDelta,
                t => (c2 * (r * t + 1) + c1 * r) * Math.Exp(r * t));
            tDelta = Math.Abs(tLast - tCurr);
        }

        return tCurr;
    }

    private static double EstimateOverDamped(double firstRootReal, double secondRootReal, double p0, double v0, double delta)
    {
        var r1 = firstRootReal;
        var r2 = secondRootReal;
        var c2 = (r1 * p0 - v0) / (r1 - r2);
        var c1 = p0 - c2;

        var t1 = Math.Log(Math.Abs(delta / c1)) / r1;
        var t2 = Math.Log(Math.Abs(delta / c2)) / r2;

        var tCurr = t1.IsNotFinite()
            ? t2
            : t2.IsNotFinite()
                ? t1
                : Math.Max(t1, t2);

        var tInflection = Math.Log((c1 * r1) / (-c2 * r2)) / (r2 - r1);
        double signedDelta;
        if (double.IsNaN(tInflection) || tInflection <= 0.0)
        {
            signedDelta = -delta;
        }
        else if (tInflection > 0.0 && -EvaluateOverDampedPosition(c1, c2, r1, r2, tInflection) < delta)
        {
            if (c2 > 0.0 && c1 < 0.0)
            {
                tCurr = 0.0;
            }

            signedDelta = -delta;
        }
        else
        {
            tCurr = Math.Log(-(c2 * r2 * r2) / (c1 * r1 * r1)) / (r1 - r2);
            signedDelta = delta;
        }

        if (Math.Abs(c1 * r1 * Math.Exp(r1 * tCurr) + c2 * r2 * Math.Exp(r2 * tCurr)) < 0.0001)
        {
            return tCurr;
        }

        var tDelta = double.MaxValue;
        var iterations = 0;
        while (tDelta > 0.001 && iterations < 100)
        {
            iterations++;
            var tLast = tCurr;
            tCurr = IterateNewtonsMethod(
                tCurr,
                t => c1 * Math.Exp(r1 * t) + c2 * Math.Exp(r2 * t) + signedDelta,
                t => c1 * r1 * Math.Exp(r1 * t) + c2 * r2 * Math.Exp(r2 * t));
            tDelta = Math.Abs(tLast - tCurr);
        }

        return tCurr;
    }

    private static TimeSpan EstimateDurationInternal(double firstRootReal, double firstRootImaginary, double secondRootReal, double dampingRatio, double initialVelocity, double initialPosition, double delta)
    {
        if (Math.Abs(initialPosition) < double.Epsilon && Math.Abs(initialVelocity) < double.Epsilon)
        {
            return TimeSpan.Zero;
        }

        var v0 = initialPosition < 0 ? -initialVelocity : initialVelocity;
        var p0 = Math.Abs(initialPosition);

        double seconds = dampingRatio switch
        {
            > 1.0 => EstimateOverDamped(firstRootReal, secondRootReal, p0, v0, delta),
            < 1.0 => EstimateUnderDamped(firstRootReal, firstRootImaginary, p0, v0, delta),
            _ => EstimateCriticallyDamped(firstRootReal, p0, v0, delta),
        };

        if (!seconds.IsFinite() || seconds < 0.0)
        {
            return TimeSpan.MaxValue;
        }

        return TimeSpan.FromSeconds(seconds);
    }

    private static double IterateNewtonsMethod(double x, Func<double, double> fn, Func<double, double> fnPrime)
    {
        var fx = fn(x);
        var derivative = fnPrime(x);
        return x - fx / derivative;
    }

    private static bool IsNotFinite(this double value) => double.IsNaN(value) || double.IsInfinity(value);

    private static bool IsFinite(this double value) => !double.IsNaN(value) && !double.IsInfinity(value);

    private static double EvaluateOverDampedPosition(double c1, double c2, double r1, double r2, double time) =>
        c1 * Math.Exp(r1 * time) + c2 * Math.Exp(r2 * time);
}
