namespace MaterialColorUtilities;

public sealed class ToneDeltaPair
{
    public DynamicColor RoleA { get; }
    public DynamicColor RoleB { get; }
    public double Delta { get; }
    public TonePolarity Polarity { get; }
    public bool StayTogether { get; }
    public DeltaConstraint Constraint { get; }

    public ToneDeltaPair(DynamicColor roleA, DynamicColor roleB, double delta, TonePolarity polarity, bool stayTogether)
    {
        RoleA = roleA;
        RoleB = roleB;
        Delta = delta;
        Polarity = polarity;
        StayTogether = stayTogether;
        Constraint = DeltaConstraint.Exact;
    }

    public ToneDeltaPair(DynamicColor roleA, DynamicColor roleB, double delta, TonePolarity polarity, DeltaConstraint constraint)
    {
        RoleA = roleA;
        RoleB = roleB;
        Delta = delta;
        Polarity = polarity;
        StayTogether = true;
        Constraint = constraint;
    }
}