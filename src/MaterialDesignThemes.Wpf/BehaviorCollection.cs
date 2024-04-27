using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf;

public class BehaviorCollection : FreezableCollection<Behavior>
{
    protected override Freezable CreateInstanceCore() => new BehaviorCollection();
}
