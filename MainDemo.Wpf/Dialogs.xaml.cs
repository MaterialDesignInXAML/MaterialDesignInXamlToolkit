using MaterialDesignThemes.Wpf;
using System;

namespace MaterialDesignDemo
{
    public partial class Dialogs
    {
        public Dialogs() => InitializeComponent();

        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine($"SAMPLE 1: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
                FruitListBox.Items.Add(FruitTextBox.Text.Trim());
        }

        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
            => Console.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine($"SAMPLE 5: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

            //you can cancel the dialog close:
            //eventArgs.Cancel();

            if (!Equals(eventArgs.Parameter, true))
                return;

            if (!string.IsNullOrWhiteSpace(AnimalTextBox.Text))
                AnimalListBox.Items.Add(AnimalTextBox.Text.Trim());
        }
    }
}
