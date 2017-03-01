using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    [TemplateVisualState(GroupName = TemplateFlipGroupName, Name = TemplateFlippedStateName)]
    [TemplateVisualState(GroupName = TemplateFlipGroupName, Name = TemplateUnflippedStateName)]
    public class Flipper : Control
    {
        public static RoutedCommand FlipCommand = new RoutedCommand();

        public const string TemplateFlipGroupName = "FlipStates";
        public const string TemplateFlippedStateName = "Flipped";
        public const string TemplateUnflippedStateName = "Unflipped";

        static Flipper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Flipper), new FrameworkPropertyMetadata(typeof(Flipper)));
        }

        public Flipper()
        {
            CommandBindings.Add(new CommandBinding(FlipCommand, FlipHandler));            
        }

        public static readonly DependencyProperty FrontContentProperty = DependencyProperty.Register(
            "FrontContent", typeof(object), typeof(Flipper), new PropertyMetadata(default(object)));

        public object FrontContent
        {
            get { return (object) GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }

        public static readonly DependencyProperty FrontContentTemplateProperty = DependencyProperty.Register(
            "FrontContentTemplate", typeof(DataTemplate), typeof(Flipper), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate FrontContentTemplate
        {
            get { return (DataTemplate) GetValue(FrontContentTemplateProperty); }
            set { SetValue(FrontContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty FrontContentTemplateSelectorProperty = DependencyProperty.Register(
            "FrontContentTemplateSelector", typeof(DataTemplateSelector), typeof(Flipper), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector FrontContentTemplateSelector
        {
            get { return (DataTemplateSelector) GetValue(FrontContentTemplateSelectorProperty); }
            set { SetValue(FrontContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty FrontContentStringFormatProperty = DependencyProperty.Register(
            "FrontContentStringFormat", typeof(string), typeof(Flipper), new PropertyMetadata(default(string)));

        public string FrontContentStringFormat
        {
            get { return (string) GetValue(FrontContentStringFormatProperty); }
            set { SetValue(FrontContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty BackContentProperty = DependencyProperty.Register(
            "BackContent", typeof(object), typeof(Flipper), new PropertyMetadata(default(object)));

        public object BackContent
        {
            get { return (object) GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }

        public static readonly DependencyProperty BackContentTemplateProperty = DependencyProperty.Register(
            "BackContentTemplate", typeof(DataTemplate), typeof(Flipper), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate BackContentTemplate
        {
            get { return (DataTemplate)GetValue(BackContentTemplateProperty); }
            set { SetValue(BackContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BackContentTemplateSelectorProperty = DependencyProperty.Register(
            "BackContentTemplateSelector", typeof(DataTemplateSelector), typeof(Flipper), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector BackContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(BackContentTemplateSelectorProperty); }
            set { SetValue(BackContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty BackContentStringFormatProperty = DependencyProperty.Register(
            "BackContentStringFormat", typeof(string), typeof(Flipper), new PropertyMetadata(default(string)));

        public string BackContentStringFormat
        {
            get { return (string)GetValue(BackContentStringFormatProperty); }
            set { SetValue(BackContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty IsFlippedProperty = DependencyProperty.Register(
            "IsFlipped", typeof(bool), typeof(Flipper), new PropertyMetadata(default(bool), FlippedPropertyChangedCallback));

        private static void FlippedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((Flipper)dependencyObject).UpdateVisualStates(true);
        }

        public bool IsFlipped
        {
            get { return (bool) GetValue(IsFlippedProperty); }
            set { SetValue(IsFlippedProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateVisualStates(false);
        }

        private void UpdateVisualStates(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsFlipped ? TemplateFlippedStateName : TemplateUnflippedStateName,
                useTransitions);
        }

        private void FlipHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            SetCurrentValue(IsFlippedProperty, !IsFlipped);            
        }
    }
}