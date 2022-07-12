using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public class DataGridTextColumn : System.Windows.Controls.DataGridTextColumn
    {
        protected override object? PrepareCellForEdit(FrameworkElement? editingElement, RoutedEventArgs editingEventArgs)
        {
            if (editingElement is TextBox textBox)
            {
                textBox.MaxLength = MaxLength;
                textBox.SelectionStart = textBox.Text.Length;
            }

            editingElement?.Focus();

            return null;
        }

        /// <summary>
        /// Set the maximum length for the text field.
        /// </summary>
        /// <remarks>Not a dependency property, as is only applied once.</remarks>
        public int MaxLength { get; set; }
    }
}