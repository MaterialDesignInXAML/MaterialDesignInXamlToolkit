using MaterialDesignColors.WpfExample.Domain;

namespace MaterialDesignDemo
{
    public partial class ComboBoxes
    {
        public ComboBoxes()
        {
            InitializeComponent();
            DataContext = new FieldsViewModel();
        }

        private void ClearFilledComboBox_Click(object sender, System.Windows.RoutedEventArgs e)
            => FilledComboBox.SelectedItem = null;
    }
}
