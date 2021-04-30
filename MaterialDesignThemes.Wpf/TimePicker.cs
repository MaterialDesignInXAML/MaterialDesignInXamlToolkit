using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = ButtonPartName, Type = typeof(Button))]
    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplatePart(Name = TextBoxPartName, Type = typeof(TimePickerTextBox))]
    public class TimePicker : Control
    {
        public const string ButtonPartName = "PART_Button";
        public const string PopupPartName = "PART_Popup";
        public const string TextBoxPartName = "PART_TextBox";

        private readonly ContentControl _clockHostContentControl;
        private readonly Clock _clock;
        private TextBox? _textBox;
        private Popup? _popup;
        private Button? _dropDownButton;
        private bool _disablePopupReopen;
        private DateTime? _lastValidTime;
        private bool _isManuallyMutatingText;

        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
            EventManager.RegisterClassHandler(typeof(TimePicker), UIElement.GotFocusEvent, new RoutedEventHandler(OnGotFocus));
        }

        /// <summary>
        ///     Called when this element gets focus.
        /// </summary>
        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            // When TimePicker gets focus move it to the TextBox
            TimePicker picker = (TimePicker)sender;
            if ((!e.Handled) && (picker._textBox != null))
            {
                if (e.OriginalSource == picker)
                {
                    picker._textBox.Focus();
                    e.Handled = true;
                }
                else if (e.OriginalSource == picker._textBox)
                {
                    picker._textBox.SelectAll();
                    e.Handled = true;
                }
            }
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
            nameof(Text), typeof(string), typeof(TimePicker), new FrameworkPropertyMetadata(default(string?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextPropertyChangedCallback));

        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var timePicker = (TimePicker)dependencyObject;
            if (!timePicker._isManuallyMutatingText)
            {
                timePicker.SetSelectedTime();
            }

            if (timePicker._textBox != null)
            {
                timePicker.UpdateTextBoxText(dependencyPropertyChangedEventArgs.NewValue as string ?? "");
            }
        }

        public string? Text
        {
            get => (string?)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty SelectedTimeProperty = DependencyProperty.Register(
            nameof(SelectedTime), typeof(DateTime?), typeof(TimePicker), new FrameworkPropertyMetadata(default(DateTime?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectedTimePropertyChangedCallback));

        private static void SelectedTimePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var timePicker = (TimePicker)dependencyObject;
            timePicker._isManuallyMutatingText = true;
            timePicker.SetCurrentValue(TextProperty, timePicker.DateTimeToString(timePicker.SelectedTime));
            timePicker._isManuallyMutatingText = false;
            timePicker._lastValidTime = timePicker.SelectedTime;

            OnSelectedTimeChanged(timePicker, dependencyPropertyChangedEventArgs);
        }

        public DateTime? SelectedTime
        {
            get => (DateTime?)GetValue(SelectedTimeProperty);
            set => SetValue(SelectedTimeProperty, value);
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
                    (DateTime?)e.NewValue)
            { RoutedEvent = SelectedTimeChangedEvent };
            instance.RaiseEvent(args);
        }

        public static readonly DependencyProperty SelectedTimeFormatProperty = DependencyProperty.Register(
            nameof(SelectedTimeFormat), typeof(DatePickerFormat), typeof(TimePicker), new PropertyMetadata(DatePickerFormat.Short));

        public DatePickerFormat SelectedTimeFormat
        {
            get => (DatePickerFormat)GetValue(SelectedTimeFormatProperty);
            set => SetValue(SelectedTimeFormatProperty, value);
        }

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            nameof(IsDropDownOpen), typeof(bool), typeof(TimePicker),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenChanged, OnCoerceIsDropDownOpen));

        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        public static readonly DependencyProperty Is24HoursProperty = DependencyProperty.Register(
            nameof(Is24Hours), typeof(bool), typeof(TimePicker), new PropertyMetadata(default(bool), Is24HoursChanged));

        private static void Is24HoursChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var timePicker = (TimePicker)dependencyObject;
            timePicker._isManuallyMutatingText = true;
            timePicker.SetCurrentValue(TextProperty, timePicker.DateTimeToString(timePicker.SelectedTime));
            timePicker._isManuallyMutatingText = false;
        }

        public bool Is24Hours
        {
            get => (bool)GetValue(Is24HoursProperty);
            set => SetValue(Is24HoursProperty, value);
        }

        private static object OnCoerceIsDropDownOpen(DependencyObject d, object baseValue)
        {
            var timePicker = (TimePicker)d;
            return timePicker.IsEnabled ? baseValue : false;
        }

        /// <summary> 
        /// IsDropDownOpenProperty property changed handler.
        /// </summary> 
        /// <param name="d">DatePicker that changed its IsDropDownOpen.</param> 
        /// <param name="e">DependencyPropertyChangedEventArgs.</param>
        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timePicker = (TimePicker)d;

            var newValue = (bool)e.NewValue;
            if (timePicker._popup == null || timePicker._popup.IsOpen == newValue) return;

            timePicker._popup.IsOpen = newValue;
            if (newValue)
            {
                //TODO set time
                //dp._originalSelectedDate = dp.SelectedDate;

                timePicker.Dispatcher?.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                {
                    timePicker._clock.Focus();
                }));
            }
        }

        public static readonly DependencyProperty ClockStyleProperty = DependencyProperty.Register(
            nameof(ClockStyle), typeof(Style), typeof(TimePicker), new PropertyMetadata(default(Style?)));

        public Style? ClockStyle
        {
            get => (Style?)GetValue(ClockStyleProperty);
            set => SetValue(ClockStyleProperty, value);
        }

        public static readonly DependencyProperty ClockHostContentControlStyleProperty = DependencyProperty.Register(
            nameof(ClockHostContentControlStyle), typeof(Style), typeof(TimePicker), new PropertyMetadata(default(Style?)));

        public Style? ClockHostContentControlStyle
        {
            get => (Style?)GetValue(ClockHostContentControlStyleProperty);
            set => SetValue(ClockHostContentControlStyleProperty, value);
        }

        public static readonly DependencyProperty IsInvalidTextAllowedProperty = DependencyProperty.Register(
            "IsInvalidTextAllowed", typeof(bool), typeof(TimePicker), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Set to true to stop invalid text reverting back to previous valid value. Useful in cases where you
        /// want to display validation messages and allow the user to correct the data without it reverting.
        /// </summary>
        public bool IsInvalidTextAllowed
        {
            get => (bool)GetValue(IsInvalidTextAllowedProperty);
            set => SetValue(IsInvalidTextAllowedProperty, value);
        }

        public static readonly DependencyProperty WithSecondsProperty = DependencyProperty.Register(
            nameof(WithSeconds), typeof(bool), typeof(TimePicker),
            new PropertyMetadata(default(bool), WithSecondsPropertyChanged));

        /// <summary>
        /// Set to true to display seconds in the time and allow the user to select seconds.
        /// </summary>
        public bool WithSeconds
        {
            get => (bool)GetValue(WithSecondsProperty);
            set => SetValue(WithSecondsProperty, value);
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

            _textBox = GetTemplateChild(TextBoxPartName) as TextBox;
            if (_textBox != null)
            {
                _textBox.AddHandler(KeyDownEvent, new KeyEventHandler(TextBoxOnKeyDown));
                _textBox.AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(TextBoxOnTextChanged));
                _textBox.AddHandler(LostFocusEvent, new RoutedEventHandler(TextBoxOnLostFocus));
                _textBox.Text = Text;
            }

            _popup = GetTemplateChild(PopupPartName) as Popup;
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

            _dropDownButton = GetTemplateChild(ButtonPartName) as Button;
            if (_dropDownButton != null)
            {
                _dropDownButton.Click += DropDownButtonOnClick;
            }

            base.OnApplyTemplate();
        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            string? text = _textBox?.Text;
            if (string.IsNullOrEmpty(text))
            {
                SetCurrentValue(SelectedTimeProperty, null);
                return;
            }

            if (IsTimeValid(text!, out DateTime time))
            {
                SetSelectedTime(time);
                UpdateTextBoxTextIfNeeded(text!);
            }
            else // Invalid time, jump back to previous good time
            {
                SetInvalidTime();
            }
        }

        private void SetInvalidTime()
        {
            if (IsInvalidTextAllowed) return;

            if (_textBox is { } textBox)
            {
                if (_lastValidTime != null)
                {
                    //SetCurrentValue(SelectedTimeProperty, _lastValidTime.Value);
                    //_textBox.Text = _lastValidTime.Value.ToString(_lastValidTime.Value.Hour % 12 > 9 ? "hh:mm tt" : "h:mm tt");
                    textBox.Text = DateTimeToString(_lastValidTime.Value, DatePickerFormat.Short);
                }

                else
                {
                    SetCurrentValue(SelectedTimeProperty, null);
                    textBox.Text = "";
                }
            }

        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs keyEventArgs)
            => keyEventArgs.Handled = ProcessKey(keyEventArgs) || keyEventArgs.Handled;

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
            if (_textBox is { } textBox &&
                (_popup?.IsOpen == true || IsInvalidTextAllowed))
            {
                _isManuallyMutatingText = true;
                SetCurrentValue(TextProperty, textBox.Text);
                _isManuallyMutatingText = false;
            }

            if (_popup?.IsOpen == false)
            {
                SetSelectedTime(true);
            }
        }

        private void UpdateTextBoxText(string? text)
        {
            // Save and restore the cursor position
            if (_textBox is { } textBox)
            {
                int caretIndex = textBox.CaretIndex;
                textBox.Text = text;
                textBox.CaretIndex = caretIndex;
            }
        }

        private void UpdateTextBoxTextIfNeeded(string lastText)
        {
            if (_textBox?.Text == lastText)
            {
                string? formattedText = DateTimeToString(SelectedTime);
                if (formattedText != lastText)
                {
                    UpdateTextBoxText(formattedText);
                }
            }
        }

        private void SetSelectedTime(in DateTime time)
            => SetCurrentValue(SelectedTimeProperty, (SelectedTime?.Date ?? DateTime.Today).Add(time.TimeOfDay));

        private void SetSelectedTime(bool beCautious = false)
        {
            string? currentText = _textBox?.Text;
            if (!string.IsNullOrEmpty(currentText))
            {
                ParseTime(currentText!, t =>
                {
                    if (!beCautious || DateTimeToString(t) == currentText)
                    {
                        SetSelectedTime(t);
                    }

                    if (!beCautious)
                    {
                        UpdateTextBoxTextIfNeeded(currentText!);
                    }
                });
            }
            else
            {
                SetCurrentValue(SelectedTimeProperty, null);
            }
        }

        private void ParseTime(string s, Action<DateTime> successContinuation)
        {
            if (IsTimeValid(s, out DateTime time))
            {
                successContinuation(time);
            }
        }

        private bool IsTimeValid(string s, out DateTime time)
        {
            CultureInfo culture = Language.GetSpecificCulture();

            return DateTime.TryParse(s,
                                     culture,
                                     DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault,
                                     out time);
        }

        private string? DateTimeToString(DateTime? d)
            => d.HasValue ? DateTimeToString(d.Value) : null;

        private string DateTimeToString(DateTime d)
            => DateTimeToString(d, SelectedTimeFormat);

        private string DateTimeToString(DateTime datetime, DatePickerFormat format)
        {
            CultureInfo culture = Language.GetSpecificCulture();
            DateTimeFormatInfo dtfi = culture.GetDateFormat();

            string hourFormatChar = Is24Hours ? "H" : "h";

            var sb = new StringBuilder();
            sb.Append(hourFormatChar);
            if (format == DatePickerFormat.Long)
            {
                sb.Append(hourFormatChar);
            }

            sb.Append(dtfi.TimeSeparator);
            sb.Append("mm");
            if (WithSeconds)
            {
                sb.Append(dtfi.TimeSeparator);
                sb.Append("ss");
            }

            if (!Is24Hours && (!string.IsNullOrEmpty(dtfi.AMDesignator) || !string.IsNullOrEmpty(dtfi.PMDesignator)))
            {
                sb.Append(" tt");
            }

            return datetime.ToString(sb.ToString(), culture);
        }

        private void PopupOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (sender is not Popup popup || popup.StaysOpen) return;

            if (_dropDownButton?.InputHitTest(mouseButtonEventArgs.GetPosition(_dropDownButton)) != null)
            {
                // This popup is being closed by a mouse press on the drop down button 
                // The following mouse release will cause the closed popup to immediately reopen. 
                // Raise a flag to block re-opening the popup
                _disablePopupReopen = true;
            }
        }

        private void PopupOnClosed(object? sender, EventArgs eventArgs)
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

        private void PopupOnOpened(object? sender, EventArgs eventArgs)
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
            if (WithSeconds && clockChoiceMadeEventArgs.Mode == ClockDisplayMode.Seconds ||
                !WithSeconds && clockChoiceMadeEventArgs.Mode == ClockDisplayMode.Minutes)
            {
                TogglePopup();
                if (SelectedTime == null)
                {
                    SelectedTime = _clock.Time;
                }
            }
        }

        private void DropDownButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
            => TogglePopup();

        private void TogglePopup()
        {
            if (IsDropDownOpen)
            {
                SetCurrentValue(IsDropDownOpenProperty, false);
            }
            else
            {
                if (_disablePopupReopen)
                {
                    _disablePopupReopen = false;
                }
                else
                {
                    SetSelectedTime();
                    SetCurrentValue(IsDropDownOpenProperty, true);
                }
            }
        }

        private BindingBase GetBinding(DependencyProperty property, IValueConverter? converter = null)
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