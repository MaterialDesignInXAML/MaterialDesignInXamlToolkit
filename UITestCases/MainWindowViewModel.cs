using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using UITestCases.TestCases.DialogHost;

namespace UITestCases
{
    public class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<ITestCase> TestCases { get; }

        public ICommand Execute { get; }

        private ITestCase _selectedTestCase;
        public ITestCase SelectedTestCase
        {
            get => _selectedTestCase;
            set => Set(ref _selectedTestCase, value);
        }

        public MainWindowViewModel()
        {
            TestCases = new ObservableCollection<ITestCase>
            {
                new DialogHostOverlay()
            };

            Execute = new RelayCommand(OnExecute);
        }

        private async void OnExecute()
        {
            if (SelectedTestCase is { } testCase)
            {
                await testCase.Execute();
            }
        }
    }
}
