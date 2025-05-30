using System.Windows.Media;

using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class CustomColorThemeTests
{
    [StaTheory]
    [MethodDataSource(nameof(GetThemeValues))]
    public async Task WhenValueIsMissingThemeIsNotSet(BaseTheme? baseTheme, Color? primaryColor, Color? secondaryColor)
    {
        //Arrange
        var bundledTheme = new CustomColorTheme();

        //Act
        bundledTheme.BaseTheme = baseTheme;
        bundledTheme.PrimaryColor = primaryColor;
        bundledTheme.SecondaryColor = secondaryColor;

        //Assert
        await Assert.That(() => bundledTheme.GetTheme()).ThrowsExactly<InvalidOperationException>();
    }

    public static IEnumerable<object?[]> GetThemeValues()
    {
        yield return new object?[] { null, null, null };
        yield return new object?[] { BaseTheme.Light, null, null };
        yield return new object?[] { BaseTheme.Inherit, null, null };
        yield return new object?[] { null, Colors.Blue, null };
        yield return new object?[] { BaseTheme.Light, Colors.Blue, null };
        yield return new object?[] { BaseTheme.Inherit, Colors.Blue, null };
        yield return new object?[] { null, null, Colors.Blue };
        yield return new object?[] { BaseTheme.Light, null, Colors.Blue };
        yield return new object?[] { BaseTheme.Inherit, null, Colors.Blue };
    }

    [Test, STAThreadExecutor]
    public async Task WhenAllValuesAreSetThemeIsSet()
    {
        //Arrange
        var bundledTheme = new CustomColorTheme();

        //Act
        bundledTheme.BaseTheme = BaseTheme.Light;
        bundledTheme.PrimaryColor = Colors.Fuchsia;
        bundledTheme.SecondaryColor = Colors.Lime;

        //Assert
        Theme theme = bundledTheme.GetTheme();
        await Assert.That(theme.PrimaryMid.Color).IsEqualTo(Colors.Fuchsia);
        await Assert.That(theme.SecondaryMid.Color).IsEqualTo(Colors.Lime);

        var lightTheme = new Theme();
        lightTheme.SetLightTheme();
        await Assert.That(theme.Foreground).IsEqualTo(lightTheme.Foreground);
        Assert.NotEqual(default, theme.Foreground);
    }
}
