using System.ComponentModel;


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
}
