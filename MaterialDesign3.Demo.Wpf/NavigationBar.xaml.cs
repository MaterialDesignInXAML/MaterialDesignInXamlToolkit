using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesign3Demo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public List<SampleItem> SampleList { get; set; }
        public NavigationBar()
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
}
