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
    [TemplateVisualState(GroupName = TemplateTopDrawerGroupName, Name = TemplateTopClosedStateName)]
    [TemplateVisualState(GroupName = TemplateTopDrawerGroupName, Name = TemplateTopOpenStateName)]
    [TemplateVisualState(GroupName = TemplateRightDrawerGroupName, Name = TemplateRightClosedStateName)]
    [TemplateVisualState(GroupName = TemplateRightDrawerGroupName, Name = TemplateRightOpenStateName)]
    [TemplateVisualState(GroupName = TemplateBottomDrawerGroupName, Name = TemplateBottomClosedStateName)]
    [TemplateVisualState(GroupName = TemplateBottomDrawerGroupName, Name = TemplateBottomOpenStateName)]
    [TemplatePart(Name = TemplateContentCoverPartName, Type = typeof(FrameworkElement))]
    public class DrawerHost : ContentControl
    {
        public const string TemplateAllDrawersGroupName = "AllDrawers";
        public const string TemplateAllDrawersAllClosedStateName = "AllClosed";
        public const string TemplateAllDrawersAnyOpenStateName = "AnyOpen";
        public const string TemplateLeftDrawerGroupName = "LeftDrawer";
        public const string TemplateLeftClosedStateName = "LeftDrawerClosed";
        public const string TemplateLeftOpenStateName = "LeftDrawerOpen";
        public const string TemplateTopDrawerGroupName = "TopDrawer";
        public const string TemplateTopClosedStateName = "TopDrawerClosed";
        public const string TemplateTopOpenStateName = "TopDrawerOpen";
        public const string TemplateRightDrawerGroupName = "RightDrawer";
        public const string TemplateRightClosedStateName = "RightDrawerClosed";
        public const string TemplateRightOpenStateName = "RightDrawerOpen";
        public const string TemplateBottomDrawerGroupName = "BottomDrawer";
        public const string TemplateBottomClosedStateName = "BottomDrawerClosed";
        public const string TemplateBottomOpenStateName = "BottomDrawerOpen";

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

        public static readonly DependencyProperty TopDrawerContentProperty = DependencyProperty.Register(
            nameof(TopDrawerContent), typeof(object), typeof(DrawerHost), new PropertyMetadata(default(object)));

        public object TopDrawerContent
        {
            get { return (object)GetValue(TopDrawerContentProperty); }
            set { SetValue(TopDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(TopDrawerContentTemplate), typeof(DataTemplate), typeof(DrawerHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate TopDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(TopDrawerContentTemplateProperty); }
            set { SetValue(TopDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(TopDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(DrawerHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector TopDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(TopDrawerContentTemplateSelectorProperty); }
            set { SetValue(TopDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(TopDrawerContentStringFormat), typeof(string), typeof(DrawerHost), new PropertyMetadata(default(string)));

        public string TopDrawerContentStringFormat
        {
            get { return (string)GetValue(TopDrawerContentStringFormatProperty); }
            set { SetValue(TopDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(TopDrawerBackground), typeof(Brush), typeof(DrawerHost), new PropertyMetadata(default(Brush)));

        public Brush TopDrawerBackground
        {
            get { return (Brush)GetValue(TopDrawerBackgroundProperty); }
            set { SetValue(TopDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsTopDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsTopDrawerOpen), typeof(bool), typeof(DrawerHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));

        public bool IsTopDrawerOpen
        {
            get { return (bool)GetValue(IsTopDrawerOpenProperty); }
            set { SetValue(IsTopDrawerOpenProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentProperty = DependencyProperty.Register(
            nameof(LeftDrawerContent), typeof (object), typeof (DrawerHost), new PropertyMetadata(default(object)));

        public object LeftDrawerContent
        {
            get { return (object) GetValue(LeftDrawerContentProperty); }
            set { SetValue(LeftDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(LeftDrawerContentTemplate), typeof (DataTemplate), typeof (DrawerHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate LeftDrawerContentTemplate
        {
            get { return (DataTemplate) GetValue(LeftDrawerContentTemplateProperty); }
            set { SetValue(LeftDrawerContentTemplateProperty, value); }
        }
        
        public static readonly DependencyProperty LeftDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(LeftDrawerContentTemplateSelector), typeof (DataTemplateSelector), typeof (DrawerHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector LeftDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector) GetValue(LeftDrawerContentTemplateSelectorProperty); }
            set { SetValue(LeftDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(LeftDrawerContentStringFormat), typeof (string), typeof (DrawerHost), new PropertyMetadata(default(string)));

        public string LeftDrawerContentStringFormat
        {
            get { return (string) GetValue(LeftDrawerContentStringFormatProperty); }
            set { SetValue(LeftDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(LeftDrawerBackground), typeof (Brush), typeof (DrawerHost), new PropertyMetadata(default(Brush)));

        public Brush LeftDrawerBackground
        {
            get { return (Brush) GetValue(LeftDrawerBackgroundProperty); }
            set { SetValue(LeftDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsLeftDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsLeftDrawerOpen), typeof (bool), typeof (DrawerHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));

        public bool IsLeftDrawerOpen
        {
            get { return (bool) GetValue(IsLeftDrawerOpenProperty); }
            set { SetValue(IsLeftDrawerOpenProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentProperty = DependencyProperty.Register(
            nameof(RightDrawerContent), typeof(object), typeof(DrawerHost), new PropertyMetadata(default(object)));

        public object RightDrawerContent
        {
            get { return (object)GetValue(RightDrawerContentProperty); }
            set { SetValue(RightDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(RightDrawerContentTemplate), typeof(DataTemplate), typeof(DrawerHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate RightDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(RightDrawerContentTemplateProperty); }
            set { SetValue(RightDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(RightDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(DrawerHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector RightDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(RightDrawerContentTemplateSelectorProperty); }
            set { SetValue(RightDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(RightDrawerContentStringFormat), typeof(string), typeof(DrawerHost), new PropertyMetadata(default(string)));

        public string RightDrawerContentStringFormat
        {
            get { return (string)GetValue(RightDrawerContentStringFormatProperty); }
            set { SetValue(RightDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(RightDrawerBackground), typeof(Brush), typeof(DrawerHost), new PropertyMetadata(default(Brush)));

        public Brush RightDrawerBackground
        {
            get { return (Brush)GetValue(RightDrawerBackgroundProperty); }
            set { SetValue(RightDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsRightDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsRightDrawerOpen), typeof(bool), typeof(DrawerHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));

        public bool IsRightDrawerOpen
        {
            get { return (bool)GetValue(IsRightDrawerOpenProperty); }
            set { SetValue(IsRightDrawerOpenProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentProperty = DependencyProperty.Register(
            nameof(BottomDrawerContent), typeof(object), typeof(DrawerHost), new PropertyMetadata(default(object)));

        public object BottomDrawerContent
        {
            get { return (object)GetValue(BottomDrawerContentProperty); }
            set { SetValue(BottomDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(BottomDrawerContentTemplate), typeof(DataTemplate), typeof(DrawerHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate BottomDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(BottomDrawerContentTemplateProperty); }
            set { SetValue(BottomDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(BottomDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(DrawerHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector BottomDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(BottomDrawerContentTemplateSelectorProperty); }
            set { SetValue(BottomDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(BottomDrawerContentStringFormat), typeof(string), typeof(DrawerHost), new PropertyMetadata(default(string)));

        public string BottomDrawerContentStringFormat
        {
            get { return (string)GetValue(BottomDrawerContentStringFormatProperty); }
            set { SetValue(BottomDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(BottomDrawerBackground), typeof(Brush), typeof(DrawerHost), new PropertyMetadata(default(Brush)));

        public Brush BottomDrawerBackground
        {
            get { return (Brush)GetValue(BottomDrawerBackgroundProperty); }
            set { SetValue(BottomDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsBottomDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsBottomDrawerOpen), typeof(bool), typeof(DrawerHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));

        public bool IsBottomDrawerOpen
        {
            get { return (bool)GetValue(IsBottomDrawerOpenProperty); }
            set { SetValue(IsBottomDrawerOpenProperty, value); }
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
            SetCurrentValue(IsRightDrawerOpenProperty, false);
            SetCurrentValue(IsTopDrawerOpenProperty, false);
            SetCurrentValue(IsBottomDrawerOpenProperty, false);
        }

        private void UpdateVisualStates(bool? useTransitions = null)
        {
            var anyOpen = IsTopDrawerOpen || IsLeftDrawerOpen || IsBottomDrawerOpen || IsRightDrawerOpen;
            
            VisualStateManager.GoToState(this,
                !anyOpen ? TemplateAllDrawersAllClosedStateName : TemplateAllDrawersAnyOpenStateName, useTransitions.HasValue ? useTransitions.Value : !TransitionAssist.GetDisableTransitions(this));

            VisualStateManager.GoToState(this,
                IsLeftDrawerOpen ? TemplateLeftOpenStateName : TemplateLeftClosedStateName, useTransitions.HasValue ? useTransitions.Value : !TransitionAssist.GetDisableTransitions(this));

            VisualStateManager.GoToState(this,
                IsTopDrawerOpen ? TemplateTopOpenStateName : TemplateTopClosedStateName, useTransitions.HasValue ? useTransitions.Value : !TransitionAssist.GetDisableTransitions(this));

            VisualStateManager.GoToState(this,
                IsRightDrawerOpen ? TemplateRightOpenStateName : TemplateRightClosedStateName, useTransitions.HasValue ? useTransitions.Value : !TransitionAssist.GetDisableTransitions(this));

            VisualStateManager.GoToState(this,
                IsBottomDrawerOpen ? TemplateBottomOpenStateName : TemplateBottomClosedStateName, useTransitions.HasValue ? useTransitions.Value : !TransitionAssist.GetDisableTransitions(this));
        }

        private static void IsDrawerOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((DrawerHost)dependencyObject).UpdateVisualStates();            
        }

        private void CloseDrawerHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            SetOpenFlag(executedRoutedEventArgs, false);

            executedRoutedEventArgs.Handled = true;
        }

        private void OpenDrawerHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            SetOpenFlag(executedRoutedEventArgs, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void SetOpenFlag(ExecutedRoutedEventArgs executedRoutedEventArgs, bool value)
        {
            if (executedRoutedEventArgs.Parameter is Dock)
            {
                switch ((Dock)executedRoutedEventArgs.Parameter)
                {
                    case Dock.Left:
                        SetCurrentValue(IsLeftDrawerOpenProperty, value);
                        break;
                    case Dock.Top:
                        SetCurrentValue(IsTopDrawerOpenProperty, value);
                        break;
                    case Dock.Right:
                        SetCurrentValue(IsRightDrawerOpenProperty, value);
                        break;
                    case Dock.Bottom:
                        SetCurrentValue(IsBottomDrawerOpenProperty, value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                SetCurrentValue(IsLeftDrawerOpenProperty, value);
                SetCurrentValue(IsTopDrawerOpenProperty, value);
                SetCurrentValue(IsRightDrawerOpenProperty, value);
                SetCurrentValue(IsBottomDrawerOpenProperty, value);
            }
        }
    }
}