using System.Windows;
using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class Trees
    {
        private readonly TreesViewModel _viewModel;

        public Trees()
        {
            _viewModel = new TreesViewModel();
            DataContext = _viewModel;
            InitializeComponent();
        }

        /// <summary>
        /// TreesView's SelectedItem is read-only. Hence we can't bind it. There is a way to obtain a selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) => _viewModel.SelectedItem = e.NewValue;
    }
}
