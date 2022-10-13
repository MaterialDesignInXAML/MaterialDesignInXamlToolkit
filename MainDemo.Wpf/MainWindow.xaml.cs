using System.Diagnostics;
using System.Threading;
using System.Windows.Media;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Theming;
using Theme = MaterialDesignThemes.Wpf.Theming.Theme;

namespace MaterialDesignDemo
{
    public partial class MainWindow
    {
        public static Snackbar Snackbar = new();
        public MainWindow()
        {
            InitializeComponent();

            Task.Factory.StartNew(() => Thread.Sleep(2500)).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue?.Enqueue("Welcome to Material Design In XAML Toolkit");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue!);

            var themeManager = ThemeManager.GetApplicationThemeManager()!;
            var theme = themeManager.GetTheme();

            DarkModeToggleButton.IsChecked = theme.GetBaseTheme() == BaseTheme.Dark;

            themeManager.ThemeChanged += (_, e)
                => DarkModeToggleButton.IsChecked = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;

            Snackbar = MainSnackbar;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen flag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() }
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private void OnCopy(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string stringValue)
            {
                try
                {
                    Clipboard.SetDataObject(stringValue);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
            => DemoItemsSearchBox.Focus();

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
            => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private void FlowDirectionButton_Click(object sender, RoutedEventArgs e)
            => FlowDirection = FlowDirectionToggleButton.IsChecked.GetValueOrDefault(false)
                ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;

        private static void ModifyTheme(bool isDarkTheme)
        {
            ThemeManager themeManager = ThemeManager.GetApplicationThemeManager()!;
            var theme = themeManager.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            themeManager.SetTheme(theme);
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
            => MainScrollViewer.ScrollToHome();
    }
}
