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


}
