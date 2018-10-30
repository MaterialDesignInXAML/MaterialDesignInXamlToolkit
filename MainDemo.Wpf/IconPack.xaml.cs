using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialDesignDemo
{
    /// <summary>
    /// Interaction logic for Icons.xaml
    /// </summary>
    public partial class IconPack : UserControl
    {
        public IconPack()
        {
            InitializeComponent();
        }

        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(textBox.SelectAll));
        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
                SearchButton.Command.Execute(textBox.Text);
        }
    }
}
