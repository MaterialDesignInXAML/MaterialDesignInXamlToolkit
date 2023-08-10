using MaterialDesignThemes.UITests.Samples.AutoSuggestTextBoxes;

namespace MaterialDesignThemes.UITests.WPF.AutoSuggestBoxes;

public class AutoSuggestBoxTests : TestBase
{
    public AutoSuggestBoxTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
    public async Task CanFilterItems_WithSuggestionsAndDisplayMember_FiltersSuggestions()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        //Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("B"));

        //Assert
        Assert.True(await suggestBox.GetIsPopupOpen());
        Assert.True(await popup.GetIsOpen());

        //Validates these elements are found
        _ = suggestionListBox.GetElement(ElementQuery.PropertyExpression<TextBlock>(x => x.Text, "Bananas"));
        _ = suggestionListBox.GetElement(ElementQuery.PropertyExpression<TextBlock>(x => x.Text, "Beans"));

        //TODO: Validate other items are hidden

        recorder.Success();
    }

    //TODO: Test case with ICollectionSource
}
