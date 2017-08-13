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
using CodeDisplayer;
using MaterialDesignDemo.Helper;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Pickers.xaml
    /// </summary>
    public partial class Pickers : UserControl
    {
        public Pickers()
        {
            InitializeComponent();
            FutureDatePicker.BlackoutDates.AddDatesInPast();
            LoadLocales();
            LocaleCombo.SelectionChanged += LocaleCombo_SelectionChanged;
            LocaleCombo.SelectedItem = "fr-CA";
			
        }

        private void LocaleCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                var lang = System.Windows.Markup.XmlLanguage.GetLanguage((string) LocaleCombo.SelectedItem);
                LocaleDatePicker.Language = lang;
                LocaleDatePickerRTL.Language = lang;
            }
            catch {
                LocaleCombo.SelectedItem = "fr-CA";
            }
            //HACK: The calendar only refresh when we change the date
            LocaleDatePicker.DisplayDate = LocaleDatePicker.DisplayDate.AddDays(1);
            LocaleDatePicker.DisplayDate = LocaleDatePicker.DisplayDate.AddDays(-1);
            LocaleDatePickerRTL.DisplayDate = LocaleDatePicker.DisplayDate.AddDays(1);
            LocaleDatePickerRTL.DisplayDate = LocaleDatePicker.DisplayDate.AddDays(-1);
        }

        private void LoadLocales()
        {
            foreach (var ci in CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(ci => ci.Calendar is GregorianCalendar)
                .OrderBy(ci => ci.Name))
            {
                LocaleCombo.Items.Add(ci.Name);
            }
        }

        public void CalendarDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            Calendar.SelectedDate = ((PickersViewModel)DataContext).Date;
        }

        public void CalendarDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, "1")) return;

            if (!Calendar.SelectedDate.HasValue)
            {
                eventArgs.Cancel();
                return;
            }

            ((PickersViewModel)DataContext).Date = Calendar.SelectedDate.Value;
        }

        public void ClockDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            Clock.Time = ((PickersViewModel) DataContext).Time;
        }

        public void ClockDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1"))
                ((PickersViewModel)DataContext).Time = Clock.Time;
        }
    }
}
