using System.Globalization;
using MaterialDesignThemes.Wpf.Converters.Internal;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class UpDownButtonsPaddingConverterTests
{
    [Test]
    public async Task Convert_WhenButtonsVisible_AddsButtonsWidthToRightPadding()
    {
        UpDownButtonsPaddingConverter converter = new();

        var result = (Thickness)converter.Convert([new Thickness(1, 2, 3, 4), 20d], typeof(Thickness), null!, CultureInfo.InvariantCulture);

        await Assert.That(result).IsEqualTo(new Thickness(1, 2, 23, 4));
    }

    [Test]
    public async Task Convert_WhenButtonsWidthIsNotFinite_ReturnsOriginalPadding()
    {
        UpDownButtonsPaddingConverter converter = new();

        var result = (Thickness)converter.Convert([new Thickness(1, 2, 3, 4), double.NaN], typeof(Thickness), null!, CultureInfo.InvariantCulture);

        await Assert.That(result).IsEqualTo(new Thickness(1, 2, 3, 4));
    }
}
