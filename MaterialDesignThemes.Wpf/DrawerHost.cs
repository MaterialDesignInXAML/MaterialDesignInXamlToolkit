using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignThemes.Wpf
{
    [TemplateVisualState(GroupName = TemplateAllDrawersGroupName, Name = TemplateAllDrawersAllClosedStateName)]
    [TemplateVisualState(GroupName = TemplateAllDrawersGroupName, Name = TemplateAllDrawersAnyOpenStateName)]
    [TemplateVisualState(GroupName = TemplateLeftDrawerGroupName, Name = TemplateLeftClosedStateName)]
    [TemplateVisualState(GroupName = TemplateLeftDrawerGroupName, Name = TemplateLeftOpenStateName)]
    [TemplatePart(Name = TemplateContentCoverPartName, Type = typeof(FrameworkElement))]
    public class DrawerHost : ContentControl
    {
        public const string TemplateAllDrawersGroupName = "AllDrawers";
        public const string TemplateAllDrawersAllClosedStateName = "AllClosed";
        public const string TemplateAllDrawersAnyOpenStateName = "AnyOpen";
        public const string TemplateLeftDrawerGroupName = "LeftDrawer";
        public const string TemplateLeftClosedStateName = "LeftDrawerClosed";
        public const string TemplateLeftOpenStateName = "LeftDrawerOpen";

        public const string TemplateContentCoverPartName = "PART_ContentCover";

        public static RoutedCommand OpenDrawerCommand = new RoutedCommand();
        public static RoutedCommand CloseDrawerCommand = new RoutedCommand();

        private FrameworkElement _templateContentCoverElement;

        static DrawerHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DrawerHost), new FrameworkPropertyMetadata(typeof(DrawerHost)));
        }

        public DrawerHost()
        {
            CommandBindings.Add(new CommandBinding(OpenDrawerCommand, OpenDrawerHandler));
            CommandBindings.Add(new CommandBinding(CloseDrawerCommand, CloseDrawerHandler));
        }

        public static readonly DependencyProperty LeftDrawerContentProperty = DependencyProperty.Register(
            "LeftDrawerContent", typeof (object), typeof (DrawerHost), new PropertyMetadata(default(object)));

        public object LeftDrawerContent
        {
            get { return (object) GetValue(LeftDrawerContentProperty); }
            set { SetValue(LeftDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentTemplateProperty = DependencyProperty.Register(
            "LeftDrawerContentTemplate", typeof (DataTemplate), typeof (DrawerHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate LeftDrawerContentTemplate
        {
            get { return (DataTemplate) GetValue(LeftDrawerContentTemplateProperty); }
            set { SetValue(LeftDrawerContentTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty LeftDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            "LeftDrawerContentTemplateSelector", typeof (DataTemplateSelector), typeof (DrawerHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector LeftDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector) GetValue(LeftDrawerContentTemplateSelectorProperty); }
            set { SetValue(LeftDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentStringFormatProperty = DependencyProperty.Register(
            "LeftDrawerContentStringFormat", typeof (string), typeof (DrawerHost), new PropertyMetadata(default(string)));

        public string LeftDrawerContentStringFormat
        {
            get { return (string) GetValue(LeftDrawerContentStringFormatProperty); }
            set { SetValue(LeftDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerBackgroundProperty = DependencyProperty.Register(
            "LeftDrawerBackground", typeof (Brush), typeof (DrawerHost), new PropertyMetadata(default(Brush)));

        public Brush LeftDrawerBackground
        {
            get { return (Brush) GetValue(LeftDrawerBackgroundProperty); }
            set { SetValue(LeftDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsLeftDrawerOpenProperty = DependencyProperty.Register(
            "IsLeftDrawerOpen", typeof (bool), typeof (DrawerHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsLeftDrawerOpenPropertyChangedCallback));        

        public bool IsLeftDrawerOpen
        {
            get { return (bool) GetValue(IsLeftDrawerOpenProperty); }
            set { SetValue(IsLeftDrawerOpenProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            if (_templateContentCoverElement != null)
                _templateContentCoverElement.PreviewMouseLeftButtonUp += TemplateContentCoverElementOnPreviewMouseLeftButtonUp;

            base.OnApplyTemplate();

            _templateContentCoverElement = GetTemplateChild(TemplateContentCoverPartName) as FrameworkElement;
            if (_templateContentCoverElement != null)
                _templateContentCoverElement.PreviewMouseLeftButtonUp += TemplateContentCoverElementOnPreviewMouseLeftButtonUp;


            UpdateVisualStates(false);
        }

        private void TemplateContentCoverElementOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            SetCurrentValue(IsLeftDrawerOpenProperty, false);
        }

        private void UpdateVisualStates(bool useTransitions)
        {
            var anyOpen = IsLeftDrawerOpen;
            
            VisualStateManager.GoToState(this,
                !anyOpen ? TemplateAllDrawersAllClosedStateName : TemplateAllDrawersAnyOpenStateName, !TransitionAssist.GetDisableTransitions(this));

            VisualStateManager.GoToState(this,
                IsLeftDrawerOpen ? TemplateLeftOpenStateName : TemplateLeftClosedStateName, !TransitionAssist.GetDisableTransitions(this));
        }

        private static void IsLeftDrawerOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((DrawerHost)dependencyObject).UpdateVisualStates(true);            
        }

        private void CloseDrawerHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            SetCurrentValue(IsLeftDrawerOpenProperty, false);

            executedRoutedEventArgs.Handled = true;
        }

        private void OpenDrawerHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            SetCurrentValue(IsLeftDrawerOpenProperty, true);

            executedRoutedEventArgs.Handled = true;
        }
    }
}