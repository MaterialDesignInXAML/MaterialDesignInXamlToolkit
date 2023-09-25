using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo;

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

        App app = (App)Application.Current;
        DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue!, app.StartupPage);

        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();

        switch (app.InitialTheme)
        {
            case BaseTheme.Dark:
                ModifyTheme(true);
                break;
            case BaseTheme.Light:
                ModifyTheme(false);
                break;
        }

        if (app.InitialFlowDirection == FlowDirection.RightToLeft)
        {
            FlowDirectionToggleButton.IsChecked = true;
            FlowDirection = FlowDirection.RightToLeft;
        }

        DarkModeToggleButton.IsChecked = theme.GetBaseTheme() == BaseTheme.Dark;

        if (paletteHelper.GetThemeManager() is { } themeManager)
        {
            themeManager.ThemeChanged += (_, e)
                => DarkModeToggleButton.IsChecked = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
        }

        Snackbar = MainSnackbar;
    }

    private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (NavDrawer.OpenMode is not DrawerHostOpenMode.Standard)
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
    {
        DemoItemsSearchBox.Focus();
        if (ActualWidth > 1600)
        {
            NavRail.Visibility = Visibility.Collapsed;
            MenuToggleButton.Visibility = Visibility.Collapsed;
        }

    }


    private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        => ModifyTheme(DarkModeToggleButton.IsChecked == true);

    private void FlowDirectionButton_Click(object sender, RoutedEventArgs e)
        => FlowDirection = FlowDirectionToggleButton.IsChecked.GetValueOrDefault(false)
            ? FlowDirection.RightToLeft
            : FlowDirection.LeftToRight;

    private static void ModifyTheme(bool isDarkTheme)
    {
        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();

        theme.SetBaseTheme(isDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
        paletteHelper.SetTheme(theme);
    }

    private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
        => MainScrollViewer.ScrollToHome();

    private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        => Link.OpenInBrowser(ConfigurationManager.AppSettings["GitHub"]);

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (ActualWidth <= 700)
        {
            NavRail.Visibility = Visibility.Collapsed;
            NavBar.Visibility = Visibility.Visible;
            NavDrawer.OpenMode = DrawerHostOpenMode.Modal;
            NavDrawer.IsLeftDrawerOpen = false;
            MenuToggleButton.Visibility = Visibility.Visible;
            FAB.Visibility = Visibility.Visible;
            DrawerFAB.Visibility = Visibility.Collapsed;
        }
        else if (ActualWidth > 700 && ActualWidth <= 1600)
        {
            NavRail.Visibility = Visibility.Visible;
            NavBar.Visibility = Visibility.Collapsed;
            NavDrawer.OpenMode = DrawerHostOpenMode.Modal;
            NavDrawer.IsLeftDrawerOpen = false;
            MenuToggleButton.Visibility = Visibility.Visible;
            FAB.Visibility = Visibility.Collapsed;
            DrawerFAB.Visibility = Visibility.Collapsed;
        }
        else if (ActualWidth > 1600)
        {
            NavRail.Visibility = Visibility.Collapsed;
            NavBar.Visibility = Visibility.Collapsed;
            NavDrawer.OpenMode = DrawerHostOpenMode.Standard;
            NavDrawer.IsLeftDrawerOpen = true;
            MenuToggleButton.Visibility = Visibility.Collapsed;
            FAB.Visibility = Visibility.Collapsed;
            DrawerFAB.Visibility = Visibility.Visible;
        }
    }

    private void MenuOpen_Click(object sender, RoutedEventArgs e)
    {
        NavDrawer.IsLeftDrawerOpen = false;
        if (ActualWidth > 1600)
        {
            NavRail.Visibility = Visibility.Visible;
            MenuToggleButton.Visibility = Visibility.Visible;
        }

    }

    private void CloseNotificationPanel_Click(object sender, RoutedEventArgs e) => NotificationPanel.Visibility = Visibility.Collapsed;
}
