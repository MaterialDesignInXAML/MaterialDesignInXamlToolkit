using System.Collections.Generic;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo
{
    public partial class NavigationRail
    {

        public List<ItemSample> SampleList { get; set; }

        public NavigationRail()
        {
            InitializeComponent();
            DataContext = this;

            SampleList = new()
            {
                new ItemSample
                {
                    Title = "Payment",
                    SelectedIcon = PackIconKind.CreditCard,
                    UnselectedIcon = PackIconKind.CreditCardOutline,
                },
                new ItemSample
                {
                    Title = "Home",
                    SelectedIcon = PackIconKind.Home,
                    UnselectedIcon = PackIconKind.HomeOutline,
                },
                new ItemSample
                {
                    Title = "Special",
                    SelectedIcon = PackIconKind.Star,
                    UnselectedIcon = PackIconKind.StarOutline,
                },
                new ItemSample
                {
                    Title = "Shared",
                    SelectedIcon = PackIconKind.Users,
                    UnselectedIcon = PackIconKind.UsersOutline,
                },
                new ItemSample
                {
                    Title = "Files",
                    SelectedIcon = PackIconKind.Folder,
                    UnselectedIcon = PackIconKind.FolderOutline,
                },
                new ItemSample
                {
                    Title = "Library",
                    SelectedIcon = PackIconKind.Bookshelf,
                    UnselectedIcon = PackIconKind.Bookshelf,
                },
            };
        }


    }

    public class ItemSample
    {
        public string Title { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
    }
}