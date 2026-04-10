---
name: mtp-test-running
description: Run MaterialDesignInXamlToolkit tests correctly with dotnet test, TUnit, and Microsoft Testing Platform. Use when validating changes, choosing a safe test scope, or filtering to the exact affected tests.
---

# Test running with MTP

Use this skill when you need to run or suggest test commands in this repository.

## Repository-specific runner setup

1. Test projects use `UseMicrosoftTestingPlatformRunner=true`.
2. Test projects also set `TestingPlatformDotnetTestSupport=true`.
3. `global.json` does **not** opt into `test.runner = Microsoft.Testing.Platform`.
4. That means this repo currently uses `dotnet test` in the legacy CLI mode while delegating execution to MTP-backed test applications.
5. Runner-specific arguments must therefore be passed after an extra `--`.
6. Do **not** rely on top-level `dotnet test --filter ...` for these projects.

## Strong preferences

1. Start with the smallest non-UI test scope that covers the change.
2. Avoid `MaterialDesignThemes.UITests` unless the change is inherently runtime WPF behavior.
3. Only run UI tests after the user explicitly asks or grants permission.
4. When UI tests are necessary, run only the affected tests, never the whole UI suite.

## TUnit filter syntax

TUnit uses `--treenode-filter`.

Format:

`/<Assembly>/<Namespace>/<Class>/<Test>`

- Use `*` as a wildcard within a segment.
- Use `(A)|(B)` inside a segment for OR.
- Prefer exact class and test names over namespace-wide filters.
- Add `--minimum-expected-tests <n>` so an empty selection fails loudly.

## Command patterns

### Targeted non-UI test run

```powershell
dotnet test tests\MaterialDesignThemes.Wpf.Tests\MaterialDesignThemes.Wpf.Tests.csproj -c Release --no-build -- --treenode-filter "/*/*/UpDownButtonsPaddingConverterTests/*" --minimum-expected-tests 1 --no-ansi --no-progress
```

### Exact DecimalUpDown UI validation

```powershell
dotnet test tests\MaterialDesignThemes.UITests\MaterialDesignThemes.UITests.csproj -c Release --no-build -- --treenode-filter "/*/*/DecimalUpDownTests/(UpDownButtonsVisibility_Collapsed_RemovesReservedPadding)|(UpDownButtonsVisibility_Hidden_PreservesReservedPadding)" --minimum-expected-tests 2 --no-ansi --no-progress
```

### Verify a UI filter before running

```powershell
tests\MaterialDesignThemes.UITests\bin\Release\net10.0-windows\MaterialDesignThemes.UITests.exe --list-tests --no-ansi --treenode-filter "/*/*/DecimalUpDownTests/(UpDownButtonsVisibility_Collapsed_RemovesReservedPadding)|(UpDownButtonsVisibility_Hidden_PreservesReservedPadding)"
```

## Mistakes to avoid

- Using top-level `dotnet test --filter ...` for these MTP-backed projects.
- Passing runner-specific flags before the extra `--`.
- Running broad UI suites when one or two targeted tests are enough.
- Forgetting `--minimum-expected-tests`, which can hide a bad filter.

## Useful references

- `global.json`
- `tests/MaterialDesignThemes.Wpf.Tests/MaterialDesignThemes.Wpf.Tests.csproj`
- `tests/MaterialDesignThemes.UITests/MaterialDesignThemes.UITests.csproj`
- https://learn.microsoft.com/dotnet/core/testing/unit-testing-with-dotnet-test
- https://tunit.dev/docs/execution/test-filters
