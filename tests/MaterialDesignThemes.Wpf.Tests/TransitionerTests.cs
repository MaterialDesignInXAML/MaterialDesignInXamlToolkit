using MaterialDesignThemes.Wpf.Transitions;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class TransitionerTests
{
    [Test, STAThreadExecutor]
    public async Task WhenMoveNext_ItCanAdvanceMultipleSlides()
    {
        //Arrange
        var child1 = new UserControl();
        var child2 = new UserControl();
        var child3 = new UserControl();

        var transitioner = new Transitioner();
        transitioner.Items.Add(child1);
        transitioner.Items.Add(child2);
        transitioner.Items.Add(child3);
        transitioner.SelectedIndex = 0;

        object parameter = 2;

        //Act
        await Assert.That(Transitioner.MoveNextCommand.CanExecute(parameter, transitioner)).IsTrue();
        Transitioner.MoveNextCommand.Execute(parameter, transitioner);

        //Assert
        await Assert.That(transitioner.SelectedIndex).IsEqualTo(2);
    }

    [Test, STAThreadExecutor]
    public void WhenMovePrevious_ItCanRetreatMultipleSlides()
    {
        //Arrange
        var child1 = new UserControl();
        var child2 = new UserControl();
        var child3 = new UserControl();

        var transitioner = new Transitioner();
        transitioner.Items.Add(child1);
        transitioner.Items.Add(child2);
        transitioner.Items.Add(child3);
        transitioner.SelectedIndex = 2;

        object parameter = 2;

        //Act
        Assert.True(Transitioner.MovePreviousCommand.CanExecute(parameter, transitioner));
        Transitioner.MovePreviousCommand.Execute(parameter, transitioner);

        //Assert
        await Assert.That(transitioner.SelectedIndex).IsEqualTo(0);
    }

    [Test, STAThreadExecutor]
    public void ShortCircuitIssue3268()
    {
        //Arrange
        Grid child1 = new();
        ListBox lb = new();
        lb.Items.Add(new Label());
        lb.Items.Add(new Label());
        child1.Children.Add(lb);

        UserControl child2 = new();

        Transitioner transitioner = new();
        transitioner.Items.Add(child1);
        transitioner.Items.Add(child2);

        int selectionChangedCounter = 0;
        transitioner.SelectionChanged += (s, e) =>
        {
            selectionChangedCounter++;
        };
        Transitioner.MoveNextCommand.Execute(0, transitioner);

        //Act
        Assert.NotNull(transitioner.SelectedItem);
        Assert.True(transitioner.SelectedItem == child1);
        lb.SelectedItem = lb.Items[1];

        //Assert
        await Assert.That(selectionChangedCounter).IsEqualTo(1);
        await Assert.That(transitioner.SelectedIndex).IsEqualTo(0);
    }
}
