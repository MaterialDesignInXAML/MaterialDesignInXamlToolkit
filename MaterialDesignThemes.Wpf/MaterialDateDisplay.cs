using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Calendar = System.Windows.Controls.Calendar;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDateDisplay : Control
    {
        static MaterialDateDisplay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialDateDisplay), new FrameworkPropertyMetadata(typeof(MaterialDateDisplay)));
        }

        public MaterialDateDisplay()
        {
            SetCurrentValue(DisplayDateProperty, DateTime.Today);
        }

        public static readonly DependencyProperty DisplayDateProperty = DependencyProperty.Register(
            nameof(DisplayDate), typeof(DateTime), typeof(MaterialDateDisplay), new PropertyMetadata(default(DateTime), DisplayDatePropertyChangedCallback, DisplayDateCoerceValue));

        private static object DisplayDateCoerceValue(DependencyObject d, object baseValue)
        {
            if (d is FrameworkElement element &&
                element.Language.GetSpecificCulture() is CultureInfo culture &&
                baseValue is DateTime displayDate)
            {
                if (displayDate < culture.Calendar.MinSupportedDateTime)
                {
                    return culture.Calendar.MinSupportedDateTime;
                }
                if (displayDate > culture.Calendar.MaxSupportedDateTime)
                {
                    return culture.Calendar.MaxSupportedDateTime;
                }
            }
            return baseValue;
        }

        private static void DisplayDatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((MaterialDateDisplay)dependencyObject).UpdateComponents();
        }

        public DateTime DisplayDate
        {
            get { return (DateTime)GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); }
        }

        private static readonly DependencyPropertyKey ComponentOneContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ComponentOneContent), typeof(string), typeof(MaterialDateDisplay),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ComponentOneContentProperty =
            ComponentOneContentPropertyKey.DependencyProperty;

        public string? ComponentOneContent
        {
            get => (string)GetValue(ComponentOneContentProperty);
            private set => SetValue(ComponentOneContentPropertyKey, value);
        }

        private static readonly DependencyPropertyKey ComponentTwoContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ComponentTwoContent), typeof(string), typeof(MaterialDateDisplay),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ComponentTwoContentProperty =
            ComponentTwoContentPropertyKey.DependencyProperty;

        public string? ComponentTwoContent
        {
            get => (string?)GetValue(ComponentTwoContentProperty);
            private set => SetValue(ComponentTwoContentPropertyKey, value);
        }

        private static readonly DependencyPropertyKey ComponentThreeContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ComponentThreeContent), typeof(string), typeof(MaterialDateDisplay),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ComponentThreeContentProperty =
            ComponentThreeContentPropertyKey.DependencyProperty;

        public string? ComponentThreeContent
        {
            get => (string?)GetValue(ComponentThreeContentProperty);
            private set => SetValue(ComponentThreeContentPropertyKey, value);
        }

        private static readonly DependencyPropertyKey IsDayInFirstComponentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(IsDayInFirstComponent), typeof(bool), typeof(MaterialDateDisplay),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsDayInFirstComponentProperty =
            IsDayInFirstComponentPropertyKey.DependencyProperty;

        public bool IsDayInFirstComponent
        {
            get => (bool)GetValue(IsDayInFirstComponentProperty);
            private set => SetValue(IsDayInFirstComponentPropertyKey, value);
        }

        private void UpdateComponents()
        {
            var culture = Language.GetSpecificCulture();
            var dateTimeFormatInfo = culture.GetDateFormat();
            var minDateTime = dateTimeFormatInfo.Calendar.MinSupportedDateTime;
            var maxDateTime = dateTimeFormatInfo.Calendar.MaxSupportedDateTime;

            if (DisplayDate < minDateTime)
            {
                SetDisplayDateOfCalendar(minDateTime);

                // return to avoid second formatting of the same value
                return;
            }

            if (DisplayDate > maxDateTime)
            {
                SetDisplayDateOfCalendar(maxDateTime);

                // return to avoid second formatting of the same value
                return;
            }

            var calendarFormatInfo = CalendarFormatInfo.FromCultureInfo(culture);
            var displayDate = DisplayDate;
            ComponentOneContent = FormatDate(calendarFormatInfo.ComponentOnePattern, displayDate, culture);
            ComponentTwoContent = FormatDate(calendarFormatInfo.ComponentTwoPattern, displayDate, culture);
            ComponentThreeContent = FormatDate(calendarFormatInfo.ComponentThreePattern, displayDate, culture);
        }

        private static string FormatDate(string format, DateTime displayDate, CultureInfo culture)
        {
            return string.IsNullOrEmpty(format) ? string.Empty : displayDate.ToString(format, culture).ToTitleCase(culture);
        }

        private void SetDisplayDateOfCalendar(DateTime displayDate)
        {
            Calendar calendarControl = this.GetVisualAncestry().OfType<Calendar>().FirstOrDefault();

            if (calendarControl != null)
            {
                calendarControl.DisplayDate = displayDate;
            }
        }
    }
}
