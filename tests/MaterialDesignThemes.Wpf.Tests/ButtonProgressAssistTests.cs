using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class ButtonProgressAssistTests
{

    [Test]
    public async Task TestMinimumProperty()
    {
        Button testElement = new();

        // Assert defaults
        await Assert.That(ButtonProgressAssist.MinimumProperty.Name).IsEqualTo("Minimum");
        await Assert.That(ButtonProgressAssist.GetMinimum(testElement)).IsEqualTo(default(double));

        // Assert setting works
        ButtonProgressAssist.SetMinimum(testElement, 133.14);
        await Assert.That(ButtonProgressAssist.GetMinimum(testElement)).IsEqualTo(133.14);
    }

    [Test]
    public async Task TestMaximumProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.MaximumProperty.Name).IsEqualTo("Maximum");
        await Assert.That(ButtonProgressAssist.GetMaximum(testElement)).IsEqualTo(100.0);

        // Assert setting works
        ButtonProgressAssist.SetMaximum(testElement, 39.56);
        await Assert.That(ButtonProgressAssist.GetMaximum(testElement)).IsEqualTo(39.56);
    }

    [Test]
    public async Task TestValueProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.ValueProperty.Name).IsEqualTo("Value");
        await Assert.That(ButtonProgressAssist.GetValue(testElement)).IsEqualTo(default(double));

        // Assert setting works
        ButtonProgressAssist.SetValue(testElement, 99.1);
        await Assert.That(ButtonProgressAssist.GetValue(testElement)).IsEqualTo(99.1);
    }

    [Test]
    public async Task TestIsIndeterminateProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IsIndeterminateProperty.Name).IsEqualTo("IsIndeterminate");
        await Assert.That(ButtonProgressAssist.GetIsIndeterminate(testElement)).IsEqualTo(default(bool));

        // Assert setting works
        ButtonProgressAssist.SetIsIndeterminate(testElement, false);
        await Assert.That(ButtonProgressAssist.GetIsIndeterminate(testElement)).IsFalse();
    }

    [Test]
    public async Task TestIndicatorForegroundProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IndicatorForegroundProperty.Name).IsEqualTo("IndicatorForeground");
        await Assert.That(ButtonProgressAssist.GetIndicatorForeground(testElement)).IsEqualTo(default(Brush));

        // Assert setting works
        ButtonProgressAssist.SetIndicatorForeground(testElement, Brushes.LightBlue);
        await Assert.That(ButtonProgressAssist.GetIndicatorForeground(testElement)).IsEqualTo(Brushes.LightBlue);
    }

    [Test]
    public async Task TestIndicatorBackgroundProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IndicatorBackgroundProperty.Name).IsEqualTo("IndicatorBackground");
        await Assert.That(ButtonProgressAssist.GetIndicatorBackground(testElement)).IsEqualTo(default(Brush));

        // Assert setting works
        ButtonProgressAssist.SetIndicatorBackground(testElement, Brushes.DarkGoldenrod);
        await Assert.That(ButtonProgressAssist.GetIndicatorBackground(testElement)).IsEqualTo(Brushes.DarkGoldenrod);
    }

    [Test]
    public async Task TestIsIndicatorVisibleProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.IsIndicatorVisibleProperty.Name).IsEqualTo("IsIndicatorVisible");
        await Assert.That(ButtonProgressAssist.GetIsIndicatorVisible(testElement)).IsEqualTo(default(bool));

        // Assert setting works
        ButtonProgressAssist.SetIsIndicatorVisible(testElement, true);
        await Assert.That(ButtonProgressAssist.GetIsIndicatorVisible(testElement)).IsTrue();
    }

    [Test]
    public async Task TestOpacityProperty()
    {
        Button testElement = new();
        // Assert defaults
        await Assert.That(ButtonProgressAssist.OpacityProperty.Name).IsEqualTo("Opacity");
        await Assert.That(ButtonProgressAssist.GetOpacity(testElement)).IsEqualTo(default(double));

        // Assert setting works
        ButtonProgressAssist.SetOpacity(testElement, 311);
        await Assert.That(ButtonProgressAssist.GetOpacity(testElement)).IsEqualTo(311);
    }

}
