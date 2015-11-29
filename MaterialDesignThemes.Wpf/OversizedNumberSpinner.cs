using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// A spinner control for integers.
    /// </summary>
    public class OversizedNumberSpinner : Control
    {
        public static RoutedCommand EditValueCommand = new RoutedCommand();
        public static RoutedCommand MinusCommand = new RoutedCommand();
        public static RoutedCommand PlusCommand = new RoutedCommand();

        public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(
            "IsEditing", typeof(bool), typeof(OversizedNumberSpinner), new PropertyMetadata(false));

        private bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof(int), typeof(OversizedNumberSpinner), new PropertyMetadata(0));

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof(int), typeof(OversizedNumberSpinner), new PropertyMetadata(5));

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(int), typeof(OversizedNumberSpinner), new PropertyMetadata(1, ValuePropertyChangedCallback));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private TextBox ValueTextBox
        {
            get
            {
                return Template.FindName("ValueTextBox", this) as TextBox;
            }
        }

        static OversizedNumberSpinner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OversizedNumberSpinner), new FrameworkPropertyMetadata(typeof(OversizedNumberSpinner)));
        }

        public OversizedNumberSpinner()
            : base()
        {
            CommandBindings.Add(new CommandBinding(EditValueCommand, EditValueCommandHandler));
            CommandBindings.Add(new CommandBinding(MinusCommand, MinusCommandHandler));
            CommandBindings.Add(new CommandBinding(PlusCommand, PlusCommandHandler));
        }

        private void EditValueCommandHandler(object sender, ExecutedRoutedEventArgs args)
        {
            IsEditing = true;
            ValueTextBox.Focus();
        }

        private void MinusCommandHandler(object sender, ExecutedRoutedEventArgs args)
        {
            Value = Value - 1;
        }

        private void PlusCommandHandler(object sender, ExecutedRoutedEventArgs args)
        {
            Value = Value + 1;
        }

        private static void ValuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            OversizedNumberSpinner spinner = (OversizedNumberSpinner)dependencyObject;

            if (spinner.Value < spinner.Min)
            {
                spinner.Value = spinner.Min;
            }

            if (spinner.Value > spinner.Max)
            {
                spinner.Value = spinner.Max;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ValueTextBox.LostFocus += (sender, args) => IsEditing = false;
            ValueTextBox.KeyUp += (sender, args) => { if (args.Key == Key.Enter) Focus(); };
        }
    }
}
