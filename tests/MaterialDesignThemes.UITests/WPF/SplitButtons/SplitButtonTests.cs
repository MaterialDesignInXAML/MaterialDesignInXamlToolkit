using MaterialDesignThemes.UITests.Samples.SplitButton;

[assembly: GenerateHelpers(typeof(SplitButtonWithCommandBinding))]

namespace MaterialDesignThemes.UITests.WPF.SplitButtons;


public class SplitButtonTests : TestBase
{
    public SplitButtonTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public async Task SplitButton_ClickingSplitButton_ShowsPopup()
    {
        await using var recorder = new TestRecorder(App);

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

        recorder.Success();
    }

    [Fact]
    public async Task SplitButton_RegisterForClick_RaisesEvent()
    {
        await using var recorder = new TestRecorder(App);

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
        await Task.Delay(50);
        int leftButtonCount = (await clickEvent.GetInvocations()).Count;
        await popupBox.LeftClick();
        await Task.Delay(50);
        int rightButtonCount = (await clickEvent.GetInvocations()).Count;

        // Assert
        Assert.Equal(1, leftButtonCount);
        //NB: The popup box button should only show the popup not trigger the click event
        Assert.Equal(1, rightButtonCount);

        recorder.Success();
    }

    [Fact]
    public async Task SplitButton_WithButtonInPopup_CanBeInvoked()
    {
        await using var recorder = new TestRecorder(App);

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
        await popupContent.LeftClick();
        await Task.Delay(50);

        // Assert
        var invocations = await clickEvent.GetInvocations();
        Assert.Single(invocations);

        recorder.Success();
    }

    [Fact]
    public async Task SplitButton_RegisterCommandBinding_InvokesCommand()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        await App.InitializeWithMaterialDesign();
        IWindow window = await App.CreateWindow<SplitButtonWithCommandBindingWindow>();
        IVisualElement<SplitButtonWithCommandBinding> userControl = await window.GetElement<SplitButtonWithCommandBinding>();
        IVisualElement<SplitButton> splitButton = await userControl.GetElement<SplitButton>();

        Assert.False(await userControl.GetCommandInvoked());

        //Act
        await splitButton.LeftClick();
        await Task.Delay(50);

        // Assert
        Assert.True(await userControl.GetCommandInvoked());

        recorder.Success();
    }

    [Fact]
    public async Task SplitButton_CommandCanExecuteFalse_DisablesButton()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        await App.InitializeWithMaterialDesign();
        IWindow window = await App.CreateWindow<SplitButtonWithCommandBindingWindow>();
        IVisualElement<SplitButtonWithCommandBinding> userControl = await window.GetElement<SplitButtonWithCommandBinding>();
        IVisualElement<SplitButton> splitButton = await userControl.GetElement<SplitButton>();

        Assert.True(await splitButton.GetIsEnabled());

        //Act
        await userControl.SetProperty(nameof(SplitButtonWithCommandBinding.CommandCanExecute), false);

        // Assert
        Assert.False(await splitButton.GetIsEnabled());

        recorder.Success();
    }

    [Fact]
    public async Task SplitButton_ClickingPopupContent_DoesNotExecuteSplitButtonClick()
    {
        await using var recorder = new TestRecorder(App);

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
        await Task.Delay(50);

        // Assert
        Assert.Empty(await splitButtonClickEvent.GetInvocations());
        Assert.Single(await popupContentClickEvent.GetInvocations());

        recorder.Success();
    }
}
