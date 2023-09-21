namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

public class TreeListViewTests : TestBase
{
    public TreeListViewTests(ITestOutputHelper output)
        : base(output)
    {
        AttachedDebugger = true;
    }

    [Fact]
    public async Task TreeListView_WithHierarchicalDataTemplate_CanRemoveTopLevelElement()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        //Act
        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Remove second item
        await removeButton.LeftClick();

        //Assert
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "2");
        
        recorder.Success();
    }

    [Fact]
    public async Task TreeListView_WithHierarchicalDataTemplate_CanRemoveNestedElement()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        //Act
        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select and expand second item
        await AddChildren(secondItem, 3, addButton);

        //Expand it
        await secondItem.LeftClickExpander();

        //Add two child to each of the existing children
        IVisualElement<TreeListViewItem> child1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        await AddChildren(child1, 2, addButton);
        await child1.LeftClickExpander();

        IVisualElement<TreeListViewItem> child2 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[5]");
        await AddChildren(child2, 2, addButton);
        await child2.LeftClickExpander();

        //Remove second item
        await child1.LeftClick();
        await removeButton.LeftClick();

        //Assert
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1");
        await AssertTreeItemContent(treeListView, 2, "1_1");
        await AssertTreeItemContent(treeListView, 3, "1_1_0");
        await AssertTreeItemContent(treeListView, 4, "1_1_1");
        await AssertTreeItemContent(treeListView, 5, "1_2");
        await AssertTreeItemContent(treeListView, 6, "2");

        recorder.Success();
    }

    private static async Task AssertTreeItemContent(IVisualElement<TreeListView> treeListView, int index, string content)
    {
        await Wait.For(async () =>
        {
            IVisualElement<TreeListViewItem> foundFirstItem = await treeListView.GetElement<TreeListViewItem>($"/TreeListViewItem[{index}]");
            Assert.Equal(content, await foundFirstItem.GetContentText());
        });
    }

    private static async Task AddChildren(IVisualElement<TreeListViewItem> item, int numChildren, IVisualElement<Button> addButton)
    {
        await item.LeftClick();
        for(int i = 0; i < numChildren; i++)
        {
            await addButton.LeftClick();
        }
    }
}
