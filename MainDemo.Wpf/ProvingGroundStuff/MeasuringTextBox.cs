using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignDemo.ProvingGroundStuff
{
    public class MeasuringTextBox : TextBox
    {
        static MeasuringTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MeasuringTextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = base.ArrangeOverride(arrangeBounds);
            stopwatch.Stop();
            Debug.WriteLine($"Arrange: {stopwatch.ElapsedMilliseconds} - {stopwatch.ElapsedTicks} - ({GetHashCode()})");
            return result;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = base.MeasureOverride(constraint);
            stopwatch.Stop();
            Debug.WriteLine($"Measure: {stopwatch.ElapsedMilliseconds} - {stopwatch.ElapsedTicks} - ({GetHashCode()})");
            return result;
        }
    }
}
