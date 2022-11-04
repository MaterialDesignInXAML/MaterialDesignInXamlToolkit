using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf;

public class StylizedBehaviorCollection : FreezableCollection<Behavior>
{
    protected override Freezable CreateInstanceCore() => new StylizedBehaviorCollection();
}
