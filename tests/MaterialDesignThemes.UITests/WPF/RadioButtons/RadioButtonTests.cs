namespace MaterialDesignThemes.UITests.WPF.RadioButtons;

public class RadioButtonTests : TestBase
{
    [Test]
    [DoesNotRequireInteraction]
    [Arguments("MaterialDesignRadioButton", null, Dock.Left)]
    [Arguments("MaterialDesignRadioButton", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignRadioButton", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignRadioButton", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignRadioButton", Dock.Bottom, Dock.Top)]
    [Arguments("MaterialDesignUserForegroundRadioButton", null, Dock.Left)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Bottom, Dock.Top)]
    public async Task ContentPlacement_DocksGlyphOnOppositeSide(string styleKey, Dock? placement, Dock expectedGlyphDock)
    {
        string placementAttribute = placement is not null
            ? $"materialDesign:RadioButtonAssist.ContentPlacement=\"{placement}\""
            : "";
        var radioButton = await LoadXaml<RadioButton>($$"""
            <RadioButton Content="Label"
                         Style="{StaticResource {{styleKey}}}"
                         {{placementAttribute}}
                         HorizontalAlignment="Center"
                         VerticalAlignment="Center" />
            """);

        var glyph = await radioButton.GetElement<FrameworkElement>("IconWrapper");

        await Assert.That(await glyph.GetProperty<Dock>(DockPanel.DockProperty)).IsEqualTo(expectedGlyphDock);
    }
}
