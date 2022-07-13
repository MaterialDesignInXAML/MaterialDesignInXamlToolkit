using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.TreeViews;

public class TreeViewTests : TestBase
{
    public TreeViewTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
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
        Assert.Equal(Visibility.Hidden, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item2");
        Assert.Equal(Visibility.Hidden, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item3");
        Assert.Equal(Visibility.Collapsed, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item4");
        Assert.Equal(Visibility.Visible, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item5");
        Assert.Equal(Visibility.Visible, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item6");
        Assert.Equal(Visibility.Visible, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item7");
        Assert.Equal(Visibility.Visible, await expander.GetVisibility());

        expander = await GetExpanderForHeader("Item8");
        Assert.Equal(Visibility.Visible, await expander.GetVisibility());

        recorder.Success();

        async Task<IVisualElement<ToggleButton>> GetExpanderForHeader(string header)
        {
            var item = await treeView!.GetElement(ElementQuery.PropertyExpression<TreeViewItem>(x => x.Header, header));
            return await item.GetElement<ToggleButton>();
        }
    }

    [Theory]
    [Description("Issue 2618")]
    [InlineData(Visibility.Hidden)]
    [InlineData(Visibility.Collapsed)]
    [InlineData(Visibility.Visible)]
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
        Assert.Equal(visibility, await expander.GetVisibility());

        expander = await GetExpanderForHeader("HasChild");
        Assert.Equal(Visibility.Visible, await expander.GetVisibility());

        recorder.Success();

        async Task<IVisualElement<ToggleButton>> GetExpanderForHeader(string header)
        {
            var item = await treeView!.GetElement(ElementQuery.PropertyExpression<TreeViewItem>(x => x.Header, header));
            return await item.GetElement<ToggleButton>();
        }
    }
}
