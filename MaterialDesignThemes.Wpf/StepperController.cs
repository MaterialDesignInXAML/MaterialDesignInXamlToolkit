using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    public class StepperController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private StepperStepViewModel[] _stepViewModels;
        private ObservableCollection<StepperStepViewModel> _observableStepViewModels;

        public object ActiveStepContent
        {
            get
            {
                return ActiveStepViewModel?.Step.Content;
            }
        }

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

        public ObservableCollection<StepperStepViewModel> Steps
        {
            get
            {
                return _observableStepViewModels;
            }
        }

        public StepperController()
        {
            _stepViewModels = null;
            _observableStepViewModels = new ObservableCollection<StepperStepViewModel>();
        }

        public void InitSteps(IList<IStep> steps)
        {
            InitSteps(steps?.ToArray());
        }

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

                OnPropertyChanged(nameof(steps));
                OnPropertyChanged(nameof(ActiveStepViewModel));
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

        public void GotoStep(Step step)
        {
            if (step == null)
            {
                throw new ArgumentNullException("null is not a valid step");
            }

            GotoStep(_stepViewModels.Where(stepViewModel => stepViewModel.Step == step).FirstOrDefault());
        }

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
