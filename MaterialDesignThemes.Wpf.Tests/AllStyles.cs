using System.Collections;

namespace MaterialDesignThemes.Wpf.Tests;

public class AllStyles<T> : IEnumerable<object[]> where T : FrameworkElement
{
    private static readonly IReadOnlyList<object> _styleKeys = MdixHelper.GetStyleKeysFor<T>().ToList();

    public IEnumerator<object[]> GetEnumerator()
    {

        return _styleKeys.Select(x => new object[] { x }).GetEnumerator(); ;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
