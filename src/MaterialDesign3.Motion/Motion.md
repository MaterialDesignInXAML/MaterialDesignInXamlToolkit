# Motion in Material Design 3

Contains code ported from the following **AndroidX** libraries to support motion in Material Design 3:
- **Compose Animation** library (`androidx.compose.animation.core`)
- **Compose Material3** library (`androidx.compose.material3`)

For example:
* Animation APIs such as [animateDpAsState], [animateDecay], and [animateRect].
* Infinite animation APIs such as [infiniteRepeatable], and [rememberInfiniteTransition].
* Animation spec APIs such as [tween], [spring], [snap], and [keyframes].
* Easing APIs such as [FastOutSlowInEasing], and [CubicBezierEasing].

For more details, see the original source code at:
- [androidx/compose/material3](https://cs.android.com/androidx/platform/frameworks/support/+/androidx-main:compose/material3/material3/src/commonMain/kotlin/androidx/compose/material3/)
- [androidx/compose/animation/core](https://cs.android.com/androidx/platform/frameworks/support/+/androidx-main:compose/animation/animation-core/src/commonMain/kotlin/androidx/compose/animation/core/)

## Using Material Motion Primitives in WPF

The `MaterialDesignThemes.Motion` project does not ship a drop-in `AnimationTimeline`; instead, it exposes the pieces you need to build WPF-friendly motion:
- `MotionSchemeContext` and `MotionSchemes` give you the Material 3 spring presets (`SpringMotionSpec`) for spatial and effects motion.
- `MotionTokens` maps to the duration and easing tokens that you can feed into WPF `Storyboard`-based tweens.
- `AnimationParameters`, `Repeatable`, and `Easing` capture how long to run, which easing curve to apply, and how to repeat an animation.
- `SpringSimulation`, `SpringConstants`, and `SpringEstimation` let you drive a per-frame spring animation when WPF’s built-in easing functions are not sufficient.

### Example: Spring-driven translation on `TranslateTransform.X`

```csharp
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Motion;

public partial class SpringSampleControl : UserControl, IDisposable
{
    private readonly SpringAnimator _animator;

    public SpringSampleControl()
    {
        InitializeComponent();
        var spec = MotionSchemeContext.Current.RememberDefaultSpatialSpec();
        _animator = new SpringAnimator(
            apply: value => Translate.X = value,
            springSpec: spec);
        Loaded += (_, _) => _animator.Start(from: -120, to: 0);
        Unloaded += (_, _) => Dispose();
    }

    public void Dispose() => _animator.Dispose();

    public TranslateTransform Translate { get; } = new();

    private sealed class SpringAnimator : IDisposable
    {
        private readonly SpringSimulation _simulation;
        private readonly Action<double> _apply;
        private readonly Stopwatch _stopwatch = new();
        private double _value;
        private double _velocity;
        private bool _isRunning;

        public SpringAnimator(Action<double> apply, SpringMotionSpec springSpec)
        {
            _apply = apply;
            _simulation = new SpringSimulation(finalPosition: 0f)
            {
                Stiffness = (float)springSpec.Stiffness,
                DampingRatio = (float)springSpec.DampingRatio,
            };
        }

        public void Start(double from, double to)
        {
            _simulation.FinalPosition = (float)to;
            _value = from;
            _velocity = 0;
            _stopwatch.Restart();
            if (_isRunning)
            {
                return;
            }

            CompositionTarget.Rendering += OnRendering;
            _isRunning = true;
        }

        private void OnRendering(object? sender, EventArgs e)
        {
            var elapsed = _stopwatch.Elapsed;
            _stopwatch.Restart();
            var next = _simulation.UpdateValues((float)_value, (float)_velocity, elapsed);
            _value = next.Value;
            _velocity = next.Velocity;
            _apply(_value);

            if (Math.Abs(_value - _simulation.FinalPosition) < 0.5 &&
                Math.Abs(_velocity) < 0.5)
            {
                Stop();
            }
        }

        private void Stop()
        {
            if (!_isRunning)
            {
                return;
            }

            CompositionTarget.Rendering -= OnRendering;
            _isRunning = false;
        }

        public void Dispose() => Stop();
    }
}
```

Key takeaways for adapting the primitives to WPF:
- Use `SpringMotionSpec` from a scheme to configure stiffness and damping ratios that align with the Material 3 design guidance.
- `SpringSimulation.UpdateValues` returns both the new position and velocity, letting you decide when the animation has converged.
- When you want more traditional tweens, reuse the durations (`MotionTokens.DurationMedium2`, etc.) and cubic bezier control points (`MotionTokens.EasingEmphasizedCubicBezier`) inside standard `DoubleAnimation` / `SplineDoubleKeyFrame` definitions.

## See also

- [Animation in WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/animation-overview)
- [Android Animator](https://developer.android.com/reference/kotlin/android/animation/Animator)
