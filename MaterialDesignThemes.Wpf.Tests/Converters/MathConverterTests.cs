using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters
{
    public class MathConverterTests
    {
        [Theory]
        [EnumData]
        public void EnumValues_AreAllHandled(MathOperation operation)
        {
            var converter = new MathConverter 
            {
                Operation = operation
            };

            Assert.True(converter.Convert(1.0, null, 1.0, CultureInfo.CurrentUICulture) is double);
        }
    }
}
