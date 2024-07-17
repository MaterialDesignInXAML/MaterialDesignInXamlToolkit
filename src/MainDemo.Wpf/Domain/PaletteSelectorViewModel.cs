using MaterialDesignColors;
using MaterialDesignDemo.Shared.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain;

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

    public ICommand ApplySecondaryCommand { get; } = new AnotherCommandImplementation(o => ApplySecondary((Swatch)o!));

    private static void ApplySecondary(Swatch swatch)
    {
        if (swatch is { SecondaryExemplarHue: not null })
        {
            ModifyTheme(theme => theme.SetSecondaryColor(swatch.SecondaryExemplarHue.Color));
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
