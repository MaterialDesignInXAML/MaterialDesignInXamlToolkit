# Copilot Instructions for MaterialDesignInXamlToolkit

## Repository Overview

The MaterialDesignInXamlToolkit is a **theme library** for WPF applications that provides Material Design themes and styling. This is NOT a control library - the focus is on theming existing WPF controls and providing custom controls only when necessary to support Google's Material Design specifications.

### Key Principles
- Theme library, not control library
- Styling existing WPF controls to match Material Design
- Custom controls only for Google-specified components that don't exist in WPF
- No application or business logic belongs in this library
- Provide base tools and springboard for developers to create their own controls

## Architecture and Structure

### Core Projects
- **`MaterialDesignThemes.Wpf`** - Main theming library with styles, templates, and controls
- **`MaterialDesignColors.Wpf`** - Color palette and theme management
- **`MaterialDesignThemes.MahApps`** - Integration with MahApps.Metro
- **`MainDemo.Wpf`** - Primary demonstration application
- **`MaterialDesign3.Demo.Wpf`** - Material Design 3 demonstration
- **`MaterialDesignToolkit.ResourceGeneration`** - Build-time resource generation tools

### Key Technologies
- **WPF (Windows Presentation Foundation)** - UI framework
- **XAML** - Markup for UI definitions and styles  
- **Material Design** - Google's design system implementation
- **.NET 8** and **.NET Framework 4.7.2** - Target frameworks for the library
- **.NET 9 SDK** - Required for building (as specified in `global.json`)
- **C# 12.0** - Programming language
- **PowerShell** - Build automation scripts

## Development Environment

### Requirements
- **Windows** - Required for WPF development and compilation
- **.NET 9 SDK** - As specified in `global.json` (note: projects target .NET 8 and .NET Framework 4.7.2)
- **Visual Studio 2022** or **Visual Studio Code** with C# extension
- **PowerShell** - For build scripts

### Build and Test
```powershell
# Restore dependencies
dotnet restore MaterialDesignToolkit.Full.sln

# Build (requires Windows)
dotnet build MaterialDesignToolkit.Full.sln --configuration Release --no-restore -p:Platform="Any CPU" -p:TreatWarningsAsErrors=True

# Run tests
dotnet test MaterialDesignToolkit.Full.sln --configuration Release --no-build

# Build NuGet packages
.\build\BuildNugets.ps1 -MDIXVersion "x.x.x" -MDIXColorsVersion "x.x.x" -MDIXMahAppsVersion "x.x.x"
```

## Code Style and Conventions

