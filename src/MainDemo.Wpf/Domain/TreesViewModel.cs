using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignDemo.Shared.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain;

public class TreeExampleSimpleTemplateSelector : DataTemplateSelector
{
    public DataTemplate? PlanetTemplate { get; set; }

    public DataTemplate? SolarSystemTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is Planet)
            return PlanetTemplate;

        if (item?.ToString() == "Solar System")
            return SolarSystemTemplate;

        return TreeViewAssist.SuppressAdditionalTemplate;
    }
}

public sealed class Movie
{
    public Movie(string name, string director)
    {
        Name = name;
        Director = director;
    }

    public string Name { get; }

    public string Director { get; }
}

public class Planet
{
    public string? Name { get; set; }

    public double DistanceFromSun { get; set; }

    public double DistanceFromEarth { get; set; }

    public double Velocity { get; set; }
}

public partial class TestItem : ObservableObject
{
    public TestItem? Parent { get; set; }
    public string Name { get; }
    public ObservableCollection<TestItem> Items { get; }

    // This property is used to determine if the item is expanded or not.
    // With the TreeListView control, the UI items are virtualized. Without
    // this property, the IsExpanded state of the TreeListViewItem would be lost
    // when it is recycled.
    //
    // For more information see:
    // https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/issues/3640#issuecomment-2274086113
    //
    // https://learn.microsoft.com/dotnet/desktop/wpf/advanced/optimizing-performance-controls?view=netframeworkdesktop-4.8&WT.mc_id=DT-MVP-5003472
    [ObservableProperty]
    private bool _isExpanded;

    public TestItem(string name, IEnumerable<TestItem> items)
    {
        Name = name;
        Items = new ObservableCollection<TestItem>(items);
    }
}

public sealed class MovieCategory
{
    public MovieCategory(string name, params Movie[] movies)
    {
        Name = name;
        Movies = new ObservableCollection<Movie>(movies);
    }

    public string Name { get; }

    public ObservableCollection<Movie> Movies { get; }
}

public sealed class TreesViewModel : ViewModelBase
{
    private object? _selectedItem;
    private TestItem? _selectedTreeItem;

    public ObservableCollection<TestItem> TreeItems { get; } = new();

    public ObservableCollection<MovieCategory> MovieCategories { get; }

    public AnotherCommandImplementation AddCommand { get; }

    public AnotherCommandImplementation RemoveSelectedItemCommand { get; }

    public AnotherCommandImplementation AddListTreeItemCommand { get; }

    public AnotherCommandImplementation RemoveListTreeItemCommand { get; }

    public TestItem? SelectedTreeItem
    {
        get => _selectedTreeItem;
        set => SetProperty(ref _selectedTreeItem, value);
    }

    public object? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    public TreesViewModel()
    {
        Random random = new();
        for(int i = 0; i < 10; i++)
        {
            TreeItems.Add(CreateTestItem(random, 1));
        }

        static TestItem CreateTestItem(Random random, int depth)
        {
            int numberOfChildren = depth < 5 ? random.Next(0, 6) : 0;
            var children = Enumerable.Range(0, numberOfChildren).Select(_ => CreateTestItem(random, depth + 1));
            var rv = new TestItem(GenerateString(random.Next(4, 10)), children); 
            foreach(var child in rv.Items)
            {
                child.Parent = rv;
            }
            return rv;
        }

        AddListTreeItemCommand = new(_ =>
        {
            if (SelectedTreeItem is { } treeItem)
            {
                var newItem = CreateTestItem(random, 1);
                newItem.Parent = treeItem;
                treeItem.Items.Add(newItem);
            }
            else
            {
                TreeItems.Add(CreateTestItem(random, 1));
            }
        });

        RemoveListTreeItemCommand = new(items =>
        {
            if (items is IEnumerable enumerable)
            {
                foreach(TestItem testItem in enumerable)
                {
                    if (testItem.Parent is { } parent)
                    {
                        parent.Items.Remove(testItem);
                    }
                    else
                    {
                        TreeItems.Remove(testItem);
                    }
                }
            }
            if (SelectedTreeItem is { } selectedItem)
            {
                if (selectedItem.Parent is { } parent)
                {
                    parent.Items.Remove(selectedItem);
                }
                else
                {
                    TreeItems.Remove(selectedItem);
                }
                SelectedTreeItem = null;
            }
        });

        MovieCategories = new ObservableCollection<MovieCategory>
        {
            new MovieCategory("Action",
                new Movie ("Predator", "John McTiernan"),
                new Movie("Alien", "Ridley Scott"),
                new Movie("Prometheus", "Ridley Scott")),
            new MovieCategory("Comedy",
                new Movie("EuroTrip", "Jeff Schaffer"),
                new Movie("EuroTrip", "Jeff Schaffer")
            )
        };
        MovieCategories.Add(MovieCategories[0]);
        MovieCategories.Add(MovieCategories[1]);

        AddCommand = new AnotherCommandImplementation(
            _ =>
            {
                if (!MovieCategories.Any())
                {
                    MovieCategories.Add(new MovieCategory(GenerateString(15)));
                }
                else
                {
                    var index = new Random().Next(0, MovieCategories.Count);

                    MovieCategories[index].Movies.Add(
                        new Movie(GenerateString(15), GenerateString(20)));
                }
            });

        RemoveSelectedItemCommand = new AnotherCommandImplementation(
            _ =>
            {
                var movieCategory = SelectedItem as MovieCategory;
                if (movieCategory != null)
                {
                    MovieCategories.Remove(movieCategory);
                }
                else
                {
                    var movie = SelectedItem as Movie;
                    if (movie == null) return;
                    MovieCategories.FirstOrDefault(v => v.Movies.Contains(movie))?.Movies.Remove(movie);
                }
            },
            _ => SelectedItem != null);
    }

    private static string GenerateString(int length)
    {
        var random = new Random();

        return string.Join(string.Empty,
            Enumerable.Range(0, length)
            .Select(v => (char)random.Next('a', 'z' + 1)));
    }
}
