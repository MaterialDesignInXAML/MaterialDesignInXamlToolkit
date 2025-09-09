namespace MaterialColorUtilities;

public enum TonePolarity
{
    DARKER,
    LIGHTER,
    RELATIVE_DARKER,
    RELATIVE_LIGHTER,
    [Obsolete("Use DeltaConstraint instead.")]
    NEARER,
    [Obsolete("Use DeltaConstraint instead.")]
    FARTHER,
}