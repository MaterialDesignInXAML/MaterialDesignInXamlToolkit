using System.Windows.Media;
using System.Windows.Documents;

namespace MaterialDesignThemes.UITests.WPF.Alerts;

public class AlertTests : TestBase
{
    [Test]
    [DoesNotRequireInteraction]
    [Arguments(Severity.Info, "MaterialDesign.Brush.Info", PackIconKind.InformationOutline)]
    [Arguments(Severity.Success, "MaterialDesign.Brush.Success", PackIconKind.CheckCircleOutline)]
    [Arguments(Severity.Warning, "MaterialDesign.Brush.Warning", PackIconKind.AlertOutline)]
    [Arguments(Severity.Error, "MaterialDesign.Brush.Error", PackIconKind.AlertCircleOutline)]
    public async Task WithSeverity_UsesSemanticColorAndIcon(Severity severity, string expectedBrushName, PackIconKind expectedIcon)
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>($"""<materialDesign:Alert Severity="{severity}" Content="Hello" />""");
        Color semanticColor = await GetThemeColor(expectedBrushName);
        IVisualElement<Border> tint = await alert.GetElement<Border>("Tint");
        IVisualElement<Wpf.PackIcon> icon = await alert.GetElement<Wpf.PackIcon>("Icon");

        await Assert.That(await alert.GetForegroundColor()).IsEqualTo(semanticColor);
        await Assert.That(await tint.GetVisibility()).IsEqualTo(Visibility.Visible);
        await Assert.That(await tint.GetBackgroundColor()).IsEqualTo(semanticColor);
        await Assert.That(await icon.GetVisibility()).IsEqualTo(Visibility.Visible);
        await Assert.That(await icon.GetProperty<PackIconKind>(nameof(Wpf.PackIcon.Kind))).IsEqualTo(expectedIcon);
    }

    [Test]
    [DoesNotRequireInteraction]
    public async Task SeverityNone_HasNoTintAndUsesDefaultForeground()
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>("""<materialDesign:Alert Content="Hello" />""");
        Color foreground = await GetThemeColor("MaterialDesign.Brush.Foreground");
        IVisualElement<Border> tint = await alert.GetElement<Border>("Tint");
        IVisualElement<Wpf.PackIcon> icon = await alert.GetElement<Wpf.PackIcon>("Icon");

        await Assert.That(await alert.GetForegroundColor()).IsEqualTo(foreground);
        await Assert.That(await tint.GetVisibility()).IsEqualTo(Visibility.Collapsed);
        await Assert.That(await icon.GetProperty<PackIconKind>(nameof(Wpf.PackIcon.Kind))).IsEqualTo(PackIconKind.NoteTextOutline);
    }

    [Test]
    [DoesNotRequireInteraction]
    public async Task ExplicitIconKind_OverridesSeverityIcon()
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>("""<materialDesign:Alert Severity="Error" IconKind="Bell" Content="Hello" />""");
        IVisualElement<Wpf.PackIcon> icon = await alert.GetElement<Wpf.PackIcon>("Icon");

        await Assert.That(await icon.GetProperty<PackIconKind>(nameof(Wpf.PackIcon.Kind))).IsEqualTo(PackIconKind.Bell);
    }

    [Test]
    [DoesNotRequireInteraction]
    public async Task NullIconKind_HidesIcon()
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>("""<materialDesign:Alert Severity="Info" IconKind="{x:Null}" Content="Hello" />""");
        IVisualElement<Wpf.PackIcon> icon = await alert.GetElement<Wpf.PackIcon>("Icon");

        await Assert.That(await icon.GetVisibility()).IsEqualTo(Visibility.Collapsed);
    }

    [Test]
    [DoesNotRequireInteraction]
    [Arguments(null, Visibility.Collapsed)]
    [Arguments("Some title", Visibility.Visible)]
    public async Task Title_ControlsTitleVisibility(string? title, Visibility expectedVisibility)
    {
        string titleAttribute = title is null ? "" : $"""Title="{title}" """;
        IVisualElement<Alert> alert = await LoadXaml<Alert>($"""<materialDesign:Alert {titleAttribute}Content="Hello" />""");
        IVisualElement<ContentPresenter> titlePresenter = await alert.GetElement<ContentPresenter>("Title");

        await Assert.That(await titlePresenter.GetVisibility()).IsEqualTo(expectedVisibility);
    }

    [Test]
    [DoesNotRequireInteraction]
    [Arguments(false, Visibility.Collapsed)]
    [Arguments(true, Visibility.Visible)]
    public async Task ShowCloseButton_ControlsCloseButtonVisibility(bool showCloseButton, Visibility expectedVisibility)
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>($"""<materialDesign:Alert ShowCloseButton="{showCloseButton}" Content="Hello" />""");
        IVisualElement<Button> closeButton = await alert.GetElement<Button>(Alert.CloseButtonPartName);

        await Assert.That(await closeButton.GetVisibility()).IsEqualTo(expectedVisibility);
    }

    [Test]
    public async Task ClickCloseButton_RaisesCloseClick()
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>("""<materialDesign:Alert Severity="Warning" ShowCloseButton="True" Content="Hello" />""");
        IVisualElement<Button> closeButton = await alert.GetElement<Button>(Alert.CloseButtonPartName);
        var closeClickEvent = await alert.RegisterForEvent(nameof(Alert.CloseClick));

        await closeButton.LeftClick();

        await Wait.For(async () => await Assert.That((await closeClickEvent.GetInvocations()).Count).IsEqualTo(1));
        // Closing is left to the handler so the alert itself stays visible by default
        await Assert.That(await alert.GetVisibility()).IsEqualTo(Visibility.Visible);
    }

    [Test]
    [DoesNotRequireInteraction]
    [Arguments(Elevation.Dp0, Visibility.Collapsed)]
    [Arguments(Elevation.Dp4, Visibility.Visible)]
    public async Task Elevation_ControlsShadowVisibility(Elevation elevation, Visibility expectedVisibility)
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>($"""<materialDesign:Alert materialDesign:ElevationAssist.Elevation="{elevation}" Content="Hello" />""");
        IVisualElement<AdornerDecorator> shadow = await alert.GetElement<AdornerDecorator>("Shadow");

        await Assert.That(await shadow.GetVisibility()).IsEqualTo(expectedVisibility);
    }

    [Test]
    [DoesNotRequireInteraction]
    public async Task CornerRadius_AppliedToTint()
    {
        IVisualElement<Alert> alert = await LoadXaml<Alert>("""<materialDesign:Alert Severity="Info" CornerRadius="9" Content="Hello" />""");
        IVisualElement<Border> tint = await alert.GetElement<Border>("Tint");

        await Assert.That((await tint.GetCornerRadius()).TopLeft).IsEqualTo(9);
    }
}
