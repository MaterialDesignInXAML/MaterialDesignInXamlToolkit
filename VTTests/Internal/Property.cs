using System;

namespace VTTests.Internal
{
    internal class Property : IValue
    {
        public string Value { get; }
        public string ValueType { get; }
        public string PropertyType { get; }

        public Property(string propertyType, string valueType, string value)
        {
            PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
            ValueType = valueType;
            Value = value;
        }
    }
}
