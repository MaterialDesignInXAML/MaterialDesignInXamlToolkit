using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public static class ComboBoxAssist
    {
        private static readonly HashSet<Key> KeysToDelegate = new HashSet<Key>
        {
            Key.Up,
            Key.Down,
            Key.F4,
            Key.Escape,
            Key.Enter,
            Key.Home,
            Key.End,
            Key.PageUp,
            Key.PageDown,
            Key.Oem5
        };

        /// <summary>
        /// Intended for internal use only.
        /// </summary>
        public static readonly DependencyProperty ManagedOverlayProperty = DependencyProperty.RegisterAttached(
            "ManagedOverlay", typeof (bool), typeof (ComboBoxAssist), new PropertyMetadata(default(bool), ManagedOverlayPropertyChangedCallback));

        private static void ManagedOverlayPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var comboBox = dependencyObject as ComboBox;
            if (comboBox == null) return;

            if ((bool) dependencyPropertyChangedEventArgs.NewValue)
            {                
                comboBox.DropDownOpened += ComboBoxOnDropDownOpened;
                comboBox.DropDownClosed += ComboBoxOnDropDownClosed;
            }
            else
            {             
                comboBox.DropDownOpened -= ComboBoxOnDropDownOpened;
                comboBox.DropDownClosed -= ComboBoxOnDropDownClosed;
            }
        }

        /// <summary>
        /// Intended for internal use only.
        /// </summary>
        public static void SetManagedOverlay(DependencyObject element, bool value)
        {
            element.SetValue(ManagedOverlayProperty, value);
        }

        /// <summary>
        /// Intended for internal use only.
        /// </summary>
        public static bool GetManagedOverlay(DependencyObject element)
        {
            return (bool) element.GetValue(ManagedOverlayProperty);
        }

        public static readonly DependencyProperty ManagedOverlayInfoProperty = DependencyProperty.RegisterAttached(
            "ManagedOverlayInfo", typeof (ComboBoxAssistManagedOverlayInfo), typeof (ComboBoxAssist), new PropertyMetadata(default(ComboBoxAssistManagedOverlayInfo)));

        /// <summary>
        /// Intended for internal use only.
        /// </summary>
        public static void SetManagedOverlayInfo(DependencyObject element, object value)
        {
            element.SetValue(ManagedOverlayInfoProperty, value);
        }

        /// <summary>
        /// Intended for internal use only.
        /// </summary>
        public static object GetManagedOverlayInfo(DependencyObject element)
        {
            return (ComboBoxAssistManagedOverlayInfo) element.GetValue(ManagedOverlayInfoProperty);
        }

        private static void ComboBoxOnDropDownOpened(object sender, EventArgs eventArgs)
        {
            var comboBox = (ComboBox)sender;
            var cloneTextBox = comboBox.Template.FindName("EditableTextBoxClone", comboBox) as TextBox;
            var originalTextBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;

            if (cloneTextBox != null && originalTextBox != null)
            {
                var managedOverlayInfo = new ComboBoxAssistManagedOverlayInfo(comboBox, originalTextBox, cloneTextBox);

                cloneTextBox.PreviewKeyDown += CloneTextBoxOnPreviewKeyDown;
                cloneTextBox.SelectionLength = originalTextBox.SelectionLength;
                cloneTextBox.SelectionStart = originalTextBox.SelectionStart;
                originalTextBox.SelectionChanged += OriginalTextBoxOnSelectionChanged;
                SetManagedOverlayInfo(comboBox, managedOverlayInfo);
                SetManagedOverlayInfo(originalTextBox, managedOverlayInfo);
                SetManagedOverlayInfo(cloneTextBox, managedOverlayInfo);
            }

            cloneTextBox?.Dispatcher.BeginInvoke(new Action(() =>
            {
                cloneTextBox.Focus();
            }));
        }

        private static void OriginalTextBoxOnSelectionChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            var managedOverlayInfo = GetManagedOverlayInfo((DependencyObject)sender) as ComboBoxAssistManagedOverlayInfo;
            if (managedOverlayInfo == null) return;

            managedOverlayInfo.CloneTextBox.SelectionLength = managedOverlayInfo.OriginalTextBox.SelectionLength;
            managedOverlayInfo.CloneTextBox.SelectionStart = managedOverlayInfo.OriginalTextBox.SelectionStart;
        }

        private static void CloneTextBoxOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (!KeysToDelegate.Contains(keyEventArgs.Key)) return;

            var textBox = (TextBox) sender;
            var managedOverlayInfo = GetManagedOverlayInfo(textBox) as ComboBoxAssistManagedOverlayInfo;
            if (managedOverlayInfo == null) return;

            managedOverlayInfo.OriginalTextBox.RaiseEvent(new KeyEventArgs(keyEventArgs.KeyboardDevice, keyEventArgs.InputSource, keyEventArgs.Timestamp, keyEventArgs.Key)
            {
                RoutedEvent = keyEventArgs.RoutedEvent
            });            
            
            keyEventArgs.Handled = true;
        }

        private static void ComboBoxOnDropDownClosed(object sender, EventArgs eventArgs)
        {
            var comboBox = (ComboBox)sender;

            var managedOverlayInfo = GetManagedOverlayInfo(comboBox) as ComboBoxAssistManagedOverlayInfo;
            if (managedOverlayInfo == null) return;

            SetManagedOverlayInfo(managedOverlayInfo.Owner, null);
            SetManagedOverlayInfo(managedOverlayInfo.CloneTextBox, null);
            SetManagedOverlayInfo(managedOverlayInfo.OriginalTextBox, null);

            managedOverlayInfo.CloneTextBox.PreviewKeyDown -= CloneTextBoxOnPreviewKeyDown;
            managedOverlayInfo.OriginalTextBox.SelectionChanged -= OriginalTextBoxOnSelectionChanged;
            managedOverlayInfo.OriginalTextBox.SelectionLength = managedOverlayInfo.CloneTextBox.SelectionLength;
            managedOverlayInfo.OriginalTextBox.SelectionStart = managedOverlayInfo.CloneTextBox.SelectionStart;
        }
    }
}
