---
name: xamltest-ui-tests
description: Write and update WPF UI tests in MaterialDesignThemes.UITests using XAMLTest. Use when template, layout, focus, interaction, or visual behavior needs coverage, or when a reviewer asks to move runtime UI checks out of MaterialDesignThemes.Wpf.Tests.
---

# XAMLTest UI tests

Use this skill when working on runtime WPF behavior in this repository.

## When to use it

- A change touches templates, visual states, focus behavior, layout, coordinates, or input handling.
- You need to inspect named template parts such as `PART_TextBox` or `PART_IncreaseButton`.
- A reviewer asks to use XAMLTest instead of regular unit tests.
- You need to assert WPF-only properties like `Padding`, `Visibility`, `ActualWidth`, or `DataContext` state after layout.

## Repository-specific rules

1. Put runtime UI behavior tests in `tests/MaterialDesignThemes.UITests`.
2. Do **not** add visual-tree or layout interaction tests to `tests/MaterialDesignThemes.Wpf.Tests`.
3. Prefer extending the existing feature file under `tests/MaterialDesignThemes.UITests/WPF/<feature>/` instead of creating a generic catch-all test file.
4. Match the existing file's result-recording pattern; many current UI tests use `TestRecorder`, but it should not be treated as a blanket requirement.
5. Ask permission before running UI tests, and only run the affected ones.

## Standard pattern

1. Inherit from `TestBase`.
2. Use `LoadXaml<T>()` for focused control scenarios.
3. Use `LoadUserControl<T>()` when you need a sample view model, bindings, or additional focus targets.
4. Get named parts with `GetElement<T>("PART_Name")`.
5. Use `RemoteExecute` with static methods for properties that helpers do not expose directly.
6. Use `Wait.For(...)` for layout-sensitive assertions.

## What to prefer

- `GetCoordinates()` for alignment and spacing assertions.
- `RemoteExecute` for `Padding`, `Visibility`, `ActualWidth`, `Margin`, or `DataContext` inspection.
- Keyboard and mouse helpers such as `MoveKeyboardFocus`, `SendKeyboardInput`, and `LeftClick` for interaction.

## What to avoid

- Inspecting the visual tree from `MaterialDesignThemes.Wpf.Tests` for runtime template behavior.
- Overly broad UI suites when a single targeted XAMLTest scenario will do.
- Repeating helper code inline when a small static local or private helper method is enough.

## Example

```csharp
[Test]
public async Task DecimalUpDown_WhenButtonsAreCollapsed_RemovesReservedPadding()
{
    await using var recorder = new TestRecorder(App);

    Thickness originalPadding = new(4, 5, 6, 7);
    var decimalUpDown = await LoadXaml<DecimalUpDown>("""
    <materialDesign:DecimalUpDown Padding="4,5,6,7"
                                  Width="160"
                                  UpDownButtonsVisibility="Collapsed" />
    """);
    var buttonsHost = await decimalUpDown.GetElement<StackPanel>("ButtonsHost");
    var textBox = await decimalUpDown.GetElement<TextBox>("PART_TextBox");

    await Wait.For(async () =>
    {
        await Assert.That(await buttonsHost.RemoteExecute(GetButtonsHostVisibility)).IsEqualTo(Visibility.Collapsed);
        await Assert.That(await buttonsHost.RemoteExecute(GetButtonsHostActualWidth)).IsEqualTo(0d);
        await Assert.That(await textBox.RemoteExecute(GetTextBoxPadding)).IsEqualTo(originalPadding);
    });

    recorder.Success();
}

private static Visibility GetButtonsHostVisibility(StackPanel stackPanel) => stackPanel.Visibility;

private static double GetButtonsHostActualWidth(StackPanel stackPanel) => stackPanel.ActualWidth;

private static Thickness GetTextBoxPadding(TextBox textBox) => textBox.Padding;
```

## Useful local references

- `tests/MaterialDesignThemes.UITests/TestBase.cs`
- `tests/MaterialDesignThemes.UITests/XamlTestExtensions.cs`
- Existing feature tests under `tests/MaterialDesignThemes.UITests/WPF/`
