using System.Numerics;
using System.Runtime.CompilerServices;
using TUnit.Assertions.AssertConditions;
using TUnit.Assertions.AssertConditions.Interfaces;
using TUnit.Assertions.AssertionBuilders;

namespace MaterialDesignThemes.Wpf.Tests.TUnit;

public static class IsCloseToExtensions
{
    public static IsCloseToWrapper<double> IsCloseTo(this IValueSource<double> valueSource, double expected, double precision, [CallerArgumentExpression(nameof(expected))] string? doNotPopulateThisValue1 = null, [CallerArgumentExpression(nameof(precision))] string? doNotPopulateThisValue2 = null)
    {
        var assertionBuilder = valueSource.RegisterAssertion(new IsCloseToCondition<double>(expected, precision)
            , [doNotPopulateThisValue1, doNotPopulateThisValue2]);

        return new IsCloseToWrapper<double>(assertionBuilder);
    }

    public static IsCloseToWrapper<float> IsCloseTo(this IValueSource<float> valueSource, float expected, float precision, [CallerArgumentExpression(nameof(expected))] string? doNotPopulateThisValue1 = null, [CallerArgumentExpression(nameof(precision))] string? doNotPopulateThisValue2 = null)
    {
        var assertionBuilder = valueSource.RegisterAssertion(new IsCloseToCondition<float>(expected, precision)
            , [doNotPopulateThisValue1, doNotPopulateThisValue2]);

        return new IsCloseToWrapper<float>(assertionBuilder);
    }
}

public class IsCloseToWrapper<TActual>(InvokableAssertionBuilder<TActual> invokableAssertionBuilder)
    : InvokableValueAssertionBuilder<TActual>(invokableAssertionBuilder);

file class IsCloseToCondition<TActual>(TActual expected, TActual tolerance) : BaseAssertCondition<TActual>
    where TActual :
    IFloatingPoint<TActual>,
    INumberBase<TActual>
{
    protected override string GetExpectation() => $"to be within {tolerance} of {expected}";

    protected override ValueTask<AssertionResult> GetResult(
        TActual? actualValue, Exception? exception,
        AssertionMetadata assertionMetadata
    )
    {
        if(actualValue is null)
            return AssertionResult.Fail("received null");

        TActual difference = actualValue - expected;
        TActual absoluteDifference = TActual.Abs(difference);
        bool isInRange = absoluteDifference <= tolerance;
        return AssertionResult.FailIf(!isInRange, $"received {actualValue}");
    }
}
