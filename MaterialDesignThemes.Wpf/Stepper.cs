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
        public static RoutedCommand BackCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand ContinueCommand = new RoutedCommand();
        public static RoutedCommand StepSelectedCommand = new RoutedCommand();

        public static readonly RoutedEvent StepValidationEvent = EventManager.RegisterRoutedEvent(nameof(StepValidation), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Stepper));

        public event RoutedEventHandler StepValidation
        {
            add
            {
                AddHandler(StepValidationEvent, value);
            }

            remove
            {
                RemoveHandler(StepValidationEvent, value);
            }
        }

        public static readonly DependencyProperty BlockNavigationOnValidationErrorsProperty = DependencyProperty.Register(
                nameof(BlockNavigationOnValidationErrors), typeof(bool), typeof(Stepper), new PropertyMetadata(false));

        public bool BlockNavigationOnValidationErrors
        {
            get
            {
                return (bool)GetValue(BlockNavigationOnValidationErrorsProperty);
            }

            set
            {
                SetValue(BlockNavigationOnValidationErrorsProperty, value);
            }
        }

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
                nameof(Steps), typeof(IList<IStep>), typeof(Stepper), new PropertyMetadata(null, StepsChangedHandler));

        public IList<IStep> Steps
        {
            get
            {
                return (IList<IStep>)GetValue(StepsProperty);
            }

            set
            {
                SetValue(StepsProperty, value);
            }
        }

        public static readonly DependencyProperty StepValidationCommandProperty = DependencyProperty.Register(
            nameof(StepValidationCommand), typeof(ICommand), typeof(Stepper), new PropertyMetadata(null));

        /// <summary>
        /// A command by clicking on the action button.
        /// </summary>
        public ICommand StepValidationCommand
        {
            get
            {
                return (ICommand)GetValue(StepValidationCommandProperty);
            }

            set
            {
                SetValue(StepValidationCommandProperty, value);
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

            CommandBindings.Add(new CommandBinding(BackCommand, BackHandler, CanExecuteBack));
            CommandBindings.Add(new CommandBinding(CancelCommand, CancelHandler, CanExecuteCancel));
            CommandBindings.Add(new CommandBinding(ContinueCommand, ContinueHandler, CanExecuteContinue));
            CommandBindings.Add(new CommandBinding(StepSelectedCommand, StepSelectedHandler, CanExecuteStepSelectedHandler));
        }

        private bool ValidateActiveStep()
        {
            IStep step = _controller.ActiveStepViewModel?.Step;

            if (step != null)
            {
                // call the validation method on the step itself
                step.Validate();

                // raise the event and call the command
                StepValidationEventArgs eventArgs = new StepValidationEventArgs(StepValidationEvent, this, step);
                RaiseEvent(eventArgs);

                if (StepValidationCommand != null && StepValidationCommand.CanExecute(step))
                {
                    StepValidationCommand.Execute(step);
                }

                // the event handlers can set the validation state on the step
                return !step.HasValidationErrors;
            } else
            {
                // no active step to validate
                return true;
            }
        }

        private static void StepsChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Stepper stepper = (Stepper)obj;
            stepper.Controller.InitSteps(args.NewValue as IList<IStep>);
        }

        private void CanExecuteBack(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        private void BackHandler(object sender, ExecutedRoutedEventArgs args)
        {
            bool isValid = ValidateActiveStep();

            if (BlockNavigationOnValidationErrors && !isValid)
            {
                return;
            }

            _controller.Back();

            args.Handled = true;
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

            args.Handled = true;
        }

        private void CanExecuteContinue(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        private void ContinueHandler(object sender, ExecutedRoutedEventArgs args)
        {
            bool isValid = ValidateActiveStep();

            if (BlockNavigationOnValidationErrors && !isValid)
            {
                return;
            }

            _controller.Continue();

            args.Handled = true;
        }

        private void CanExecuteStepSelectedHandler(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !IsLinear;
        }

        private void StepSelectedHandler(object sender, ExecutedRoutedEventArgs args)
        {
            if (!IsLinear)
            {
                bool isValid = ValidateActiveStep();

                if (BlockNavigationOnValidationErrors && !isValid)
                {
                    return;
                }

                _controller.GotoStep((StepperStepViewModel)args.Parameter);

                args.Handled = true;
            }
        }
    }

    public class StepValidationEventArgs : RoutedEventArgs
    {
        public IStep Step { get; }

        public StepValidationEventArgs(RoutedEvent routedEvent, object source, IStep step)
            : base(routedEvent, source)
        {
            Step = step;
        }
    }
}
