using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf
{
    [ContentProperty("ItemsSource")]
    public class MultipleSelectionDropDown : ItemsControl
    {
        /*public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MultipleSelectionDropDown));

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }

            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }*/

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(MultipleSelectionDropDown));

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }

            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public static DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IEnumerable), typeof(MultipleSelectionDropDown));

        public IEnumerable SelectedItems
        {
            get
            {
                return (IEnumerable)GetValue(SelectedItemsProperty);
            }

            set
            {
                SetValue(SelectedItemsProperty, value);

                if (value != null && _popupList != null)
                {
                    _popupList.SelectedItems.Clear();
                }
            }
        }

        private Button _dropDownButton;
        private Popup _popup;
        private ListBox _popupList;

        static MultipleSelectionDropDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultipleSelectionDropDown), new FrameworkPropertyMetadata(typeof(MultipleSelectionDropDown)));
        }

        public MultipleSelectionDropDown()
        {
            _dropDownButton = null;
            _popup = null;
            _popupList = null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            InitControls();
        }

        private void InitControls()
        {
            _dropDownButton = GetTemplateChild("PART_DropDownButton") as Button;
            _popup = GetTemplateChild("PART_Popup") as Popup;
            _popupList = GetTemplateChild("PART_PopupList") as ListBox;

            _dropDownButton.Click -= DropDownButtonClickHandler;
            _dropDownButton.Click += DropDownButtonClickHandler;

            _popupList.SelectionChanged -= PopupListSelectionChangedHandler;
            _popupList.SelectionChanged += PopupListSelectionChangedHandler;

            _popup.Opened -= PopupOpenedHandler;
            _popup.Opened += PopupOpenedHandler;

            _popup.Closed -= PopupClosedHandler;
            _popup.Closed += PopupClosedHandler;
        }

        private void DropDownButtonClickHandler(object sender, RoutedEventArgs args)
        {
            IsOpen = !IsOpen;
        }

        private void PopupOpenedHandler(object sender, EventArgs args)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OutsideCapturedElementHandler);
        }

        private void PopupClosedHandler(object sender, EventArgs args)
        {
            ReleaseMouseCapture();
        }

        private void OutsideCapturedElementHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            IsOpen = false;
            Mouse.RemovePreviewMouseDownOutsideCapturedElementHandler(this, OutsideCapturedElementHandler);
        }

        private void PopupListSelectionChangedHandler(object sender, SelectionChangedEventArgs args)
        {
            ReleaseMouseCapture();
            Mouse.Capture(this, CaptureMode.SubTree);
        }
    }
}