### General Guidelines
- Follow standard Visual Studio settings with ReSharper suggestions
- Use .editorconfig settings (4-space indents for C#, 2-space for XAML/XML)
- Allman brace style (`csharp_new_line_before_open_brace = all`)
- No `this.` qualification unless necessary
- Prefer explicit types over `var` for built-in types
- Use PascalCase for public members, interfaces start with `I`

### C# Conventions
```csharp
// Preferred dependency property pattern
public static readonly DependencyProperty MyPropertyProperty =
    DependencyProperty.Register("MyProperty", typeof(string), typeof(MyControl), 
        new UIPropertyMetadata("DefaultValue", OnMyPropertyChanged));

public string MyProperty
{
    get => (string)GetValue(MyPropertyProperty);
    set => SetValue(MyPropertyProperty, value);
}

private static void OnMyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    var control = (MyControl)d;
    // Handle property change
}
```

### XAML Style Guidelines
- Use XamlStyler settings from `Settings.XamlStyler`
- 2-space indentation for XAML
- Keep first attribute on same line as element
- Order attributes according to defined groups
- Use `{StaticResource}` over `{DynamicResource}` where possible
- Follow resource naming: `MaterialDesign.Brush.Primary.Light`

## WPF and Material Design Context

### Theme Architecture
- **Base Themes**: Light and Dark variants
- **Color System**: Primary, Secondary, Surface, Background colors with variants
- **Elevation**: Shadow and overlay systems for depth
- **Typography**: Material Design text styles
- **Motion**: Transitions and animations

### Common Patterns
```csharp
// Theme modification pattern
private static void ModifyTheme(Action<Theme> modificationAction)
{
    var paletteHelper = new PaletteHelper();
    Theme theme = paletteHelper.GetTheme();
    
    modificationAction?.Invoke(theme);
    
    paletteHelper.SetTheme(theme);
}

// Color adjustment usage
theme.ColorAdjustment = new ColorAdjustment
{
    DesiredContrastRatio = desiredRatio,
    Contrast = contrastValue,
    Colors = colorSelection
};
```

### Resource Dictionary Patterns
```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes">
  
  <ResourceDictionary.MergedDictionaries>
    <materialDesign:BundledTheme BaseTheme="Light" PrimaryColor="DeepPurple" SecondaryColor="Lime" />
    <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/Generic.xaml" />
  </ResourceDictionary.MergedDictionaries>

</ResourceDictionary>
```

## Testing Approach

### Test Structure
- **Unit Tests**: `MaterialDesignThemes.Wpf.Tests`, `MaterialDesignColors.Wpf.Tests`
- **UI Tests**: `MaterialDesignThemes.UITests` - Visual/integration testing
- **Demo Applications**: Manual testing and showcasing functionality

### Test Patterns
```csharp
[Test]
public async Task ThemeTest_Example()
{
    await App.InitializeWithMaterialDesign(
        baseTheme: BaseTheme.Light,
        primary: PrimaryColor.Blue,
        secondary: SecondaryColor.Orange);
        
    // Test implementation
}
```

## Build Pipeline and Automation

### GitHub Actions Workflows
- **PR Verification**: `pr_verification.yml` - Build and test on PRs
- **Build Artifacts**: `build_artifacts.yml` - Main build pipeline
- **Release**: `release.yml` - Create releases and publish NuGets

### PowerShell Build Scripts
- **`BuildNugets.ps1`** - Package creation
- **`ApplyXamlStyler.ps1`** - Code formatting
- **`MigrateBrushes.ps1`** - Resource migration utilities
- **`UpdateNugets.ps1`** - Package management

## Domain-Specific Knowledge

### Material Design Implementation
- Follow Google Material Design guidelines strictly
- Implement elevation through shadows and overlays
- Use consistent color theming system
- Support both Material Design 2 and 3 specifications
- Ensure accessibility compliance (contrast ratios, touch targets)

### WPF Theming Best Practices
- Use `TemplateBinding` for connecting to parent properties
- Implement proper focus visuals and keyboard navigation
- Support high contrast mode and accessibility features
- Use appropriate triggers for state changes (hover, pressed, disabled)
- Leverage WPF's dependency property system effectively

### Resource Organization
- Brush resources: `MaterialDesign.Brush.*`
- Style resources: Clear, descriptive names matching WPF conventions
- Template resources: Match control types and variants
- Color resources: Follow Material Design naming (Primary, Secondary, Surface, etc.)

## API Design Guidelines

- **Maintain backward compatibility** - This is a widely-used library
- **Minimal public API surface** - Only expose what's necessary
- **Consistent naming** - Follow WPF and Material Design conventions
- **Proper documentation** - XML docs for all public APIs
- **Designer support** - Ensure controls work well in Visual Studio designer

## Common Tasks and Patterns

### Adding a New Style
1. Define in appropriate XAML resource dictionary
2. Follow existing naming conventions
3. Test in demo applications
4. Ensure accessibility compliance
5. Add to migration scripts if replacing existing styles

### Theme Modifications
1. Use `PaletteHelper` for runtime theme changes
2. Support both static and dynamic resource binding
3. Test with both Light and Dark themes
4. Verify color adjustments work properly

### Custom Control Development
1. Only when no WPF equivalent exists
2. Follow Material Design specifications exactly
3. Implement proper template parts and visual states
4. Support theming and color adjustments
5. Include comprehensive tests and demo usage

Remember: This library's primary goal is to provide a complete, high-quality Material Design theming solution for WPF applications while maintaining excellent performance and broad compatibility.