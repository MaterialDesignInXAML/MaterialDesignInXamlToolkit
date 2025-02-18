using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignThemes.UITests.Samples.UpDownControls;

//If needed this can be refactored to be generic so all variations of the UpDownControl can use this as a VM
public partial class BoundNumericUpDownViewModel : ObservableObject
{
    [ObservableProperty]
    private int _value;
}
