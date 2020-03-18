using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters
{
    public class MathMultipleConverterTests
    {
        [Theory]
        [EnumData]
        public void EnumValues_AreAllHandled(MathOperation operation)
        {
            var converter = new MathMultipleConverter 
            {
                Operation = operation
            };

            Assert.True(converter.Convert(new object[] { 1.0, 1.0 }, null, null, CultureInfo.CurrentUICulture) is double);
        }
    }
}
