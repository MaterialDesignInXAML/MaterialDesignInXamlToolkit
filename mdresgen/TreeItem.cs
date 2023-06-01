using System.Collections;
using System.Diagnostics;

namespace mdresgen;

[DebuggerDisplay($"{{{nameof(Name)}}} [Values: {{{nameof(Values)}.Count}}] [Children: {{{nameof(Children)}.Count}}]")]
public class TreeItem<T> : IEnumerable<T>
{
    public string Name { get; }

    public TreeItem(string name) => Name = name;

    public List<T> Values { get; } = new();

    public List<TreeItem<T>> Children { get; } = new();

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

