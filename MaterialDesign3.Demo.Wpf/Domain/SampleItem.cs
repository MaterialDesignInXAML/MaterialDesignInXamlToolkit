using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo.Domain;

public class SampleItem
{
    public string? Title { get; set; }
    public PackIconKind SelectedIcon { get; set; }
    public PackIconKind UnselectedIcon { get; set; }
}
