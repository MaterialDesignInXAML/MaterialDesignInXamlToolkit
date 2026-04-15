using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MaterialDesignThemes.UITests.WPF.PackIcon;

public class PackIconTests : TestBase
{
    [Test]
    [Arguments(14)]
    [Arguments(60)]
    public async Task PackIcon_MatchSizeWith_SyncsSizeWithSource(double fontSize)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>($$"""
            <StackPanel Orientation="Horizontal">
              <materialDesign:PackIcon VerticalAlignment="Center"
                                       Kind="Home"
                                       MatchSizeWith="{Binding ElementName=MyTextBox}" />
              <TextBlock x:Name="MyTextBox"
                         VerticalAlignment="Center"
                         FontSize="{{fontSize}}"
                         Text="Some text" />
            </StackPanel>
            """);

        var icon = await stackPanel.GetElement<Wpf.PackIcon>("/PackIcon");
        var textBox = await stackPanel.GetElement<TextBlock>("/TextBlock");

        double iconHeight = await icon.GetActualHeight();
        double iconWidth = await icon.GetActualWidth();
        double textBoxHeight = await textBox.GetActualHeight();

        await Assert.That(iconHeight).IsEqualTo(iconWidth);
        await Assert.That(iconHeight).IsEqualTo(textBoxHeight);

        recorder.Success();
    }
}
