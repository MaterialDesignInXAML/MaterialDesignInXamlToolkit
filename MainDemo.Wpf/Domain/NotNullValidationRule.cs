using System.Globalization;
using System.Windows.Controls;

namespace MaterialDesignDemo.Domain
{
    public class NotNullValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value == null ? new ValidationResult(false, "Field is required.") : ValidationResult.ValidResult;
        }
    }
}
