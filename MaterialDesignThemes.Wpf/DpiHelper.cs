using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    internal static class DpiHelper
    {
        private static readonly int _dpiX;
        private static readonly int _dpiY;

        private const double StandartDpiX = 96.0;
        private const double StandartDpiY = 96.0;

        static DpiHelper()
        {
            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

            _dpiX = (int)dpiXProperty.GetValue(null, null);
            _dpiY = (int)dpiYProperty.GetValue(null, null);
        }

        public static double TransformToDeviceY(double y)
        {
            return y * _dpiY / StandartDpiY;
        }

        public static double TransformToDeviceX(double x)
        {
            return x * _dpiX / StandartDpiX;
        }
    }
}
