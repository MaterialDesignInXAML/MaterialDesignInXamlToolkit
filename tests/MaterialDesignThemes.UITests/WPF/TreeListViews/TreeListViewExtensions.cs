namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

public static class TreeListViewExtensions
{
    public static async Task LeftClickExpander(this IVisualElement<TreeListViewItem> item, bool expectedExpansionState = true)
    {
        IVisualElement<ToggleButton> expanderButton = await item.GetElement<ToggleButton>("Expander");
        await expanderButton.LeftClick();
        await Wait.For(async () => await item.GetIsExpanded() == expectedExpansionState);
    }

    public static async Task<string?> GetContentText(this IVisualElement<TreeListViewItem> item)
    {
        IVisualElement<TextBlock> content = await item.GetElement<TextBlock>();
        return await content.GetText();
    }
}
