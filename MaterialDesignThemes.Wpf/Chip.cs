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
        public const string DeleteButtonPartName = "PART_DeleteButton";

        private ButtonBase? _deleteButton;

        static Chip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chip), new FrameworkPropertyMetadata(typeof(Chip)));
        }

        #region DependencyProperty : IconProperty
        public object? Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register(nameof(Icon), typeof(object), typeof(Chip), new PropertyMetadata(default(object?)));
        #endregion
        
        #region DependencyProperty : IconBackgroundProperty
        public Brush? IconBackground
        {
            get => (Brush?)GetValue(IconBackgroundProperty);
            set => SetValue(IconBackgroundProperty, value);
        }
        public static readonly DependencyProperty IconBackgroundProperty
            = DependencyProperty.Register(nameof(IconBackground), typeof(Brush), typeof(Chip), new PropertyMetadata(default(Brush?)));
        #endregion

        #region DependencyProperty : IconForegroundProperty
        public Brush? IconForeground
        {
            get => (Brush?)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }
        public static readonly DependencyProperty IconForegroundProperty
            = DependencyProperty.Register(nameof(IconForeground), typeof(Brush), typeof(Chip), new PropertyMetadata(default(Brush?)));
        #endregion

        #region DependencyProperty : IsDeletableProperty
        /// <summary>
        /// Indicates if the delete button should be visible.
        /// </summary>
        public bool IsDeletable
        {
            get => (bool)GetValue(IsDeletableProperty);
            set => SetValue(IsDeletableProperty, value);
        }
        public static readonly DependencyProperty IsDeletableProperty
            = DependencyProperty.Register(nameof(IsDeletable), typeof(bool), typeof(Chip), new PropertyMetadata(default(bool)));
        #endregion
        
        #region DependencyProperty : DeleteCommandProperty
        public ICommand? DeleteCommand
        {
            get => (ICommand?)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }
        public static readonly DependencyProperty DeleteCommandProperty
            = DependencyProperty.Register(nameof(DeleteCommand), typeof(ICommand), typeof(Chip), new PropertyMetadata(default(ICommand?)));
        #endregion

        #region DependencyProperty : DeleteCommandParameterProperty
        public object? DeleteCommandParameter
        {
            get => GetValue(DeleteCommandParameterProperty);
            set => SetValue(DeleteCommandParameterProperty, value);
        }
        public static readonly DependencyProperty DeleteCommandParameterProperty
            = DependencyProperty.Register(nameof(DeleteCommandParameter), typeof(object), typeof(Chip), new PropertyMetadata(default(object?)));
        #endregion
        
        #region DependencyProperty : DeleteToolTipProperty
        public object? DeleteToolTip
        {
            get => GetValue(DeleteToolTipProperty);
            set => SetValue(DeleteToolTipProperty, value);
        }
        public static readonly DependencyProperty DeleteToolTipProperty
            = DependencyProperty.Register(nameof(DeleteToolTip), typeof(object), typeof(Chip), new PropertyMetadata(default(object?)));
        #endregion

        #region Event : DeleteClickEvent
        [Category("Behavior")]
        
        public event RoutedEventHandler DeleteClick
        {
            add => AddHandler(DeleteClickEvent, value);
            remove => RemoveHandler(DeleteClickEvent, value);
        }
        
        public static readonly RoutedEvent DeleteClickEvent
            = EventManager.RegisterRoutedEvent(nameof(DeleteClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Chip));
        #endregion

        public override void OnApplyTemplate()
        {
            if (_deleteButton != null)
            {
                _deleteButton.Click -= DeleteButtonOnClick;
            }
            
            _deleteButton = GetTemplateChild(DeleteButtonPartName) as ButtonBase;
            
            if (_deleteButton != null)
            {
                _deleteButton.Click += DeleteButtonOnClick;
            }
            
            base.OnApplyTemplate();
        }

        protected virtual void OnDeleteClick()
        {
            RaiseEvent(new RoutedEventArgs(DeleteClickEvent, this));

            if (DeleteCommand?.CanExecute(DeleteCommandParameter) ?? false)
            {
                DeleteCommand.Execute(DeleteCommandParameter);
            }
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            OnDeleteClick();
            routedEventArgs.Handled = true;
        }
    }
}