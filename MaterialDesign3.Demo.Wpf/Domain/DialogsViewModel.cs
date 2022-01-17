using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain
{
    public class DialogsViewModel : ViewModelBase
    {
        public DialogsViewModel()
        {
            //Sample 4
            OpenSample4DialogCommand = new AnotherCommandImplementation(OpenSample4Dialog);
            AcceptSample4DialogCommand = new AnotherCommandImplementation(AcceptSample4Dialog);
            CancelSample4DialogCommand = new AnotherCommandImplementation(CancelSample4Dialog);
        }

        #region SAMPLE 3

        public ICommand RunDialogCommand => new AnotherCommandImplementation(ExecuteRunDialog);

        public ICommand RunExtendedDialogCommand => new AnotherCommandImplementation(ExecuteRunExtendedDialog);

        private async void ExecuteRunDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new SampleDialog
            {
                DataContext = new SampleDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine("You can intercept the closing event, and cancel here.");

        private async void ExecuteRunExtendedDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new SampleDialog
            {
                DataContext = new SampleDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion

        #region SAMPLE 4

        //pretty much ignore all the stuff provided, and manage everything via custom commands and a binding for .IsOpen
        public ICommand OpenSample4DialogCommand { get; }
        public ICommand AcceptSample4DialogCommand { get; }
        public ICommand CancelSample4DialogCommand { get; }

        private bool _isSample4DialogOpen;
        private object? _sample4Content;

        public bool IsSample4DialogOpen
        {
            get => _isSample4DialogOpen;
            set => SetProperty(ref _isSample4DialogOpen, value);
        }

        public object? Sample4Content
        {
            get => _sample4Content;
            set => SetProperty(ref _sample4Content, value);
        }

        private void OpenSample4Dialog(object obj)
        {
            Sample4Content = new Sample4Dialog();
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog(object obj) => IsSample4DialogOpen = false;

        private void AcceptSample4Dialog(object obj)
        {
            //pretend to do something for 3 seconds, then close
            Sample4Content = new SampleProgressDialog();
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => IsSample4DialogOpen = false, null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion
    }
}
