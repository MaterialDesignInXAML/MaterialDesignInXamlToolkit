using System.ComponentModel;
using MaterialDesignThemes.UITests.Samples.AutoSuggestBoxes;
using MaterialDesignThemes.UITests.Samples.AutoSuggestTextBoxes;
using TUnit.Core.Exceptions;
namespace MaterialDesignThemes.UITests.WPF.AutoSuggestBoxes;

public class AutoSuggestBoxTests : TestBase
{
    public AutoSuggestBoxTests()
    {
        AttachedDebuggerToRemoteProcess = true;
    }

    [Test]
    public async Task CanFilterItems_WithSuggestionsAndDisplayMember_FiltersSuggestions()
    {
        //Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        //Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("B"));


        //Assert
        await Assert.That(await suggestBox.GetIsSuggestionOpen()).IsTrue();
        await Assert.That(await popup.GetIsOpen()).IsTrue();

        //Validates these elements are found
        await AssertExists(suggestionListBox, "Bananas");
        await AssertExists(suggestionListBox, "Beans");

        //Validate other items are hidden
        await AssertExists(suggestionListBox, "Apples", false);
        await AssertExists(suggestionListBox, "Mtn Dew", false);
        await AssertExists(suggestionListBox, "Orange", false);
    }

    [Test]
    public async Task CanChoiceItem_FromTheSuggestions_AssertTheTextUpdated()
    {
        //Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        //Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("B"));

        //Assert
        await Assert.That(await suggestBox.GetIsSuggestionOpen()).IsTrue();
        await Assert.That(await popup.GetIsOpen()).IsTrue();

        double? lastHeight = null;
        await Wait.For(async () =>
        {
            double currentHeight = await suggestionListBox.GetActualHeight();

            bool rv = currentHeight == lastHeight && currentHeight > 50;
            lastHeight = currentHeight;
            if (!rv)
            {
                await Task.Delay(100, TestContext.Current!.Execution.CancellationToken);
            }
            return rv;
        });

        //Choose Item from the list
        var bananas = await suggestionListBox.GetElement<ListBoxItem>("/ListBoxItem[0]");
        await bananas.MoveCursorTo();
        await bananas.LeftClick();

        // Wait for the text to be updated
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);

