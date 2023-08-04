using System.Windows.Data;

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
                (cell.Column as MaterialDesignThemes.Wpf.DataGridTextColumn)?.ElementStringFormat is string stringFormat
                && element is TextBlock t
                && t.GetBindingExpression(TextBlock.TextProperty)?.ParentBinding is Binding binding
                )
            {
                var NewBinding = binding.Clone();
                NewBinding.StringFormat = stringFormat;
                t.SetBinding(TextBlock.TextProperty, NewBinding);
            }

            return element;
        }

        //This approach could also be used to implement a custom string format on the EditElement, but the same effect can be achieved via the Binding property, so it's not necessary.
        /// <summary>Set an alternate string format for cells in this column that are not in edit mode.</summary>
        /// <remarks>Cells that are in edit mode will continue to use the default binding.</remarks>
        public string? ElementStringFormat { get => (string?)GetValue(ElementStringFormatProperty); set => SetValue(ElementStringFormatProperty, value); }
        public static readonly DependencyProperty ElementStringFormatProperty = DependencyProperty.Register(nameof(ElementStringFormat), typeof(string), typeof(DataGridTextColumn), new PropertyMetadata(null));

        /// <summary>
        /// Set the maximum length for the text field.
        /// </summary>
        /// <remarks>Not a dependency property, as is only applied once.</remarks>
        public int MaxLength { get; set; }
    }
}
