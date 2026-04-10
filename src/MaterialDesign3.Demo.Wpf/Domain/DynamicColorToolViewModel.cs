using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialColorUtilities;
using MaterialDesignDemo.Shared.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain;

public class DynamicColorToolViewModel : ViewModelBase
{
    private readonly MaterialDynamicColors _materialDynamicColors = new();
    private readonly PaletteHelper _paletteHelper = new();

    public DynamicColorToolViewModel()
    {
        CopyColorCommand = new AnotherCommandImplementation(CopyColorToClipboard, CanCopyColorToClipboard);

        if (_paletteHelper.GetThemeManager() is { } themeManager)
        {
            themeManager.ThemeChanged += (_, _) =>
            {
                if (SelectedThemeMode == DynamicColorThemeMode.System)
                {
                    UpdateComputedState();
                }
            };
        }

        UpdateComputedState();
    }

    public ICommand CopyColorCommand { get; }

    public IReadOnlyList<EnumOption<DynamicColorSourceMode>> SourceModes { get; } =
    [
        new(DynamicColorSourceMode.Auto, "Auto"),
        new(DynamicColorSourceMode.Custom, "Custom")
    ];

    public IReadOnlyList<EnumOption<DynamicColorThemeMode>> ThemeModeOptions { get; } =
    [
        new(DynamicColorThemeMode.System, "System Theme"),
        new(DynamicColorThemeMode.Light, "Light"),
        new(DynamicColorThemeMode.Dark, "Dark")
    ];

    public IReadOnlyList<EnumOption<Variant>> VariantOptions { get; } =
    [
        new(Variant.Monochrome, "Monochrome"),
        new(Variant.Neutral, "Neutral"),
        new(Variant.TonalSpot, "Tonal Spot"),
        new(Variant.Vibrant, "Vibrant"),
        new(Variant.Expressive, "Expressive"),
        new(Variant.Fidelity, "Fidelity"),
        new(Variant.Content, "Content"),
        new(Variant.Rainbow, "Rainbow"),
        new(Variant.FruitSalad, "Fruit Salad")
    ];

    public IReadOnlyList<EnumOption<SpecVersion>> SpecVersionOptions { get; } =
    [
        new(SpecVersion.Spec2021, "Spec 2021"),
        new(SpecVersion.Spec2025, "Spec 2025")
    ];

    public IReadOnlyList<EnumOption<Platform>> PlatformOptions { get; } =
    [
        new(Platform.Phone, "Phone"),
        new(Platform.Watch, "Watch")
    ];

