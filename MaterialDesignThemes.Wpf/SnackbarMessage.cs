using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Defines the content of a message within a <see cref="Snackbar"/>.  Primary content should be set via the 
    /// standard <see cref="SnackbarMessage.Content"/> property.  Where an action is allowed, content
    /// can be provided in <see cref="SnackbarMessage.ActionContent"/>.  Standard button properties are 
    /// provided for actions, includiing <see cref="SnackbarMessage.ActionCommand"/>.
    /// </summary>
    [TypeConverter(typeof(SnackbarMessageTypeConverter))]
    [TemplatePart(Name = ActionButtonPartName, Type = typeof(ButtonBase))]
    public class SnackbarMessage : ContentControl
    {
        internal static readonly ResourceDictionary defaultResources = new ResourceDictionary
        {
            { "SecondaryHueMidBrush", Brushes.Transparent },
            { "MaterialDesignSnackbarRipple", Brushes.Transparent },
        };

        public const string ActionButtonPartName = "PART_ActionButton";
        private Action _templateCleanupAction = () => { };

        static SnackbarMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SnackbarMessage), new FrameworkPropertyMetadata(typeof(SnackbarMessage)));
        }

        public static readonly DependencyProperty ActionCommandProperty = DependencyProperty.Register(
            "ActionCommand", typeof(ICommand), typeof(SnackbarMessage), new PropertyMetadata(default(ICommand)));

        public ICommand ActionCommand
        {
            get { return (ICommand)GetValue(ActionCommandProperty); }
            set { SetValue(ActionCommandProperty, value); }
        }

        public static readonly DependencyProperty ActionCommandParameterProperty = DependencyProperty.Register(
            "ActionCommandParameter", typeof(object), typeof(SnackbarMessage), new PropertyMetadata(default(object)));

        public object ActionCommandParameter
        {
            get { return (object)GetValue(ActionCommandParameterProperty); }
            set { SetValue(ActionCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty ActionContentProperty = DependencyProperty.Register(
            "ActionContent", typeof(object), typeof(SnackbarMessage), new PropertyMetadata(default(object)));

        public object ActionContent
        {
            get { return (object)GetValue(ActionContentProperty); }
            set { SetValue(ActionContentProperty, value); }
        }

        public static readonly DependencyProperty ActionContentTemplateProperty = DependencyProperty.Register(
            "ActionContentTemplate", typeof(DataTemplate), typeof(SnackbarMessage), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate ActionContentTemplate
        {
            get { return (DataTemplate)GetValue(ActionContentTemplateProperty); }
            set { SetValue(ActionContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ActionContentStringFormatProperty = DependencyProperty.Register(
            "ActionContentStringFormat", typeof(string), typeof(SnackbarMessage), new PropertyMetadata(default(string)));

        public string ActionContentStringFormat
        {
            get { return (string)GetValue(ActionContentStringFormatProperty); }
            set { SetValue(ActionContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty ActionContentTemplateSelectorProperty = DependencyProperty.Register(
            "ActionContentTemplateSelector", typeof(DataTemplateSelector), typeof(SnackbarMessage), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector ActionContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ActionContentTemplateSelectorProperty); }
            set { SetValue(ActionContentTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Event correspond to left mouse button click on the Action button.
        /// </summary>
        public static readonly RoutedEvent ActionClickEvent = EventManager.RegisterRoutedEvent("ActionClick",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SnackbarMessage));

        /// <summary>
        /// Add / Remove ActionClickEvent handler 
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler ActionClick { add { AddHandler(ActionClickEvent, value); } remove { RemoveHandler(ActionClickEvent, value); } }

        protected virtual void OnActionClick()
        {
            var newEvent = new RoutedEventArgs(ActionClickEvent, this);
            RaiseEvent(newEvent);
        }

        public override void OnApplyTemplate()
        {
            _templateCleanupAction();

            var buttonBase = GetTemplateChild(ActionButtonPartName) as ButtonBase;
            if (buttonBase != null)
            {
                buttonBase.Click += ButtonBaseOnClick;

                _templateCleanupAction = () => buttonBase.Click -= ButtonBaseOnClick;
            }
            else
                _templateCleanupAction = () => { };

            base.OnApplyTemplate();
        }

        private void ButtonBaseOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            OnActionClick();
        }

        /// <summary>
        /// Maximum total height of snackbar for the action button to be inlined.
        /// <para>
        /// Default value (<c>55</c>) is between single line message (<c>48</c>) and two lined snackbar-message (<c>66</c>)
        /// because tolerance is required (see <a href="https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/issues/1812">issue</a>)
        /// </para>
        /// </summary>
        public static readonly DependencyProperty InlineActionButtonMaxHeightProperty = DependencyProperty.RegisterAttached(
            "InlineActionButtonMaxHeight", typeof(double), typeof(SnackbarMessage), new PropertyMetadata(55d));

        public static void SetInlineActionButtonMaxHeight(DependencyObject element, double value) => element.SetValue(InlineActionButtonMaxHeightProperty, value);
        public static double GetInlineActionButtonMaxHeight(DependencyObject element) => (double) element.GetValue(InlineActionButtonMaxHeightProperty);

        public static readonly DependencyProperty ContentMaxHeightProperty = DependencyProperty.RegisterAttached(
            "ContentMaxHeight", typeof(double), typeof(SnackbarMessage), new PropertyMetadata(36d));

        public static void SetContentMaxHeight(DependencyObject element, double value) => element.SetValue(ContentMaxHeightProperty, value);
        public static double GetContentMaxHeight(DependencyObject element) => (double) element.GetValue(ContentMaxHeightProperty);
    }
}