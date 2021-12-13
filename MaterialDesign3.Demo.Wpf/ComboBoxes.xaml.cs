﻿using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class ComboBoxes
    {
        public ComboBoxes()
        {
            InitializeComponent();
            DataContext = new ComboBoxesViewModel();
        }

        private void ClearFilledComboBox_Click(object sender, System.Windows.RoutedEventArgs e)
            => FilledComboBox.SelectedItem = null;

        private void ClearOutlinedComboBox_Click(object sender, System.Windows.RoutedEventArgs e)
            => OutlinedComboBox.SelectedItem = null;
    }
}
