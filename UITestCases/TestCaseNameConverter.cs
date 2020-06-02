using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace UITestCases
{
    public class TestCaseNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            Type testCaseType = value.GetType();
            return testCaseType.GetCustomAttribute<TestCaseNameAttribute>()?.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
