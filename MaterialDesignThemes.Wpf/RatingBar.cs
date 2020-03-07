using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A custom control implementing a rating bar.
    /// The icon aka content may be set as a DataTemplate via the ButtonContentTemplate property.
    /// </summary>
    public class RatingBar : Control
    {
        public static RoutedCommand SelectRatingCommand = new RoutedCommand();

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
                Value = (int)executedRoutedEventArgs.Parameter;
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Min), typeof(int), typeof(RatingBar), new PropertyMetadata(1, MinPropertyChangedCallback));

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Max), typeof(int), typeof(RatingBar), new PropertyMetadata(5, MaxPropertyChangedCallback));

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(int), typeof(RatingBar), new PropertyMetadata(0, ValuePropertyChangedCallback));

        private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ratingBar = (RatingBar)dependencyObject;
            foreach (var button in ratingBar.RatingButtons)
                button.IsWithinSelectedValue = button.Value <= (int)dependencyPropertyChangedEventArgs.NewValue;
            OnValueChanged(ratingBar, dependencyPropertyChangedEventArgs);
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Value),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<int>),
                typeof(RatingBar));

        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }

        private static void OnValueChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (RatingBar)d;
            var args = new RoutedPropertyChangedEventArgs<int>(
                    (int)e.OldValue,
                    (int)e.NewValue) { RoutedEvent = ValueChangedEvent };
            instance.RaiseEvent(args);
        }

        public ReadOnlyObservableCollection<RatingBarButton> RatingButtons => _ratingButtons;

        public static readonly DependencyProperty ValueItemContainerButtonStyleProperty = DependencyProperty.Register(
            nameof(ValueItemContainerButtonStyle), typeof(Style), typeof(RatingBar), new PropertyMetadata(default(Style)));

        public Style ValueItemContainerButtonStyle
        {
            get { return (Style)GetValue(ValueItemContainerButtonStyleProperty); }
            set { SetValue(ValueItemContainerButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ValueItemTemplateProperty = DependencyProperty.Register(
            nameof(ValueItemTemplate), typeof(DataTemplate), typeof(RatingBar), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate ValueItemTemplate
        {
            get { return (DataTemplate)GetValue(ValueItemTemplateProperty); }
            set { SetValue(ValueItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ValueItemTemplateSelectorProperty = DependencyProperty.Register(
            nameof(ValueItemTemplateSelector), typeof(DataTemplateSelector), typeof(RatingBar), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector ValueItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ValueItemTemplateSelectorProperty); }
            set { SetValue(ValueItemTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation), typeof(Orientation), typeof(RatingBar), new PropertyMetadata(default(Orientation)));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            nameof(IsReadOnly), typeof(bool), typeof(RatingBar), new PropertyMetadata(default(bool)));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
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
                _ratingButtonsInternal.Add(new RatingBarButton {
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
}
