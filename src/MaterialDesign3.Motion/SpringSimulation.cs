namespace MaterialDesignThemes.Motion;

internal sealed class SpringSimulation
{
    private double _naturalFrequency = Math.Sqrt(SpringConstants.StiffnessVeryLow);
    private float _finalPosition;
    private float _dampingRatio = SpringConstants.DampingRatioNoBouncy;

    public SpringSimulation(float finalPosition)
    {
        _finalPosition = finalPosition;
    }

    public float FinalPosition
    {
        get => _finalPosition;
        set => _finalPosition = value;
    }

    public float Stiffness
    {
        get => (float)(_naturalFrequency * _naturalFrequency);
        set
        {
            if (value <= 0f)
            {
                Preconditions.ThrowIllegalArgumentException("Spring stiffness constant must be positive.");
            }

            _naturalFrequency = Math.Sqrt(value);
        }
    }

    public float DampingRatio
    {
        get => _dampingRatio;
        set
        {
            if (value < 0f)
            {
                Preconditions.ThrowIllegalArgumentException("Damping ratio must be non-negative");
            }

            _dampingRatio = value;
        }
    }

    public float GetAcceleration(float lastDisplacement, float lastVelocity)
    {
        var adjustedDisplacement = lastDisplacement - _finalPosition;

        var stiffness = _naturalFrequency * _naturalFrequency;
        var damping = 2.0 * _naturalFrequency * _dampingRatio;

        return (float)(-stiffness * adjustedDisplacement - damping * lastVelocity);
    }

    internal Motion UpdateValues(float lastDisplacement, float lastVelocity, TimeSpan timeElapsed)
    {
        var adjustedDisplacement = lastDisplacement - _finalPosition;
        var deltaT = timeElapsed.TotalSeconds;
        var dampingRatioSquared = _dampingRatio * _dampingRatio;
        var r = -_dampingRatio * _naturalFrequency;

        double displacement;
        double currentVelocity;

        if (_dampingRatio > 1f)
        {
            var s = _naturalFrequency * Math.Sqrt(dampingRatioSquared - 1.0);
            var gammaPlus = r + s;
            var gammaMinus = r - s;

            var coeffB = (gammaMinus * adjustedDisplacement - lastVelocity) / (gammaMinus - gammaPlus);
            var coeffA = adjustedDisplacement - coeffB;
            displacement = coeffA * Math.Exp(gammaMinus * deltaT) + coeffB * Math.Exp(gammaPlus * deltaT);
            currentVelocity =
                coeffA * gammaMinus * Math.Exp(gammaMinus * deltaT) +
                coeffB * gammaPlus * Math.Exp(gammaPlus * deltaT);
        }
        else if (Math.Abs(_dampingRatio - 1f) < 1e-6f)
        {
            var coeffA = adjustedDisplacement;
            var coeffB = lastVelocity + _naturalFrequency * adjustedDisplacement;
            var nfdt = -_naturalFrequency * deltaT;
            var exp = Math.Exp(nfdt);
            displacement = (coeffA + coeffB * deltaT) * exp;
            currentVelocity = ((coeffA + coeffB * deltaT) * exp * (-_naturalFrequency)) + coeffB * exp;
        }
        else
        {
            var dampedFrequency = _naturalFrequency * Math.Sqrt(1.0 - dampingRatioSquared);
            var cosCoeff = adjustedDisplacement;
            var sinCoeff = (1 / dampedFrequency) * (((-r * adjustedDisplacement) + lastVelocity));
            var dfdT = dampedFrequency * deltaT;
            var expTerm = Math.Exp(r * deltaT);
            var cos = Math.Cos(dfdT);
            var sin = Math.Sin(dfdT);
            displacement = expTerm * (cosCoeff * cos + sinCoeff * sin);
            currentVelocity =
                displacement * r +
                expTerm * ((-dampedFrequency * cosCoeff * sin) + (dampedFrequency * sinCoeff * cos));
        }

        var newValue = (float)(displacement + _finalPosition);
        var newVelocity = (float)currentVelocity;
        return new Motion(newValue, newVelocity);
    }
}
