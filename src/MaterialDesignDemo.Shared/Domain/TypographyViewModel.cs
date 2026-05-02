using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Shared.Domain;

public sealed partial class TypographyViewModel : ObservableObject
{
    public TypographyViewModel()
    {
        PackIcons = Enum.GetValues(typeof(PackIconKind))
            .Cast<PackIconKind>()
            .Distinct()
            .ToList();

        SelectedPackIcon = PackIconKind.Account;
    }

    public IReadOnlyList<PackIconKind> PackIcons { get; }

    [ObservableProperty]
    private PackIconKind _selectedPackIcon;
}
