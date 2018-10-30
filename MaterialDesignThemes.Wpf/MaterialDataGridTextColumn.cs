using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDataGridTextColumn : DataGridTextColumn
    {
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            var textBox = editingElement as TextBox;
            if (textBox != null)
            {                
                textBox.MaxLength = MaxLength;
                textBox.SelectionStart = textBox.Text.Length;
            }

            editingElement.Focus();            

            return null;
        }

        /// <summary>
        /// Set the maximum length for the text field.
        /// </summary>
        /// <remarks>Not a dprop, as is only applied once.</remarks>
        public int MaxLength { get; set; }
    }
}