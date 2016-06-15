using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf
{
    [ContentProperty(nameof(Steps))]
    public class Stepper : Control
    {
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand StepSelectedCommand = new RoutedCommand();

        public static readonly DependencyProperty IsLinearProperty = DependencyProperty.Register(
                nameof(IsLinear), typeof(bool), typeof(Stepper), new PropertyMetadata(false));

        public bool IsLinear
        {
            get
            {
                return (bool)GetValue(IsLinearProperty);
            }

            set
            {
                SetValue(IsLinearProperty, value);
            }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
                nameof(Orientation), typeof(StepperOrientation), typeof(Stepper), new PropertyMetadata(StepperOrientation.Horizontal));

        public StepperOrientation Orientation
        {
            get
            {
                return (StepperOrientation)GetValue(OrientationProperty);
            }

            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        public static readonly DependencyProperty StepsProperty = DependencyProperty.Register(
                nameof(Steps), typeof(IList<Step>), typeof(Stepper), new PropertyMetadata(null, StepsChangedHandler));

        public IList<Step> Steps
        {
            get
            {
                return (IList<Step>)GetValue(StepsProperty);
            }

            set
            {
                SetValue(StepsProperty, value);
            }
        }

        public StepperController Controller
        {
            get
            {
                return _controller;
            }
        }

        private StepperController _controller;

        static Stepper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Stepper), new FrameworkPropertyMetadata(typeof(Stepper)));
        }

        public Stepper()
            : base()
        {
            _controller = new StepperController();

            CommandBindings.Add(new CommandBinding(CancelCommand, CancelHandler, CanExecuteCancel));
            CommandBindings.Add(new CommandBinding(StepSelectedCommand, StepSelectedHandler, CanExecuteStepSelectedHandler));
        }

        private static void StepsChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Stepper stepper = (Stepper)obj;
            stepper.Controller.InitSteps(args.NewValue as IList<Step>);
        }

        private void CanExecuteCancel(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        private void CancelHandler(object sender, ExecutedRoutedEventArgs args)
        {
            if (args.Handled)
            {
                return;
            }

            //Close(executedRoutedEventArgs.Parameter);

            args.Handled = true;
        }

        private void CanExecuteStepSelectedHandler(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !IsLinear;
        }

        private void StepSelectedHandler(object sender, ExecutedRoutedEventArgs args)
        {
            _controller.GotoStep((StepperStepViewModel)args.Parameter);
        }
    }
}
