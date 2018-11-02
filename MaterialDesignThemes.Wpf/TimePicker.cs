using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = ElementButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementPopup, Type = typeof(Popup))]
    [TemplatePart(Name = ElementTextBox, Type = typeof(DatePickerTextBox))]
    public class TimePicker : Control
    {
        private const string ElementButton = "PART_Button";
        private const string ElementPopup = "PART_Popup";
        private const string ElementTextBox = "PART_TextBox";

        private readonly ContentControl _clockHostContentControl;
        private readonly Clock _clock;
        private TextBox _textBox;
        private Popup _popup;
        private Button _dropDownButton;
        private bool _disablePopupReopen;
        private DateTime? _lastValidTime;
        private bool _isManuallyMutatingText;

        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        public TimePicker()
        {
            _clock = new Clock
            {
                DisplayAutomation = ClockDisplayAutomation.ToMinutesOnly
            };
            _clockHostContentControl = new ContentControl
            {
                Content = _clock
            };
            InitializeClock();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof (string), typeof (TimePicker), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextPropertyChangedCallback));

        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var timePicker = (TimePicker) dependencyObject;
            if (!timePicker._isManuallyMutatingText)
                timePicker.SetSelectedTime();
            if (timePicker._textBox != null)
                timePicker._textBox.Text = dependencyPropertyChangedEventArgs.NewValue as string;
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register(
            nameof(SelectedTime), typeof (DateTime?), typeof (TimePicker), new FrameworkPropertyMetadata(default(DateTime?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectedTimePropertyChangedCallback));

        private static void SelectedTimePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var timePicker = (TimePicker) dependencyObject;
            timePicker._isManuallyMutatingText = true;
            timePicker.SetCurrentValue(TextProperty, timePicker.DateTimeToString(timePicker.SelectedTime));
            timePicker._isManuallyMutatingText = false;
            timePicker._lastValidTime = timePicker.SelectedTime;

            OnSelectedTimeChanged(timePicker, dependencyPropertyChangedEventArgs);
        }

        public DateTime? SelectedTime
		{
			get { return (DateTime?) GetValue(SelectedTimeProperty); }
			set { SetValue(SelectedTimeProperty, value); }
        }

        public static readonly RoutedEvent SelectedTimeChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(SelectedTime),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<DateTime?>),
                typeof(TimePicker));

        public event RoutedPropertyChangedEventHandler<DateTime?> SelectedTimeChanged
        {
            add => AddHandler(SelectedTimeChangedEvent, value);
            remove => RemoveHandler(SelectedTimeChangedEvent, value);
        }

        private static void OnSelectedTimeChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (TimePicker)d;
            var args = new RoutedPropertyChangedEventArgs<DateTime?>(
                    (DateTime?)e.OldValue,
                    (DateTime?)e.NewValue) { RoutedEvent = SelectedTimeChangedEvent };
            instance.RaiseEvent(args);
        }

        public static readonly DependencyProperty SelectedTimeFormatProperty = DependencyProperty.Register(
            nameof(SelectedTimeFormat), typeof (DatePickerFormat), typeof (TimePicker), new PropertyMetadata(DatePickerFormat.Short));

        public DatePickerFormat SelectedTimeFormat
        {
            get { return (DatePickerFormat) GetValue(SelectedTimeFormatProperty); }
            set { SetValue(SelectedTimeFormatProperty, value); }
        }

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            nameof(IsDropDownOpen), typeof (bool), typeof (TimePicker),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenChanged, OnCoerceIsDropDownOpen));

        public bool IsDropDownOpen
        {
            get { return (bool) GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public static readonly DependencyProperty Is24HoursProperty = DependencyProperty.Register(
            nameof(Is24Hours), typeof (bool), typeof (TimePicker), new PropertyMetadata(default(bool), Is24HoursChanged));

        private static void Is24HoursChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var timePicker = (TimePicker)dependencyObject;
            timePicker._isManuallyMutatingText = true;
            timePicker.SetCurrentValue(TextProperty, timePicker.DateTimeToString(timePicker.SelectedTime));
            timePicker._isManuallyMutatingText = false;
        }

        public bool Is24Hours
        {
            get { return (bool) GetValue(Is24HoursProperty); }
            set { SetValue(Is24HoursProperty, value); }
        }

        private static object OnCoerceIsDropDownOpen(DependencyObject d, object baseValue)
        {
            var timePicker = (TimePicker)d;
        
            if (!timePicker.IsEnabled)
            {
                return false;
            }

            return baseValue;
        }

        /// <summary> 
        /// IsDropDownOpenProperty property changed handler.
        /// </summary> 
        /// <param name="d">DatePicker that changed its IsDropDownOpen.</param> 
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timePicker = (TimePicker)d;

            var newValue = (bool) e.NewValue;
            if (timePicker._popup == null || timePicker._popup.IsOpen == newValue) return;

            timePicker._popup.IsOpen = newValue;
            if (newValue)
            {
                //TODO set time
                //dp._originalSelectedDate = dp.SelectedDate;

                timePicker.Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action) delegate()
                {
                    timePicker._clock.Focus();
                });
            }
        }

        public static readonly DependencyProperty ClockStyleProperty = DependencyProperty.Register(
            nameof(ClockStyle), typeof (Style), typeof (TimePicker), new PropertyMetadata(default(Style)));

        public Style ClockStyle
        {
            get { return (Style) GetValue(ClockStyleProperty); }
            set { SetValue(ClockStyleProperty, value); }
        }

        public static readonly DependencyProperty ClockHostContentControlStyleProperty = DependencyProperty.Register(
            nameof(ClockHostContentControlStyle), typeof (Style), typeof (TimePicker), new PropertyMetadata(default(Style)));

        public Style ClockHostContentControlStyle
        {
            get { return (Style) GetValue(ClockHostContentControlStyleProperty); }
            set { SetValue(ClockHostContentControlStyleProperty, value); }
        }

        public static readonly DependencyProperty IsInvalidTextAllowedProperty = DependencyProperty.Register(
            "IsInvalidTextAllowed", typeof (bool), typeof (TimePicker), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Set to true to stop invalid text reverting back to previous valid value. Useful in cases where you
        /// want to display validation messages and allow the user to correct the data without it reverting.
        /// </summary>
        public bool IsInvalidTextAllowed
        {
            get { return (bool) GetValue(IsInvalidTextAllowedProperty); }
            set { SetValue(IsInvalidTextAllowedProperty, value); }
        }

        public static readonly DependencyProperty WithSecondsProperty = DependencyProperty.Register(
            nameof(WithSeconds), typeof (bool), typeof (TimePicker),
            new PropertyMetadata(default(bool), WithSecondsPropertyChanged));

        /// <summary>
        /// Set to true to display seconds in the time and allow the user to select seconds.
        /// </summary>
        public bool WithSeconds
        {
            get { return (bool) GetValue(WithSecondsProperty); }
            set { SetValue(WithSecondsProperty, value); }
        }

        private static void WithSecondsPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TimePicker picker)
            {
                // update the clock's behavior as needed when the WithSeconds value changes
                picker._clock.DisplayAutomation = picker.WithSeconds ? ClockDisplayAutomation.ToSeconds : ClockDisplayAutomation.ToMinutesOnly;
            }
        }

        public override void OnApplyTemplate()
        {
            if (_popup != null)
            {
                _popup.RemoveHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(PopupOnPreviewMouseLeftButtonDown));
                _popup.Opened -= PopupOnOpened;
                _popup.Closed -= PopupOnClosed;
                _popup.Child = null;
            }
            if (_dropDownButton != null)
            {
                _dropDownButton.Click -= DropDownButtonOnClick;
            }
            if (_textBox != null)
            {
                _textBox.RemoveHandler(KeyDownEvent, new KeyEventHandler(TextBoxOnKeyDown));
                _textBox.RemoveHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(TextBoxOnTextChanged));
                _textBox.AddHandler(LostFocusEvent, new RoutedEventHandler(TextBoxOnLostFocus));
            }

            _textBox = GetTemplateChild(ElementTextBox) as TextBox;
            if (_textBox != null)
            {
                _textBox.AddHandler(KeyDownEvent, new KeyEventHandler(TextBoxOnKeyDown));
                _textBox.AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(TextBoxOnTextChanged));
                _textBox.AddHandler(LostFocusEvent, new RoutedEventHandler(TextBoxOnLostFocus));
                _textBox.Text = Text;
            }

            _popup = GetTemplateChild(ElementPopup) as Popup;
            if (_popup != null)
            {
                _popup.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(PopupOnPreviewMouseLeftButtonDown));
                _popup.Opened += PopupOnOpened;
                _popup.Closed += PopupOnClosed;
                _popup.Child = _clockHostContentControl; 
                if (IsDropDownOpen)
                {
                    _popup.IsOpen = true;
                }
            }

            _dropDownButton = GetTemplateChild(ElementButton) as Button;
            if (_dropDownButton != null)
            {
                _dropDownButton.Click += DropDownButtonOnClick;
            }

            base.OnApplyTemplate();
        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (string.IsNullOrEmpty(_textBox?.Text))
            {
                SetCurrentValue(SelectedTimeProperty, null);
                return;
            }

            if (IsTimeValid(_textBox.Text, out DateTime time))
                SetCurrentValue(SelectedTimeProperty, time);

            else // Invalid time, jump back to previous good time
                SetInvalidTime();
        }

        private void SetInvalidTime()
        {
            if (IsInvalidTextAllowed) return;

            if (_lastValidTime != null)
            {
                //SetCurrentValue(SelectedTimeProperty, _lastValidTime.Value);
                //_textBox.Text = _lastValidTime.Value.ToString(_lastValidTime.Value.Hour % 12 > 9 ? "hh:mm tt" : "h:mm tt");
                _textBox.Text = DateTimeToString(_lastValidTime.Value, DatePickerFormat.Short);
            }

            else
            {
                SetCurrentValue(SelectedTimeProperty, null);
                _textBox.Text = "";
            }

        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            keyEventArgs.Handled = ProcessKey(keyEventArgs) || keyEventArgs.Handled;
        }

        private bool ProcessKey(KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.System:
                    {
                        switch (keyEventArgs.SystemKey)
                        {
                            case Key.Down:
                                {
                                    if ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                                    {
                                        TogglePopup();
                                        return true;
                                    }

                                    break;
                                }
                        }

                        break;
                    }

                case Key.Enter:
                    {
                        SetSelectedTime();
                        return true;
                    }
            }

            return false;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_popup?.IsOpen == true || IsInvalidTextAllowed)
                SetCurrentValue(TextProperty, _textBox.Text);

            if (_popup?.IsOpen == false)
                SetSelectedTime(true);
        }

        private void SetSelectedTime(bool beCautious = false)
        {
            if (!string.IsNullOrEmpty(_textBox?.Text))
            {
                ParseTime(_textBox.Text, t =>
                {
                    if (!beCautious || DateTimeToString(t) == _textBox.Text)
                        SetCurrentValue(SelectedTimeProperty, t);
                });
            }
            else
            {
                SetCurrentValue(SelectedTimeProperty, null);
            }
        }

        private static void ParseTime(string s, Action<DateTime> successContinuation)
        {
            if (IsTimeValid(s, out DateTime time))
                successContinuation(time);
        }

        private static bool IsTimeValid(string s, out DateTime time)
        {
            return DateTime.TryParse(s,
                                     CultureInfo.CurrentCulture,
                                     DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                                     out time);
        }

        private string DateTimeToString(DateTime? d)
        {
            return d.HasValue ? DateTimeToString(d.Value) : null;
        }

        private string DateTimeToString(DateTime d)
        {
            return DateTimeToString(d, SelectedTimeFormat);
        }

        private string DateTimeToString(DateTime datetime, DatePickerFormat format)
        {
            string res = null;

            var dtfi = GetCultureInfo().GetDateFormat();

            switch (format)
            {
                case DatePickerFormat.Short:
                    if (WithSeconds)
                        if (Is24Hours)
                            res = datetime.ToString("H:mm:ss");
                        else
                            res = datetime.ToString("h:mm:ss tt");
                    else if (Is24Hours)
                        res = datetime.ToString("H:mm");
                    else
                        res = string.Format(CultureInfo.CurrentCulture, datetime.ToString(dtfi.ShortTimePattern, dtfi));
                    break;
                case DatePickerFormat.Long:
                    if (Is24Hours)
                        if (WithSeconds)
                            res = datetime.ToString("HH:mm:ss");
                        else
                            res = datetime.ToString("HH:mm");
                    else
                        res = string.Format(CultureInfo.CurrentCulture, datetime.ToString(dtfi.LongTimePattern, dtfi));
                    break;
            }

            return res;
        }

        private CultureInfo GetCultureInfo()
        {
            CultureInfo ci;
            ci = Language.GetSpecificCulture();
            if (ci != null)
                return ci;
            return Language.GetEquivalentCulture();
        }

        private void PopupOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var popup = sender as Popup;
            if (popup == null || popup.StaysOpen) return;

            if (_dropDownButton?.InputHitTest(mouseButtonEventArgs.GetPosition(_dropDownButton)) != null)
            {
                // This popup is being closed by a mouse press on the drop down button 
                // The following mouse release will cause the closed popup to immediately reopen. 
                // Raise a flag to block reopeneing the popup
                _disablePopupReopen = true;
            }
        }

        private void PopupOnClosed(object sender, EventArgs eventArgs)
        {
            if (IsDropDownOpen)
            {
                SetCurrentValue(IsDropDownOpenProperty, false);
            }

            if (_clock.IsKeyboardFocusWithin)
            {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }

            //TODO Clock closed event
            //OnCalendarClosed(new RoutedEventArgs());
        }

        private void PopupOnOpened(object sender, EventArgs eventArgs)
        {
            if (!IsDropDownOpen)
            {
                SetCurrentValue(IsDropDownOpenProperty, true);
            }

            if (_clock != null)
            {
                _clock.DisplayMode = ClockDisplayMode.Hours;
                _clock.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }

            //TODO ClockOpenedEvent
            //this.OnCalendarOpened(new RoutedEventArgs());
        }

        private void InitializeClock()
        {
            _clock.AddHandler(Clock.ClockChoiceMadeEvent, new ClockChoiceMadeEventHandler(ClockChoiceMadeHandler));
            _clock.SetBinding(ForegroundProperty, GetBinding(ForegroundProperty));
            _clock.SetBinding(StyleProperty, GetBinding(ClockStyleProperty));
            _clock.SetBinding(Clock.TimeProperty, GetBinding(SelectedTimeProperty, new NullableDateTimeToCurrentDateConverter()));
            _clock.SetBinding(Clock.Is24HoursProperty, GetBinding(Is24HoursProperty));
            _clockHostContentControl.SetBinding(StyleProperty, GetBinding(ClockHostContentControlStyleProperty));
        }

        private void ClockChoiceMadeHandler(object sender, ClockChoiceMadeEventArgs clockChoiceMadeEventArgs)
        {
            if (
                ( WithSeconds && (clockChoiceMadeEventArgs.Mode == ClockDisplayMode.Seconds)) ||
                (!WithSeconds && (clockChoiceMadeEventArgs.Mode == ClockDisplayMode.Minutes))
            )
            {
                TogglePopup();
                if (SelectedTime == null)
                {
                    SelectedTime = _clock.Time;
                }
            }
        }

        private void DropDownButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            TogglePopup();
        }

        private void TogglePopup()
        {
            if (IsDropDownOpen)
                SetCurrentValue(IsDropDownOpenProperty, false);
            else
            {
                if (_disablePopupReopen)
                    _disablePopupReopen = false;
                else
                {
                    SetSelectedTime();
                    SetCurrentValue(IsDropDownOpenProperty, true);
                }
            }
        }

        private BindingBase GetBinding(DependencyProperty property, IValueConverter converter = null)
        {
            var binding = new Binding(property.Name)
            {
                Source = this,
                Converter = converter
            };
            return binding;
        }
    }
}