    public Color PrimaryColor
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(PrimaryBrush));
                OnPropertyChanged(nameof(PrimaryForegroundBrush));
                OnPropertyChanged(nameof(PrimaryColorHex));
                OnPropertyChanged(nameof(PrimaryOverrideColor));
                UpdateComputedState();
            }
        }
    } = Color.FromRgb(0x67, 0x50, 0xA4);

    public DynamicColorSourceMode PrimarySourceMode
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(IsPrimaryCustomColorEnabled));
                OnPropertyChanged(nameof(PrimaryOverrideColor));
                OnPropertyChanged(nameof(PrimarySourceDescription));
                UpdateComputedState();
            }
        }
    }

    public Color SecondaryColor
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(SecondaryBrush));
                OnPropertyChanged(nameof(SecondaryForegroundBrush));
                OnPropertyChanged(nameof(SecondaryColorHex));
                OnPropertyChanged(nameof(SecondaryOverrideColor));
                UpdateComputedState();
            }
        }
    } = Color.FromRgb(0x62, 0x5B, 0x71);

    public DynamicColorSourceMode SecondarySourceMode
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(IsSecondaryCustomColorEnabled));
                OnPropertyChanged(nameof(SecondaryOverrideColor));
                OnPropertyChanged(nameof(SecondarySourceDescription));
                UpdateComputedState();
            }
        }
    }

    public Color TertiaryColor
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(TertiaryBrush));
                OnPropertyChanged(nameof(TertiaryForegroundBrush));
                OnPropertyChanged(nameof(TertiaryColorHex));
                OnPropertyChanged(nameof(TertiaryOverrideColor));
                UpdateComputedState();
            }
        }
    } = Color.FromRgb(0x7D, 0x52, 0x60);

    public DynamicColorSourceMode TertiarySourceMode
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(IsTertiaryCustomColorEnabled));
                OnPropertyChanged(nameof(TertiaryOverrideColor));
                OnPropertyChanged(nameof(TertiarySourceDescription));
                UpdateComputedState();
            }
        }
    }

    public DynamicColorThemeMode SelectedThemeMode
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                UpdateComputedState();
            }
        }
    } = DynamicColorThemeMode.System;

    public Variant SelectedVariant
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                UpdateComputedState();
            }
        }
    } = Variant.Neutral;

    public SpecVersion SelectedSpecVersion
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                UpdateComputedState();
            }
        }
    } = SpecVersion.Spec2025;

    public Platform SelectedPlatform
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                UpdateComputedState();
            }
        }
    } = Platform.Phone;

    public double ContrastLevel
    {
        get;
        set
        {
            if (SetProperty(ref field, ClampContrastLevel(value)))
            {
                OnPropertyChanged(nameof(ContrastLevelText));
                UpdateComputedState();
            }
        }
    } = 0.5;

    public string ContrastLevelText => $"{ContrastLevel:P0} ({ContrastLevel:0.00})";

    public Color CustomPaletteColor
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(CustomPaletteBrush));
                OnPropertyChanged(nameof(CustomPaletteForegroundBrush));
                OnPropertyChanged(nameof(CustomPaletteColorHex));
                UpdateComputedState();
            }
        }
    } = Color.FromRgb(0xA3, 0x17, 0x1A);

    public DynamicColorGridTile? Primary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Secondary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Tertiary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Error { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnPrimary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSecondary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnTertiary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnError { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? PrimaryContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SecondaryContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? TertiaryContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? ErrorContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnPrimaryContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSecondaryContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnTertiaryContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnErrorContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? PrimaryFixed { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? PrimaryFixedDim { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SecondaryFixed { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SecondaryFixedDim { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? TertiaryFixed { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? TertiaryFixedDim { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? InverseSurface { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnPrimaryFixed { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSecondaryFixed { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnTertiaryFixed { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? InverseOnSurface { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnPrimaryFixedVariant { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSecondaryFixedVariant { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnTertiaryFixedVariant { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? InversePrimary { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceDim { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Surface { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceBright { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceContainerLowest { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceContainerLow { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceContainerHigh { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SurfaceContainerHighest { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSurface { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSurfaceVariant { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Outline { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OutlineVariant { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Scrim { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Shadow { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? WarningHigh { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? WarningLow { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Information { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? Safe { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnWarningHigh { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnWarningLow { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnInformation { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSafe { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? WarningHighContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? WarningLowContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? InformationContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? SafeContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnWarningHighContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnWarningLowContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnInformationContainer { get; private set => SetProperty(ref field, value); }

    public DynamicColorGridTile? OnSafeContainer { get; private set => SetProperty(ref field, value); }

    public IReadOnlyList<DynamicColorGridTile> CustomTonalPaletteTiles
    {
        get;
        private set => SetProperty(ref field, value);
    } = [];

    public IReadOnlyList<DynamicColorGridTile> CustomSemanticColorTiles
    {
        get;
        private set => SetProperty(ref field, value);
    } = [];

    public Brush PrimaryBrush => CreateBrush(PrimaryColor);

    public Brush PrimaryForegroundBrush => CreateBrush(GetContrastingColor(PrimaryColor));

    public string PrimaryColorHex => PrimaryColor.ToString();

    public bool IsPrimaryCustomColorEnabled => PrimarySourceMode == DynamicColorSourceMode.Custom;

    public Color? PrimaryOverrideColor => IsPrimaryCustomColorEnabled ? PrimaryColor : null;

    public string PrimarySourceDescription => IsPrimaryCustomColorEnabled
        ? "This color is used as the seed and as an explicit primary override."
        : "This color is used as the seed while primary stays scheme-generated.";

    public Brush SecondaryBrush => CreateBrush(SecondaryColor);

    public Brush SecondaryForegroundBrush => CreateBrush(GetContrastingColor(SecondaryColor));

    public string SecondaryColorHex => SecondaryColor.ToString();

    public bool IsSecondaryCustomColorEnabled => SecondarySourceMode == DynamicColorSourceMode.Custom;

    public Color? SecondaryOverrideColor => IsSecondaryCustomColorEnabled ? SecondaryColor : null;

    public string SecondarySourceDescription => IsSecondaryCustomColorEnabled
        ? "Custom color override is enabled for the secondary palette."
        : "Secondary stays scheme-generated until you switch this card to Custom.";

    public Brush TertiaryBrush => CreateBrush(TertiaryColor);

    public Brush TertiaryForegroundBrush => CreateBrush(GetContrastingColor(TertiaryColor));

    public string TertiaryColorHex => TertiaryColor.ToString();

    public bool IsTertiaryCustomColorEnabled => TertiarySourceMode == DynamicColorSourceMode.Custom;

    public Color? TertiaryOverrideColor => IsTertiaryCustomColorEnabled ? TertiaryColor : null;

    public string TertiarySourceDescription => IsTertiaryCustomColorEnabled
        ? "Custom color override is enabled for the tertiary palette."
        : "Tertiary stays scheme-generated until you switch this card to Custom.";

    public Brush CustomPaletteBrush => CreateBrush(CustomPaletteColor);

    public Brush CustomPaletteForegroundBrush => CreateBrush(GetContrastingColor(CustomPaletteColor));

    public string CustomPaletteColorHex => CustomPaletteColor.ToString();

    private void UpdateComputedState()
    {
        DynamicScheme scheme = CreateDynamicScheme();
        UpdateGeneratedColorTiles(scheme);
        UpdateCustomColorState(scheme);
    }

    private void UpdateGeneratedColorTiles(DynamicScheme scheme)
    {
        Primary = CreateTile("Primary", _materialDynamicColors.Primary, _materialDynamicColors.OnPrimary, scheme, 0, 2);
        Secondary = CreateTile("Secondary", _materialDynamicColors.Secondary, _materialDynamicColors.OnSecondary, scheme, 2, 2);
        Tertiary = CreateTile("Tertiary", _materialDynamicColors.Tertiary, _materialDynamicColors.OnTertiary, scheme, 4, 2);
        Error = CreateTile("Error", _materialDynamicColors.Error, _materialDynamicColors.OnError, scheme, 6, 2);

        OnPrimary = CreateTile("On Primary", _materialDynamicColors.OnPrimary, _materialDynamicColors.Primary, scheme, 0, 2);
        OnSecondary = CreateTile("On Secondary", _materialDynamicColors.OnSecondary, _materialDynamicColors.Secondary, scheme, 2, 2);
        OnTertiary = CreateTile("On Tertiary", _materialDynamicColors.OnTertiary, _materialDynamicColors.Tertiary, scheme, 4, 2);
        OnError = CreateTile("On Error", _materialDynamicColors.OnError, _materialDynamicColors.Error, scheme, 6, 2);

        PrimaryContainer = CreateTile("Primary Container", _materialDynamicColors.PrimaryContainer, _materialDynamicColors.OnPrimaryContainer, scheme, 0, 2);
        SecondaryContainer = CreateTile("Secondary Container", _materialDynamicColors.SecondaryContainer, _materialDynamicColors.OnSecondaryContainer, scheme, 2, 2);
        TertiaryContainer = CreateTile("Tertiary Container", _materialDynamicColors.TertiaryContainer, _materialDynamicColors.OnTertiaryContainer, scheme, 4, 2);
        ErrorContainer = CreateTile("Error Container", _materialDynamicColors.ErrorContainer, _materialDynamicColors.OnErrorContainer, scheme, 6, 2);

        OnPrimaryContainer = CreateTile("On Primary Container", _materialDynamicColors.OnPrimaryContainer, _materialDynamicColors.PrimaryContainer, scheme, 0, 2);
        OnSecondaryContainer = CreateTile("On Secondary Container", _materialDynamicColors.OnSecondaryContainer, _materialDynamicColors.SecondaryContainer, scheme, 2, 2);
        OnTertiaryContainer = CreateTile("On Tertiary Container", _materialDynamicColors.OnTertiaryContainer, _materialDynamicColors.TertiaryContainer, scheme, 4, 2);
        OnErrorContainer = CreateTile("On Error Container", _materialDynamicColors.OnErrorContainer, _materialDynamicColors.ErrorContainer, scheme, 6, 2);

        PrimaryFixed = CreateTile("Primary Fixed", _materialDynamicColors.PrimaryFixed, _materialDynamicColors.OnPrimaryFixed, scheme, 0, 1);
        PrimaryFixedDim = CreateTile("Primary Fixed Dim", _materialDynamicColors.PrimaryFixedDim, _materialDynamicColors.OnPrimaryFixed, scheme, 1, 1);
        SecondaryFixed = CreateTile("Secondary Fixed", _materialDynamicColors.SecondaryFixed, _materialDynamicColors.OnSecondaryFixed, scheme, 2, 1);
        SecondaryFixedDim = CreateTile("Secondary Fixed Dim", _materialDynamicColors.SecondaryFixedDim, _materialDynamicColors.OnSecondaryFixed, scheme, 3, 1);
        TertiaryFixed = CreateTile("Tertiary Fixed", _materialDynamicColors.TertiaryFixed, _materialDynamicColors.OnTertiaryFixed, scheme, 4, 1);
        TertiaryFixedDim = CreateTile("Tertiary Fixed Dim", _materialDynamicColors.TertiaryFixedDim, _materialDynamicColors.OnTertiaryFixed, scheme, 5, 1);
        InverseSurface = CreateTile("Inverse Surface", _materialDynamicColors.InverseSurface, _materialDynamicColors.InverseOnSurface, scheme, 6, 2);

        OnPrimaryFixed = CreateTile("On Primary Fixed", _materialDynamicColors.OnPrimaryFixed, _materialDynamicColors.PrimaryFixed, scheme, 0, 2);
        OnSecondaryFixed = CreateTile("On Secondary Fixed", _materialDynamicColors.OnSecondaryFixed, _materialDynamicColors.SecondaryFixed, scheme, 2, 2);
        OnTertiaryFixed = CreateTile("On Tertiary Fixed", _materialDynamicColors.OnTertiaryFixed, _materialDynamicColors.TertiaryFixed, scheme, 4, 2);
        InverseOnSurface = CreateTile("Inverse On Surface", _materialDynamicColors.InverseOnSurface, _materialDynamicColors.InverseSurface, scheme, 6, 2);

        OnPrimaryFixedVariant = CreateTile("On Primary Fixed Variant", _materialDynamicColors.OnPrimaryFixedVariant, _materialDynamicColors.PrimaryFixed, scheme, 0, 2);
        OnSecondaryFixedVariant = CreateTile("On Secondary Fixed Variant", _materialDynamicColors.OnSecondaryFixedVariant, _materialDynamicColors.SecondaryFixed, scheme, 2, 2);
        OnTertiaryFixedVariant = CreateTile("On Tertiary Fixed Variant", _materialDynamicColors.OnTertiaryFixedVariant, _materialDynamicColors.TertiaryFixed, scheme, 4, 2);
        InversePrimary = CreateTile("Inverse Primary", _materialDynamicColors.InversePrimary, _materialDynamicColors.InverseSurface, scheme, 6, 2);

        SurfaceDim = CreateTile("Surface Dim", _materialDynamicColors.SurfaceDim, _materialDynamicColors.OnSurface, scheme, 0, 2);
        Surface = CreateTile("Surface", _materialDynamicColors.Surface, _materialDynamicColors.OnSurface, scheme, 2, 2);
        SurfaceBright = CreateTile("Surface Bright", _materialDynamicColors.SurfaceBright, _materialDynamicColors.OnSurface, scheme, 4, 2);

        SurfaceContainerLowest = CreateTile("Surface Container Lowest", _materialDynamicColors.SurfaceContainerLowest, _materialDynamicColors.OnSurface, scheme, 0, 1);
        SurfaceContainerLow = CreateTile("Surface Container Low", _materialDynamicColors.SurfaceContainerLow, _materialDynamicColors.OnSurface, scheme, 1, 1);
        SurfaceContainer = CreateTile("Surface Container", _materialDynamicColors.SurfaceContainer, _materialDynamicColors.OnSurface, scheme, 2, 1);
        SurfaceContainerHigh = CreateTile("Surface Container High", _materialDynamicColors.SurfaceContainerHigh, _materialDynamicColors.OnSurface, scheme, 3, 1);
        SurfaceContainerHighest = CreateTile("Surface Container Highest", _materialDynamicColors.SurfaceContainerHighest, _materialDynamicColors.OnSurface, scheme, 4, 1);

        OnSurface = CreateTile("On Surface", _materialDynamicColors.OnSurface, _materialDynamicColors.Surface, scheme, 0, 1);
        OnSurfaceVariant = CreateTile("On Surface Variant", _materialDynamicColors.OnSurfaceVariant, _materialDynamicColors.SurfaceVariant, scheme, 1, 1);
        Outline = CreateTile("Outline", _materialDynamicColors.Outline, _materialDynamicColors.Surface, scheme, 2, 1);
        OutlineVariant = CreateTile("Outline Variant", _materialDynamicColors.OutlineVariant, _materialDynamicColors.Surface, scheme, 3, 1);
        Scrim = CreateTile("Scrim", _materialDynamicColors.Scrim, _materialDynamicColors.Surface, scheme, 4, 1);
        Shadow = CreateTile("Shadow", _materialDynamicColors.Shadow, _materialDynamicColors.Surface, scheme, 5, 1);

        WarningHigh = CreateTile("Warning High", _materialDynamicColors.WarningHigh, _materialDynamicColors.OnWarningHigh, scheme, 0, 2);
        WarningLow = CreateTile("Warning Low", _materialDynamicColors.WarningLow, _materialDynamicColors.OnWarningLow, scheme, 2, 2);
        Information = CreateTile("Information", _materialDynamicColors.Information, _materialDynamicColors.OnInformation, scheme, 4, 2);
        Safe = CreateTile("Safe", _materialDynamicColors.Safe, _materialDynamicColors.OnSafe, scheme, 6, 2);

        OnWarningHigh = CreateTile("On Warning High", _materialDynamicColors.OnWarningHigh, _materialDynamicColors.WarningHigh, scheme, 0, 2);
        OnWarningLow = CreateTile("On Warning Low", _materialDynamicColors.OnWarningLow, _materialDynamicColors.WarningLow, scheme, 2, 2);
        OnInformation = CreateTile("On Information", _materialDynamicColors.OnInformation, _materialDynamicColors.Information, scheme, 4, 2);
        OnSafe = CreateTile("On Safe", _materialDynamicColors.OnSafe, _materialDynamicColors.Safe, scheme, 6, 2);

        WarningHighContainer = CreateTile("Warning High Container", _materialDynamicColors.WarningHighContainer, _materialDynamicColors.OnWarningHighContainer, scheme, 0, 2);
        WarningLowContainer = CreateTile("Warning Low Container", _materialDynamicColors.WarningLowContainer, _materialDynamicColors.OnWarningLowContainer, scheme, 2, 2);
        InformationContainer = CreateTile("Information Container", _materialDynamicColors.InformationContainer, _materialDynamicColors.OnInformationContainer, scheme, 4, 2);
        SafeContainer = CreateTile("Safe Container", _materialDynamicColors.SafeContainer, _materialDynamicColors.OnSafeContainer, scheme, 6, 2);

        OnWarningHighContainer = CreateTile("On Warning High Container", _materialDynamicColors.OnWarningHighContainer, _materialDynamicColors.WarningHighContainer, scheme, 0, 2);
        OnWarningLowContainer = CreateTile("On Warning Low Container", _materialDynamicColors.OnWarningLowContainer, _materialDynamicColors.WarningLowContainer, scheme, 2, 2);
        OnInformationContainer = CreateTile("On Information Container", _materialDynamicColors.OnInformationContainer, _materialDynamicColors.InformationContainer, scheme, 4, 2);
        OnSafeContainer = CreateTile("On Safe Container", _materialDynamicColors.OnSafeContainer, _materialDynamicColors.SafeContainer, scheme, 6, 2);
    }

    private DynamicScheme CreateDynamicScheme()
    {
        return DynamicSchemeFactory.Create(
            PrimaryColor,
            SelectedVariant,
            ResolveIsDarkMode(),
            ContrastLevel,
            SelectedPlatform,
            SelectedSpecVersion,
            PrimaryOverrideColor,
            SecondaryOverrideColor,
            TertiaryOverrideColor,
            null,
            null,
            null);
    }

    private bool ResolveIsDarkMode()
    {
        return SelectedThemeMode switch
        {
            DynamicColorThemeMode.Light => false,
            DynamicColorThemeMode.Dark => true,
            _ => _paletteHelper.GetTheme().GetBaseTheme() == BaseTheme.Dark
        };
    }

    private DynamicColorGridTile CreateTile(
        string label,
        DynamicColor backgroundColor,
        DynamicColor foregroundColor,
        DynamicScheme scheme,
        int column,
        int columnSpan)
    {
        Color background = backgroundColor.GetColor(scheme);
        Color foreground = foregroundColor.GetColor(scheme);

        return new DynamicColorGridTile(
            label,
            background.ToString(),
            CreateBrush(background),
            CreateBrush(foreground),
            column,
            columnSpan);
    }

    private DynamicColorGridTile CreateTile(
        string label,
        Color background,
        Color foreground,
        int column,
        int columnSpan)
    {
        return new DynamicColorGridTile(
            label,
            background.ToString(),
            CreateBrush(background),
            CreateBrush(foreground),
            column,
            columnSpan);
    }

    private static Brush CreateBrush(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();

        return brush;
    }

    private static double ClampContrastLevel(double value)
    {
        if (value < 0)
        {
            return 0;
        }

        if (value > 1)
        {
            return 1;
        }

        return value;
    }

    private static Color GetContrastingColor(Color color)
    {
        double normalizedRed = color.R / 255d;
        double normalizedGreen = color.G / 255d;
        double normalizedBlue = color.B / 255d;

        double luminance = (0.2126d * normalizedRed) + (0.7152d * normalizedGreen) + (0.0722d * normalizedBlue);

        return luminance >= 0.5d ? Colors.Black : Colors.White;
    }

    private void UpdateCustomColorState(DynamicScheme baseScheme)
    {
        TonalPalette tonalPalette = TonalPalette.FromInt(ColorUtils.ArgbFromColor(CustomPaletteColor));

        List<DynamicColorGridTile> tonalTiles = [];
        for (int tone = 0; tone <= 100; tone += 5)
        {
            tonalTiles.Add(CreateToneTile(tonalPalette, tone));
        }

        CustomTonalPaletteTiles = tonalTiles;

        DynamicScheme customSemanticScheme = CreateCustomSemanticScheme(baseScheme, tonalPalette);

        Color paletteKey = _materialDynamicColors.ErrorPaletteKeyColor.GetColor(customSemanticScheme);
        Color role = _materialDynamicColors.Error.GetColor(customSemanticScheme);
        Color onRole = _materialDynamicColors.OnError.GetColor(customSemanticScheme);
        Color container = _materialDynamicColors.ErrorContainer.GetColor(customSemanticScheme);
        Color onContainer = _materialDynamicColors.OnErrorContainer.GetColor(customSemanticScheme);

        CustomSemanticColorTiles =
        [
            CreateTile("Palette Key", paletteKey, GetContrastingColor(paletteKey), 0, 1),
            CreateTile("Role", role, onRole, 0, 1),
            CreateTile("On Role", onRole, role, 0, 1),
            CreateTile("Container", container, onContainer, 0, 1),
            CreateTile("On Container", onContainer, container, 0, 1)
        ];
    }

    private DynamicColorGridTile CreateToneTile(TonalPalette tonalPalette, int tone)
    {
        Color background = ColorUtils.ColorFromArgb(tonalPalette.Tone(tone));
        Color foreground = tone <= 40
            ? ColorUtils.ColorFromArgb(tonalPalette.Tone(100))
            : ColorUtils.ColorFromArgb(tonalPalette.Tone(0));

        return CreateTile($"Tone {tone}", background, foreground, 0, 1);
    }

    private DynamicScheme CreateCustomSemanticScheme(DynamicScheme baseScheme, TonalPalette customPalette)
    {
        return new DynamicScheme(
            baseScheme.SourceColorHct,
            baseScheme.Variant,
            baseScheme.IsDark,
            baseScheme.ContrastLevel,
            baseScheme.PlatformType,
            baseScheme.SpecVersion,
            baseScheme.PrimaryPalette,
            baseScheme.SecondaryPalette,
            baseScheme.TertiaryPalette,
            baseScheme.NeutralPalette,
            baseScheme.NeutralVariantPalette,
            customPalette);
    }

    private static bool CanCopyColorToClipboard(object? parameter)
    {
        return parameter is string colorHex && !string.IsNullOrWhiteSpace(colorHex);
    }

    private static void CopyColorToClipboard(object? parameter)
    {
        if (parameter is not string colorHex || string.IsNullOrWhiteSpace(colorHex))
        {
            return;
        }

        Clipboard.SetDataObject(colorHex);
    }
}
