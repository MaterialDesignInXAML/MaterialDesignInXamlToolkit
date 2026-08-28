using System.Threading;

namespace MaterialDesignThemes.Wpf.Internal;

internal sealed class VisualStateMonitor(VisualStateGroup visualStateGroup)
{
    private readonly VisualStateGroup _visualStateGroup = visualStateGroup ??
            throw new ArgumentNullException(nameof(visualStateGroup));

    public Task WaitForState(string state, CancellationToken cancellationToken)
    {
        string currentState = _visualStateGroup.CurrentState.Name;
        if (currentState == state) return Task.CompletedTask;

        TaskCompletionSource<string> tcs = new();

        EventHandler<VisualStateChangedEventArgs> stateChanged = null!;
        stateChanged = (sender, e) =>
        {
            if (e.NewState.Name == state)
            {
                _visualStateGroup.CurrentStateChanged -= stateChanged;
                tcs.TrySetResult(state);
            }
        };

        cancellationToken.Register(() =>
        {
            _visualStateGroup.CurrentStateChanged -= stateChanged;
            tcs.TrySetCanceled(cancellationToken);
        });

        _visualStateGroup.CurrentStateChanged += stateChanged;

        currentState = _visualStateGroup.CurrentState.Name;
        if (currentState == state)
        {
            _visualStateGroup.CurrentStateChanged -= stateChanged;

            return Task.CompletedTask;
        }

        return tcs.Task;
    }
}
