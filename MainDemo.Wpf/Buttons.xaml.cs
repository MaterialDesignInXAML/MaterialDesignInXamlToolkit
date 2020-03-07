using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignColors.WpfExample.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Buttons.xaml
    /// </summary>
    public partial class Buttons : UserControl
    {
        public Buttons()
        {
            InitializeComponent();
            FloatingActionDemoCommand = new AnotherCommandImplementation(Execute);
        }

        public ICommand FloatingActionDemoCommand { get; }

        private void Execute(object o)
        {
            Console.WriteLine("Floating action button command. - " + (o ?? "NULL").ToString());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just checking we haven't suppressed the button.");
        }

        private void PopupBox_OnOpened(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just making sure the popup has opened.");
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Just making sure the popup has closed.");
        }

        private void CountingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CountingBadge.Badge == null || Equals(CountingBadge.Badge, ""))
                CountingBadge.Badge = 0;

            var next = int.Parse(CountingBadge.Badge.ToString()) + 1;

            CountingBadge.Badge = next < 21 ? (object)next : null;
        }
    }
}
