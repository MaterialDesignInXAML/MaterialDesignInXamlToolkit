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

        public StepperStepViewModel ActiveStepViewModel
        {
            get
            {
                if (_stepViewModels == null)
                {
                    return null;
                }

                return _stepViewModels.Where(stepViewModel => stepViewModel.IsActive).FirstOrDefault(null);
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

        public void InitSteps(IList<Step> steps)
        {
            InitSteps(steps?.ToArray());
        }

        public void InitSteps(Step[] steps)
        {
            _observableStepViewModels.Clear();

            if (steps != null)
            {
                _stepViewModels = new StepperStepViewModel[steps.Length];

                for (int i = 0; i < steps.Length; i++)
                {
                    _stepViewModels[i] = new StepperStepViewModel()
                    {
                        Controller = this,
                        Step = steps[i],
                        IsActive = false,
                        Number = (i + 1),
                        NeedsSpacer = i < (steps.Length - 1)
                    };

                    _observableStepViewModels.Add(_stepViewModels[i]);
                }

                if (_stepViewModels.Length > 0)
                {
                    _stepViewModels[0].IsActive = true;
                }

                OnPropertyChanged(nameof(steps));
            }
        }

        public void Next()
        {
            if (_stepViewModels == null)
            {
                return;
            }

            // find the active step
            int activeStepIndex = -1;

            for (int i = 0; i < _stepViewModels.Length && activeStepIndex == -1; i++)
            {
                if (_stepViewModels[i].IsActive)
                {
                    activeStepIndex = i;
                }
            }

            if (activeStepIndex > -1)
            {
                GotoStep(activeStepIndex + 1);

                /*// set all steps inactive
                foreach (StepperStepViewModel stepViewModel in _stepViewModels)
                {
                    stepViewModel.IsActive = false;
                }

                // set the next step active
                activeStepIndex++;

                if (activeStepIndex < _stepViewModels.Length)
                {
                    _stepViewModels[activeStepIndex].IsActive = true;
                }*/
            }
        }

        public void Previous()
        {
            //
        }

        public void GotoStep(int index)
        {
            GotoStep(_stepViewModels[index]);
        }

        public void GotoStep(Step step)
        {
            GotoStep(_stepViewModels.Where(stepViewModel => stepViewModel.Step == step).FirstOrDefault(null));
        }

        public void GotoStep(StepperStepViewModel stepViewModel)
        {
            // set all steps inactive exepct the next one to show
            foreach (StepperStepViewModel stepViewModelItem in _stepViewModels)
            {
                stepViewModelItem.IsActive = stepViewModelItem == stepViewModel;
            }
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
