using System.Windows.Controls;
using MaterialDesignThemes.Wpf.Transitions;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class TransitionerTests
    {
        [StaFact]
        public void WhenMoveNext_ItCanAdvanceMultipleSlides()
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
            Assert.True(Transitioner.MoveNextCommand.CanExecute(parameter, transitioner));
            Transitioner.MoveNextCommand.Execute(parameter, transitioner);

            //Assert
            Assert.Equal(2, transitioner.SelectedIndex);
        }

        [StaFact]
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
            Assert.Equal(0, transitioner.SelectedIndex);
        }
    }
}
