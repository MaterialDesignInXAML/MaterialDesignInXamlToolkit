using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Data;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignDemo.Domain
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            DemoItems = new ObservableCollection<DemoItem>(new[]
            {
                new DemoItem(
                    "Home",
                    typeof(Home),
                    new[]
                    {
                        new DocumentationLink(
                            DocumentationLinkType.Wiki,
                            $"{ConfigurationManager.AppSettings["GitHub"]}/wiki",
                            "WIKI"),
                        DocumentationLink.DemoPageLink<Home>()
                    }
                )
            });

            foreach (var item in GenerateDemoItems(snackbarMessageQueue).OrderBy(i => i.Name))
            {
                DemoItems.Add(item);
            }

            _demoItemsView = CollectionViewSource.GetDefaultView(DemoItems);
            _demoItemsView.Filter = DemoItemsFilter;

            HomeCommand = new AnotherCommandImplementation(
                _ =>
                {
                    SearchKeyword = string.Empty;
                    SelectedIndex = 0;
                });

            MovePrevCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!string.IsNullOrWhiteSpace(SearchKeyword))
                        SearchKeyword = string.Empty;

                    SelectedIndex--;
                },
                _ => SelectedIndex > 0);

            MoveNextCommand = new AnotherCommandImplementation(
               _ =>
               {
                   if (!string.IsNullOrWhiteSpace(SearchKeyword))
                       SearchKeyword = string.Empty;

                   SelectedIndex++;
               },
               _ => SelectedIndex < DemoItems.Count - 1);
        }

        private readonly ICollectionView _demoItemsView;
        private DemoItem? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;
        private bool _controlsEnabled = true;

        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    _demoItemsView.Refresh();
                }
            }
        }

        public ObservableCollection<DemoItem> DemoItems { get; }

        public DemoItem? SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        public bool ControlsEnabled
        {
            get => _controlsEnabled;
            set => SetProperty(ref _controlsEnabled, value);
        }

        public AnotherCommandImplementation HomeCommand { get; }
        public AnotherCommandImplementation MovePrevCommand { get; }
        public AnotherCommandImplementation MoveNextCommand { get; }

        private static IEnumerable<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue is null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            yield return new DemoItem(
                "Palette",
                typeof(PaletteSelector),
                new[]
                {
                    DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                    DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                    DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                    DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                    DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PaletteHelper>()
                });

            yield return new DemoItem(
                "Color Tool",
                typeof(ColorTool),
                new[]
                {
                    DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                    DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                    DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                    DocumentationLink.DemoPageLink<ColorTool>("Demo View"),
                    DocumentationLink.DemoPageLink<ColorToolViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PaletteHelper>()
                });

            yield return new DemoItem(
                "Buttons",
                typeof(Buttons),
                new[]
                {
                    DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                    DocumentationLink.DemoPageLink<Buttons>("Demo View"),
                    DocumentationLink.DemoPageLink<ButtonsViewModel>("Demo View Model"),
                    DocumentationLink.StyleLink("Button"),
                    DocumentationLink.StyleLink("PopupBox"),
                    DocumentationLink.ApiLink<PopupBox>()
                });

            yield return new DemoItem(
                "Toggles",
                typeof(Toggles),
                new[]
                {
                    DocumentationLink.DemoPageLink<Toggles>(),
                    DocumentationLink.StyleLink("ToggleButton"),
                    DocumentationLink.StyleLink("CheckBox"),
                    DocumentationLink.ApiLink<Toggles>()
                });

            yield return new DemoItem(
                "Rating Bar",
                typeof(RatingBar),
                new[]
                {
                    DocumentationLink.DemoPageLink<RatingBar>(),
                    DocumentationLink.StyleLink("RatingBar"),
                    DocumentationLink.ApiLink<RatingBar>()
                });

            yield return new DemoItem(
                "Fields",
                typeof(Fields),
                new[]
                {
                    DocumentationLink.DemoPageLink<Fields>(),
                    DocumentationLink.StyleLink("TextBox")
                });

            yield return new DemoItem(
                "Fields line up",
                typeof(FieldsLineUp),
                new[]
                {
                    DocumentationLink.DemoPageLink<FieldsLineUp>()
                });

            yield return new DemoItem(
                "ComboBoxes",
                typeof(ComboBoxes),
                new[]
                {
                    DocumentationLink.DemoPageLink<ComboBoxes>(),
                    DocumentationLink.StyleLink("ComboBox")
                });

            yield return new DemoItem(
                "Pickers",
                typeof(Pickers),
                new[]
                {
                    DocumentationLink.DemoPageLink<Pickers>(),
                    DocumentationLink.StyleLink("Clock"),
                    DocumentationLink.StyleLink("DatePicker"),
                    DocumentationLink.ApiLink<TimePicker>()
                });

            yield return new DemoItem(
                "Sliders",
                typeof(Sliders),
                new[]
                {
                    DocumentationLink.DemoPageLink<Sliders>(),
                    DocumentationLink.StyleLink("Slider")
                });

            yield return new DemoItem(
                "Chips",
                typeof(Chips),
                new[]
                {
                    DocumentationLink.DemoPageLink<Chips>(),
                    DocumentationLink.StyleLink("Chip"),
                    DocumentationLink.ApiLink<Chip>()
                });

            yield return new DemoItem(
                "Typography",
                typeof(Typography),
                new[]
                {
                    DocumentationLink.DemoPageLink<Typography>(),
                    DocumentationLink.StyleLink("TextBlock")
                })
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Cards",
                typeof(Cards),
                new[]
                {
                    DocumentationLink.DemoPageLink<Cards>(),
                    DocumentationLink.StyleLink("Card"),
                    DocumentationLink.ApiLink<Card>()
                });

            yield return new DemoItem(
                "Icon Pack",
                typeof(IconPack),
                new[]
                {
                    DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                    DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PackIcon>()
                },
                new IconPackViewModel(snackbarMessageQueue))
                {
                    //The icons view handles its own scrolling
                    HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled,
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
                };

            yield return new DemoItem(
                "Colour Zones",
                typeof(ColorZones),
                new[]
                {
                    DocumentationLink.DemoPageLink<ColorZones>(),
                    DocumentationLink.ApiLink<ColorZone>()
                });

            yield return new DemoItem(
                "Lists",
                typeof(Lists),
                new[]
                {
                    DocumentationLink.DemoPageLink<Lists>("Demo View"),
                    DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.StyleLink("ListBox"),
                    DocumentationLink.StyleLink("ListView")
                });

            yield return new DemoItem(
                "Tabs",
                typeof(Tabs),
                new[]
                {
                    DocumentationLink.DemoPageLink<Tabs>(),
                    DocumentationLink.StyleLink("TabControl")
                });

            yield return new DemoItem(
                "Trees",
                typeof(Trees),
                new[]
                {
                    DocumentationLink.DemoPageLink<Trees>("Demo View"),
                    DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model"),
                    DocumentationLink.StyleLink("TreeView")
                });

            yield return new DemoItem(
                "Data Grids",
                typeof(DataGrids),
                new[]
                {
                    DocumentationLink.DemoPageLink<DataGrids>("Demo View"),
                    DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.StyleLink("DataGrid")
                });

            yield return new DemoItem(
                "Expander",
                typeof(Expander),
                new[]
                {
                    DocumentationLink.DemoPageLink<Expander>(),
                    DocumentationLink.StyleLink("Expander")
                });

            yield return new DemoItem(
                "Group Boxes",
                typeof(GroupBoxes),
                new[]
                {
                    DocumentationLink.DemoPageLink<GroupBoxes>(),
                    DocumentationLink.StyleLink("GroupBox")
                });

            yield return new DemoItem(
                "Menus & Tool Bars",
                typeof(MenusAndToolBars),
                new[]
                {
                    DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                    DocumentationLink.StyleLink("Menu"),
                    DocumentationLink.StyleLink("ToolBar")
                });

            yield return new DemoItem(
                "Progress Indicators",
                typeof(Progress),
                new[]
                {
                    DocumentationLink.DemoPageLink<Progress>(),
                    DocumentationLink.StyleLink("ProgressBar")
                });

            yield return new DemoItem(
                "Navigation Rail",
                typeof(NavigationRail),
                new[]
                {
                    DocumentationLink.DemoPageLink<NavigationRail>("Demo View"),
                    DocumentationLink.StyleLink("TabControl"),
                });

            yield return new DemoItem(
                "Dialogs",
                typeof(Dialogs),
                new[]
                {
                    DocumentationLink.WikiLink("Dialogs", "Dialogs"),
                    DocumentationLink.DemoPageLink<Dialogs>("Demo View"),
                    DocumentationLink.DemoPageLink<DialogsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.ApiLink<DialogHost>()
                })
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Drawer",
                typeof(Drawers),
                new[]
                {
                    DocumentationLink.DemoPageLink<Drawers>("Demo View"),
                    DocumentationLink.ApiLink<DrawerHost>()
                });

            yield return new DemoItem(
                "Snackbar",
                typeof(Snackbars),
                new[]
                {
                    DocumentationLink.WikiLink("Snackbar", "Snackbar"),
                    DocumentationLink.DemoPageLink<Snackbars>(),
                    DocumentationLink.StyleLink("Snackbar"),
                    DocumentationLink.ApiLink<Snackbar>(),
                    DocumentationLink.ApiLink<ISnackbarMessageQueue>()
                })
            {
                HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
            };

            yield return new DemoItem(
                "Transitions",
                typeof(Transitions),
                new[]
                {
                    DocumentationLink.WikiLink("Transitions", "Transitions"),
                    DocumentationLink.DemoPageLink<Transitions>(),
                    DocumentationLink.ApiLink<Transitioner>("Transitions"),
                    DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                    DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
                });

            yield return new DemoItem(
                "Elevation",
                typeof(Elevation),
                new[]
                {
                    DocumentationLink.DemoPageLink<Elevation>(),
                    DocumentationLink.StyleLink("Shadows"),
                    DocumentationLink.SpecsLink("https://material.io/design/environment/elevation.html", "Elevation")
                });
        }

        private bool DemoItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchKeyword))
            {
                return true;
            }

            return obj is DemoItem item
                   && item.Name.ToLower().Contains(_searchKeyword!.ToLower());
        }
    }
}