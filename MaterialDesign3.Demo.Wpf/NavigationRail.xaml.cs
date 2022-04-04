using System.Collections.Generic;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo;

public partial class NavigationRail
{

    public List<SampleItem> SampleList { get; set; }

    public NavigationRail()
    {
        InitializeComponent();
        DataContext = this;

        SampleList = new()
        {
            new SampleItem
            {
                Title = "Payment",
                SelectedIcon = PackIconKind.CreditCard,
                UnselectedIcon = PackIconKind.CreditCardOutline,
                Notification = 1
            },
            new SampleItem
            {
                Title = "Home",
                SelectedIcon = PackIconKind.Home,
                UnselectedIcon = PackIconKind.HomeOutline,
            },
            new SampleItem
            {
                Title = "Special",
                SelectedIcon = PackIconKind.Star,
                UnselectedIcon = PackIconKind.StarOutline,
            },
            new SampleItem
            {
                Title = "Shared",
                SelectedIcon = PackIconKind.Users,
                UnselectedIcon = PackIconKind.UsersOutline,
            },
            new SampleItem
            {
                Title = "Files",
                SelectedIcon = PackIconKind.Folder,
                UnselectedIcon = PackIconKind.FolderOutline,
            },
            new SampleItem
            {
                Title = "Library",
                SelectedIcon = PackIconKind.Bookshelf,
                UnselectedIcon = PackIconKind.Bookshelf,
            },
        };
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        => SampleList[0].Notification = SampleList[0].Notification is null ? 1 : null;

    private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        => SampleList[0].Notification = SampleList[0].Notification is null ? "123+" : null;
}
