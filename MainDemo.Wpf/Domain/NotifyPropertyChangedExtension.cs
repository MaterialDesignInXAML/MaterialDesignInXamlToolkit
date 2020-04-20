using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MaterialDesignDemo.Domain
{
    public static class NotifyPropertyChangedExtension
    {
        public static bool MutateVerbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return false;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
