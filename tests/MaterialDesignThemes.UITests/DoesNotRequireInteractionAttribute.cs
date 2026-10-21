using TUnit.Core.Interfaces;

[assembly: NotInParallel("Default")]

namespace MaterialDesignThemes.UITests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class DoesNotRequireInteractionAttribute : Attribute, ITestDiscoveryEventReceiver
{
    public ValueTask OnTestDiscovered(DiscoveredTestContext context)
    {
        context.AddParallelConstraint(new ParallelGroupConstraint("NoInteraction", 0));
        context.AddParallelConstraint(new NotInParallelConstraint([Guid.NewGuid().ToString()]));
        return default;
    }
}
