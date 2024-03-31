
namespace MaterialDesignThemes.Wpf.Internal;

public class TreeListViewContentPresenter : ContentPresenter
{
    public event EventHandler<EventArgs>? TemplateChanged;

    public DataTemplate? Template { get; private set; }

    protected override void OnTemplateChanged(DataTemplate oldTemplate, DataTemplate newTemplate)
    {
        Template = newTemplate;
        base.OnTemplateChanged(oldTemplate, newTemplate);
        TemplateChanged?.Invoke(this, EventArgs.Empty);
    }
}
