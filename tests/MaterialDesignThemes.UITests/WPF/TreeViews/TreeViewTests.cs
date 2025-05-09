using System.ComponentModel;


namespace MaterialDesignThemes.UITests.WPF.TreeViews;

public class TreeViewTests : TestBase
{
    [Test]
    [Description("Issue 2618")]
    public async Task HasNoItemsExpanderVisibility_ChangesVisibilityOnExpander()
    {
        await using var recorder = new TestRecorder(App);

        var treeView = await LoadXaml<TreeView>(@"
<TreeView>
    <TreeViewItem Header=""Item1"" />
    <TreeViewItem Header=""Item2"" materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""Hidden"" />
    <TreeViewItem Header=""Item3"" materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""Collapsed"" />
    <TreeViewItem Header=""Item4"" materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""Visible"" />

    <TreeViewItem Header=""Item5"">
        <TreeViewItem Header=""Child"" />
    </TreeViewItem>
    <TreeViewItem Header=""Item6"" materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""Hidden"">
        <TreeViewItem Header=""Child"" />
    </TreeViewItem>
    <TreeViewItem Header=""Item7"" materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""Collapsed"">
        <TreeViewItem Header=""Child"" />
    </TreeViewItem>
    <TreeViewItem Header=""Item8"" materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""Visible"">
        <TreeViewItem Header=""Child"" />
    </TreeViewItem>
</TreeView>");

        var expander = await GetExpanderForHeader("Item1");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Hidden);

        expander = await GetExpanderForHeader("Item2");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Hidden);

        expander = await GetExpanderForHeader("Item3");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Collapsed);

        expander = await GetExpanderForHeader("Item4");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Visible);

        expander = await GetExpanderForHeader("Item5");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Visible);

        expander = await GetExpanderForHeader("Item6");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Visible);

        expander = await GetExpanderForHeader("Item7");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Visible);

        expander = await GetExpanderForHeader("Item8");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Visible);

        recorder.Success();

        async Task<IVisualElement<ToggleButton>> GetExpanderForHeader(string header)
        {
            var item = await treeView!.GetElement(ElementQuery.PropertyExpression<TreeViewItem>(x => x.Header, header));
            return await item.GetElement<ToggleButton>();
        }
    }

    [Test]
    [Description("Issue 2618")]
    [Arguments(Visibility.Hidden)]
    [Arguments(Visibility.Collapsed)]
    [Arguments(Visibility.Visible)]
    public async Task HasNoItemsExpanderVisibility_SetOnTreeView_ChangesVisibilityOnExpanders(Visibility visibility)
    {
        await using var recorder = new TestRecorder(App);

        var treeView = await LoadXaml<TreeView>($@"
<TreeView materialDesign:TreeViewAssist.HasNoItemsExpanderVisibility=""{visibility}"">
    <TreeViewItem Header=""NoChild"" />
    
    <TreeViewItem Header=""HasChild"">
        <TreeViewItem Header=""Child"" />
    </TreeViewItem>
</TreeView>");

        var expander = await GetExpanderForHeader("NoChild");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(visibility);

        expander = await GetExpanderForHeader("HasChild");
        await Assert.That(await expander.GetVisibility()).IsEqualTo(Visibility.Visible);

        recorder.Success();

        async Task<IVisualElement<ToggleButton>> GetExpanderForHeader(string header)
        {
            var item = await treeView!.GetElement(ElementQuery.PropertyExpression<TreeViewItem>(x => x.Header, header));
            return await item.GetElement<ToggleButton>();
        }
    }
}
