using System.Diagnostics;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo
{
    public partial class Dialogs
    {
        public Dialogs()
        {
            DataContext = new DialogsViewModel();
            InitializeComponent();
        }

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine($"SAMPLE 1: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
                FruitListBox.Items.Add(FruitTextBox.Text.Trim());
        }

        // Used for DialogHost.DialogClosingAttached
        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine($"SAMPLE 5: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(AnimalTextBox.Text))
                AnimalListBox.Items.Add(AnimalTextBox.Text.Trim());
        }
    }
}
