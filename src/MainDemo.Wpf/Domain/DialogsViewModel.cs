using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain;

public partial class DialogsViewModel : ObservableObject
{
    #region SAMPLE 3

    [RelayCommand]
    private async Task RunDialog() 
    {
        //let's set up a little MVVM, cos that's what the cool kids are doing:
        object? view = new SampleDialog
        {
            DataContext = new SampleDialogViewModel()
        };

        //show the dialog
        object? result = await DialogHost.Show(view, "RootDialog", null, ClosingEventHandler, ClosedEventHandler);

        //check the result...
        Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
    }

    private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        => Debug.WriteLine("You can intercept the closing event, and cancel here.");

    private void ClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
        => Debug.WriteLine("You can intercept the closed event here (1).");

    [RelayCommand]
    private async Task RunExtendedDialog()
    {
        //let's set up a little MVVM, cos that's what the cool kids are doing:
        object? view = new SampleDialog
        {
            DataContext = new SampleDialogViewModel()
        };

        //show the dialog
        object? result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);

        //check the result...
        Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
    }

    private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

    private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
    {
        Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
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

    private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
        => Debug.WriteLine("You could intercept the closed event here (2).");

    #endregion

    #region SAMPLE 4

    //pretty much ignore all the stuff provided, and manage everything via custom commands and a binding for .IsOpen   

    [ObservableProperty]
    private object? _sample4Content;

    [ObservableProperty]
    private bool _isSample4DialogOpen;
   
    [RelayCommand]
    private void OpenSample4Dialog()
    {
        Sample4Content = new Sample4Dialog();
        IsSample4DialogOpen = true;
    }

    [RelayCommand]
    private void CancelSample4Dialog() => IsSample4DialogOpen = false;

    [RelayCommand]
    private void AcceptSample4Dialog()
    {
        //pretend to do something for 3 seconds, then close
        Sample4Content = new SampleProgressDialog();
        Task.Delay(TimeSpan.FromSeconds(3))
            .ContinueWith((t, _) => IsSample4DialogOpen = false, null,
                TaskScheduler.FromCurrentSynchronizationContext());
    }

    #endregion
}
