namespace MaterialColorUtilities;

public static class ColorSpecs
{
    private static readonly ColorSpec SPEC_2021 = new ColorSpec2021();
    private static readonly ColorSpec SPEC_2025 = new ColorSpec2025();

    public static ColorSpec Get() => Get(SpecVersion.SPEC_2021);

    public static ColorSpec Get(SpecVersion specVersion) => specVersion == SpecVersion.SPEC_2025 ? SPEC_2025 : SPEC_2021;
}