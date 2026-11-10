namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class RadioButtonAssistTests
{
    [Test, STAThreadExecutor]
    public async Task TestContentPlacementProperty()
    {
        RadioButton testElement = new();

        await Assert.That(RadioButtonAssist.ContentPlacementProperty.Name).IsEqualTo("ContentPlacement");
        await Assert.That(RadioButtonAssist.GetContentPlacement(testElement)).IsEqualTo(Dock.Right);

        RadioButtonAssist.SetContentPlacement(testElement, Dock.Left);
        await Assert.That(RadioButtonAssist.GetContentPlacement(testElement)).IsEqualTo(Dock.Left);
    }

    [Test, STAThreadExecutor]
    // Dont parse the theme dictionaries in parallel, as it causes race conditions in WPFs XAML reader
    [NotInParallel("ThemeDictionaryParsing")]
    [Arguments("MaterialDesignRadioButton", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignRadioButton", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignRadioButton", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignRadioButton", Dock.Bottom, Dock.Top)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Right, Dock.Left)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Left, Dock.Right)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Top, Dock.Bottom)]
    [Arguments("MaterialDesignUserForegroundRadioButton", Dock.Bottom, Dock.Top)]
    public async Task ContentPlacement_DocksGlyphOnOppositeSide(string styleKey, Dock contentPlacement, Dock expectedGlyphDock)
    {
        RadioButton radioButton = new() { Content = "Label" };
        RadioButtonAssist.SetContentPlacement(radioButton, contentPlacement);
        radioButton.ApplyStyle(styleKey);

        var glyph = (FrameworkElement)radioButton.Template.FindName("IconWrapper", radioButton);
        await Assert.That(DockPanel.GetDock(glyph)).IsEqualTo(expectedGlyphDock);
    }
}
