using System.Threading;

namespace MaterialDesignThemes.Wpf.Internal;

public sealed class VisualStateMonitor
{
    public static VisualStateMonitor? GetMonitor(DependencyObject obj)
        => (VisualStateMonitor?)obj.GetValue(MonitorProperty);

    public static void SetMonitor(DependencyObject obj, VisualStateMonitor? value)
        => obj.SetValue(MonitorProperty, value);

    public static readonly DependencyProperty MonitorProperty =
        DependencyProperty.RegisterAttached("Monitor", typeof(VisualStateMonitor), typeof(VisualStateMonitor), new PropertyMetadata(null));

    public static string? GetCurrentState(DependencyObject obj)
        => (string?)obj.GetValue(CurrentStateProperty);

    public static void SetCurrentState(DependencyObject obj, string? value)
        => obj.SetValue(CurrentStateProperty, value);

    public static readonly DependencyProperty CurrentStateProperty =
        DependencyProperty.RegisterAttached("CurrentState", typeof(string), typeof(VisualStateMonitor), new PropertyMetadata("", OnCurrentStateChanged));

    private static void OnCurrentStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (GetMonitor(d) is { } monitor)
        {
            monitor.StateChanged((string)e.NewValue);
        }
    }

    private string _currentState = "";
    private event EventHandler<string>? CurrentStateChanged;

    public VisualStateMonitor(DependencyObject source) => SetMonitor(source, this);

    public void StateChanged(string state)
    {
        _currentState = state;
        CurrentStateChanged?.Invoke(this, state);
    }

    public Task WaitForState(string state, CancellationToken cancellationToken)
    {
        if (_currentState == state) return Task.CompletedTask;

        TaskCompletionSource<string> tcs = new();

        EventHandler<string> stateChanged = null!;
        stateChanged = (sender, e) =>
        {
            if (e == state)
            {
                CurrentStateChanged -= stateChanged;
                tcs.TrySetResult(state);
            }
        };

        cancellationToken.Register(() =>
        {
            CurrentStateChanged -= stateChanged;
            tcs.TrySetCanceled(cancellationToken);
        });

        CurrentStateChanged += stateChanged;

        if (_currentState == state)
        {
            CurrentStateChanged -= stateChanged;

            return Task.CompletedTask;
        }

        return tcs.Task;
    }
}
