using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            if (executedRoutedEventArgs.Parameter is int)
                Value = (int) executedRoutedEventArgs.Parameter;
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof (int), typeof (RatingBar), new PropertyMetadata(1, MinPropertyChangedCallback));        

        public int Min
        {
            get { return (int) GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof (int), typeof (RatingBar), new PropertyMetadata(5, MaxPropertyChangedCallback));

        public int Max
        {
            get { return (int) GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof (int), typeof (RatingBar), new PropertyMetadata(0, ValuePropertyChangedCallback));

        private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            foreach (var button in ((RatingBar) dependencyObject).RatingButtons)
                button.IsWithinSelectedValue = button.Value <= (int)dependencyPropertyChangedEventArgs.NewValue;
        }

        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public ReadOnlyObservableCollection<RatingBarButton> RatingButtons => _ratingButtons;

        public static readonly DependencyProperty ValueItemContainerButtonStyleProperty = DependencyProperty.Register(
            "ValueItemContainerButtonStyle", typeof (Style), typeof (RatingBar), new PropertyMetadata(default(Style)));

        public Style ValueItemContainerButtonStyle
        {
            get { return (Style) GetValue(ValueItemContainerButtonStyleProperty); }
            set { SetValue(ValueItemContainerButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ValueItemTemplateProperty = DependencyProperty.Register(
            "ValueItemTemplate", typeof (DataTemplate), typeof (RatingBar), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate ValueItemTemplate
        {
            get { return (DataTemplate) GetValue(ValueItemTemplateProperty); }
            set { SetValue(ValueItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ValueItemTemplateSelectorProperty = DependencyProperty.Register(
            "ValueItemTemplateSelector", typeof (DataTemplateSelector), typeof (RatingBar), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector ValueItemTemplateSelector
        {
            get { return (DataTemplateSelector) GetValue(ValueItemTemplateSelectorProperty); }
            set { SetValue(ValueItemTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof (Orientation), typeof (RatingBar), new PropertyMetadata(default(Orientation)));

        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
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
}
