using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Converters.CircularProgressBar
{
    internal static class LocalEx
    {
        public static double ExtractDouble(this object val)
        {
            var d = val as double? ?? double.NaN;
            return double.IsInfinity(d) ? double.NaN : d;
        }


        public static bool AnyNan(this IEnumerable<double> vals)
        {
            return vals.Any(double.IsNaN);
        }
    }
}
