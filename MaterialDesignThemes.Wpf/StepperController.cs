using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Controller which holds the steps and implements the navigation between them.
    /// </summary>
    public class StepperController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private StepperStepViewModel[] _stepViewModels;
        private ObservableCollection<StepperStepViewModel> _observableStepViewModels;

        /// <summary>
        /// Gets the active <see cref="IStep"/> or null.
        /// </summary>
        public IStep ActiveStep
        {
            get
            {
                return ActiveStepViewModel?.Step;
            }
        }

        /// <summary>
        /// Gets the content of the active <see cref="IStep"/> or null.
        /// </summary>
        public object ActiveStepContent
        {
            get
            {
                return ActiveStep?.Content;
            }
        }

        /// <summary>
        /// Gets the view model of the active <see cref="IStep"/> or null.
        /// </summary>
        public StepperStepViewModel ActiveStepViewModel
        {
            get
            {
                if (_stepViewModels == null)
                {
                    return null;
                }

                return _stepViewModels.Where(stepViewModel => stepViewModel.IsActive).FirstOrDefault();
            }
        }

        /// <summary>
        /// Internal getter for the binding in the XAML of the <see cref="Stepper"/>.
        /// It is considered for internal use only.
        /// </summary>
        public ObservableCollection<StepperStepViewModel> InternalSteps
        {
            get
            {
                return _observableStepViewModels;
            }
        }

        /// <summary>
        /// Gets all the steps of the <see cref="Stepper"/>.
        /// </summary>
        public IStep[] Steps
        {
            get
            {
                if (_stepViewModels == null)
                {
                    return null;
                }

                return _stepViewModels.Select(viewModel => viewModel.Step).ToArray();
            }
        }

        public StepperController()
        {
            _stepViewModels = null;
            _observableStepViewModels = new ObservableCollection<StepperStepViewModel>();
        }

        /// <summary>
        /// Initialises the steps which will be shown inside the <see cref="Stepper"/>.
        /// </summary>
        /// <param name="steps"></param>
        public void InitSteps(IList<IStep> steps)
        {
            InitSteps(steps?.ToArray());
        }

        /// <summary>
        /// Initialises the steps which will be shown inside the <see cref="Stepper"/>.
        /// Throws an <see cref="ArgumentNullException"/> if any of the steps is null.
        /// </summary>
        /// <param name="steps"></param>
        public void InitSteps(IStep[] steps)
        {
            _observableStepViewModels.Clear();

            if (steps != null)
            {
                _stepViewModels = new StepperStepViewModel[steps.Length];

                for (int i = 0; i < steps.Length; i++)
                {
                    IStep step = steps[i];

                    if (step == null)
                    {
                        throw new ArgumentNullException("null is not a valid step");
                    }

                    _stepViewModels[i] = new StepperStepViewModel()
                    {
                        Controller = this,
                        Step = step,
                        IsActive = false,
                        Number = (i + 1),
                        NeedsSpacer = i < (steps.Length - 1),
                        IsFirstStep = i == 0,
                        IsLastStep = i == (steps.Length - 1)
                    };

                    _observableStepViewModels.Add(_stepViewModels[i]);
                }

                if (_stepViewModels.Length > 0)
                {
                    _stepViewModels[0].IsActive = true;
                }

                OnPropertyChanged(nameof(Steps));
                OnPropertyChanged(nameof(InternalSteps));
                OnPropertyChanged(nameof(ActiveStepViewModel));
                OnPropertyChanged(nameof(ActiveStep));
                OnPropertyChanged(nameof(ActiveStepContent));
            }
        }

        private int GetActiveStepIndex()
        {
            if (_stepViewModels != null)
            {
                for (int i = 0; i < _stepViewModels.Length; i++)
                {
                    if (_stepViewModels[i].IsActive)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Goes to the next <see cref="IStep"/> if the active <see cref="IStep"/> is not the last one.
        /// </summary>
        public void Continue()
        {
            if (_stepViewModels == null)
            {
                return;
            }

            // find the active step and go to the next one
            int activeStepIndex = GetActiveStepIndex();

            if (activeStepIndex >= 0 && activeStepIndex  < (_stepViewModels.Length - 1))
            {
                GotoStep(activeStepIndex + 1);
            }
        }

        /// <summary>
        /// Goes to the previous <see cref="IStep"/> if the active <see cref="IStep"/> is not the first one.
        /// </summary>
        public void Back()
        {
            if (_stepViewModels == null)
            {
                return;
            }

            // find the active step and go to the previous one
            int activeStepIndex = GetActiveStepIndex();

            if (activeStepIndex > 0 && activeStepIndex < _stepViewModels.Length)
            {
                GotoStep(activeStepIndex - 1);
            }
        }

        /// <summary>
        /// Goes to the <see cref="IStep"/> specified by the index.
        /// </summary>
        /// <param name="index"></param>
        public void GotoStep(int index)
        {
            if (_stepViewModels != null && index >= 0 && index < _stepViewModels.Length)
            {
                GotoStep(_stepViewModels[index]);
            }
            else
            {
                throw new ArgumentException("there is no step with the index " + index);
            }
        }

        /// <summary>
        /// Goes to the specified <see cref="IStep"/>.
        /// Throws an <see cref="ArgumentNullException"/> if step is null or step is not inside the <see cref="Stepper"/>.
        /// </summary>
        /// <param name="step"></param>
        public void GotoStep(IStep step)
        {
            if (step == null)
            {
                throw new ArgumentNullException("null is not a valid step");
            }

            GotoStep(_stepViewModels.Where(stepViewModel => stepViewModel.Step == step).FirstOrDefault());
        }

        /// <summary>
        /// Goes to the specified <see cref="StepperStepViewModel"/>.
        /// Throws an <see cref="ArgumentNullException"/> if stepViewModel is null.
        /// </summary>
        /// <param name="stepViewModel"></param>
        public void GotoStep(StepperStepViewModel stepViewModel)
        {
            if (stepViewModel == null)
            {
                throw new ArgumentNullException(nameof(stepViewModel) + " must not be null");
            }

            // set all steps inactive except the next one to show
            foreach (StepperStepViewModel stepViewModelItem in _stepViewModels)
            {
                stepViewModelItem.IsActive = stepViewModelItem == stepViewModel;
            }

            OnPropertyChanged(nameof(ActiveStepViewModel));
            OnPropertyChanged(nameof(ActiveStep));
            OnPropertyChanged(nameof(ActiveStepContent));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
