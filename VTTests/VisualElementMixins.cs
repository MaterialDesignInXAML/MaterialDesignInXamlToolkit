using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using PInvoke;
using static PInvoke.User32;

namespace VTTests
{

    public static class VisualElementMixins
    {
        public static async Task<IVisualElement?> SetXamlContent(this IVisualElement containerElement, string xaml)
        {
            if (containerElement is null)
            {
                throw new ArgumentNullException(nameof(containerElement));
            }

            if (await containerElement.SetProperty("Content", xaml, Types.XamlString) is { })
            {
                return await containerElement.GetElement(".Content");
            }
            return null;
        }

        public static async Task MoveCursorToElement(
            this IVisualElement element, Position position = Position.Center)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            Rect coordinates = await element.GetCoordinates();

            Point location = position switch
            {
                Position.TopLeft => coordinates.TopLeft,
                _ => coordinates.Center()
            };

            SetCursorPos((int)location.X, (int)location.Y);
        }

        public static async Task Click(this IVisualElement element)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            await MoveCursorToElement(element);
            LeftClick();
        }

        private static unsafe void LeftClick()
            => mouse_event(mouse_eventFlags.MOUSEEVENTF_LEFTDOWN | mouse_eventFlags.MOUSEEVENTF_LEFTUP, 0, 0, 0, null);

        public static async Task<T> GetProperty<T>(this IVisualElement element, string propertyName)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            IValue value = await element.GetProperty(propertyName);
            return value.GetValueAs<T>();
        }

        public static async Task<bool> GetIsVisible(this IVisualElement element)
            => await element.GetProperty<bool>("IsVisible");

        public static async Task<string> GetText(this IVisualElement element)
            => await element.GetProperty<string>("Text");

        public static async Task<double> GetOpacity(this IVisualElement element)
            => await element.GetProperty<double>("Opacity");

        public static async Task<Color> GetBackgroundColor(this IVisualElement element)
            => await element.GetProperty<Color>("Background");

        public static async Task<Color> GetForegroundColor(this IVisualElement element)
            => await element.GetProperty<Color>("Foreground");
    }
}
