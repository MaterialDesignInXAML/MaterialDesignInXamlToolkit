using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain;

/// <summary>
/// Interaction logic for QuickSearchDialog.xaml
/// </summary>
public partial class QuickSearchDialog : UserControl
{
    public QuickSearchDialog()
    {
        InitializeComponent();
    }

    private void SearchBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            DialogHost.Close("RootDialog", null);
        }
    }
}
