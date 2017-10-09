using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public static class DotNet4Extensions
    {
        public static Task<T> InvokeAsync<T>(this Dispatcher dispatcher, Func<T> d)
        {
            return Task.Factory.StartNew(() => (T)dispatcher.Invoke(d));
        }

        public static Task InvokeAsync(this Dispatcher dispatcher, Action d)
        {
            return Task.Factory.StartNew(() => dispatcher.Invoke(d));
        }

        public static Task InvokeAsync(this Dispatcher dispatcher, Action d, DispatcherPriority dispatcherPriority)
        {
            return Task.Factory.StartNew(() => dispatcher.Invoke(d, dispatcherPriority));
        }

        public static bool IsItemItsOwnContainer(this ItemsControl itemsControl, object item)
        {
            return (bool)itemsControl.GetType().GetMethod("IsItemItsOwnContainer").Invoke(itemsControl, new[] { item });
        }
    }
}
