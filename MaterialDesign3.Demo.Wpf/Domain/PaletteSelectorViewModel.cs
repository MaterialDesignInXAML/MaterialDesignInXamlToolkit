using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain;

public class PaletteSelectorViewModel : ViewModelBase
{
    public PaletteSelectorViewModel()
    {
        Swatches = new SwatchesProvider().Swatches;
    }

    public IEnumerable<Swatch> Swatches { get; }

    public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o!));

    private static void ApplyPrimary(Swatch swatch)
        => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

    public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o!));

    private static void ApplyAccent(Swatch swatch)
    {
        if (swatch is { AccentExemplarHue: not null })
        {
            ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
        }
    }

    private static void ModifyTheme(Action<Theme> modificationAction)
    {
        var paletteHelper = new PaletteHelper();
        Theme theme = paletteHelper.GetTheme();

        modificationAction?.Invoke(theme);

        paletteHelper.SetTheme(theme);
    }
}
