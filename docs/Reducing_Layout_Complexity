# Reducing Layout Complexity

## Background information

Complex layouts can slow down WPF performance, especially with nested controls or excessive use of `Grid` and `StackPanel`.

## Avoid Nested Grids and StackPanels

Overuse of nested layouts can create rendering bottlenecks. Try to simplify the structure or use a `UniformGrid` or `DockPanel` for simpler arrangements.

```xaml
<!-- Instead of nesting multiple StackPanels, use a single DockPanel -->
<DockPanel>
    <TextBlock Text="Header" DockPanel.Dock="Top" />
    <ListView DockPanel.Dock="Bottom" />
</DockPanel>
```

## Prefer Static Resources Over Dynamic Resources

Static resources are faster to load compared to dynamic ones. Use dynamic resources only if you need runtime changes in resource values.

```xaml
<!-- Use StaticResource instead of DynamicResource for better performance -->
<Style x:Key="ButtonStyle" TargetType="Button" BasedOn="{StaticResource BaseButtonStyle}" />
```

> [!NOTE]
> Dynamic resources are reevaluated each time they’re used, which may impact performance.
