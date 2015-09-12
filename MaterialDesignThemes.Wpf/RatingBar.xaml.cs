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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A custom control implementing a rating bar.
    /// The icon may be set via the Icon or the PathData property.
    /// </summary>
    public partial class RatingBar : UserControl
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(BitmapImage), typeof(RatingBar));

        public BitmapImage Icon
        {
            get { return (BitmapImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData", typeof(Geometry), typeof(RatingBar));

        public Geometry PathData
        {
            get { return (Geometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
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

                if (rating > 5)
                {
                    rating = 5;
                }

                SetValue(RatingProperty, rating);
            }
        }

        private Button[] _ratingButtons;

        public RatingBar()
        {
            Icon = null;
            PathData = null;
            Rating = 3;

            InitializeComponent();

            _ratingButtons = new Button[] { RatingButtonOne, RatingButtonTwo, RatingButtonThree, RatingButtonFour, RatingButtonFive };

            foreach (Button ratingButton in _ratingButtons)
            {
                ratingButton.DataContext = this;
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
