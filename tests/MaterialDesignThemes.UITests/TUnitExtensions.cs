using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Expression = System.Linq.Expressions.Expression;

namespace MaterialDesignThemes.UITests;

internal static class TUnitExtensions
{
    private const string GetDisplayNameMethodName = "GetDisplayName";

    private static readonly Lazy<Func<TestContext, string>> _getDisplayNameFunc = new(() =>
    {
        var testContextType = typeof(TestContext);
        MethodInfo? getDisplayNameMethod = testContextType.GetMethod(
            GetDisplayNameMethodName,
            BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException($"Could not find {GetDisplayNameMethodName} method on {testContextType.FullName}.");

        if (getDisplayNameMethod.ReturnType != typeof(string) || getDisplayNameMethod.GetParameters().Length != 0)
        {
            throw new InvalidOperationException(
                $"{GetDisplayNameMethodName} on {testContextType.FullName} has an unexpected signature.");
        }

        ParameterExpression testContextParameter = Expression.Parameter(testContextType, "testContext");
        MethodCallExpression callExpression = Expression.Call(testContextParameter, getDisplayNameMethod);
        var lambdaExpression = Expression.Lambda<Func<TestContext, string>>(callExpression, testContextParameter);

        return lambdaExpression.Compile();
    });

    extension(TestContext testContext)
    {
        public string GetDisplayName()
            => _getDisplayNameFunc.Value(testContext);
    }
}
