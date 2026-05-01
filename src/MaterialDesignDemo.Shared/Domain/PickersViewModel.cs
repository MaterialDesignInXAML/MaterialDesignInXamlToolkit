using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignDemo.Domain;

public partial class PickersViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime _date;
    [ObservableProperty]
    private DateTime _time;
    [ObservableProperty]
    private string? _validatingTime;
    [ObservableProperty]
    private DateTime? _futureValidatingDate;

    public IReadOnlyList<int> AvailableMinuteSelectionSteps { get; }

    [ObservableProperty]
    private int _selectedMinuteSelectionStep;

    public PickersViewModel()
    {
        Date = DateTime.Now;
        Time = DateTime.Now;

        AvailableMinuteSelectionSteps = [ 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60 ];
        SelectedMinuteSelectionStep = 15;
    }
}
