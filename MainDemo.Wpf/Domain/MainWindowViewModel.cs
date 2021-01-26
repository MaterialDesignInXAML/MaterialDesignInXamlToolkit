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
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            DemoItems = new ObservableCollection<DemoItem>(new[]
            {
                new DemoItem(
                    "Home",
                    new Home(),
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

            HomeCommand = new AnotherCommandImplementation(_ => { SelectedIndex = 0; });

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

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ICollectionView _demoItemsView;
        private DemoItem? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;

        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchKeyword)));
                _demoItemsView.Refresh();
            }
        }

        public ObservableCollection<DemoItem> DemoItems { get; }

        public DemoItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
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
                new PaletteSelector { DataContext = new PaletteSelectorViewModel() },
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
                new ColorTool { DataContext = new ColorToolViewModel() },
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
                new Buttons { DataContext = new ButtonsViewModel() },
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
                new Toggles(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Toggles>(),
                    DocumentationLink.StyleLink("ToggleButton"),
                    DocumentationLink.StyleLink("CheckBox"),
                    DocumentationLink.ApiLink<Toggles>()
                });

            yield return new DemoItem(
                "Rating Bar",
                new RatingBar(),
                new[]
                {
                    DocumentationLink.DemoPageLink<RatingBar>(),
                    DocumentationLink.StyleLink("RatingBar"),
                    DocumentationLink.ApiLink<RatingBar>()
                });

            yield return new DemoItem(
                "Fields",
                new Fields(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Fields>(),
                    DocumentationLink.StyleLink("TextBox")
                });

            yield return new DemoItem(
                "Fields line up",
                new FieldsLineUp(),
                new[]
                {
                    DocumentationLink.DemoPageLink<FieldsLineUp>()
                });

            yield return new DemoItem(
                "ComboBoxes",
                new ComboBoxes(),
                new[]
                {
                    DocumentationLink.DemoPageLink<ComboBoxes>(),
                    DocumentationLink.StyleLink("ComboBox")
                });

            yield return new DemoItem(
                "Pickers",
                new Pickers { DataContext = new PickersViewModel() },
                new[]
                {
                    DocumentationLink.DemoPageLink<Pickers>(),
                    DocumentationLink.StyleLink("Clock"),
                    DocumentationLink.StyleLink("DatePicker"),
                    DocumentationLink.ApiLink<TimePicker>()
                });

            yield return new DemoItem(
                "Sliders",
                new Sliders(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Sliders>(),
                    DocumentationLink.StyleLink("Slider")
                });

            yield return new DemoItem(
                "Chips",
                new Chips(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Chips>(),
                    DocumentationLink.StyleLink("Chip"),
                    DocumentationLink.ApiLink<Chip>()
                });

            yield return new DemoItem(
                "Typography",
                new Typography(),
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
                new Cards(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Cards>(),
                    DocumentationLink.StyleLink("Card"),
                    DocumentationLink.ApiLink<Card>()
                });

            yield return new DemoItem(
                "Icon Pack",
                new IconPack { DataContext = new IconPackViewModel(snackbarMessageQueue) },
                new[]
                {
                    DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                    DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model"),
                    DocumentationLink.ApiLink<PackIcon>()
                })
            {
                VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled
            };

            yield return new DemoItem(
                "Colour Zones",
                new ColorZones(),
                new[]
                {
                    DocumentationLink.DemoPageLink<ColorZones>(),
                    DocumentationLink.ApiLink<ColorZone>()
                });

            yield return new DemoItem(
                "Lists",
                new Lists { DataContext = new ListsAndGridsViewModel() },
                new[]
                {
                    DocumentationLink.DemoPageLink<Lists>("Demo View"),
                    DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.StyleLink("ListBox"),
                    DocumentationLink.StyleLink("ListView")
                });

            yield return new DemoItem(
                "Trees",
                new Trees { DataContext = new TreesViewModel() },
                new[]
                {
                    DocumentationLink.DemoPageLink<Trees>("Demo View"),
                    DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model"),
                    DocumentationLink.StyleLink("TreeView")
                });

            yield return new DemoItem(
                "Data Grids",
                new DataGrids { DataContext = new ListsAndGridsViewModel() },
                new[]
                {
                    DocumentationLink.DemoPageLink<DataGrids>("Demo View"),
                    DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                    DocumentationLink.StyleLink("DataGrid")
                });

            yield return new DemoItem(
                "Expander",
                new Expander(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Expander>(),
                    DocumentationLink.StyleLink("Expander")
                });

            yield return new DemoItem(
                "Group Boxes",
                new GroupBoxes(),
                new[]
                {
                    DocumentationLink.DemoPageLink<GroupBoxes>(),
                    DocumentationLink.StyleLink("GroupBox")
                });

            yield return new DemoItem(
                "Menus & Tool Bars",
                new MenusAndToolBars(),
                new[]
                {
                    DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                    DocumentationLink.StyleLink("Menu"),
                    DocumentationLink.StyleLink("ToolBar")
                });

            yield return new DemoItem(
                "Progress Indicators",
                new Progress(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Progress>(),
                    DocumentationLink.StyleLink("ProgressBar")
                });

            yield return new DemoItem(
                "Navigation Rail",
                new NavigationRail(),
                new[]
                {
                    DocumentationLink.DemoPageLink<NavigationRail>("Demo View"),
                    DocumentationLink.StyleLink("TabControl"),
                });

            yield return new DemoItem(
                "Dialogs",
                new Dialogs { DataContext = new DialogsViewModel() },
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
                new Drawers(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Drawers>("Demo View"),
                    DocumentationLink.ApiLink<DrawerHost>()
                });

            yield return new DemoItem(
                "Snackbar",
                new Snackbars(),
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
                new Transitions(),
                new[]
                {
                    DocumentationLink.WikiLink("Transitions", "Transitions"),
                    DocumentationLink.DemoPageLink<Transitions>(),
                    DocumentationLink.ApiLink<Transitioner>("Transitions"),
                    DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                    DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
                });

            yield return new DemoItem(
                "Shadows",
                new Shadows(),
                new[]
                {
                    DocumentationLink.DemoPageLink<Shadows>(),
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