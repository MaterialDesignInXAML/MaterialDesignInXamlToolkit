using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A custom control implementing the concept of the app bar.
    /// It provides an optional toggle button for a side nav, a title, an optional search form and a content area (to add buttons for example).
    /// </summary>
    [ContentProperty("Children")]
    public partial class AppBar : UserControl
    {
        public static readonly RoutedEvent SearchEvent = EventManager.RegisterRoutedEvent("Search", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AppBar));

        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly("Children", typeof(UIElementCollection), typeof(AppBar), new PropertyMetadata());

        public UIElementCollection Children
        {
            get { return (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty); }
            private set { SetValue(ChildrenProperty, value); }
        }

        public static readonly DependencyProperty IsNavigationDrawerOpenProperty = DependencyProperty.Register("IsNavigationDrawerOpen", typeof(bool), typeof(AppBar));

        public bool IsNavigationDrawerOpen
        {
            get { return (bool)GetValue(IsNavigationDrawerOpenProperty); }
            set { SetValue(IsNavigationDrawerOpenProperty, value); }
        }

        public static readonly DependencyProperty SearchFormVisibilityProperty = DependencyProperty.Register("SearchFormVisibility", typeof(SearchFormVisibility), typeof(AppBar));

        public SearchFormVisibility SearchFormVisibility
        {
            get { return (SearchFormVisibility)GetValue(SearchFormVisibilityProperty); }
            set { SetValue(SearchFormVisibilityProperty, value); }
        }

        public static readonly DependencyProperty SearchHintProperty = DependencyProperty.Register("SearchHint", typeof(string), typeof(AppBar));

        public string SearchHint
        {
            get { return (string)GetValue(SearchHintProperty); }
            set { SetValue(SearchHintProperty, value); }
        }

        public static readonly DependencyProperty SearchTermProperty = DependencyProperty.Register("SearchTerm", typeof(string), typeof(AppBar));

        public string SearchTerm
        {
            get { return (string)GetValue(SearchTermProperty); }
            set { SetValue(SearchTermProperty, value); }
        }

        public static readonly DependencyProperty ShowNavigationDrawerButtonProperty = DependencyProperty.Register("ShowNavigationDrawerButton", typeof(bool), typeof(AppBar));

        public bool ShowNavigationDrawerButton
        {
            get { return (bool)GetValue(ShowNavigationDrawerButtonProperty); }
            set { SetValue(ShowNavigationDrawerButtonProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(AppBar));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public AppBar()
        {
            SearchFormVisibility = SearchFormVisibility.Folded;
            ShowNavigationDrawerButton = true;
            IsNavigationDrawerOpen = false;

            InitializeComponent();

            MenuToggleButton.DataContext = this;
            TitleText.DataContext = this;
            OpenSearchButton.DataContext = this;
            SearchFormStackPanel.DataContext = this;
            SearchTextBox.DataContext = this;
            PartHostActionButtons.DataContext = this;

            Children = PartHostActionButtons.Children;
        }

        private void SearchButtonClickHandler(object sender, RoutedEventArgs args)
        {
            if (SearchFormVisibility == SearchFormVisibility.Folded)
            {
                SearchFormVisibility = SearchFormVisibility.Visible;
                SearchTextBox.Focus();
            }
            else if (SearchFormVisibility == SearchFormVisibility.Visible)
            {
                RaiseEvent(new ActionBarSearchEventArgs(SearchEvent, SearchTerm));
            }
        }

        private void KeyDownHandler(object sender, KeyEventArgs args)
        {
            if (sender == SearchTextBox && args.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                RaiseEvent(new ActionBarSearchEventArgs(SearchEvent, SearchTerm));
            }
        }
    }

    public enum SearchFormVisibility
    {
        Folded,
        Hidden,
        Visible
    }

    public class ActionBarSearchEventArgs : RoutedEventArgs
    {
        public string SearchTerm
        {
            get;
            protected set;
        }

        public ActionBarSearchEventArgs(RoutedEvent routedEvent, string searchTerm)
            : base(routedEvent)
        {
            SearchTerm = searchTerm;
        }
    }
}
