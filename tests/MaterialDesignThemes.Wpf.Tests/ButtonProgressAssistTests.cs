using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

public class ButtonProgressAssistTests
{
    private readonly Button _testElement;

    public ButtonProgressAssistTests()
    {
        _testElement = new Button();
    }

    [Test, STAThreadExecutor]
    public async Task TestMinimumProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.MinimumProperty.Name).IsEqualTo("Minimum");
        await Assert.That(ButtonProgressAssist.GetMinimum(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetMinimum(_testElement, 133.14);
        await Assert.That(ButtonProgressAssist.GetMinimum(_testElement)).IsEqualTo(133.14);
    }

    [Test, STAThreadExecutor]
    public async Task TestMaximumProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.MaximumProperty.Name).IsEqualTo("Maximum");
        await Assert.That(ButtonProgressAssist.GetMaximum(_testElement)).IsEqualTo(100.0);

        // Assert setting works
        ButtonProgressAssist.SetMaximum(_testElement, 39.56);
        await Assert.That(ButtonProgressAssist.GetMaximum(_testElement)).IsEqualTo(39.56);
    }

    [Test, STAThreadExecutor]
    public async Task TestValueProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.ValueProperty.Name).IsEqualTo("Value");
        await Assert.That(ButtonProgressAssist.GetValue(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetValue(_testElement, 99.1);
        await Assert.That(ButtonProgressAssist.GetValue(_testElement)).IsEqualTo(99.1);
    }

    [Test, STAThreadExecutor]
    public async Task TestIsIndeterminateProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IsIndeterminateProperty.Name).IsEqualTo("IsIndeterminate");
        await Assert.That(ButtonProgressAssist.GetIsIndeterminate(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetIsIndeterminate(_testElement, false);
        await Assert.That(ButtonProgressAssist.GetIsIndeterminate(_testElement)).IsFalse();
    }

    [Test, STAThreadExecutor]
    public async Task TestIndicatorForegroundProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IndicatorForegroundProperty.Name).IsEqualTo("IndicatorForeground");
        await Assert.That(ButtonProgressAssist.GetIndicatorForeground(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetIndicatorForeground(_testElement, Brushes.LightBlue);
        await Assert.That(ButtonProgressAssist.GetIndicatorForeground(_testElement)).IsEqualTo(Brushes.LightBlue);
    }

    [Test, STAThreadExecutor]
    public async Task TestIndicatorBackgroundProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IndicatorBackgroundProperty.Name).IsEqualTo("IndicatorBackground");
        await Assert.That(ButtonProgressAssist.GetIndicatorBackground(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetIndicatorBackground(_testElement, Brushes.DarkGoldenrod);
        await Assert.That(ButtonProgressAssist.GetIndicatorBackground(_testElement)).IsEqualTo(Brushes.DarkGoldenrod);
    }

    [Test, STAThreadExecutor]
    public async Task TestIsIndicatorVisibleProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IsIndicatorVisibleProperty.Name).IsEqualTo("IsIndicatorVisible");
        await Assert.That(ButtonProgressAssist.GetIsIndicatorVisible(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetIsIndicatorVisible(_testElement, true);
        await Assert.That(ButtonProgressAssist.GetIsIndicatorVisible(_testElement)).IsTrue();
    }

    [Test, STAThreadExecutor]
    public async Task TestOpacityProperty()
    {
        // Assert defaults
        await Assert.That(ButtonProgressAssist.OpacityProperty.Name).IsEqualTo("Opacity");
        await Assert.That(ButtonProgressAssist.GetOpacity(_testElement)).IsEqualTo(default);

        // Assert setting works
        ButtonProgressAssist.SetOpacity(_testElement, 311);
        await Assert.That(ButtonProgressAssist.GetOpacity(_testElement)).IsEqualTo(311);
    }

}
