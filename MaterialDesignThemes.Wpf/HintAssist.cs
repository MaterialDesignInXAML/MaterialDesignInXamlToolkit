using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    internal static class HintAssist
    {
        public const string StateTextEmptyName = "MaterialDesignStateTextEmpty";
        public const string StateTextNotEmptyName = "MaterialDesignStateTextNotEmpty";

        public static readonly DependencyProperty ManagedProperty = DependencyProperty.RegisterAttached(
            "Managed", typeof(Control), typeof(HintAssist), new PropertyMetadata(default(Control), ManagedPropertyChangedCallback));

        public static void SetManaged(DependencyObject element, Control value)
        {
            element.SetValue(ManagedProperty, value);
        }
        
        public static TextBox GetManaged(DependencyObject element)
        {
            return (TextBox)element.GetValue(ManagedProperty);
        }

        private static void ManagedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyPropertyChangedEventArgs.OldValue as Control;
            
            if (control != null)
            {
                control.IsVisibleChanged -= ManagedControlOnIsVisibleChanged;
            }

            control = dependencyPropertyChangedEventArgs.NewValue as Control;
            if (control != null)
            {
                control.IsVisibleChanged += ManagedControlOnIsVisibleChanged;
            }
        }

        private static void ManagedControlOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!RefreshState(sender as TextBox, textBox => textBox.Text))
                RefreshState(sender as ComboBox, comboBox => comboBox.Text);
        }

        private static bool RefreshState<TControl>(TControl control, Func<TControl, string> textAccessor) where TControl : Control
        {
            if (control == null) return false;
            if (!control.IsVisible) return true;
            
            control.Dispatcher.BeginInvoke(new Action(() =>
            {
                var state = string.IsNullOrEmpty(textAccessor(control))
                    ? StateTextEmptyName
                    : StateTextNotEmptyName;

                VisualStateManager.GoToState(control, state, false);
            }));

            return true;
        }

        private interface IHintProxy
        {
            string Text { get; }

            event EventHandler TextChanged;
        }

        private static class HintProxyFabric
        {
            public static IHintProxy Get(Control control)
            {
                return null;
            }

            //private sealed class TextBoxHintProxy : IHintProxy
            //{
            //    private readonly TextBox _textBox;
            //
            //    public string Text { get; }
            //
            //    public TextBoxHintProxy(TextBox textBox)
            //    {
            //        if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            //
            //        _textBox = textBox;
            //        WeakEventManager<>
            //    }
            //
            //    public event EventHandler TextChanged;
            //}
        }
    }
}