        var suggestBoxText = await suggestBox.GetText();
        //Validate that the current text is the same as the selected item
        await Assert.That(suggestBoxText).IsEqualTo("Bananas");
    }

    [Test]
    public async Task CanFilterItems_WithCollectionView_FiltersSuggestions()
    {
        //Arrange
        IVisualElement userControl = await LoadUserControl<AutoSuggestTextBoxWithCollectionView>();
        IVisualElement<AutoSuggestBox> suggestBox = await userControl.GetElement<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        //Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("B"));


        //Assert
        await Assert.That(await suggestBox.GetIsSuggestionOpen()).IsTrue();
        await Assert.That(await popup.GetIsOpen()).IsTrue();

        //Validates these elements are found
        await AssertExists(suggestionListBox, "Bananas");
        await AssertExists(suggestionListBox, "Beans");

        //Validate other items are hidden
        await AssertExists(suggestionListBox, "Apples", false);
        await AssertExists(suggestionListBox, "Mtn Dew", false);
        await AssertExists(suggestionListBox, "Orange", false);
    }

    [Test]
    [Description("Issue 3761")]
    public async Task AutoSuggestBox_MovesFocusToNextElement_WhenPopupIsClosed()
    {
        // Arrange
        string xaml = """
            <StackPanel>
                <local:AutoSuggestTextBoxWithCollectionView x:Name="AutoSuggestBoxSample" />
                <TextBox x:Name="NextTextBox" />
            </StackPanel>
        """;

        IVisualElement<StackPanel> stackPanel = await LoadXaml<StackPanel>(xaml, ("local", typeof(AutoSuggestTextBoxWithCollectionView)));
        var suggestBoxSample = await stackPanel.GetElement<AutoSuggestTextBoxWithCollectionView>("AutoSuggestBoxSample");
        IVisualElement<AutoSuggestBox> suggestBox = await suggestBoxSample.GetElement<AutoSuggestBox>();
        IVisualElement<TextBox> nextTextBox = await stackPanel.GetElement<TextBox>("NextTextBox");

        // Act
        await suggestBox.MoveKeyboardFocus();
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);
        await suggestBox.SendInput(new KeyboardInput("B")); // Open the popup
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);
        await suggestBox.SendInput(new KeyboardInput(Key.Escape)); // Close the popup
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);
        await suggestBox.SendInput(new KeyboardInput(Key.Tab)); // Press TAB to focus the next element
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);

        // Assert
        await Assert.That(await suggestBox.GetIsFocused()).IsFalse();
        await Assert.That(await nextTextBox.GetIsFocused()).IsTrue();
    }

    [Test]
    [Description("Issue 3815")]
    public async Task AutoSuggestBox_KeysUpAndDown_WrapAround()
    {
        //Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        const int delay = 50;

        //Act & Assert
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("e"));
        await Task.Delay(delay, TestContext.Current!.Execution.CancellationToken);

        static int? GetSuggestionCount(AutoSuggestBox autoSuggestBox)
        {
            int? count = autoSuggestBox.Suggestions?.OfType<object>().Count();
            return count;
        }

        int itemCount = await suggestBox.RemoteExecute(GetSuggestionCount) ?? 0;

        //Assert that initially the first item is selected
        int selectedIndex = await suggestionListBox.GetSelectedIndex();
        await Assert.That(selectedIndex).IsEqualTo(0);
        await Task.Delay(delay, TestContext.Current.Execution.CancellationToken);

        //Assert that the last item is selected after pressing ArrowUp
        await suggestBox.SendInput(new KeyboardInput(Key.Up));
        await Assert.That(await suggestionListBox.GetSelectedIndex()).IsEqualTo(itemCount - 1);
        await Task.Delay(delay, TestContext.Current.Execution.CancellationToken);

        //Assert that the first item is selected after pressing ArrowDown
        await suggestBox.SendInput(new KeyboardInput(Key.Down));
        await Assert.That(await suggestionListBox.GetSelectedIndex()).IsEqualTo(0);
    }

    [Test]
    [Description("Issue 3845")]
    public async Task AutoSuggestBox_SelectingAnItem_SetsSelectedItem()
    {
        //Arrange
        IVisualElement userControl = await LoadUserControl<AutoSuggestTextBoxWithCollectionView>();
        IVisualElement<AutoSuggestBox> suggestBox = await userControl.GetElement<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        //Act
        await suggestBox.MoveKeyboardFocus();
        await Task.Delay(50);
        await suggestBox.SendKeyboardInput($"B{Key.Down}{Key.Enter}");
        await Task.Delay(50);

        //Assert
        string? selectedItem = (await suggestBox.GetSelectedItem()) as string;
        await Assert.That(selectedItem).IsEqualTo("Bananas");

        static async Task AssertViewModelProperty(AutoSuggestBox autoSuggestBox)
        {
            var viewModel = (AutoSuggestTextBoxWithCollectionViewViewModel)autoSuggestBox.DataContext;
            await Assert.That(viewModel.SelectedItem).IsEqualTo("Bananas");
        }
        await suggestBox.RemoteExecute(AssertViewModelProperty);
    }

    [Test]
    public async Task AutoSuggestBox_ClickingButtonInInteractiveItemTemplate_DoesNotSelectOrClosePopup()
    {
        // Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithInteractiveTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        // Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("a"));
        await Task.Delay(500, TestContext.Current!.Execution.CancellationToken);

        // Find the button in the first suggestion item
        var thirdListBoxItem = await suggestionListBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
        var button = await thirdListBoxItem.GetElement<Button>();

        // Click the button
        await button.LeftClick();
        await Task.Delay(500, TestContext.Current!.Execution.CancellationToken);

        // Assert
        await Assert.That(await suggestBox.GetIsSuggestionOpen()).IsTrue();
        int selectedIndex = await suggestionListBox.GetSelectedIndex();
        await Assert.That(selectedIndex).IsNotEqualTo(2); // Should not select the item

        static async Task AssertViewModelProperty(AutoSuggestBox autoSuggestBox)
        {
            var viewModel = (AutoSuggestTextBoxWithInteractiveTemplateViewModel)autoSuggestBox.DataContext;
            var thirdItem = viewModel.Suggestions[2];
            await Assert.That(thirdItem.Count).IsEqualTo(1);
        }
        await suggestBox.RemoteExecute(AssertViewModelProperty);
    }

    [Test]
    public async Task AutoSuggestBox_ClickingButtonForcingNonInteractive_SelectsItemAndClosesPopup()
    {
        // Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithInteractiveTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        // Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("a"));
        await Task.Delay(500, TestContext.Current!.Execution.CancellationToken);

        // Find the button in the first suggestion item
        var thirdListBoxItem = await suggestionListBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
        var button = await thirdListBoxItem.GetElement<Button>();

        static void SetNonInteractive(Button button)
            => AutoSuggestBox.SetIsInteractiveElement(button, false);
        await button.RemoteExecute(SetNonInteractive);

        // Click the button
        await button.LeftClick();
        await Task.Delay(500, TestContext.Current!.Execution.CancellationToken);

        // Assert
        await Assert.That(await suggestBox.GetIsSuggestionOpen()).IsFalse();
    }

    [Test]
    public async Task AutoSuggestBox_ClickingTextblockThatIsInteractive_DoesNotSelectOrClosePopup()
    {
        // Arrange
        IVisualElement<AutoSuggestBox> suggestBox = (await LoadUserControl<AutoSuggestTextBoxWithInteractiveTemplate>()).As<AutoSuggestBox>();
        IVisualElement<Popup> popup = await suggestBox.GetElement<Popup>();
        IVisualElement<ListBox> suggestionListBox = await popup.GetElement<ListBox>();

        // Act
        await suggestBox.MoveKeyboardFocus();
        await suggestBox.SendInput(new KeyboardInput("a"));
        await Task.Delay(500, TestContext.Current!.Execution.CancellationToken);

        // Find the button in the first suggestion item
        var thirdListBoxItem = await suggestionListBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
        var textBlock = await thirdListBoxItem.GetElement<TextBlock>("NameTextblock");

        static void SetInteractive(TextBlock textBlock)
            => AutoSuggestBox.SetIsInteractiveElement(textBlock, true);
        await textBlock.RemoteExecute(SetInteractive);

        // Click the button
        await textBlock.LeftClick();
        await Task.Delay(500, TestContext.Current!.Execution.CancellationToken);

        // Assert
        await Assert.That(await suggestBox.GetIsSuggestionOpen()).IsTrue();

        // The list box item should selected because the TextBlock does not handle the click
        int selectedIndex = await suggestionListBox.GetSelectedIndex();
        await Assert.That(selectedIndex).IsEqualTo(2); 
    }

    private static async Task AssertExists(IVisualElement<ListBox> suggestionListBox, string text, bool existsOrNotCheck = true)
    {
        try
        {
            _ = await suggestionListBox.GetElement(ElementQuery.PropertyExpression<TextBlock>(x => x.Text, text));
            await Assert.That(existsOrNotCheck).IsTrue();
        }
        catch (Exception e) when (e is not TUnitException)
        {
            await Assert.That(existsOrNotCheck).IsFalse();
        }
    }
}
