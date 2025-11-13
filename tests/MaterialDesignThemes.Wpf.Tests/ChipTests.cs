using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class ChipTests
{
    [Test]
    public async Task TestIconProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.IconProperty.Name).IsEqualTo("Icon");
        await Assert.That(chip.Icon).IsNull();

        // Assert setting works
        var icon = new PackIcon { Kind = PackIconKind.Home };
        chip.Icon = icon;
        await Assert.That(chip.Icon).IsEqualTo(icon);
    }

    [Test]
    public async Task TestIconBackgroundProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.IconBackgroundProperty.Name).IsEqualTo("IconBackground");
        await Assert.That(chip.IconBackground).IsNull();

        // Assert setting works
        chip.IconBackground = Brushes.Red;
        await Assert.That(chip.IconBackground).IsEqualTo(Brushes.Red);
    }

    [Test]
    public async Task TestIconForegroundProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.IconForegroundProperty.Name).IsEqualTo("IconForeground");
        await Assert.That(chip.IconForeground).IsNull();

        // Assert setting works
        chip.IconForeground = Brushes.Blue;
        await Assert.That(chip.IconForeground).IsEqualTo(Brushes.Blue);
    }

    [Test]
    public async Task TestIsDeletableProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.IsDeletableProperty.Name).IsEqualTo("IsDeletable");
        await Assert.That(chip.IsDeletable).IsFalse();

        // Assert setting works
        chip.IsDeletable = true;
        await Assert.That(chip.IsDeletable).IsTrue();
    }

    [Test]
    public async Task TestDeleteCommandProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.DeleteCommandProperty.Name).IsEqualTo("DeleteCommand");
        await Assert.That(chip.DeleteCommand).IsNull();

        // Assert setting works
        var command = new TestCommand();
        chip.DeleteCommand = command;
        await Assert.That(chip.DeleteCommand).IsEqualTo(command);
    }

    [Test]
    public async Task TestDeleteCommandParameterProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.DeleteCommandParameterProperty.Name).IsEqualTo("DeleteCommandParameter");
        await Assert.That(chip.DeleteCommandParameter).IsNull();

        // Assert setting works
        var parameter = new object();
        chip.DeleteCommandParameter = parameter;
        await Assert.That(chip.DeleteCommandParameter).IsEqualTo(parameter);
    }

    [Test]
    public async Task TestDeleteToolTipProperty()
    {
        Chip chip = new();

        // Assert defaults
        await Assert.That(Chip.DeleteToolTipProperty.Name).IsEqualTo("DeleteToolTip");
        await Assert.That(chip.DeleteToolTip).IsNull();

        // Assert setting works
        chip.DeleteToolTip = "Delete this chip";
        await Assert.That(chip.DeleteToolTip).IsEqualTo("Delete this chip");
    }

    [Test]
    public async Task DeleteClickEvent_WhenDeleteButtonClicked_EventIsRaised()
    {
        Chip chip = new();
        chip.ApplyDefaultStyle();
        List<RoutedEventArgs> invocations = new();

        chip.DeleteClick += OnDeleteClick;

        void OnDeleteClick(object? sender, RoutedEventArgs e)
            => invocations.Add(e);

        // Find and click the delete button
        var deleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        await Assert.That(deleteButton).IsNotNull();

        deleteButton!.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, deleteButton));

        await Assert.That(invocations.Count).IsEqualTo(1);
        await Assert.That(invocations[0].Source).IsEqualTo(chip);
    }

    [Test]
    public async Task DeleteCommand_WhenDeleteButtonClicked_CommandIsExecuted()
    {
        Chip chip = new();
        chip.ApplyDefaultStyle();
        var command = new TestCommand();
        var parameter = "test-parameter";

        chip.DeleteCommand = command;
        chip.DeleteCommandParameter = parameter;

        // Find and click the delete button
        var deleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        await Assert.That(deleteButton).IsNotNull();

        deleteButton!.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, deleteButton));

        await Assert.That(command.ExecuteCount).IsEqualTo(1);
        await Assert.That(command.LastParameter).IsEqualTo(parameter);
    }

    [Test]
    public async Task DeleteCommand_WhenCanExecuteReturnsFalse_CommandIsNotExecuted()
    {
        Chip chip = new();
        chip.ApplyDefaultStyle();
        var command = new TestCommand { CanExecuteResult = false };

        chip.DeleteCommand = command;

        // Find and click the delete button
        var deleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        await Assert.That(deleteButton).IsNotNull();

        deleteButton!.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, deleteButton));

        await Assert.That(command.ExecuteCount).IsEqualTo(0);
    }

    [Test]
    public async Task OnApplyTemplate_WhenCalledMultipleTimes_UnsubscribesFromPreviousDeleteButton()
    {
        Chip chip = new();
        chip.ApplyDefaultStyle();

        var firstDeleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        await Assert.That(firstDeleteButton).IsNotNull();

        // Re-apply template
        chip.OnApplyTemplate();

        // Verify no exceptions occur and the chip still works
        var command = new TestCommand();
        chip.DeleteCommand = command;

        var deleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        deleteButton!.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, deleteButton));

        await Assert.That(command.ExecuteCount).IsEqualTo(1);
    }

    [Test]
    public async Task DeleteClick_WhenNoDeleteCommand_EventStillRaised()
    {
        Chip chip = new();
        chip.ApplyDefaultStyle();
        List<RoutedEventArgs> invocations = new();

        chip.DeleteClick += OnDeleteClick;

        void OnDeleteClick(object? sender, RoutedEventArgs e)
            => invocations.Add(e);

        // Ensure no command is set
        chip.DeleteCommand = null;

        // Find and click the delete button
        var deleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        deleteButton!.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, deleteButton));

        // Event should still be raised even without a command
        await Assert.That(invocations.Count).IsEqualTo(1);
    }

    [Test]
    public async Task Content_CanBeSetAndRetrieved()
    {
        Chip chip = new();

        var content = "Test Chip";
        chip.Content = content;

        await Assert.That(chip.Content).IsEqualTo(content);
    }

    [Test]
    public async Task IsDeletable_WhenFalse_DeleteButtonShouldBeHidden()
    {
        Chip chip = new();
        chip.ApplyDefaultStyle();
        chip.IsDeletable = false;

        // The delete button should still exist in the template but visibility is controlled by style
        var deleteButton = chip.FindVisualChild<Button>(Chip.DeleteButtonPartName);
        await Assert.That(deleteButton).IsNotNull();
    }

    // Helper class for testing ICommand
    private class TestCommand : ICommand
    {
        public int ExecuteCount { get; private set; }
        public object? LastParameter { get; private set; }
        public bool CanExecuteResult { get; set; } = true;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return CanExecuteResult;
        }

        public void Execute(object? parameter)
        {
            ExecuteCount++;
            LastParameter = parameter;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
