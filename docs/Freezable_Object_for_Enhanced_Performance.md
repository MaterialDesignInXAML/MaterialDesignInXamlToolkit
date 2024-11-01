[Home](..\README.md) > Using `Freezable` Objects for Enhanced Performance

---

# Using `Freezable` Objects for Enhanced Performance

## Background information

WPF provides the `Freezable` class for objects that can be made immutable to optimize performance, such as brushes, pens, and transforms.

## Setting Objects as `Freezable`

When an object is frozen, it becomes immutable, which reduces memory usage and allows WPF to optimize rendering.

```csharp
SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
if (myBrush.CanFreeze)
{
    myBrush.Freeze();
}
```

Frozen objects cannot be modified, so only freeze objects that don’t require changes.

## Benefits of Freezable Objects

Freezable objects improve rendering speed, especially for large visuals or repeated animations. This technique is particularly useful in complex UIs with many reused resources.
