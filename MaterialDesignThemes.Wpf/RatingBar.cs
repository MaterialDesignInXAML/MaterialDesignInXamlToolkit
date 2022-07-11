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
                if (!IsFractionValueEnabled)
                {
                    Value = (int)executedRoutedEventArgs.Parameter;
                    return;
                }

                // Get mouse offset inside source
                RatingBarButton b = (RatingBarButton)executedRoutedEventArgs.OriginalSource;
                Point p = Mouse.GetPosition(b);
                double percentSelected = Orientation == Orientation.Horizontal ? p.X / b.ActualWidth : p.Y / b.ActualHeight;
                Value = b.Value - 1 + percentSelected;
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

        private static readonly DependencyPropertyKey IsFractionValueEnabledPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsFractionValueEnabled", typeof(bool), typeof(RatingBar),
                new PropertyMetadata(default(bool)));

        internal static readonly DependencyProperty IsFractionValueEnabledProperty =
            IsFractionValueEnabledPropertyKey.DependencyProperty;

        internal bool IsFractionValueEnabled
        {
            get => (bool)GetValue(IsFractionValueEnabledProperty);
            private set => SetValue(IsFractionValueEnabledPropertyKey, value);
        }

        public static readonly DependencyProperty ValueIncrementsProperty = DependencyProperty.Register(
            nameof(ValueIncrements), typeof(double), typeof(RatingBar), new PropertyMetadata(1.0, ValueIncrementsPropertyChangedCallback, ValueIncrementsCoerceValueCallback));

        private static void ValueIncrementsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ratingBar = (RatingBar)d;
            ratingBar.IsFractionValueEnabled = Math.Abs(ratingBar.ValueIncrements - 1.0) > 1e-10;
            ratingBar.RebuildButtons();
        }

        private static object ValueIncrementsCoerceValueCallback(DependencyObject d, object baseValue)
            => Math.Max(double.Epsilon, Math.Min(1.0, (double)baseValue));

        /// <summary>
        /// Gets or sets the value increments. Set to a value between 0.0 and 1.0 (both exclusive) to enable fractional values. Default value is 1.0 (i.e. fractional values disabled)
        /// </summary>
        public double ValueIncrements
        {
            get { return (double) GetValue(ValueIncrementsProperty); }
            set { SetValue(ValueIncrementsProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(RatingBar), new PropertyMetadata(0.0, ValuePropertyChangedCallback, ValueCoerceValueCallback));

        private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ratingBar = (RatingBar)dependencyObject;
            foreach (var button in ratingBar.RatingButtons)
            {
                // The property being set here is no longer used. If the RatingBarButton (and the DP) was not public I would have just removed it.
                button.IsWithinSelectedValue = button.Value <= (double)dependencyPropertyChangedEventArgs.NewValue;
            }
            OnValueChanged(ratingBar, dependencyPropertyChangedEventArgs);
        }

        private static object ValueCoerceValueCallback(DependencyObject d, object baseValue)
        {
            var ratingBar = (RatingBar) d;

            // If factional values are disabled we don't do any coercion. This maintains back-compat where coercion was not applied. 
            if (!ratingBar.IsFractionValueEnabled)
                return baseValue;

            if (baseValue is double value)
            {
                // Coerce the value into a multiple of ValueIncrements and within the bounds.
                double valueInCorrectMultiple = Math.Round(value / ratingBar.ValueIncrements, MidpointRounding.AwayFromZero) * ratingBar.ValueIncrements;
                return Math.Min(ratingBar.Max, Math.Max(ratingBar.Min, valueInCorrectMultiple));
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
            // When fractional values are enabled, the first rating button represents the value Min when not selected at all and Min+1 when fully selected;
            // thus we start with the value Min+1 for the values of the rating buttons.
            int start = IsFractionValueEnabled ? Min + 1 : Min;
            for (int i = start; i <= Max; i++)
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
        internal static byte SemiTransparent => 0x42; // ~26% opacity

        public static TextBlockForegroundConverter Instance { get; } = new();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 4
                && values[0] is SolidColorBrush brush
                && values[1] is Orientation orientation
                && values[2] is double value
                && values[3] is int buttonValue)
            {
                if (value >= buttonValue)
                    return brush;

                var originalColor = brush.Color;
                var semiTransparent = Color.FromArgb(SemiTransparent, brush.Color.R, brush.Color.G, brush.Color.B);

                if (value > buttonValue - 1.0)
                {
                    double offset = value - buttonValue + 1;
                    return new LinearGradientBrush
                    {
                        StartPoint = orientation == Orientation.Horizontal ? new Point(0, 0.5) : new Point(0.5, 0),
                        EndPoint = orientation == Orientation.Horizontal ? new Point(1, 0.5) : new Point(0.5, 1),
                        GradientStops = new()
                        {
                            new GradientStop {Color = originalColor, Offset = offset},
                            new GradientStop {Color = semiTransparent, Offset = offset}
                        }
                    };
                }
                return new SolidColorBrush(semiTransparent);
            }

            // This should never happen (returning actual brush to avoid the compiler squiggly line warnings)
            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
