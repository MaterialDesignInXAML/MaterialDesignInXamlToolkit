using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaterialDesignColors.WpfExample.Domain
{
    public sealed class Movie : BindableBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _director;

        public string Director
        {
            get { return _director; }
            set
            {
                _director = value;
                OnPropertyChanged();
            }
        }
    }

    public sealed class MovieCategory : BindableBase
    {
        public string Name { get; }

        public ObservableCollection<Movie> Movies { get; }

        public MovieCategory(string name)
        {
            Name = name;
            Movies = new ObservableCollection<Movie>();
        }
    }

    public sealed class TreesViewModel : BindableBase
    {
        public ObservableCollection<MovieCategory> MovieCategories { get; }

        public AnotherCommandImplementation AddCommand { get; }

        public AnotherCommandImplementation RemoveSelectedItemCommand { get; }

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public TreesViewModel()
        {
            MovieCategories = new ObservableCollection<MovieCategory>
            {
                new MovieCategory("Action")
                {
                    Movies =
                    {
                        new Movie
                        {
                            Name = "Predator",
                            Director = "John McTiernan"
                        },
                        new Movie
                        {
                            Name = "Alien",
                            Director = "Ridley Scott"
                        },
                        new Movie
                        {
                            Name = "Prometheus",
                            Director = "Ridley Scott"
                        }
                    }
                },
                new MovieCategory("Comedy")
                {
                    Movies =
                    {
                        new Movie
                        {
                            Name = "EuroTrip",
                            Director = "Jeff Schaffer"
                        },
                        new Movie
                        {
                            Name = "EuroTrip",
                            Director = "Jeff Schaffer"
                        }
                    }
                }
            };

            AddCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!MovieCategories.Any())
                    {
                        MovieCategories.Add(new MovieCategory(GenerateString(15)));
                    }
                    else
                    {
                        int index = new Random().Next(0, MovieCategories.Count);

                        MovieCategories[index].Movies.Add(
                            new Movie
                            {
                                Name = GenerateString(15),
                                Director = GenerateString(20)
                            });
                    }
                });

            RemoveSelectedItemCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (SelectedItem is MovieCategory)
                    {
                        MovieCategories.Remove(SelectedItem as MovieCategory);
                    }
                    else if (SelectedItem is Movie)
                    {
                        var movie = SelectedItem as Movie;
                        MovieCategories.FirstOrDefault(v => v.Movies.Contains(movie))?.Movies.Remove(movie);
                    }
                },
                _ => SelectedItem != null);
        }

        private static string GenerateString(int length)
        {
            var random = new Random();

            return String.Join(String.Empty,
                Enumerable.Range(0, length)
                .Select(v => (char) random.Next((int)'a', (int)'z' + 1)));
        }
    }
}
