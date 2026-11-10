namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class CheckBoxAssistTests
{
    [Test, STAThreadExecutor]
    public async Task TestCheckBoxSizeProperty()
    {
        CheckBox testElement = new();

        // Assert defaults
        await Assert.That(CheckBoxAssist.CheckBoxSizeProperty.Name).IsEqualTo("CheckBoxSize");
        await Assert.That(CheckBoxAssist.GetCheckBoxSize(testElement)).IsEqualTo(18.0);

        // Assert setting works
        CheckBoxAssist.SetCheckBoxSize(testElement, 27.1);
        await Assert.That(CheckBoxAssist.GetCheckBoxSize(testElement)).IsEqualTo(27.1);
    }

    [Test, STAThreadExecutor]
    public async Task TestContentPlacementProperty()
    {
        CheckBox testElement = new();

        // Assert defaults
        await Assert.That(CheckBoxAssist.ContentPlacementProperty.Name).IsEqualTo("ContentPlacement");
        await Assert.That(CheckBoxAssist.GetContentPlacement(testElement)).IsEqualTo(Dock.Right);

        // Assert setting works
        CheckBoxAssist.SetContentPlacement(testElement, Dock.Top);
        await Assert.That(CheckBoxAssist.GetContentPlacement(testElement)).IsEqualTo(Dock.Top);
    }

    [Test, STAThreadExecutor]
    // Dont parse the theme dictionaries in parallel, as it causes race conditions in WPFs XAML reader
    [NotInParallel("ThemeDictionaryParsing")]
    [Arguments("MaterialDesignCheckBox", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignCheckBox", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignCheckBox", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignCheckBox", Dock.Bottom, Dock.Top)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignUserForegroundCheckBox", Dock.Bottom, Dock.Top)]
    public async Task ContentPlacement_DocksGlyphOnOppositeSide(string styleKey, Dock placement, Dock expectedGlyphDock)
    {
        CheckBox checkBox = new() { Content = "Label" };
        CheckBoxAssist.SetContentPlacement(checkBox, placement);
        checkBox.ApplyStyle(styleKey);

        var glyph = (FrameworkElement)checkBox.Template.FindName("IconWrapper", checkBox);
        await Assert.That(DockPanel.GetDock(glyph)).IsEqualTo(expectedGlyphDock);
    }
}
