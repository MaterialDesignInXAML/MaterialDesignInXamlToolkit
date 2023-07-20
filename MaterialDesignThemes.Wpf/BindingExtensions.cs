using System;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf
{
    internal static class BindingExtensions
    {
        /// <summary>Creates a shallow copy of a System.Windows.Data.Binding object.</summary>
        /// <remarks>Bindings have an isSealed internal field that prevents it from being modified after it has been used.</remarks>
        public static Binding Clone(this Binding source)
        {
            var result = new Binding()
            {
                AsyncState = source.AsyncState,
                BindingGroupName = source.BindingGroupName,
                BindsDirectlyToSource = source.BindsDirectlyToSource,
                Converter = source.Converter,
                ConverterCulture = source.ConverterCulture,
                ConverterParameter = source.ConverterParameter,
                FallbackValue = source.FallbackValue,
                IsAsync = source.IsAsync,
                Mode = source.Mode,
                NotifyOnSourceUpdated = source.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = source.NotifyOnTargetUpdated,
                NotifyOnValidationError = source.NotifyOnValidationError,
                Path = source.Path,
                StringFormat = source.StringFormat,
                TargetNullValue = source.TargetNullValue,
                UpdateSourceExceptionFilter = source.UpdateSourceExceptionFilter,
                UpdateSourceTrigger = source.UpdateSourceTrigger,
                ValidatesOnDataErrors = source.ValidatesOnDataErrors,
                ValidatesOnExceptions = source.ValidatesOnExceptions,
                XPath = source.XPath,
            };

            //Note: setting more than one of [Source, ElementName, RelativeSource] will throw an exception.
            if (source.Source is not null)
            {
                result.Source = source.Source;
            }
            else if (source.ElementName is not null)
            {
                result.ElementName = source.ElementName;
            }
            else if (source.RelativeSource is not null)
            {
                result.RelativeSource = source.RelativeSource;
            }

            return result;
        }
    }
}