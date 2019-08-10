using System.Globalization;
using System.Windows.Controls;

namespace MaterialDesignDemo.Validation
{
    public class ColorHexValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string hexString = value as string;

            if (hexString.StartsWith("#"))
            {
                hexString = hexString.Remove(0, 1);
            }

            int hexNumber;

            bool isHex = int.TryParse(hexString, NumberStyles.HexNumber, cultureInfo, out hexNumber);

            if (!isHex || hexString.Length != 6 || hexNumber > 0xFFFFFF || hexNumber < 0)
            {
                return new ValidationResult(false, "not convertable to a color");
            }

            return new ValidationResult(true, null);
        }
    }
}
