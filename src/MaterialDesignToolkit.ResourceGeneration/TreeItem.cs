using System.Collections;
using System.Diagnostics;

namespace MaterialDesignToolkit.ResourceGeneration;

[DebuggerDisplay($"{{{nameof(Name)}}} [Values: {{{nameof(Values)}.Count}}] [Children: {{{nameof(Children)}.Count}}]")]
public class TreeItem<T>(string name) : IEnumerable<T>
{
    public string Name { get; } = name;

    public List<T> Values { get; } = [];

    public List<TreeItem<T>> Children { get; } = [];

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T value in Values)
        {
            yield return value;
        }
        foreach (TreeItem<T> child in Children)
        {
            foreach (T value in child)
            {
                yield return value;
            }
        }
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

