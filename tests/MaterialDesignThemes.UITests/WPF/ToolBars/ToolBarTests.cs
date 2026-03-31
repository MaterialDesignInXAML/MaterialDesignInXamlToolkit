using System.ComponentModel;
using System.Windows.Media;


namespace MaterialDesignThemes.UITests.WPF.ToolBars;

public class ToolBarTests : TestBase
{
    [Description("Issue 2991")]
    [Test]
    [Arguments(Orientation.Horizontal, Dock.Right)]
    [Arguments(Orientation.Vertical, Dock.Bottom)]
    public async Task ToolBar_OverflowGrid_RespectsOrientation(Orientation orientation, Dock expectedOverflowGridDock)
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var toolBarTray = await LoadXaml<ToolBarTray>($@"
<ToolBarTray Orientation=""{orientation}"" DockPanel.Dock=""Top"">
  <ToolBar Style=""{{StaticResource MaterialDesignToolBar}}"">
    <Button Content=""{{materialDesign:PackIcon Kind=File}}""/>
  </ToolBar>
</ToolBarTray>");
        var overflowGrid = await toolBarTray.GetElement<Grid>("OverflowGrid");

        //Act
        Dock dock = await overflowGrid.GetProperty<Dock>(DockPanel.DockProperty);

        //Assert
        await Assert.That(dock).IsEqualTo(expectedOverflowGridDock);

        recorder.Success();
    }

    [Description("Issue 3694")]
    [Test]
    public async Task ToolBar_OverflowButton_InheritsCustomBackground()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var toolBarTray = await LoadXaml<ToolBarTray>(@"
<ToolBarTray DockPanel.Dock=""Top"">
  <ToolBar Style=""{StaticResource MaterialDesignToolBar}"" Background=""Fuchsia"" OverflowMode=""Always"">
    <Button Content=""{materialDesign:PackIcon Kind=File}""/>
  </ToolBar>
</ToolBarTray>");
        var overflowButton = await toolBarTray.GetElement<ToggleButton>("OverflowButton");

        //Act
        Color? overflowBackground = await overflowButton.GetBackgroundColor();

        //Assert
        await Assert.That(overflowBackground).IsEqualTo(Colors.Fuchsia);

        recorder.Success();
    }
}
