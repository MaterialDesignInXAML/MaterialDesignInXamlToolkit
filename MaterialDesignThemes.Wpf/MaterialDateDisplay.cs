using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignThemes.Wpf
{    
    public class MaterialDateDisplay : Control
    {
        private readonly IDictionary<int, Action<string>> _setters;
        private readonly IDictionary<char, string> _formats = new Dictionary<char, string>
            {
                {'d', "dd"},
                {'m', "ddd, MMM"},
                {'y', "yyyy"}
            };

        static MaterialDateDisplay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialDateDisplay), new FrameworkPropertyMetadata(typeof(MaterialDateDisplay)));
        }

        public MaterialDateDisplay()
        {
            _setters = new Dictionary<int, Action<string>>
            {
                {0, s => ComponentOneContent = s},
                {1, s => ComponentTwoContent = s},
                {2, s => ComponentThreeContent = s},
            };
            SetCurrentValue(DisplayDateProperty, DateTime.Now.Date);
        }

        public static readonly DependencyProperty DisplayDateProperty = DependencyProperty.Register(
            nameof(DisplayDate), typeof (DateTime), typeof (MaterialDateDisplay), new PropertyMetadata(default(DateTime), DisplayDatePropertyChangedCallback));

        private static void DisplayDatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((MaterialDateDisplay)dependencyObject).UpdateComponents();
        }

        public DateTime DisplayDate
        {
            get { return (DateTime) GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); }
        }

        private static readonly DependencyPropertyKey ComponentOneContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "ComponentOneContent", typeof (string), typeof (MaterialDateDisplay),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ComponentOneContentProperty =
            ComponentOneContentPropertyKey.DependencyProperty;

        public string ComponentOneContent
        {
            get { return (string) GetValue(ComponentOneContentProperty); }
            private set { SetValue(ComponentOneContentPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey ComponentTwoContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "ComponentTwoContent", typeof (string), typeof (MaterialDateDisplay),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ComponentTwoContentProperty =
            ComponentTwoContentPropertyKey.DependencyProperty;

        public string ComponentTwoContent
        {
            get { return (string) GetValue(ComponentTwoContentProperty); }
            private set { SetValue(ComponentTwoContentPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey ComponentThreeContentPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "ComponentThreeContent", typeof (string), typeof (MaterialDateDisplay),
                new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ComponentThreeContentProperty =
            ComponentThreeContentPropertyKey.DependencyProperty;

        public string ComponentThreeContent
        {
            get { return (string) GetValue(ComponentThreeContentProperty); }
            private set { SetValue(ComponentThreeContentPropertyKey, value); }
        }

	    private static readonly DependencyPropertyKey IsDayInFirstComponentPropertyKey =
		    DependencyProperty.RegisterReadOnly(
			    "IsDayInFirstComponent", typeof (bool), typeof (MaterialDateDisplay),
			    new PropertyMetadata(default(bool)));

	    public static readonly DependencyProperty IsDayInFirstComponentProperty =
		    IsDayInFirstComponentPropertyKey.DependencyProperty;

	    public bool IsDayInFirstComponent
	    {
		    get { return (bool) GetValue(IsDayInFirstComponentProperty); }
		    private set { SetValue(IsDayInFirstComponentPropertyKey, value); }
	    }

        //FrameworkElement.LanguageProperty.OverrideMetadata(typeof (Calendar), (PropertyMetadata) new FrameworkPropertyMetadata(new PropertyChangedCallback(Calendar.OnLanguageChanged)));
        private void UpdateComponents()
        {
            var culture = Language.GetSpecificCulture();
            var dateTimeFormatInfo = culture.GetDateFormat();

            foreach (var component in dateTimeFormatInfo.ShortDatePattern.Split(new[] {dateTimeFormatInfo.DateSeparator},
                StringSplitOptions.RemoveEmptyEntries).Select((s, index) => new {code = s.ToLower()[0], index}))
            {
	            if (component.index == 0)
		            IsDayInFirstComponent = component.code == 'd';
				_setters[component.index](DisplayDate.ToString(_formats[component.code], culture).ToTitleCase(culture));
            }					       
        }

        /// <summary>
        /// Ripped straight from .Net FX.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal static DateTimeFormatInfo GetDateFormat(CultureInfo culture)
        {
            if (culture.Calendar is GregorianCalendar)
            {
                return culture.DateTimeFormat;
            }
            else
            {
                GregorianCalendar foundCal = null;
                DateTimeFormatInfo dtfi = null;

                foreach (System.Globalization.Calendar cal in culture.OptionalCalendars)
                {
                    if (cal is GregorianCalendar)
                    {
                        // Return the first Gregorian calendar with CalendarType == Localized 
                        // Otherwise return the first Gregorian calendar
                        if (foundCal == null)
                        {
                            foundCal = cal as GregorianCalendar;
                        }

                        if (((GregorianCalendar)cal).CalendarType == GregorianCalendarTypes.Localized)
                        {
                            foundCal = cal as GregorianCalendar;
                            break;
                        }
                    }
                }

                if (foundCal == null)
                {
                    // if there are no GregorianCalendars in the OptionalCalendars list, use the invariant dtfi 
                    dtfi = ((CultureInfo)CultureInfo.InvariantCulture.Clone()).DateTimeFormat;
                    dtfi.Calendar = new GregorianCalendar();
                }
                else
                {
                    dtfi = ((CultureInfo)culture.Clone()).DateTimeFormat;
                    dtfi.Calendar = foundCal;
                }

                return dtfi;
            }
        } 

    }
}
