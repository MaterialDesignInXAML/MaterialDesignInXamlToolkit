using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// A control that implement placeholder behavior. Can work as a simple placeholder either as a floating hint, see <see cref="UseFloating"/> property.
/// <para/>
/// To set a target control you should set the HintProxy property. Use the <see cref="HintProxyFabricConverter.Instance"/> converter which converts a control into the IHintProxy interface.
/// </summary>
[TemplateVisualState(GroupName = ContentStatesGroupName, Name = HintRestingPositionName)]
[TemplateVisualState(GroupName = ContentStatesGroupName, Name = HintFloatingPositionName)]
public class SmartHintNew : SmartHint
{
    static SmartHintNew()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SmartHintNew), new FrameworkPropertyMetadata(typeof(SmartHintNew)));
    }
}
