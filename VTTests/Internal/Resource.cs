using System;

namespace VTTests.Internal
{
    internal class Resource : IResource
    {
        public string Key { get; }

        public string Value { get; }

        public string ValueType { get; }

        public Resource(string key, string valueType, string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ValueType = valueType;
            Value = value;
        }
    }
}
