[Home](..\README.md) > How to increase rendering performance?

---

# How to increase rendering performance?

## Background information

Every class inheriting from [`UIElement`](https://learn.microsoft.com/dotnet/api/system.windows.uielement?view=windowsdesktop-8.0) 
contains a property [`CacheMode`](https://learn.microsoft.com/dotnet/api/system.windows.uielement.cachemode?view=windowsdesktop-8.0). To quote Microsoft's documentation:

> Set the CacheMode property when you need to increase performance for content that is time consuming to render. For 
> more information, see [BitmapCache](https://learn.microsoft.com/dotnet/api/system.windows.media.bitmapcache?view=windowsdesktop-8.0).

The default value is `null` as to not use any form of caching. This makes the controls sharp and crisp.

## Setting `UIElement.CacheMode`

An example how to set a `CacheMode`:

```xaml
<!-- This should decrease rendering time -->

<ToggleButton>
    <ToggleButton.CacheMode>
        <BitmapCache 
            EnableClearType="True"
            RenderAtScale="1"
            SnapsToDevicePixels="True" />
    </ToggleButton.CacheMode>
</ToggleButton>
```

Increase the `RenderAtScale` value, will sharpen the control, but it will also make it more pixelized when drawn smaller.

> [!NOTE]
> The default value of `UIElement.CacheMode` is `null`.

## Advanced: setting `ShadowAssist.CacheMode`

Material Design in XAML toolkit also provides you with an attached property `ShadowAssist.CacheMode`. 
This attached property is used in places where a simple `CacheMode` property would not suffice. This could be in situations 
where the property should be inherited, as `UIElement.CacheMode` does not support property inheritance.

This attached property is set through binding on a `CacheMode` property under the parent control.

An example of this property being used:
```xaml
<!-- Found inside MaterialDesignTheme.ToggleButton.xaml -->

<AdornerDecorator CacheMode="{Binding RelativeSource={RelativeSource Self}, Path=(wpf:ShadowAssist.CacheMode)}">
    <Ellipse x:Name="Thumb" ... />
</AdornerDecorator>
```

> [!NOTE]
> The default value of `ShadowAssist.CacheMode` is `null`.

## Example

| With `CacheMode` set                                                                                                              | Without `CacheMode` set                                                                                                           |
| --------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| ![image](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/assets/6505319/9401be9c-9939-4c02-b37e-610707ea9e5c) | ![image](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/assets/6505319/928e6f70-60a2-4e0a-b8e5-f1955d3cc6f4) |

## Further reading

Some interesting articles with more in-depth information:
* [Property value inheritance (WPF .NET)](https://learn.microsoft.com/dotnet/desktop/wpf/properties/property-value-inheritance?view=netdesktop-7.0)
* [UIElement.CacheMode Property](https://learn.microsoft.com/dotnet/api/system.windows.uielement.cachemode?view=windowsdesktop-8.0)