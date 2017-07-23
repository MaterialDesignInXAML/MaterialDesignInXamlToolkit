using System.Configuration;
using MaterialDesignDemo;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            DemoItems = new[]
            {
                new DemoItem("Home", new Home(),
                    new []
                    {
                        new DocumentationLink(DocumentationLinkType.Wiki, $"{ConfigurationManager.AppSettings["GitHub"]}/wiki", "WIKI"),
                        DocumentationLink.DemoPageLink<Home>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/#"), 
                    }
                ),
                new DemoItem("Palette", new PaletteSelector { DataContext = new PaletteSelectorViewModel() },
                    new []
                    {
                        DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                        DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                        DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PaletteHelper>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/style/color.html#color-color-palette"), 
                    }),
                new DemoItem("Buttons & Toggles", new Buttons(),
                    new []
                    {
                        DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                        DocumentationLink.DemoPageLink<Buttons>(),
                        DocumentationLink.StyleLink("Button"),
                        DocumentationLink.StyleLink("CheckBox"),
                        DocumentationLink.StyleLink("PopupBox"),
                        DocumentationLink.StyleLink("ToggleButton"),
                        DocumentationLink.ApiLink<PopupBox>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/buttons.html#"), 
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Fields", new TextFields(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<TextFields>(),
                        DocumentationLink.StyleLink("TextBox"),
                        DocumentationLink.StyleLink("ComboBox"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/text-fields.html#"), 
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Pickers", new Pickers { DataContext = new PickersViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<TextFields>(),
                        DocumentationLink.StyleLink("Clock"),
                        DocumentationLink.StyleLink("DatePicker"),
                        DocumentationLink.ApiLink<TimePicker>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/pickers.html"), 
                    }),
                new DemoItem("Sliders", new Sliders(), new []
                    {
                        DocumentationLink.DemoPageLink<Sliders>(),
                        DocumentationLink.StyleLink("Sliders"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/sliders.html#"), 
                    }),
                new DemoItem("Chips", new Chips(), new []
                    {
                        DocumentationLink.DemoPageLink<Chips>(),
                        DocumentationLink.StyleLink("Chip"),
                        DocumentationLink.ApiLink<Chip>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/chips.html#chips-usage"), 
                    }),
                new DemoItem("Typography", new Typography(), new []
                    {
                        DocumentationLink.DemoPageLink<Typography>(),
                        DocumentationLink.StyleLink("TextBlock"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/style/typography.html#typography-styles")
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto,
                        HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Cards", new Cards(), new []
                    {
                        DocumentationLink.DemoPageLink<Cards>(),
                        DocumentationLink.StyleLink("Card"),
                        DocumentationLink.ApiLink<Card>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/cards.html"), 
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new DemoItem("Icon Pack", new IconPack { DataContext = new IconPackViewModel() },
                    new []
                    {
                        DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                        DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PackIcon>()
                    }),
                new DemoItem("Colour Zones", new ColorZones(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<ColorZones>(),
                        DocumentationLink.ApiLink<ColorZone>()
                    }),
                new DemoItem("Lists", new Lists { DataContext = new ListsAndGridsViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Lists>("Demo View"),
                        DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.StyleLink("ListBox"),
                        DocumentationLink.StyleLink("ListView")
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new DemoItem("Trees", new Trees { DataContext = new TreesViewModel() },
                    new []
                    {
                        DocumentationLink.DemoPageLink<Trees>("Demo View"),
                        DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("TreeView")
                    }),
                new DemoItem("Grids", new Grids { DataContext = new ListsAndGridsViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Lists>("Demo View"),
                        DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("DataGrid")
                    }),
                new DemoItem("Expander", new Expander(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Expander>(),
                        DocumentationLink.StyleLink("Expander"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/expansion-panels.html#"), 
                    }),
                new DemoItem("Group Boxes", new GroupBoxes(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Cards>(),
                        DocumentationLink.StyleLink("Card")
                    }),
                new DemoItem("Menus & Tool Bars", new MenusAndToolBars(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                        DocumentationLink.StyleLink("Menu"),
                        DocumentationLink.StyleLink("ToolBar"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/menus.html#menus-usage"), 
                    }),
                new DemoItem("Progress Indicators", new Progress(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Progress>(),
                        DocumentationLink.StyleLink("ProgressBar"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/progress-activity.html#progress-activity-types-of-indicators"), 
                    }),
                new DemoItem("Dialogs", new Dialogs { DataContext = new DialogsViewModel()},
                    new []
                    {
                        DocumentationLink.WikiLink("Dialogs", "Dialogs"),
                        DocumentationLink.DemoPageLink<Dialogs>("Demo View"),
                        DocumentationLink.DemoPageLink<DialogsViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<DialogHost>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/dialogs.html#"), 
                    }),
                new DemoItem("Drawer", new Drawers(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Drawers>(),
                        DocumentationLink.ApiLink<DrawerHost>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/patterns/navigation-drawer.html#"), 
                    }),
                new DemoItem("Snackbar", new Snackbars(),
                    new []
                    {
                        DocumentationLink.WikiLink("Snackbar", "Snackbar"),
                        DocumentationLink.DemoPageLink<Snackbars>(),
                        DocumentationLink.StyleLink("Snackbar"),
                        DocumentationLink.ApiLink<Snackbar>(),
                        DocumentationLink.ApiLink<ISnackbarMessageQueue>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/components/snackbars-toasts.html#"), 
                    }),
                new DemoItem("Transitions", new Transitions(),
                    new []
                    {
                        DocumentationLink.WikiLink("Transitions", "Transitions"),
                        DocumentationLink.DemoPageLink<Transitions>(),
                        DocumentationLink.ApiLink<Transitioner>("Transitions"),
                        DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                        DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/motion/choreography.html#choreography-radial-reaction"), 
                    }),
                new DemoItem("Shadows", new Shadows(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Shadows>(),
                        DocumentationLink.GuidelineLink("https://material.io/guidelines/material-design/elevation-shadows.html#"), 
                    }),
            };
        }

        public DemoItem[] DemoItems { get; }
    }
}