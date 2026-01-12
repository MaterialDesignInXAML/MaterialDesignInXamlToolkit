using MaterialDesignThemes.UITests.Samples.SplitButton;


[assembly: GenerateHelpers(typeof(SplitButtonWithCommandBinding))]

namespace MaterialDesignThemes.UITests.WPF.SplitButtons;

public class SplitButtonTests : TestBase
{
    [Test]
    public async Task SplitButton_ClickingSplitButton_ShowsPopup()
    {
        //Arrange
        IVisualElement<SplitButton> splitButton = await LoadXaml<SplitButton>("""
            <materialDesign:SplitButton Content="Split Button" VerticalAlignment="Center" HorizontalAlignment="Center">
              <materialDesign:SplitButton.PopupContent>
                <TextBlock x:Name="PopupContent" Text="Popup Content"/>
              </materialDesign:SplitButton.PopupContent>
            </materialDesign:SplitButton>
            """);

        IVisualElement<PopupBox> popupBox = await splitButton.GetElement<PopupBox>();
        IVisualElement<TextBlock> popupContent = await popupBox.GetElement<TextBlock>("PopupContent");

        //Act
        await popupBox.LeftClick();

        // Assert
        await Wait.For(async () => await popupBox.GetIsPopupOpen());
        await Wait.For(async () => await popupContent.GetIsVisible());
    }

    [Test]
    public async Task SplitButton_RegisterForClick_RaisesEvent()
    {
        //Arrange
        IVisualElement<SplitButton> splitButton = await LoadXaml<SplitButton>("""
            <materialDesign:SplitButton Content="Split Button" VerticalAlignment="Center" HorizontalAlignment="Center">
              <materialDesign:SplitButton.PopupContent>
                <TextBlock x:Name="PopupContent" Text="Popup Content"/>
              </materialDesign:SplitButton.PopupContent>
            </materialDesign:SplitButton>
            """);

        IEventRegistration clickEvent = await splitButton.RegisterForEvent(ButtonBase.ClickEvent.Name);

        IVisualElement<Button> leftButton = await splitButton.GetElement<Button>("PART_LeftButton");
        IVisualElement<PopupBox> popupBox = await splitButton.GetElement<PopupBox>();

        //Act
        await leftButton.LeftClick();
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);
        int leftButtonCount = (await clickEvent.GetInvocations()).Count;
        await popupBox.LeftClick();
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);
        int rightButtonCount = (await clickEvent.GetInvocations()).Count;

        // Assert
        await Assert.That(leftButtonCount).IsEqualTo(1);
        //NB: The popup box button should only show the popup not trigger the click event
        await Assert.That(rightButtonCount).IsEqualTo(1);
    }

    [Test]
    public async Task SplitButton_WithButtonInPopup_CanBeInvoked()
    {
        //Arrange
        IVisualElement<SplitButton> splitButton = await LoadXaml<SplitButton>("""
            <materialDesign:SplitButton Content="Split Button" VerticalAlignment="Center" HorizontalAlignment="Center">
              <materialDesign:SplitButton.PopupContent>
                <Button x:Name="PopupContent" Content="Popup Content" />
              </materialDesign:SplitButton.PopupContent>
            </materialDesign:SplitButton>
            """);

        IVisualElement<Button> popupContent = await splitButton.GetElement<Button>("PopupContent");
        IVisualElement<PopupBox> popupBox = await splitButton.GetElement<PopupBox>();

        IEventRegistration clickEvent = await popupContent.RegisterForEvent(ButtonBase.ClickEvent.Name);

        //Act
        await popupBox.LeftClick();
        //NB: give the popup some time to show
        await Wait.For(async () => await popupContent.GetIsVisible());
        await Wait.For(async () => await popupContent.GetActualHeight() > 10);

        await Recorder.SaveScreenshot("PopupOpen");

        await popupContent.LeftClick();
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);

        // Assert
        var invocations = await clickEvent.GetInvocations();
        await Assert.That(invocations).HasSingleItem();
    }

    [Test]
    public async Task SplitButton_RegisterCommandBinding_InvokesCommand()
    {
        //Arrange
        await App.InitializeWithMaterialDesign();
        IWindow window = await App.CreateWindow<SplitButtonWithCommandBindingWindow>();
        IVisualElement<SplitButtonWithCommandBinding> userControl = await window.GetElement<SplitButtonWithCommandBinding>();
        IVisualElement<SplitButton> splitButton = await userControl.GetElement<SplitButton>();

        await Assert.That(await userControl.GetCommandInvoked()).IsFalse();

        //Act
        await splitButton.LeftClick();
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);

        // Assert
        await Assert.That(await userControl.GetCommandInvoked()).IsTrue();
    }

    [Test]
    public async Task SplitButton_CommandCanExecuteFalse_DisablesButton()
    {
        //Arrange
        await App.InitializeWithMaterialDesign();
        IWindow window = await App.CreateWindow<SplitButtonWithCommandBindingWindow>();
        IVisualElement<SplitButtonWithCommandBinding> userControl = await window.GetElement<SplitButtonWithCommandBinding>();
        IVisualElement<SplitButton> splitButton = await userControl.GetElement<SplitButton>();

        await Assert.That(await splitButton.GetIsEnabled()).IsTrue();

        //Act
        await userControl.SetProperty(nameof(SplitButtonWithCommandBinding.CommandCanExecute), false);

        // Assert
        await Assert.That(await splitButton.GetIsEnabled()).IsFalse();
    }

    [Test]
    public async Task SplitButton_ClickingPopupContent_DoesNotExecuteSplitButtonClick()
    {
        //Arrange
        IVisualElement<SplitButton> splitButton = await LoadXaml<SplitButton>("""
            <materialDesign:SplitButton VerticalAlignment="Bottom"
                                        Content="Split Button"
                                        Style="{StaticResource MaterialDesignRaisedLightSplitButton}">
              <materialDesign:SplitButton.PopupContent>
                <Button x:Name="PopupContent" />
              </materialDesign:SplitButton.PopupContent>
            </materialDesign:SplitButton>
            """);

        IVisualElement<PopupBox> popupBox = await splitButton.GetElement<PopupBox>();
        IVisualElement<Button> popupContent = await splitButton.GetElement<Button>("PopupContent");

        IEventRegistration splitButtonClickEvent = await splitButton.RegisterForEvent(ButtonBase.ClickEvent.Name);
        IEventRegistration popupContentClickEvent = await popupContent.RegisterForEvent(ButtonBase.ClickEvent.Name);

        //Act
        await popupBox.LeftClick();
        //NB: give the popup some time to show
        await Wait.For(async () => await popupContent.GetIsVisible());
        await Wait.For(async () => await popupContent.GetActualHeight() > 10);
        await popupContent.LeftClick();
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);

        // Assert
        await Assert.That(await splitButtonClickEvent.GetInvocations()).IsEmpty();
        await Assert.That(await popupContentClickEvent.GetInvocations()).HasSingleItem();
    }
}
