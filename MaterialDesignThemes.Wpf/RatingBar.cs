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
            MouseLeave += RatingBar_MouseLeave;
        }

        private void SelectItemHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Parameter is int && !IsReadOnly)
            {
                if (!IsFractionalValueEnabled)
                {
                    Value = (int)executedRoutedEventArgs.Parameter;
                    return;
                }
                Value = GetValueAtMousePosition((RatingBarButton)executedRoutedEventArgs.OriginalSource);
            }
        }

        private double GetValueAtMousePosition(RatingBarButton ratingBarButton)
        {
            // Get mouse offset inside source
            Point p = Mouse.GetPosition(ratingBarButton);
            double percentSelected = Orientation == Orientation.Horizontal ? p.X / ratingBarButton.ActualWidth : p.Y / ratingBarButton.ActualHeight;
            return ratingBarButton.Value - 1 + percentSelected;
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Min), typeof(int), typeof(RatingBar), new PropertyMetadata(1, MinPropertyChangedCallback, MinPropertyCoerceValueCallback));

        public int Min
        {
            get => (int)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Max), typeof(int), typeof(RatingBar), new PropertyMetadata(5, MaxPropertyChangedCallback, MaxPropertyCoerceValueCallback));

        public int Max
        {
            get => (int)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        private static readonly DependencyPropertyKey IsFractionalValueEnabledPropertyKey = DependencyProperty.RegisterReadOnly(
                nameof(IsFractionalValueEnabled), typeof(bool), typeof(RatingBar), new PropertyMetadata(false));

        internal static readonly DependencyProperty IsFractionalValueEnabledProperty =
            IsFractionalValueEnabledPropertyKey.DependencyProperty;

        internal bool IsFractionalValueEnabled
        {
            get => (bool)GetValue(IsFractionalValueEnabledProperty);
            private set => SetValue(IsFractionalValueEnabledPropertyKey, value);
        }

        public static readonly DependencyProperty ValueIncrementsProperty = DependencyProperty.Register(
            nameof(ValueIncrements), typeof(double), typeof(RatingBar), new PropertyMetadata(1.0, ValueIncrementsPropertyChangedCallback, ValueIncrementsCoerceValueCallback));

        private static void ValueIncrementsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ratingBar = (RatingBar)d;
            ratingBar.IsFractionalValueEnabled = Math.Abs(ratingBar.ValueIncrements - 1.0) > 1e-10;
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

        public static readonly DependencyProperty IsPreviewValueEnabledProperty = DependencyProperty.Register(
            nameof(IsPreviewValueEnabled), typeof(bool), typeof(RatingBar), new PropertyMetadata(false));

        public bool IsPreviewValueEnabled
        {
            get { return (bool) GetValue(IsPreviewValueEnabledProperty); }
            set { SetValue(IsPreviewValueEnabledProperty, value); }
        }

        private static readonly DependencyPropertyKey PreviewValuePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(PreviewValue), typeof(double?), typeof(RatingBar), new PropertyMetadata(null, null, PreviewValuePropertyCoerceValueCallback));

        private static object? PreviewValuePropertyCoerceValueCallback(DependencyObject d, object? baseValue)
        {
            if (baseValue == null)
                return null;

            var ratingBar = (RatingBar)d;
            if (baseValue is double value)
            {
                if (!ratingBar.IsFractionalValueEnabled)
                    value = Math.Ceiling(value);
                return ratingBar.CoerceToValidIncrement(value);
            }
            return (double)ratingBar.Min;
        }

        internal static readonly DependencyProperty PreviewValueProperty =
            PreviewValuePropertyKey.DependencyProperty;

        internal double? PreviewValue
        {
            get => (double?)GetValue(PreviewValueProperty);
            private set => SetValue(PreviewValuePropertyKey, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(RatingBar), new PropertyMetadata(0.0, ValuePropertyChangedCallback, ValuePropertyCoerceValueCallback));

        private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ratingBar = (RatingBar)dependencyObject;
            foreach (var button in ratingBar.RatingButtons)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                // The property being set here is no longer used. If the RatingBarButton (and the DP) was not public I would have just removed it.
                button.IsWithinSelectedValue = button.Value <= (double)dependencyPropertyChangedEventArgs.NewValue;
#pragma warning restore CS0618 // Type or member is obsolete
            }
            OnValueChanged(ratingBar, dependencyPropertyChangedEventArgs);
        }

        private static object ValuePropertyCoerceValueCallback(DependencyObject d, object baseValue)
        {
            var ratingBar = (RatingBar) d;

            // If factional values are disabled we don't do any coercion. This maintains back-compat where coercion was not applied and Value could be outside of Min/Max range. 
            if (!ratingBar.IsFractionalValueEnabled)
                return baseValue;

            if (baseValue is double value)
            {
                return ratingBar.CoerceToValidIncrement(value);
            }
            return (double)ratingBar.Min;
        }

        private double CoerceToValidIncrement(double value)
        {
            // Coerce the value into a multiple of ValueIncrements and within the bounds.
            double valueInCorrectMultiple = Math.Round(value / ValueIncrements, MidpointRounding.AwayFromZero) * ValueIncrements;
            return Math.Min(Max, Math.Max(Min, valueInCorrectMultiple));
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
            var ratingBar = (RatingBar)dependencyObject;
            ratingBar.CoerceValue(ValueProperty);
            ratingBar.RebuildButtons();
        }

        private static object MinPropertyCoerceValueCallback(DependencyObject d, object baseValue)
        {
            var ratingBar = (RatingBar)d;
            return Math.Min((int)baseValue, ratingBar.Max);
        }

        private static void MinPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ratingBar = (RatingBar)dependencyObject;
            ratingBar.CoerceValue(ValueProperty);
            ratingBar.RebuildButtons();
        }

        private static object MaxPropertyCoerceValueCallback(DependencyObject d, object baseValue)
        {
            var ratingBar = (RatingBar)d;
            return Math.Max((int)baseValue, ratingBar.Min);
        }

        private void RebuildButtons()
        {
            foreach (var ratingBarButton in _ratingButtonsInternal)
            {
                ratingBarButton.MouseMove -= RatingBarButton_MouseMove;
            }
            _ratingButtonsInternal.Clear();

            // When fractional values are enabled, the first rating button represents the value Min when not selected at all and Min+1 when fully selected;
            // thus we start with the value Min+1 for the values of the rating buttons.
            int start = IsFractionalValueEnabled ? Min + 1 : Min;
            for (int i = start; i <= Max; i++)
            {
                var ratingBarButton = new RatingBarButton
                {
                    Content = i,
                    ContentTemplate = ValueItemTemplate,
                    ContentTemplateSelector = ValueItemTemplateSelector,
#pragma warning disable CS0618 // Type or member is obsolete
                    IsWithinSelectedValue = i <= Value,
#pragma warning restore CS0618 // Type or member is obsolete
                    Style = ValueItemContainerButtonStyle,
                    Value = i,
                };
                ratingBarButton.MouseMove += RatingBarButton_MouseMove;
                _ratingButtonsInternal.Add(ratingBarButton);
            }
        }

        private void RatingBar_MouseLeave(object sender, MouseEventArgs e) => PreviewValue = null;

        private void RatingBarButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPreviewValueEnabled)
                return;

            var ratingBarButton = (RatingBarButton) sender;
            PreviewValue = GetValueAtMousePosition(ratingBarButton);
        }

        public override void OnApplyTemplate()
        {
            RebuildButtons();

            base.OnApplyTemplate();
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

                // This should never happen (returning actual brush to avoid the compilers squiggly line warning)
                return Brushes.Transparent;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
        }

        internal class PreviewIndicatorTransformXConverter : IMultiValueConverter
        {
            public static PreviewIndicatorTransformXConverter Instance { get; } = new();

            internal static double Margin => 2.0;

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values.Length >= 6
                    && values[0] is double ratingBarButtonActualWidth
                    && values[1] is double previewValueActualWidth
                    && values[2] is Orientation ratingBarOrientation
                    && values[3] is bool isFractionalValueEnabled
                    && values[4] is double previewValue
                    && values[5] is int ratingButtonValue)
                {
                    if (!isFractionalValueEnabled)
                    {
                        return ratingBarOrientation switch
                        {
                            Orientation.Horizontal => (ratingBarButtonActualWidth - previewValueActualWidth) / 2,
                            Orientation.Vertical => -previewValueActualWidth - Margin,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }

                    // Special handling of edge cases due to the inaccuracy of how double values are stored
                    double percent = previewValue % 1;
                    if (Math.Abs(ratingButtonValue - previewValue) <= double.Epsilon)
                        percent = 1.0;
                    else if (percent <= double.Epsilon)
                        percent = 0.0;

                    return ratingBarOrientation switch
                    {
                        Orientation.Horizontal => percent * ratingBarButtonActualWidth - (previewValueActualWidth / 2),
                        Orientation.Vertical => -previewValueActualWidth - Margin,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                return 1.0;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
        }

        internal class PreviewIndicatorTransformYConverter : IMultiValueConverter
        {
            public static PreviewIndicatorTransformYConverter Instance { get; } = new();

            internal static double Margin => 2.0;

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values.Length >= 6
                    && values[0] is double ratingBarButtonActualHeight
                    && values[1] is double previewValueActualHeight
                    && values[2] is Orientation ratingBarOrientation
                    && values[3] is bool isFractionalValueEnabled
                    && values[4] is double previewValue
                    && values[5] is int ratingButtonValue)
                {
                    if (!isFractionalValueEnabled)
                    {
                        return ratingBarOrientation switch
                        {
                            Orientation.Horizontal => -previewValueActualHeight - Margin,
                            Orientation.Vertical => (ratingBarButtonActualHeight - previewValueActualHeight) / 2,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }

                    // Special handling of edge cases due to the inaccuracy of how double values are stored
                    double percent = previewValue % 1;
                    if (Math.Abs(ratingButtonValue - previewValue) <= double.Epsilon)
                        percent = 1.0;
                    else if (percent <= double.Epsilon)
                        percent = 0.0;

                    return ratingBarOrientation switch
                    {
                        Orientation.Horizontal => -previewValueActualHeight - Margin,
                        Orientation.Vertical => percent * ratingBarButtonActualHeight - (previewValueActualHeight / 2),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                return 1.0;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
        }
    }
}
