using System.Numerics;
using System.Runtime.CompilerServices;
using TUnit.Assertions.Core;

namespace MaterialDesignThemes.Tests.TUnit;

public static class IsCloseToExtensions
{
    public static IsCloseToAssertion<double> IsCloseTo(
        this IAssertionSource<double> source, double expected, double precision,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpression = null,
        [CallerArgumentExpression(nameof(precision))] string? precisionExpression = null)
    {
        source.Context.ExpressionBuilder.Append(".IsCloseTo(");
        source.Context.ExpressionBuilder.Append(expectedExpression);
        source.Context.ExpressionBuilder.Append(", ");
        source.Context.ExpressionBuilder.Append(precisionExpression);
        source.Context.ExpressionBuilder.Append(')');
        return new IsCloseToAssertion<double>(source.Context, expected, precision);
    }

    public static IsCloseToAssertion<float> IsCloseTo(
        this IAssertionSource<float> source, float expected, float precision,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpression = null,
        [CallerArgumentExpression(nameof(precision))] string? precisionExpression = null)
    {
        source.Context.ExpressionBuilder.Append(".IsCloseTo(");
        source.Context.ExpressionBuilder.Append(expectedExpression);
        source.Context.ExpressionBuilder.Append(", ");
        source.Context.ExpressionBuilder.Append(precisionExpression);
        source.Context.ExpressionBuilder.Append(')');
        return new IsCloseToAssertion<float>(source.Context, expected, precision);
    }
}

public class IsCloseToAssertion<TValue>(AssertionContext<TValue> context, TValue expected, TValue precision) : Assertion<TValue>(context)
    where TValue : IFloatingPoint<TValue>, INumberBase<TValue>
{
    protected override string GetExpectation()
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(15, 1);
        defaultInterpolatedStringHandler.AppendLiteral("to be within ");
        defaultInterpolatedStringHandler.AppendFormatted($"\"{precision}\"");
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted($"\"{expected}\"");
        return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    protected override Task<AssertionResult> CheckAsync(EvaluationMetadata<TValue> metadata)
    {
        TValue? actualValue = metadata.Value;
        Exception? exception = metadata.Exception;
        if (exception != null)
        {
            return Task.FromResult(AssertionResult.Failed("threw " + exception.GetType().FullName));
        }
        if (actualValue is null)
        {
            return Task.FromResult(AssertionResult.Failed($"found <null>"));
        }

        TValue difference = actualValue - expected;
        TValue absoluteDifference = TValue.Abs(difference);
        bool isInRange = absoluteDifference <= precision;

        if (isInRange)
        {
            return Task.FromResult(AssertionResult.Passed);
        }
        return Task.FromResult(AssertionResult.Failed($"found {actualValue}"));
    }
}
