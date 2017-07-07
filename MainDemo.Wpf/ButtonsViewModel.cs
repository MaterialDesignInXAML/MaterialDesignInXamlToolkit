using MaterialDesignColors.WpfExample.Domain;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace MaterialDesignColors.WpfExample
{
    public class ButtonsViewModel : INotifyPropertyChanged
    {
        private bool _showDismissButton;
        private double _dismissButtonProgress;

        public ButtonsViewModel()
        {
            var autoStartingActionCountdownStart = DateTime.Now;
            DismissComand = new AnotherCommandImplementation(_ => { });
            ShowDismissButton = true;

            new DispatcherTimer(
                TimeSpan.FromMilliseconds(100), 
                DispatcherPriority.Normal, 
                new EventHandler((o, e) => 
                {
                    if (ShowDismissButton)
                    {
                        var totalDuration = autoStartingActionCountdownStart.AddSeconds(20).Ticks - autoStartingActionCountdownStart.Ticks;
                        var currentDuration = DateTime.Now.Ticks - autoStartingActionCountdownStart.Ticks;
                        var autoCountdownPercentComplete = 100.0 / totalDuration * currentDuration;
                        DismissButtonProgress = autoCountdownPercentComplete;
                    }
                }), Dispatcher.CurrentDispatcher);
        }

        public ICommand DismissComand { get; }

        public bool ShowDismissButton
        {
            get { return _showDismissButton; }
            set { this.MutateVerbose(ref _showDismissButton, value, RaisePropertyChanged()); }
        }        

        public double DismissButtonProgress
        {
            get { return _dismissButtonProgress; }
            set { this.MutateVerbose(ref _dismissButtonProgress, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
