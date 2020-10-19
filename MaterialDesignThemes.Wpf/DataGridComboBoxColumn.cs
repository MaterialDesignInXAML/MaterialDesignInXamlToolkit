using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf
{
    public class DataGridComboBoxColumn : System.Windows.Controls.DataGridComboBoxColumn //DataGridBoundColumn
    {
        static DataGridComboBoxColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridComboBoxColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridComboBoxColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        public Binding ItemsSourceBinding { get; set; }

        public bool? IsEditable { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var comboBox = base.GenerateElement(cell, cell);

            if (ItemsSourceBinding != null)
                comboBox.SetBinding(ItemsControl.ItemsSourceProperty, ItemsSourceBinding);
            ApplyStyle(false, false, comboBox);

            return comboBox;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var comboBox = (ComboBox)base.GenerateElement(cell, cell);
            if (IsEditable is bool isEditable)
            {
                comboBox.IsEditable = isEditable;
            }

            if (ItemsSourceBinding != null)
                comboBox.SetBinding(ItemsControl.ItemsSourceProperty, ItemsSourceBinding);
            ApplyStyle(true, false, comboBox);

            return comboBox;
        }

        public static new Style DefaultElementStyle
        {
            get
            {
                var comboBox = new ComboBox();

                var brushKey = new ComponentResourceKey(typeof(ComboBox), "MaterialDataGridComboBoxColumnStyle");
                var style = (Style)comboBox.TryFindResource(brushKey);

                return style;
            }
        }

        public static new Style DefaultEditingElementStyle
        {
            get
            {
                var comboBox = new ComboBox();

                var brushKey = new ComponentResourceKey(typeof(ComboBox), "MaterialDataGridComboBoxColumnEditingStyle");
                var style = (Style)comboBox.TryFindResource(brushKey);

                return style;
            }
        }

        private void ApplyStyle(bool isEditing, bool defaultToElementStyle, FrameworkElement element)
        {
            var style = PickStyle(isEditing, defaultToElementStyle);
            if (style != null)
            {
                element.Style = style;
            }
        }

        private Style PickStyle(bool isEditing, bool defaultToElementStyle)
        {
            var style = isEditing ? EditingElementStyle : ElementStyle;
            if (isEditing && defaultToElementStyle && (style == null))
            {
                style = ElementStyle;
            }

            return style;
        }

        protected override void CancelCellEdit(FrameworkElement editingElement, object uneditedValue)
        {
            if (editingElement is ComboBox comboBox)
                comboBox.SetCurrentValue(ComboBox.IsDropDownOpenProperty, false);
            base.CancelCellEdit(editingElement, uneditedValue);
        }

        protected override bool CommitCellEdit(FrameworkElement editingElement)
        {
            if (editingElement is ComboBox comboBox)
                comboBox.SetCurrentValue(ComboBox.IsDropDownOpenProperty, false);
            return base.CommitCellEdit(editingElement);
        }

        /*
        /// <summary>
        ///     Assigns the Binding to the desired property on the target object.
        /// </summary>
        private void ApplyBinding(DependencyObject target, DependencyProperty property)
        {
            var binding = Binding;
            if (binding != null)
            {
                BindingOperations.SetBinding(target, property, binding);
            }
            else
            {
                BindingOperations.ClearBinding(target, property);
            }
        }
        */

    }
}