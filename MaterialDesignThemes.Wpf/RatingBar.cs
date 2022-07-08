using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A custom control implementing a rating bar.
    /// The icon aka content may be set as a DataTemplate via the ButtonContentTemplate property.
    /// </summary>
    public class RatingBar : Control
    {
        public static readonly RoutedCommand SelectRatingCommand = new();

        static RatingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingBar), new FrameworkPropertyMetadata(typeof(RatingBar)));
        }

        private readonly ObservableCollection<RatingBarButton> _ratingButtonsInternal = new ObservableCollection<RatingBarButton>();
        private readonly ReadOnlyObservableCollection<RatingBarButton> _ratingButtons;

        public RatingBar()
        {
            CommandBindings.Add(new CommandBinding(SelectRatingCommand, SelectItemHandler));
            _ratingButtons = new ReadOnlyObservableCollection<RatingBarButton>(_ratingButtonsInternal);
        }

        private void SelectItemHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Parameter is int && !IsReadOnly)
            {
                // Get mouse offset inside source
                int buttonValue = (int) executedRoutedEventArgs.Parameter;
                RatingBarButton b = (RatingBarButton)executedRoutedEventArgs.OriginalSource;
                Point p = Mouse.GetPosition(b);
                if (Orientation == Orientation.Horizontal)
                {
                    double value = b.ActualWidth / p.X;
                    Value = value <= 2 ? buttonValue : buttonValue - 0.5;
                }
                else
                {
                    double value = b.ActualHeight / p.Y;
                    Value = value <= 2 ? buttonValue : buttonValue - 0.5;
                }
            }
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Min), typeof(int), typeof(RatingBar), new PropertyMetadata(1, MinPropertyChangedCallback));

        public int Min
        {
            get => (int)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Max), typeof(int), typeof(RatingBar), new PropertyMetadata(5, MaxPropertyChangedCallback));

        public int Max
        {
            get => (int)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(RatingBar), new PropertyMetadata(0.0, ValuePropertyChangedCallback, ValueCoerceValueCallback));

        private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ratingBar = (RatingBar)dependencyObject;
            foreach (var button in ratingBar.RatingButtons)
            {
                button.IsWithinSelectedValue = button.Value <= (double)dependencyPropertyChangedEventArgs.NewValue;
                button.IsHalfwayWithinSelectedValue = button.Value <= (double)dependencyPropertyChangedEventArgs.NewValue + 0.5;
            }
            OnValueChanged(ratingBar, dependencyPropertyChangedEventArgs);
        }

        private static object ValueCoerceValueCallback(DependencyObject d, object baseValue)
        {
            var ratingBar = (RatingBar) d;
            if (baseValue is double value)
            {
                // Coerce the value into a multiple of 0.5 and within the bounds
                double valueInCorrectMultiple = Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
                return Math.Min(ratingBar.Max, Math.Max(ratingBar.Min - 1, valueInCorrectMultiple));
            }
            return (double)ratingBar.Min;
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Value),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<double>),
                typeof(RatingBar));

        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }

        private static void OnValueChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (RatingBar)d;
            var args = new RoutedPropertyChangedEventArgs<double>(
                    (double)e.OldValue,
                    (double)e.NewValue)
            { RoutedEvent = ValueChangedEvent };
            instance.RaiseEvent(args);
        }

        public ReadOnlyObservableCollection<RatingBarButton> RatingButtons => _ratingButtons;

        public static readonly DependencyProperty ValueItemContainerButtonStyleProperty = DependencyProperty.Register(
            nameof(ValueItemContainerButtonStyle), typeof(Style), typeof(RatingBar), new PropertyMetadata(default(Style)));

        public Style? ValueItemContainerButtonStyle
        {
            get => (Style?)GetValue(ValueItemContainerButtonStyleProperty);
            set => SetValue(ValueItemContainerButtonStyleProperty, value);
        }

        public static readonly DependencyProperty ValueItemTemplateProperty = DependencyProperty.Register(
            nameof(ValueItemTemplate), typeof(DataTemplate), typeof(RatingBar), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate? ValueItemTemplate
        {
            get => (DataTemplate?)GetValue(ValueItemTemplateProperty);
            set => SetValue(ValueItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ValueItemTemplateSelectorProperty = DependencyProperty.Register(
            nameof(ValueItemTemplateSelector), typeof(DataTemplateSelector), typeof(RatingBar), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector? ValueItemTemplateSelector
        {
            get => (DataTemplateSelector?)GetValue(ValueItemTemplateSelectorProperty);
            set => SetValue(ValueItemTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation), typeof(Orientation), typeof(RatingBar), new PropertyMetadata(default(Orientation)));

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            nameof(IsReadOnly), typeof(bool), typeof(RatingBar), new PropertyMetadata(default(bool)));

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private static void MaxPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((RatingBar)dependencyObject).RebuildButtons();
        }

        private static void MinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((RatingBar)dependencyObject).RebuildButtons();
        }

        private void RebuildButtons()
        {
            _ratingButtonsInternal.Clear();
            for (var i = Min; i <= Max; i++)
            {
                _ratingButtonsInternal.Add(new RatingBarButton
                {
                    Content = i,
                    ContentTemplate = ValueItemTemplate,
                    ContentTemplateSelector = ValueItemTemplateSelector,
                    IsWithinSelectedValue = i <= Value,
                    Style = ValueItemContainerButtonStyle,
                    Value = i,
                });
            }
        }

        public override void OnApplyTemplate()
        {
            RebuildButtons();

            base.OnApplyTemplate();
        }
    }

    internal class TextBlockForegroundConverter : IMultiValueConverter
    {
        public static TextBlockForegroundConverter Instance { get; } = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO add orientation as another parameter (might also need min/max to derive percentage)
            if (values?.Length == 6
                && values[0] is SolidColorBrush brush
                && values[1] is int min
                && values[2] is int max
                && values[3] is Orientation orientation
                && values[4] is double value
                && values[5] is int buttonValue)
            {
                if (value >= buttonValue)
                    return brush;

                var opaque = brush.Color;
                var semiTransparent = Color.FromArgb(0x42, brush.Color.R, brush.Color.G, brush.Color.B);

                if (value >= buttonValue - 0.5)
                {
                    return new LinearGradientBrush
                    {
                        StartPoint = orientation == Orientation.Horizontal ? new Point(0, 0.5) : new Point(0.5, 0),
                        EndPoint = orientation == Orientation.Horizontal ? new Point(1, 0.5) : new Point(0.5, 1),
                        GradientStops = new()
                        {
                            new GradientStop {Color = opaque, Offset = 0.5},
                            new GradientStop {Color = semiTransparent, Offset = 0.5}
                        }
                    };
                }

                return new SolidColorBrush(semiTransparent);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
