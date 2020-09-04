using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using XamlTest;

namespace MaterialDesignThemes.UITests.WPF.TimePicker
{
    static class MaterialDesignTimePicker
    {
        private static async Task<T>GetNullableProperty<T>(this IVisualElement element, string propertyName)
        {
            try
            {
                return await element.GetProperty<T>(propertyName);
            }
            catch
            {
                return default!;
            }
        }

        public static async Task<DateTime?>GetSelectedTime(this IVisualElement timePicker)
            => await timePicker.GetNullableProperty<DateTime?>(nameof(Wpf.TimePicker.SelectedTime));

        public static async Task SetSelectedTime(this IVisualElement timePicker, DateTime value)
            => await timePicker.SetProperty(nameof(Wpf.TimePicker.SelectedTime), (DateTime?)value);

        public static async Task PickClock(this IVisualElement timePicker, int hour, int minute)
        {
            var button = await timePicker.GetElement("PART_Button");
            await button.Click();

            var popup = await timePicker.GetElement("PART_Popup");
            await popup.ClickHour(hour);
            await popup.ClickMinute(minute);
        }

        private static async Task ClickHour(this IVisualElement popup, int hour)
            => await popup.ClickButton(Clock.HoursCanvasPartName, (hour + 11) % 12);

        private static async Task ClickMinute(this IVisualElement popup, int minute)
            => await popup.ClickButton(Clock.MinutesCanvasPartName, (minute % 5 == 0 ? (minute / 5 + 11) % 12 : minute - minute / 5 + 11));

        private static async Task ClickButton(this IVisualElement popup, string partName, int index)
        {
            const int delay = 200;

            await Task.Delay(delay);
            var canvas = await Wait.For(async () => await popup.GetElement(partName));
            var button = await canvas.GetElement($"/ClockItemButton[{index}]");
            await Task.Delay(delay);
            await Wait.For(async () =>
            {
                await button.Click();
                await Task.Delay(delay);
                return await button.GetNullableProperty<bool?>(nameof(ClockItemButton.IsChecked)) == true;
            });
        }
    }
}
