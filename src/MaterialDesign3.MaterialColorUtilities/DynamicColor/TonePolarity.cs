namespace MaterialColorUtilities;

public enum TonePolarity
{
    Darker,
    Lighter,
    RelativeDarker,
    RelativeLighter,
    [Obsolete("Use DeltaConstraint instead.")]
    Nearer,
    [Obsolete("Use DeltaConstraint instead.")]
    Farther,
}
