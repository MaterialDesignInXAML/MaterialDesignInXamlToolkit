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

        protected override FrameworkElement? GenerateElement(DataGridCell cell, object dataItem)
        {
            var element = base.GenerateElement(cell, dataItem);

            if (
                element is TextBlock t
                && cell.Column is System.Windows.Controls.DataGridTextColumn col
                && DataGridAssist.GetElementStringFormat(col) is string stringFormat
                && t.GetBindingExpression(TextBlock.TextProperty)?.ParentBinding is Binding binding
                )
            {
                var NewBinding = binding.Clone();
                NewBinding.StringFormat = stringFormat;
                t.SetBinding(TextBlock.TextProperty, NewBinding);
            }

            return element;
        }

        /// <summary>
        /// Set the maximum length for the text field.
        /// </summary>
        /// <remarks>Not a dependency property, as is only applied once.</remarks>
        public int MaxLength { get; set; }
    }
}
