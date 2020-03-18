using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class EnumDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            ParameterInfo[] parameters = testMethod.GetParameters();
            if (parameters.Length != 1 ||
                !parameters[0].ParameterType.IsEnum)
            {
                throw new Exception($"{testMethod.DeclaringType.FullName}.{testMethod.Name} must have a single enum parameter");
            }

            return GetDataImplementation(parameters[0].ParameterType);

            static IEnumerable<object[]> GetDataImplementation(Type parameterType)
            {
                foreach (object enumValue in Enum.GetValues(parameterType))
                {
                    yield return new[] { enumValue };
                }
            }
        }
    }
}
