namespace MaterialDesignThemes.UITests.WPF.Chips;

public class ChipTests : TestBase
{
    [Test]
    public async Task Chip_OnLoad_RendersCorrectly()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>(@"
            <materialDesign:Chip Content=""Test Chip"" />
        ");

        //Act
        string? content = await chip.GetContent();

        //Assert
        await Assert.That(content).IsEqualTo("Test Chip");

        recorder.Success();
    }

    [Test]
    public async Task Chip_WithIcon_DisplaysIcon()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>("""
            <materialDesign:Chip Content="Chip with Icon">
                <materialDesign:Chip.Icon>
                    <materialDesign:PackIcon x:Name="ChipIcon" Kind="Home" />
                </materialDesign:Chip.Icon>
            </materialDesign:Chip>
            """);

        //Act
        IVisualElement<PackIcon> icon = await chip.GetElement<PackIcon>("ChipIcon");

        //Assert
        await Assert.That(icon).IsNotNull();
        await Wait.For(async () => await icon.GetIsVisible());

        recorder.Success();
    }

    [Test]
    public async Task Chip_WhenIsDeletableTrue_ShowsDeleteButton()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>(@"
            <materialDesign:Chip Content=""Deletable Chip"" IsDeletable=""True"" />
        ");

        //Act
        IVisualElement<Button> deleteButton = await chip.GetElement<Button>("PART_DeleteButton");

        //Assert
        await Assert.That(deleteButton).IsNotNull();
        await Wait.For(async () => await deleteButton.GetIsVisible());

        recorder.Success();
    }

    [Test]
    public async Task Chip_WhenIsDeletableFalse_HidesDeleteButton()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>(@"
            <materialDesign:Chip Content=""Non-Deletable Chip"" IsDeletable=""False"" />
        ");

        //Act
        IVisualElement<Button> deleteButton = await chip.GetElement<Button>("PART_DeleteButton");
        bool isVisible = await deleteButton.GetIsVisible();

        //Assert
        await Assert.That(isVisible).IsFalse();

        recorder.Success();
    }

    [Test]
    public async Task Chip_ClickingDeleteButton_RaisesDeleteClickEvent()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>(@"
            <materialDesign:Chip Content=""Deletable Chip"" IsDeletable=""True"" />
        ");

        IEventRegistration deleteClickEvent = await chip.RegisterForEvent(nameof(Chip.DeleteClick));
        IVisualElement<Button> deleteButton = await chip.GetElement<Button>("PART_DeleteButton");

        //Act
        await deleteButton.LeftClick();
        await Task.Delay(50, TestContext.Current!.CancellationToken);

        //Assert
        var invocations = await deleteClickEvent.GetInvocations();
        await Assert.That(invocations.Count).IsEqualTo(1);

        recorder.Success();
    }

    [Test]
    public async Task Chip_WithDeleteCommand_ExecutesCommandOnDelete()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>("""
            <materialDesign:Chip x:Name="TestChip" Content="Chip with Command" IsDeletable="True">
                <materialDesign:Chip.DeleteCommand>
                    <Binding Path="DeleteCommand" />
                </materialDesign:Chip.DeleteCommand>
            </materialDesign:Chip>
            """);

        // Set up a command in the DataContext
        await chip.SetProperty(nameof(FrameworkElement.DataContext), new TestViewModel());
        
        IVisualElement<Button> deleteButton = await chip.GetElement<Button>("PART_DeleteButton");

        //Act
        await deleteButton.LeftClick();
        await Task.Delay(50, TestContext.Current!.CancellationToken);

        //Assert
        var dataContext = await chip.GetProperty<TestViewModel>(nameof(FrameworkElement.DataContext));
        await Assert.That(dataContext.DeleteCommandExecuted).IsTrue();

        recorder.Success();
    }

    [Test]
    public async Task Chip_WithIconBackground_AppliesCorrectBrush()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>("""
            <materialDesign:Chip Content="Chip" IconBackground="Red">
                <materialDesign:Chip.Icon>
                    <materialDesign:PackIcon Kind="Home" />
                </materialDesign:Chip.Icon>
            </materialDesign:Chip>
            """);

        //Act
        var iconBackground = await chip.GetIconBackground();

        //Assert
        await Assert.That(iconBackground).IsNotNull();

        recorder.Success();
    }

    [Test]
    public async Task Chip_WithIconForeground_AppliesCorrectBrush()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>("""
            <materialDesign:Chip Content="Chip" IconForeground="Blue">
                <materialDesign:Chip.Icon>
                    <materialDesign:PackIcon Kind="Home" />
                </materialDesign:Chip.Icon>
            </materialDesign:Chip>
            """);

        //Act
        var iconForeground = await chip.GetIconForeground();

        //Assert
        await Assert.That(iconForeground).IsNotNull();

        recorder.Success();
    }

    [Test]
    public async Task Chip_WithDeleteToolTip_ShowsToolTipOnDeleteButton()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>(@"
            <materialDesign:Chip Content=""Chip"" IsDeletable=""True"" DeleteToolTip=""Remove this chip"" />
        ");

        //Act
        IVisualElement<Button> deleteButton = await chip.GetElement<Button>("PART_DeleteButton");
        var toolTip = await deleteButton.GetToolTip();

        //Assert
        await Assert.That(toolTip).IsEqualTo("Remove this chip");

        recorder.Success();
    }

    [Test]
    public async Task Chip_ClickingChip_RaisesClickEvent()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>(@"
            <materialDesign:Chip Content=""Clickable Chip"" />
        ");

        IEventRegistration clickEvent = await chip.RegisterForEvent(ButtonBase.ClickEvent.Name);

        //Act
        await chip.LeftClick();
        await Task.Delay(50, TestContext.Current!.CancellationToken);

        //Assert
        var invocations = await clickEvent.GetInvocations();
        await Assert.That(invocations.Count).IsEqualTo(1);

        recorder.Success();
    }

    [Test]
    public async Task Chip_WithDeleteCommandParameter_PassesParameterToCommand()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Chip> chip = await LoadXaml<Chip>("""
            <materialDesign:Chip Content="Chip" IsDeletable="True" DeleteCommandParameter="TestParameter">
                <materialDesign:Chip.DeleteCommand>
                    <Binding Path="DeleteCommand" />
                </materialDesign:Chip.DeleteCommand>
            </materialDesign:Chip>
            """);

        await chip.SetProperty(nameof(FrameworkElement.DataContext), new TestViewModel());
        IVisualElement<Button> deleteButton = await chip.GetElement<Button>("PART_DeleteButton");

        //Act
        await deleteButton.LeftClick();
        await Task.Delay(50, TestContext.Current!.CancellationToken);

        //Assert
        var dataContext = await chip.GetProperty<TestViewModel>(nameof(FrameworkElement.DataContext));
        await Assert.That(dataContext.DeleteCommandParameter).IsEqualTo("TestParameter");

        recorder.Success();
    }

    // Helper class for command testing
    [Serializable]
    public class TestViewModel
    {
        public bool DeleteCommandExecuted { get; private set; }
        public object? DeleteCommandParameter { get; private set; }

        public ICommand DeleteCommand => new RelayCommand(Execute);

        private void Execute(object? parameter)
        {
            DeleteCommandExecuted = true;
            DeleteCommandParameter = parameter;
        }
    }

    [Serializable]
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;

        public RelayCommand(Action<object?> execute)
        {
            _execute = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _execute(parameter);
    }
}
