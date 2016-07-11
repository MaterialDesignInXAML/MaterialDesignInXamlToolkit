using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    public class StepperStepViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private StepperController _controller;

        private IStep _step;

        private int _number;
        private bool _needsSpacer;
        private bool _isActive;
        private bool _isFirstStep;
        private bool _isLastStep;

        public StepperController Controller
        {
            get
            {
                return _controller;
            }

            set
            {
                _controller = value;

                OnPropertyChanged(nameof(Controller));
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                _isActive = value;

                OnPropertyChanged(nameof(IsActive));
            }
        }

        public bool IsFirstStep
        {
            get
            {
                return _isFirstStep;
            }

            set
            {
                _isFirstStep = value;

                OnPropertyChanged(nameof(IsFirstStep));
            }
        }

        public bool IsLastStep
        {
            get
            {
                return _isLastStep;
            }

            set
            {
                _isLastStep = value;

                OnPropertyChanged(nameof(IsLastStep));
            }
        }

        public bool NeedsSpacer
        {
            get
            {
                return _needsSpacer;
            }

            set
            {
                _needsSpacer = value;

                OnPropertyChanged(nameof(NeedsSpacer));
            }
        }

        public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;

                OnPropertyChanged(nameof(Number));
            }
        }

        public IStep Step
        {
            get
            {
                return _step;
            }

            set
            {
                _step = value;

                OnPropertyChanged(nameof(Step));
            }
        }

        public StepperStepViewModel()
        {
            _controller = null;

            _step = null;

            _number = 0;
            _needsSpacer = false;
            _isActive = false;
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
