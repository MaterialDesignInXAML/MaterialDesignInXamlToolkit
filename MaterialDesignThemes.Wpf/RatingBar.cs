using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A custom control implementing a rating bar.
    /// The icon aka content may be set as a DataTemplate via the ButtonContentTemplate property.
    /// </summary>
    public class RatingBar : Control
    {
        private const string RatingBarGridPartName = "PART_ratingBarGrid";

        private static readonly DependencyProperty ButtonContentTemplateProperty = DependencyProperty.Register("ButtonContentTemplate", typeof(DataTemplate), typeof(RatingBar));

        public DataTemplate ButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(ButtonContentTemplateProperty); }

            set {
                SetValue(ButtonContentTemplateProperty, value);

                RebuildUi();
            }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxRating", typeof(int), typeof(RatingBar));

        public int MaxRating
        {
            get { return (int)GetValue(MaxValueProperty); }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("the maximum value must be greater than 1");
                }

                SetValue(MaxValueProperty, value);

                RebuildUi();
            }
        }

        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("Rating", typeof(int), typeof(RatingBar));

        public int Rating
        {
            get { return (int)GetValue(RatingProperty); }

            set
            {
                int rating = value;

                if (rating < 1)
                {
                    rating = 1;
                }

                if (rating > MaxRating)
                {
                    rating = MaxRating;
                }

                SetValue(RatingProperty, rating);

                UpdateButtonOpacity();
            }
        }

        private Button[] _ratingButtons;

        static RatingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingBar), new FrameworkPropertyMetadata(typeof(RatingBar)));
        }

        public RatingBar()
        {
            MaxRating = 5;
            Rating = 3;
        }

        public override void OnApplyTemplate()
        {
            RebuildUi();

            base.OnApplyTemplate();
        }

        private void RebuildUi()
        {
            // rebuild the grid as basic layout
            Grid grid = GetTemplateChild(RatingBarGridPartName) as Grid;

            if (grid != null)
            {
                grid.ColumnDefinitions.Clear();
                grid.Children.Clear();

                for (int i = 0; i < MaxRating; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
                }

                // create buttons
                if (_ratingButtons != null)
                {
                    foreach (Button button in _ratingButtons)
                    {
                        button.Click -= RatingButtonClick;
                    }
                }

                _ratingButtons = new Button[MaxRating];

                for (int i = 0; i < MaxRating; i++)
                {
                    Button button = new Button();
                    button.ContentTemplate = ButtonContentTemplate;

                    button.DataContext = this;
                    Binding enabledBinding = new Binding("IsEnabled");
                    enabledBinding.Mode = BindingMode.OneWay;
                    enabledBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(button, Button.IsEnabledProperty, enabledBinding);

                    button.Click += RatingButtonClick;

                    _ratingButtons[i] = button;

                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, 0);
                    grid.Children.Add(button);

                    Style style = null;
                    Style basedOn = TryFindResource("MaterialDesignRatingBarButton") as Style;

                    if (basedOn != null)
                    {
                        style = new Style(typeof(Button), basedOn);
                    }
                    else
                    {
                        style = new Style(typeof(Button));
                    }

                    button.Style = style;
                }

                UpdateButtonOpacity();
            }
        }

        private void UpdateButtonOpacity()
        {
            if (_ratingButtons != null)
            {
                for (int i = 0; i < _ratingButtons.Length; i++)
                {
                    if ((i + 1) <= Rating)
                    {
                        _ratingButtons[i].Opacity = 1.0;
                    }
                    else
                    {
                        _ratingButtons[i].Opacity = 0.5;
                    }
                }
            }
        }

        private int GetRatingForButton(Button ratingButton)
        {
            for (int i = 0; i < _ratingButtons.Length; i++)
            {
                if (_ratingButtons[i] == ratingButton)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        private void RatingButtonClick(object sender, RoutedEventArgs args)
        {
            Rating = GetRatingForButton(sender as Button);
        }
    }
}
