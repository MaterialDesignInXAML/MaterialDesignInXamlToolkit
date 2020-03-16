using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = DeleteButtonPartName, Type = typeof(Button))]
    public class Chip : ButtonBase
    {
        private ButtonBase _deleteButton;

        public const string DeleteButtonPartName = "PART_DeleteButton";

        static Chip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chip), new FrameworkPropertyMetadata(typeof(Chip)));
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(object), typeof(Chip), new PropertyMetadata(default(object)));

        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty = DependencyProperty.Register(
            "IconBackground", typeof(Brush), typeof(Chip), new PropertyMetadata(default(Brush)));

        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.Register(
            "IconForeground", typeof(Brush), typeof(Chip), new PropertyMetadata(default(Brush)));

        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }

        public static readonly DependencyProperty IsDeletableProperty = DependencyProperty.Register(
            "IsDeletable", typeof(bool), typeof(Chip), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates if the delete button should be visible.
        /// </summary>
        public bool IsDeletable
        {
            get { return (bool)GetValue(IsDeletableProperty); }
            set { SetValue(IsDeletableProperty, value); }
        }

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register(
            "DeleteCommand", typeof(ICommand), typeof(Chip), new PropertyMetadata(default(ICommand)));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static readonly DependencyProperty DeleteCommandParameterProperty = DependencyProperty.Register(
            "DeleteCommandParameter", typeof(object), typeof(Chip), new PropertyMetadata(default(object)));

        public object DeleteCommandParameter
        {
            get { return (object)GetValue(DeleteCommandParameterProperty); }
            set { SetValue(DeleteCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty DeleteToolTipProperty = DependencyProperty.Register(
            "DeleteToolTip", typeof(object), typeof(Chip), new PropertyMetadata(default(object)));

        public object DeleteToolTip
        {
            get { return (object)GetValue(DeleteToolTipProperty); }
            set { SetValue(DeleteToolTipProperty, value); }
        }

        /// <summary>
        /// Event correspond to delete button left mouse button click 
        /// </summary>
        public static readonly RoutedEvent DeleteClickEvent = EventManager.RegisterRoutedEvent("DeleteClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Chip));

        /// <summary>
        /// Add / Remove DeleteClickEvent handler 
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler DeleteClick { add { AddHandler(DeleteClickEvent, value); } remove { RemoveHandler(DeleteClickEvent, value); } }

        public override void OnApplyTemplate()
        {
            if (_deleteButton != null)
                _deleteButton.Click -= DeleteButtonOnClick;

            _deleteButton = GetTemplateChild(DeleteButtonPartName) as ButtonBase;
            if (_deleteButton != null)
                _deleteButton.Click += DeleteButtonOnClick;

            base.OnApplyTemplate();
        }

        protected virtual void OnDeleteClick()
        {
            var newEvent = new RoutedEventArgs(DeleteClickEvent, this);
            RaiseEvent(newEvent);

            var command = DeleteCommand;
            if (command != null && command.CanExecute(DeleteCommandParameter))
                command.Execute(DeleteCommandParameter);
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            OnDeleteClick();
            routedEventArgs.Handled = true;
        }
    }
}
