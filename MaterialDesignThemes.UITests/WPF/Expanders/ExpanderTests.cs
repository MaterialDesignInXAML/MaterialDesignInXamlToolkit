using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.Expanders;

public class ExpanderTests : TestBase
{
    public ExpanderTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
    [Description("Issue 2791")]
    public async Task TextBoxInHeader_CanBeFocused()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<StackPanel> stackPanel = await LoadXaml<StackPanel>(
            """
            <StackPanel>
              <TextBox />
              <Expander>
                <Expander.Header>
                  <TextBox />
                </Expander.Header>
                <TextBlock Text="Some content" />
              </Expander>
            </StackPanel>
            """);
        IVisualElement<Expander> expander = await stackPanel.GetElement<Expander>();
        IVisualElement<TextBox> textBox = await expander.GetElement<TextBox>();
        await textBox.MoveKeyboardFocus();

        Assert.True(await textBox.GetIsKeyboardFocusWithin());

        recorder.Success();
    }
}
