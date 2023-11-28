﻿using MaterialDesignThemes.UITests.Samples.AutoSuggestBoxes;
using MaterialDesignThemes.UITests.Samples.AutoSuggestTextBoxes;
using Xunit.Sdk;

namespace MaterialDesignThemes.UITests.WPF.AutoSuggestBoxes;

public class AutoSuggestBoxTests : TestBase
{
    public AutoSuggestBoxTests(ITestOutputHelper output)
        : base(output)
    {
        AttachedDebuggerToRemoteProcess = true;
    }

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
        Assert.True(await suggestBox.GetIsSuggestionOpen());
        Assert.True(await popup.GetIsOpen());

        //Validates these elements are found
        await AssertExists(suggestionListBox, "Bananas");
        await AssertExists(suggestionListBox, "Beans");

        //Validate other items are hidden
        await AssertExists(suggestionListBox, "Apples", false);
        await AssertExists(suggestionListBox, "Mtn Dew", false);
        await AssertExists(suggestionListBox, "Orange", false);

        recorder.Success();
    }

    [Fact]
    public async Task CanChoiceItem_FromTheSuggestions_AssertTheTextUpdated()
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
        Assert.True(await suggestBox.GetIsSuggestionOpen());
        Assert.True(await popup.GetIsOpen());

        double? lastHeight = null;
        await Wait.For(async () =>
        {
            double currentHeight = await suggestionListBox.GetActualHeight();

            bool rv = currentHeight == lastHeight && currentHeight > 50;
            lastHeight = currentHeight;
            if (!rv)
            {
                await Task.Delay(100);
            }
            return rv;
        });

        //Choose Item from the list
        var bananas = await suggestionListBox.GetElement<ListBoxItem>("/ListBoxItem[0]");
        await bananas.MoveCursorTo();
        await recorder.SaveScreenshot("BeforeClick");
        await bananas.LeftClick();
        var suggestBoxText = await suggestBox.GetText();
        //Validate that the current text is the same as the selected item
        Assert.Equal("Bananas", suggestBoxText);

        recorder.Success();
    }

    [Fact]
    public async Task CanFilterItems_WithCollectionView_FiltersSuggestions()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement userControl = await LoadUserControl<AutoSuggestTextBoxWithCollectionView>();
        IVisualElement<AutoSuggestBox> suggestBox = await userControl.GetElement<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        //Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("B"));


        //Assert
        Assert.True(await suggestBox.GetIsSuggestionOpen());
        Assert.True(await popup.GetIsOpen());

        //Validates these elements are found
        await AssertExists(suggestionListBox, "Bananas");
        await AssertExists(suggestionListBox, "Beans");

        //Validate other items are hidden
        await AssertExists(suggestionListBox, "Apples", false);
        await AssertExists(suggestionListBox, "Mtn Dew", false);
        await AssertExists(suggestionListBox, "Orange", false);

        recorder.Success();
    }

    private static async Task AssertExists(IVisualElement<ListBox> suggestionListBox, string text, bool existsOrNotCheck = true)
    {
        try
        {
            _ = await suggestionListBox.GetElement(ElementQuery.PropertyExpression<TextBlock>(x => x.Text, text));
            Assert.True(existsOrNotCheck);
        }
        catch (Exception e) when (e is not TrueException)
        {
            Assert.False(existsOrNotCheck);
        }
    }
}
