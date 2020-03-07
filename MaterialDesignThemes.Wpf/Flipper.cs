using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = Plane3DPartName, Type = typeof(Plane3D))]
    [TemplateVisualState(GroupName = TemplateFlipGroupName, Name = TemplateFlippedStateName)]
    [TemplateVisualState(GroupName = TemplateFlipGroupName, Name = TemplateUnflippedStateName)]
    public class Flipper : Control
    {
        public static RoutedCommand FlipCommand = new RoutedCommand();

        public const string Plane3DPartName = "PART_Plane3D";
        public const string TemplateFlipGroupName = "FlipStates";
        public const string TemplateFlippedStateName = "Flipped";
        public const string TemplateUnflippedStateName = "Unflipped";

        private Plane3D _plane3D;

        static Flipper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Flipper), new FrameworkPropertyMetadata(typeof(Flipper)));
        }

        public Flipper()
        {
            CommandBindings.Add(new CommandBinding(FlipCommand, FlipHandler));
        }

        public static readonly DependencyProperty FrontContentProperty = DependencyProperty.Register(
            nameof(FrontContent), typeof(object), typeof(Flipper), new PropertyMetadata(default(object)));

        public object FrontContent
        {
            get => GetValue(FrontContentProperty);
            set => SetValue(FrontContentProperty, value);
        }

        public static readonly DependencyProperty FrontContentTemplateProperty = DependencyProperty.Register(
            nameof(FrontContentTemplate), typeof(DataTemplate), typeof(Flipper), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate FrontContentTemplate
        {
            get => (DataTemplate)GetValue(FrontContentTemplateProperty);
            set => SetValue(FrontContentTemplateProperty, value);
        }

        public static readonly DependencyProperty FrontContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(FrontContentTemplateSelector), typeof(DataTemplateSelector), typeof(Flipper), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector FrontContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(FrontContentTemplateSelectorProperty);
            set => SetValue(FrontContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty FrontContentStringFormatProperty = DependencyProperty.Register(
            nameof(FrontContentStringFormat), typeof(string), typeof(Flipper), new PropertyMetadata(default(string)));

        public string FrontContentStringFormat
        {
            get => (string)GetValue(FrontContentStringFormatProperty);
            set => SetValue(FrontContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty BackContentProperty = DependencyProperty.Register(
            nameof(BackContent), typeof(object), typeof(Flipper), new PropertyMetadata(default(object)));

        public object BackContent
        {
            get => (object)GetValue(BackContentProperty);
            set => SetValue(BackContentProperty, value);
        }

        public static readonly DependencyProperty BackContentTemplateProperty = DependencyProperty.Register(
            nameof(BackContentTemplate), typeof(DataTemplate), typeof(Flipper), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate BackContentTemplate
        {
            get => (DataTemplate)GetValue(BackContentTemplateProperty);
            set => SetValue(BackContentTemplateProperty, value);
        }

        public static readonly DependencyProperty BackContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(BackContentTemplateSelector), typeof(DataTemplateSelector), typeof(Flipper), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector BackContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(BackContentTemplateSelectorProperty);
            set => SetValue(BackContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty BackContentStringFormatProperty = DependencyProperty.Register(
            nameof(BackContentStringFormat), typeof(string), typeof(Flipper), new PropertyMetadata(default(string)));

        public string BackContentStringFormat
        {
            get => (string)GetValue(BackContentStringFormatProperty);
            set => SetValue(BackContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty IsFlippedProperty = DependencyProperty.Register(
            nameof(IsFlipped), typeof(bool), typeof(Flipper), new PropertyMetadata(default(bool), IsFlippedPropertyChangedCallback));

        private static void IsFlippedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var flipper = (Flipper)dependencyObject;
            flipper.UpdateVisualStates(true);
            flipper.RemeasureDuringFlip();
            OnIsFlippedChanged(flipper, dependencyPropertyChangedEventArgs);
        }

        public bool IsFlipped
        {
            get => (bool)GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }

        public static readonly RoutedEvent IsFlippedChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(IsFlipped),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>),
                typeof(Flipper));

        public event RoutedPropertyChangedEventHandler<bool> IsFlippedChanged
        {
            add => AddHandler(IsFlippedChangedEvent, value);
            remove => RemoveHandler(IsFlippedChangedEvent, value);
        }

        private static void OnIsFlippedChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Flipper)d;
            var args = new RoutedPropertyChangedEventArgs<bool>(
                    (bool)e.OldValue,
                    (bool)e.NewValue) { RoutedEvent = IsFlippedChangedEvent };
            instance.RaiseEvent(args);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateVisualStates(false);

            _plane3D = GetTemplateChild(Plane3DPartName) as Plane3D;
        }

        private void RemeasureDuringFlip()
        {
            //not entirely happy hardcoding this, but I have explored other options I am not happy with, and this will do for now
            const int storyboardMs = 400;
            const int granularity = 6;

            var remeasureInterval = new TimeSpan(0, 0, 0, 0, storyboardMs / granularity);
            var refreshCount = 0;
            var plane3D = _plane3D;
            if (plane3D == null) return;

            DispatcherTimer dt = null;
            dt = new DispatcherTimer(remeasureInterval, DispatcherPriority.Normal,
                (sender, args) =>
                {
                    plane3D.InvalidateMeasure();
                    if (refreshCount++ == granularity)
                        dt.Stop();
                }, Dispatcher);
            dt.Start();
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