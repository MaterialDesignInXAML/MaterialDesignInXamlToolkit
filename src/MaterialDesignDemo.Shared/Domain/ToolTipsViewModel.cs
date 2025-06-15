using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Shared.Domain;

public partial class ToolTipsViewModel : ObservableObject
{
    public ToolTipsViewModel()
    {
        ResetToDefaults();
    }

    [RelayCommand]
    private void ResetToDefaults()
    {
        IsPopupOpen = false;
        SelectedElevation = Elevation.Dp6;
        PopupUniformCornerRadius = 8;
        PopupHorizontalOffset = 0;
        PopupVerticalOffset = 0;
        SelectedPopupBoxPlacementMode = PopupBoxPlacementMode.TopAndAlignCentres;
        SelectedPopupAnimation = PopupAnimation.Fade;
        SelectedPopupBoxPopupMode = PopupBoxPopupMode.Click;
    }

    [ObservableProperty]
    private bool _isPopupOpen;

    public List<Elevation> Elevations { get; } = EnumToEnumerable<Elevation>().ToList();
    [ObservableProperty]
    private Elevation _selectedElevation;

    [ObservableProperty]
    private int _popupUniformCornerRadius;

    [ObservableProperty]
    private int _popupHorizontalOffset;

    [ObservableProperty]
    private int _popupVerticalOffset;

    public List<PopupBoxPlacementMode> PopupBoxPlacementModes { get; } = EnumToEnumerable<PopupBoxPlacementMode>().ToList();
    [ObservableProperty]
    private PopupBoxPlacementMode _selectedPopupBoxPlacementMode;

    public List<PopupAnimation> PopupAnimations { get; } = EnumToEnumerable<PopupAnimation>().ToList();
    [ObservableProperty]
    private PopupAnimation _selectedPopupAnimation;

    public List<PopupBoxPopupMode> PopupBoxPopupModes { get; } = EnumToEnumerable<PopupBoxPopupMode>().ToList();
    [ObservableProperty]
    private PopupBoxPopupMode _selectedPopupBoxPopupMode;

    private static IEnumerable<T> EnumToEnumerable<T>() where T : Enum
        => Enum.GetValues(typeof(T)).Cast<T>();
}
