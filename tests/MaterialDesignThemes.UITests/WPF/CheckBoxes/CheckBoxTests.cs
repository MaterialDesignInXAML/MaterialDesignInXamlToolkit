namespace MaterialDesignThemes.UITests.WPF.CheckBoxes;

public class CheckBoxTests : TestBase
{
    [Test]
    [DoesNotRequireInteraction]
    [Arguments("MaterialDesignCheckBox", null, Dock.Left)]
    [Arguments("MaterialDesignCheckBox", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignCheckBox", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignCheckBox", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignCheckBox", Dock.Bottom, Dock.Top)]
    [Arguments("MaterialDesignUserForegroundCheckBox", null, Dock.Left)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Bottom, Dock.Top)]
    public async Task ContentPlacement_DocksGlyphOnOppositeSide(string styleKey, Dock? placement, Dock expectedGlyphDock)
    {
        string placementAttribute = placement is not null
            ? $"materialDesign:CheckBoxAssist.ContentPlacement=\"{placement}\""
            : "";

        var checkBox = await LoadXaml<CheckBox>($$"""
            <CheckBox Content="Label"
                      Style="{StaticResource {{styleKey}}}"
                      {{placementAttribute}}
                      HorizontalAlignment="Center"
                      VerticalAlignment="Center" />
            """);

        var glyph = await checkBox.GetElement<FrameworkElement>("IconWrapper");

        await Assert.That(await glyph.GetProperty<Dock>(DockPanel.DockProperty)).IsEqualTo(expectedGlyphDock);
    }
}
