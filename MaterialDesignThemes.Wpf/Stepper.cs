using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A control which implements the Stepper of the Material design specification (https://material.google.com/components/steppers.html).
    /// </summary>
    [ContentProperty(nameof(Steps))]
    public class Stepper : Control
    {
        public static RoutedCommand BackCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand ContinueCommand = new RoutedCommand();
        public static RoutedCommand StepSelectedCommand = new RoutedCommand();

        public static readonly RoutedEvent BackNavigationEvent = EventManager.RegisterRoutedEvent(nameof(BackNavigation), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Stepper));

        /// <summary>
        /// An event raised by navigating to the previous <see cref="IStep"/> in a linear order.
        /// </summary>
        public event RoutedEventHandler BackNavigation
        {
            add
            {
                AddHandler(BackNavigationEvent, value);
            }

            remove
            {
                RemoveHandler(BackNavigationEvent, value);
            }
        }

        public static readonly RoutedEvent CancelNavigationEvent = EventManager.RegisterRoutedEvent(nameof(CancelNavigation), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Stepper));

        /// <summary>
        /// An event raised by cancelling the process.
        /// </summary>
        public event RoutedEventHandler CancelNavigation
        {
            add
            {
                AddHandler(CancelNavigationEvent, value);
            }

            remove
            {
                RemoveHandler(CancelNavigationEvent, value);
            }
        }

        public static readonly RoutedEvent ContinueNavigationEvent = EventManager.RegisterRoutedEvent(nameof(ContinueNavigation), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Stepper));

        /// <summary>
        /// An event raised by navigating to the next <see cref="IStep"/> in a linear order.
        /// </summary>
        public event RoutedEventHandler ContinueNavigation
        {
            add
            {
                AddHandler(ContinueNavigationEvent, value);
            }

            remove
            {
                RemoveHandler(ContinueNavigationEvent, value);
            }
        }

        public static readonly RoutedEvent StepNavigationEvent = EventManager.RegisterRoutedEvent(nameof(StepNavigation), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Stepper));

        /// <summary>
        /// An event raised by navigating to an arbitrary <see cref="IStep"/> in a non-linear <see cref="Stepper"/>.
        /// </summary>
        public event RoutedEventHandler StepNavigation
        {
            add
            {
                AddHandler(StepNavigationEvent, value);
            }

            remove
            {
                RemoveHandler(StepNavigationEvent, value);
            }
        }

        public static readonly RoutedEvent StepValidationEvent = EventManager.RegisterRoutedEvent(nameof(StepValidation), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Stepper));

        /// <summary>
        /// An event raised by starting the validation of an <see cref="IStep"/>.
        /// </summary>
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

        /// <summary>
        /// Specifies whether validation errors will block the navigation or not.
        /// </summary>
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

        /// <summary>
        /// Enables the linear mode by disabling the buttons of the header.
        /// The navigation must be accomplished by using the navigation commands.
        /// </summary>
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

        /// <summary>
        /// Defines this <see cref="Stepper"/> as either horizontal or vertical.
        /// </summary>
        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register(
                nameof(Layout), typeof(StepperLayout), typeof(Stepper), new PropertyMetadata(StepperLayout.Horizontal));

        public StepperLayout Layout
        {
            get
            {
                return (StepperLayout)GetValue(LayoutProperty);
            }

            set
            {
                SetValue(LayoutProperty, value);
            }
        }

        public static readonly DependencyProperty StepsProperty = DependencyProperty.Register(
                nameof(Steps), typeof(IList<IStep>), typeof(Stepper), new PropertyMetadata(null, StepsChangedHandler));

        /// <summary>
        /// Gets or sets the steps which will be shown inside this <see cref="Stepper"/>.
        /// </summary>
        public IEnumerable<IStep> Steps
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
        /// A command called by starting the validation of an <see cref="IStep"/>.
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

        /// <summary>
        /// Gets the controller for this <see cref="Stepper"/>.
        /// </summary>
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

            Loaded += LoadedHandler;
            Unloaded += UnloadedHandler;

            CommandBindings.Add(new CommandBinding(BackCommand, BackHandler, CanExecuteBack));
            CommandBindings.Add(new CommandBinding(CancelCommand, CancelHandler, CanExecuteCancel));
            CommandBindings.Add(new CommandBinding(ContinueCommand, ContinueHandler, CanExecuteContinue));
            CommandBindings.Add(new CommandBinding(StepSelectedCommand, StepSelectedHandler, CanExecuteStepSelectedHandler));
        }

        private void LoadedHandler(object sender, RoutedEventArgs args)
        {
            _controller.PropertyChanged += PropertyChangedHandler;

            // there is no event raised if the Content of a ContentControl changes
            //     therefore trigger the animation in code
            PlayHorizontalContentAnimation();
        }

        private void UnloadedHandler(object sender, RoutedEventArgs args)
        {
            _controller.PropertyChanged -= PropertyChangedHandler;
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
            stepper.Controller.InitSteps(args.NewValue as IEnumerable<IStep>);
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

            StepperNavigationEventArgs navigationArgs = new StepperNavigationEventArgs(BackNavigationEvent, this, _controller.ActiveStep, _controller.PreviousStep, false);
            RaiseEvent(navigationArgs);

            if (!navigationArgs.Cancel)
            {
                _controller.Back();
            }
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

            StepperNavigationEventArgs navigationArgs = new StepperNavigationEventArgs(CancelNavigationEvent, this, _controller.ActiveStep, null, false);
            RaiseEvent(navigationArgs);
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

            StepperNavigationEventArgs navigationArgs = new StepperNavigationEventArgs(ContinueNavigationEvent, this, _controller.ActiveStep, _controller.NextStep, false);
            RaiseEvent(navigationArgs);

            if (!navigationArgs.Cancel)
            {
                _controller.Continue();
            }
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

                StepperNavigationEventArgs navigationArgs = new StepperNavigationEventArgs(StepNavigationEvent, this, _controller.ActiveStep, ((StepperStepViewModel)args.Parameter).Step, false);
                RaiseEvent(navigationArgs);

                if (!navigationArgs.Cancel)
                {
                    _controller.GotoStep((StepperStepViewModel)args.Parameter);
                }
            }
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs args)
        {
            if (sender == _controller && args.PropertyName == nameof(_controller.ActiveStepContent)
                    && _controller.ActiveStepContent != null && Layout == StepperLayout.Horizontal)
            {
                // there is no event raised if the Content of a ContentControl changes
                //     therefore trigger the animation in code
                PlayHorizontalContentAnimation();
            }
        }

        private void PlayHorizontalContentAnimation()
        {
            // there is no event raised if the Content of a ContentControl changes
            //     therefore trigger the animation in code
            if (Layout == StepperLayout.Horizontal)
            {
                Storyboard storyboard = (Storyboard)FindResource("horizontalContentChangedStoryboard");
                FrameworkElement element = GetTemplateChild("PART_horizontalContent") as FrameworkElement;

                if (storyboard != null && element != null)
                {
                    storyboard.Begin(element);
                }
            }
        }
    }

    /// <summary>
    /// The layout of a <see cref="Stepper"/>.
    /// </summary>
    public enum StepperLayout : byte
    {
        Horizontal,
        Vertical
    }

    /// <summary>
    /// The argument for the <see cref="Stepper.StepValidation"/> event and the <see cref="Stepper.StepValidationCommand"/> command.
    /// It holds the <see cref="IStep"/> to validate.
    /// </summary>
    public class StepValidationEventArgs : RoutedEventArgs
    {
        public IStep Step { get; }

        public StepValidationEventArgs(RoutedEvent routedEvent, object source, IStep step)
            : base(routedEvent, source)
        {
            Step = step;
        }
    }

    /// <summary>
    /// The argument for the <see cref="Stepper.BackNavigation"/>, <see cref="Stepper.ContinueNavigation"/>, <see cref="Stepper.StepNavigation"/> and <see cref="Stepper.CancelNavigation"/> event.
    /// It holds the current <see cref="IStep"/> an the one to navigate to.
    /// The events are raised before the actal navigation and the navigation can be cancelled by setting <see cref="Stepper.ContinueNavigation"/> to false.
    /// </summary>
    public class StepperNavigationEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// The current <see cref="IStep"/> of the <see cref="Stepper"/>.
        /// </summary>
        public IStep CurrentStep { get; }

        /// <summary>
        /// The next <see cref="IStep"/> to navigate to.
        /// </summary>
        public IStep NextStep { get; }

        /// <summary>
        /// A flag to cancel the navigation by setting it to false.
        /// </summary>
        public bool Cancel { get; set; }

        public StepperNavigationEventArgs(RoutedEvent routedEvent, object source, IStep currentStep, IStep nextStep, bool cancel)
            : base(routedEvent, source)
        {
            CurrentStep = currentStep;
            NextStep = nextStep;
            Cancel = cancel;
        }
    }
}
