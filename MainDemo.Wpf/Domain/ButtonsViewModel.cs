using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace MaterialDesignDemo.Domain
{
    public class ButtonsViewModel : ViewModelBase
    {
        private bool _showDismissButton;
        private double _dismissButtonProgress;
        private string? _demoRestartCountdownText;
        private int _orClickMeCount;

        public ButtonsViewModel()
        {
            FloatingActionDemoCommand = new AnotherCommandImplementation(FloatingActionDemo);

            var autoStartingActionCountdownStart = DateTime.Now;
            var demoRestartCountdownComplete = DateTime.Now;
            var dismissRequested = false;
            DismissCommand = new AnotherCommandImplementation(_ => dismissRequested = true);
            ShowDismissButton = true;

            #region DISMISS button demo control
            //just some demo code for the DISMISS button...it's up to you to set 
            //up the progress on the button as it would be with a progress bar.
            //and then hide the button, do whatever action you want to do
            new DispatcherTimer(
                TimeSpan.FromMilliseconds(100),
                DispatcherPriority.Normal,
                new EventHandler((o, e) =>
                {
                    if (dismissRequested)
                    {
                        ShowDismissButton = false;
                        dismissRequested = false;
                        demoRestartCountdownComplete = DateTime.Now.AddSeconds(3);
                        DismissButtonProgress = 0;
                    }

                    if (ShowDismissButton)
                    {
                        var totalDuration = autoStartingActionCountdownStart.AddSeconds(5).Ticks - autoStartingActionCountdownStart.Ticks;
                        var currentDuration = DateTime.Now.Ticks - autoStartingActionCountdownStart.Ticks;
                        var autoCountdownPercentComplete = 100.0 / totalDuration * currentDuration;
                        DismissButtonProgress = autoCountdownPercentComplete;

                        if (DismissButtonProgress >= 100)
                        {
                            demoRestartCountdownComplete = DateTime.Now.AddSeconds(3);
                            ShowDismissButton = false;
                            UpdateDemoRestartCountdownText(demoRestartCountdownComplete, out _);
                        }
                    }
                    else
                    {
                        UpdateDemoRestartCountdownText(demoRestartCountdownComplete, out bool isComplete);
                        if (isComplete)
                        {
                            autoStartingActionCountdownStart = DateTime.Now;
                            ShowDismissButton = true;
                        }
                    }

                }), Dispatcher.CurrentDispatcher);
            #endregion

            IncrementOrClickMeCountCommand = new AnotherCommandImplementation(_ => OrClickMeCount += 1);
            OrClickMeCount = 0;

            //just some demo code for the SAVE button
            SaveCommand = new AnotherCommandImplementation(_ =>
            {
                if (IsSaveComplete == true)
                {
                    IsSaveComplete = false;
                    return;
                }

                if (SaveProgress != 0) return;

                var started = DateTime.Now;
                IsSaving = true;

                new DispatcherTimer(
                    TimeSpan.FromMilliseconds(50),
                    DispatcherPriority.Normal,
                    new EventHandler((o, e) =>
                    {
                        var totalDuration = started.AddSeconds(3).Ticks - started.Ticks;
                        var currentProgress = DateTime.Now.Ticks - started.Ticks;
                        var currentProgressPercent = 100.0 / totalDuration * currentProgress;

                        SaveProgress = currentProgressPercent;

                        if (SaveProgress >= 100)
                        {
                            IsSaveComplete = true;
                            IsSaving = false;
                            SaveProgress = 0;
                            if (o is DispatcherTimer timer)
                            {
                                timer.Stop();
                            }
                        }

                    }), Dispatcher.CurrentDispatcher);
            });
        }

        public ICommand FloatingActionDemoCommand { get; }

        private static void FloatingActionDemo(object? o)
            => Console.WriteLine($"Floating action button command. - {o ?? "NULL"}");

        #region Dismiss button demo

        public ICommand DismissCommand { get; }

        public bool ShowDismissButton
        {
            get => _showDismissButton;
            set => SetProperty(ref _showDismissButton, value);
        }

        public double DismissButtonProgress
        {
            get => _dismissButtonProgress;
            set => SetProperty(ref _dismissButtonProgress, value);
        }

        public string? DemoRestartCountdownText
        {
            get => _demoRestartCountdownText;
            private set => SetProperty(ref _demoRestartCountdownText, value);
        }

        private void UpdateDemoRestartCountdownText(DateTime endTime, out bool isComplete)
        {
            var span = endTime - DateTime.Now;
            var seconds = Math.Round(span.TotalSeconds < 0 ? 0 : span.TotalSeconds);
            DemoRestartCountdownText = "Demo in " + seconds;
            isComplete = seconds == 0;
        }

        #endregion

        #region OrClickMe Demo
        public int OrClickMeCount
        {
            get => _orClickMeCount;
            private set => SetProperty(ref _orClickMeCount, value);
        }
        public ICommand IncrementOrClickMeCountCommand { get; }

        #endregion

        #region floating Save button demo

        public ICommand SaveCommand { get; }

        private bool _isSaving;
        public bool IsSaving
        {
            get => _isSaving;
            private set => SetProperty(ref _isSaving, value);
        }

        private bool _isSaveComplete;
        public bool IsSaveComplete
        {
            get => _isSaveComplete;
            private set => SetProperty(ref _isSaveComplete, value);
        }

        private double _saveProgress;
        public double SaveProgress
        {
            get => _saveProgress;
            private set => SetProperty(ref _saveProgress, value);
        }

        #endregion
    }
}
