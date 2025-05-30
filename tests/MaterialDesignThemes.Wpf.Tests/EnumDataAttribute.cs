using System.Reflection;

namespace MaterialDesignThemes.Wpf.Tests;

public class EnumDataAttribute : DataAttribute
{
    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        ParameterInfo[] parameters = testMethod.GetParameters();
        if (parameters.Length != 1 ||
            !parameters[0].ParameterType.IsEnum)
        {
            throw new Exception($"{testMethod.DeclaringType?.FullName}.{testMethod.Name} must have a single enum parameter");
        }

        return new([..GetDataImplementation(parameters[0].ParameterType)]);

        static IEnumerable<ITheoryDataRow> GetDataImplementation(Type parameterType)
        {
            foreach (object enumValue in Enum.GetValues(parameterType).OfType<object>())
            {
                yield return new TheoryDataRow(enumValue);
            }
        }
    }
    public override bool SupportsDiscoveryEnumeration() => true;
}
