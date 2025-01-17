using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignColors;
using MaterialDesignDemo.Shared.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignDemo.Domain;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue, string? startupPage)
    {
        DemoItems =
        [
            new DemoItem(
                "Home",
                typeof(Home),
                [
                    new DocumentationLink(
                        DocumentationLinkType.Wiki,
                        $"{ConfigurationManager.AppSettings["GitHub"]}/wiki",
                        "WIKI"),
                    DocumentationLink.DemoPageLink<Home>()
                ]
            )
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
            },
            .. GenerateDemoItems(snackbarMessageQueue).OrderBy(i => i.Name),
        ];
        SelectedItem = DemoItems.FirstOrDefault(di => string.Equals(di.Name, startupPage, StringComparison.CurrentCultureIgnoreCase)) ?? DemoItems.First();
        _demoItemsView = CollectionViewSource.GetDefaultView(DemoItems);
        _demoItemsView.Filter = DemoItemsFilter;

        LoadVersions();
    }

    private readonly ICollectionView _demoItemsView;

    [ObservableProperty]
    private string? _searchKeyword;

    partial void OnSearchKeywordChanged(string? oldValue, string? newValue)
    {
        _demoItemsView.Refresh();
    }

    [ObservableProperty]
    private string? _nugetVersions;

    private void LoadVersions()
    {
        string? mdixVersion = GetVersion<Theme>("MDIXVersion");
        string? mdixColorsVersion = GetVersion<Swatch>("MDIXColorsVersion");

        NugetVersions = $"""
            MDIX: {mdixVersion}
            MDIX Colors: {mdixColorsVersion}
            """;

        static string? GetVersion<T>(string attributeKey)
        {
            return typeof(T).Assembly
                .GetCustomAttributes<AssemblyMetadataAttribute>()
                .SingleOrDefault(x => x.Key == attributeKey)?.Value;
        }
    }

    public ObservableCollection<DemoItem> DemoItems { get; }


    [ObservableProperty]
    private DemoItem? _selectedItem;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(MoveNextCommand))]
    [NotifyCanExecuteChangedFor(nameof(MovePrevCommand))]
    private int _selectedIndex;

    [ObservableProperty]
    private bool _controlsEnabled = true;

    [RelayCommand]
    private void OnHome()
    {
        SearchKeyword = string.Empty;
        SelectedIndex = 0;
    }

    [RelayCommand(CanExecute = nameof(CanMovePrevious))]
    private void OnMovePrev()
    {
        if (!string.IsNullOrWhiteSpace(SearchKeyword))
            SearchKeyword = string.Empty;

        SelectedIndex--;
    }

    private bool CanMovePrevious() => SelectedIndex > 0;

    [RelayCommand(CanExecute = nameof(CanMoveNext))]
    private void OnMoveNext()
    {
        if (!string.IsNullOrWhiteSpace(SearchKeyword))
            SearchKeyword = string.Empty;

        SelectedIndex++;
    }

    private bool CanMoveNext() => SelectedIndex < DemoItems.Count - 1;

    private static IEnumerable<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
    {
        yield return new DemoItem(
            "Palette",
            typeof(PaletteSelector),
            [
                DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model","Domain"),
                DocumentationLink.ApiLink<PaletteHelper>()
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
        };

        yield return new DemoItem(
            "ColorTool",
            typeof(ColorTool),
            [
                DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                DocumentationLink.DemoPageLink<ColorTool>("Demo View"),
                DocumentationLink.DemoPageLink<ColorToolViewModel>("Demo View Model","Domain"),
                DocumentationLink.ApiLink<PaletteHelper>()
            ]);

        yield return new DemoItem(
            "Button",
            typeof(Buttons),
            [
                DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                DocumentationLink.DemoPageLink<Buttons>("Demo View"),
                DocumentationLink.DemoPageLink<ButtonsViewModel>("Demo View Model", "Domain"),
                DocumentationLink.StyleLink("Button"),
                DocumentationLink.StyleLink("PopupBox"),
                DocumentationLink.ApiLink<PopupBox>()
            ]);

        yield return new DemoItem(
            "Toggle",
            typeof(Toggles),
            [
                DocumentationLink.DemoPageLink<Toggles>(),
                DocumentationLink.StyleLink("ToggleButton"),
                DocumentationLink.StyleLink("CheckBox"),
                DocumentationLink.ApiLink<Toggles>()
            ]);

        yield return new DemoItem(
            "RatingBar",
            typeof(RatingBar),
            [
                DocumentationLink.DemoPageLink<RatingBar>(),
                DocumentationLink.StyleLink("RatingBar"),
                DocumentationLink.ApiLink<RatingBar>()
            ]);

        yield return new DemoItem(
            "Field",
            typeof(Fields),
            [
                DocumentationLink.DemoPageLink<Fields>(),
                DocumentationLink.StyleLink("TextBox")
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
        };

        yield return new DemoItem(
            "Fields line up",
            typeof(FieldsLineUp),
            [
                DocumentationLink.DemoPageLink<FieldsLineUp>()
            ]);

        yield return new DemoItem(
            "ComboBox",
            typeof(ComboBoxes),
            [
                DocumentationLink.DemoPageLink<ComboBoxes>(),
                DocumentationLink.StyleLink("ComboBox")
            ]);

        yield return new DemoItem(
            "Picker",
            typeof(Pickers),
            [
                DocumentationLink.DemoPageLink<Pickers>(),
                DocumentationLink.StyleLink("Clock"),
                DocumentationLink.StyleLink("DatePicker"),
                DocumentationLink.ApiLink<TimePicker>()
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
        };

        yield return new DemoItem(
            "Slider",
            typeof(Sliders),
            [
                DocumentationLink.DemoPageLink<Sliders>(),
                DocumentationLink.StyleLink("Slider")
            ]);

        yield return new DemoItem(
            "Chip",
            typeof(Chips),
            [
                DocumentationLink.DemoPageLink<Chips>(),
                DocumentationLink.StyleLink("Chip"),
                DocumentationLink.ApiLink<Chip>()
            ]);

        yield return new DemoItem(
            "Typography",
            typeof(Typography),
            [
                DocumentationLink.DemoPageLink<Typography>(),
                DocumentationLink.StyleLink("TextBlock")
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
        };

        yield return new DemoItem(
            "Card",
            typeof(Cards),
            [
                DocumentationLink.DemoPageLink<Cards>(),
                DocumentationLink.StyleLink("Card"),
                DocumentationLink.ApiLink<Card>()
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
        };

        yield return new DemoItem(
            "Icons",
            typeof(IconPack),
            [
                DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model", "Domain"),
                DocumentationLink.ApiLink<PackIcon>()
            ],
            new IconPackViewModel(snackbarMessageQueue))
        {
            //The icons view handles its own scrolling
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
            VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
        };

        yield return new DemoItem(
            "ColorZone",
            typeof(ColorZones),
            [
                DocumentationLink.DemoPageLink<ColorZones>(),
                DocumentationLink.ApiLink<ColorZone>()
            ]);

        yield return new DemoItem(
            "List",
            typeof(Lists),
            [
                DocumentationLink.DemoPageLink<Lists>("Demo View"),
                DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                DocumentationLink.StyleLink("ListBox"),
                DocumentationLink.StyleLink("ListView")
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
        };

        yield return new DemoItem(
            "Tab",
            typeof(Tabs),
            [
                DocumentationLink.DemoPageLink<Tabs>(),
                DocumentationLink.StyleLink("TabControl")
            ]);

        yield return new DemoItem(
            "Tree",
            typeof(Trees),
            [
                DocumentationLink.DemoPageLink<Trees>("Demo View"),
                DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model", "Domain"),
                DocumentationLink.StyleLink("TreeView")
            ]);

        yield return new DemoItem(
            "DataGrid",
            typeof(DataGrids),
            [
                DocumentationLink.DemoPageLink<DataGrids>("Demo View"),
                DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                DocumentationLink.StyleLink("DataGrid")
            ]);

        yield return new DemoItem(
            "Expander",
            typeof(Expander),
            [
                DocumentationLink.DemoPageLink<Expander>(),
                DocumentationLink.StyleLink("Expander")
            ]);

        yield return new DemoItem(
            "GroupBox",
            typeof(GroupBoxes),
            [
                DocumentationLink.DemoPageLink<GroupBoxes>(),
                DocumentationLink.StyleLink("GroupBox")
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
        };

        yield return new DemoItem(
            "Menu & ToolBar",
            typeof(MenusAndToolBars),
            [
                DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                DocumentationLink.StyleLink("Menu"),
                DocumentationLink.StyleLink("ToolBar")
            ]);

        yield return new DemoItem(
            "Progress Indicator",
            typeof(Progress),
            [
                DocumentationLink.DemoPageLink<Progress>(),
                DocumentationLink.StyleLink("ProgressBar")
            ]);

        yield return new DemoItem(
            "NavigationRail",
            typeof(NavigationRail),
            [
                DocumentationLink.DemoPageLink<NavigationRail>("Demo View"),
                DocumentationLink.StyleLink("TabControl"),
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
        };

        yield return new DemoItem(
            "Dialog",
            typeof(Dialogs),
            [
                DocumentationLink.WikiLink("Dialogs", "Dialogs"),
                DocumentationLink.DemoPageLink<Dialogs>("Demo View"),
                DocumentationLink.DemoPageLink<DialogsViewModel>("Demo View Model", "Domain"),
                DocumentationLink.ApiLink<DialogHost>()
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
        };

        yield return new DemoItem(
            "Drawer",
            typeof(Drawers),
            [
                DocumentationLink.DemoPageLink<Drawers>("Demo View"),
                DocumentationLink.ApiLink<DrawerHost>()
            ]);

        yield return new DemoItem(
            "Snackbar",
            typeof(Snackbars),
            [
                DocumentationLink.WikiLink("Snackbar", "Snackbar"),
                DocumentationLink.DemoPageLink<Snackbars>(),
                DocumentationLink.StyleLink("Snackbar"),
                DocumentationLink.ApiLink<Snackbar>(),
                DocumentationLink.ApiLink<ISnackbarMessageQueue>()
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
        };

        yield return new DemoItem(
            "Transition",
            typeof(Transitions),
            [
                DocumentationLink.WikiLink("Transitions", "Transitions"),
                DocumentationLink.DemoPageLink<Transitions>(),
                DocumentationLink.ApiLink<Transitioner>("Transitions"),
                DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
            ]);

        yield return new DemoItem(
            "Elevation",
            typeof(Elevation),
            [
                DocumentationLink.DemoPageLink<Elevation>(),
                DocumentationLink.StyleLink("Shadows"),
                DocumentationLink.SpecsLink("https://material.io/design/environment/elevation.html", "Elevation")
            ])
        {
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
        };

        yield return new DemoItem(
            "Smart Hint",
            typeof(SmartHint),
            [
                DocumentationLink.DemoPageLink<SmartHint>(),
                DocumentationLink.StyleLink("SmartHint"),
            ])
        {
            //The smart hint view handles its own scrolling
            HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
            VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
        };

        yield return new DemoItem(
            "PopupBox",
            typeof(PopupBox),
            [
                DocumentationLink.DemoPageLink<PopupBox>(),
                DocumentationLink.StyleLink("PopupBox"),
            ]);

        yield return new DemoItem(nameof(NumericUpDown), typeof(NumericUpDown),
        [
            DocumentationLink.DemoPageLink<NumericUpDown>(),
            DocumentationLink.StyleLink(nameof(NumericUpDown)),
            DocumentationLink.ApiLink<NumericUpDown>(),
            DocumentationLink.ApiLink<DecimalUpDown>(),
            DocumentationLink.ApiLink<UpDownBase>()
        ]);
    }

    private bool DemoItemsFilter(object obj)
    {
        string? searchKeyword = SearchKeyword;

        if (string.IsNullOrWhiteSpace(searchKeyword))
        {
            return true;
        }

        return obj is DemoItem item
#if NET6_0_OR_GREATER
               && item.Name.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase);
#else
               && item.Name.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0;
#endif
    }
}
