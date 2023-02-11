using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.ContentControls;

public class ContentControlTests : TestBase
{
    public ContentControlTests(ITestOutputHelper output)
           : base(output)
    {
    }


    [Fact]
    [Description("Issue 2510")]
    public async Task ClearButton_InsideOfControlTemplate_CanStillClearContent()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var grid = await LoadXaml<ContentControl>(@"
<ContentControl>
  <ContentControl.ContentTemplate>
    <DataTemplate>
      <TextBox Text=""Some Text"" materialDesign:TextFieldAssist.HasClearButton=""True""/>
    </DataTemplate>
  </ContentControl.ContentTemplate>
</ContentControl>");
        var textBox = await grid.GetElement<TextBox>("/TextBox");
        var clearButton = await grid.GetElement<Button>("PART_ClearButton");

        string? initial = await textBox.GetText();

        //Act
        await clearButton.LeftClick();

        //Assert
        Assert.Equal("Some Text", initial);
        await Wait.For(async () =>
        {
            Assert.True(string.IsNullOrEmpty(await textBox.GetText()));
        });

        recorder.Success();
    }

}
