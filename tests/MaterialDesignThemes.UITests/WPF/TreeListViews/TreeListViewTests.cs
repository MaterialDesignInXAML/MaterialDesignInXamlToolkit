using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.TreeListViews;

public class TreeListViewTests : TestBase
{
    public TreeListViewTests(ITestOutputHelper output)
        : base(output)
    {
        AttachedDebuggerToRemoteProcess = true;
    }

    public static IEnumerable<object[]> GetTestControls()
    {
        yield return new object[] { typeof(TreeListViewDataBinding) };
        yield return new object[] { typeof(TreeListViewImplicitTemplate) };
    }

    [Theory]
    [MemberData(nameof(GetTestControls))]
    public async Task CanResetNestedElements(Type userControlType)
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl(userControlType)).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> resetButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Reset"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item and select second item
        await secondItem.LeftClickExpander();
        await secondItem.LeftClick();
        await Wait.For(() => secondItem.GetIsSelected());

        //Reset children
        await resetButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0_x");
        await AssertTreeItemContent(treeListView, 3, "1_1_x");
        await AssertTreeItemContent(treeListView, 4, "1_2_x");
        await AssertTreeItemContent(treeListView, 5, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveNestedElementDown()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> moveDownButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Move child item
        IVisualElement<TreeListViewItem> childElement;
        await Wait.For(async () =>
        {
            childElement = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
            await childElement.LeftClick();
            bool isSelected = await childElement.GetIsSelected();
            if (!isSelected)
            {
                await Task.Delay(MouseInput.GetDoubleClickTime);
            }
            return isSelected;
        });
        await moveDownButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0");
        await AssertTreeItemContent(treeListView, 3, "1_2");
        await AssertTreeItemContent(treeListView, 4, "1_1");
        await AssertTreeItemContent(treeListView, 5, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveNestedElementUp()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> moveUpButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Up"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Move child item
        IVisualElement<TreeListViewItem> childElement;
        await Wait.For(async () =>
        {
            childElement = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
            await childElement.LeftClick();
            bool isSelected = await childElement.GetIsSelected();
            if (!isSelected)
            {
                await Task.Delay(MouseInput.GetDoubleClickTime);
            }
            return isSelected;
        });
        await moveUpButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_1");
        await AssertTreeItemContent(treeListView, 3, "1_0");
        await AssertTreeItemContent(treeListView, 4, "1_2");
        await AssertTreeItemContent(treeListView, 5, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveNestedElementWithExpandedChildrenDown()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> moveDownButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        // Select child item and add three children and expand it
        IVisualElement<TreeListViewItem>? childElement =  await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
        await AddChildren(childElement!, 3, addButton);

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);
        await childElement!.LeftClickExpander();

        //Move child item
        await moveDownButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0");
        await AssertTreeItemContent(treeListView, 3, "1_2");
        await AssertTreeItemContent(treeListView, 4, "1_1", true);
        await AssertTreeItemContent(treeListView, 5, "1_1_0");  // NOTE: If expansion state is lost when moving, this child and the next two should not be present.
        await AssertTreeItemContent(treeListView, 6, "1_1_1");
        await AssertTreeItemContent(treeListView, 7, "1_1_2");
        await AssertTreeItemContent(treeListView, 8, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveNestedElementWithExpandedChildrenUp()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> moveUpButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Up"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        // Select child item and add three children and expand it
        IVisualElement<TreeListViewItem>? childElement = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
        await AddChildren(childElement!, 3, addButton);
        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);
        await childElement!.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        //Move child item
        await moveUpButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_1", true);
        await AssertTreeItemContent(treeListView, 3, "1_1_0");  // NOTE: If expansion state is lost when moving, this child and the next two should not be present.
        await AssertTreeItemContent(treeListView, 4, "1_1_1");
        await AssertTreeItemContent(treeListView, 5, "1_1_2");
        await AssertTreeItemContent(treeListView, 6, "1_0");
        await AssertTreeItemContent(treeListView, 7, "1_2");
        await AssertTreeItemContent(treeListView, 8, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveTopLevelElementDown()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> moveDownButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        await secondItem.LeftClick();
        await Wait.For(async () =>
        {
            return await secondItem.GetIsSelected();
        });
        //Move item
        await moveDownButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "2");
        await AssertTreeItemContent(treeListView, 2, "1");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveTopLevelElementUp()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> moveUpButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Up"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Move item
        await Wait.For(async () =>
        {
            await secondItem.LeftClick();
            return await secondItem.GetIsSelected();
        });
        await moveUpButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "1");
        await AssertTreeItemContent(treeListView, 1, "0");
        await AssertTreeItemContent(treeListView, 2, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveTopLevelElementWithExpandedChildrenDown()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> moveDownButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Replace item
        await Wait.For(async () =>
        {
            await secondItem.LeftClick();
            return await secondItem.GetIsSelected();
        });
        await moveDownButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "2");
        await AssertTreeItemContent(treeListView, 2, "1", true);
        await AssertTreeItemContent(treeListView, 3, "1_0");    // NOTE: If expansion state is lost when moving, these 3 children should not be present.
        await AssertTreeItemContent(treeListView, 4, "1_1");
        await AssertTreeItemContent(treeListView, 5, "1_2");

        recorder.Success();
    }

    [Fact]
    public async Task CanMoveTopLevelElementWithExpandedChildrenUp()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> moveUpButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Up"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Replace item
        await Wait.For(async () =>
        {
            await secondItem.LeftClick();
            return await secondItem.GetIsSelected();
        });
        await moveUpButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "1", true);
        await AssertTreeItemContent(treeListView, 1, "1_0");    // NOTE: If expansion state is lost when moving, these 3 children should not be present.
        await AssertTreeItemContent(treeListView, 2, "1_1");
        await AssertTreeItemContent(treeListView, 3, "1_2");
        await AssertTreeItemContent(treeListView, 4, "0");
        await AssertTreeItemContent(treeListView, 5, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanReplaceTopLevelElement()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> replaceButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Replace"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Replace item
        await Wait.For(async () =>
        {
            await secondItem.LeftClick();
            return await secondItem.GetIsSelected();
        });
        await replaceButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1_r");
        await AssertTreeItemContent(treeListView, 2, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanReplaceTopLevelElementWithExpandedChildren()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> replaceButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Replace"));
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Replace item
        await replaceButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1_r"); // NOTE: The three children should have been dropped by the replace call.
        await AssertTreeItemContent(treeListView, 2, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanReplaceNestedChildElement()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> replaceButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Replace"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Replace child item
        IVisualElement<TreeListViewItem> childElement;
        await Wait.For(async () =>
        {
            childElement = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
            await childElement.LeftClick();
            return await childElement.GetIsSelected();
        });
        await replaceButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0");
        await AssertTreeItemContent(treeListView, 3, "1_1_r");
        await AssertTreeItemContent(treeListView, 4, "1_2");
        await AssertTreeItemContent(treeListView, 5, "2");

        recorder.Success();
    }

    [Fact]
    public async Task CanReplaceNestedChildElementWithExpandedChildren()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> replaceButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Replace"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        // Select child item and add three children and expand it
        IVisualElement<TreeListViewItem>? childElement = null;
        await Wait.For(async () =>
        {
            childElement = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
            await childElement.LeftClick();
            return await childElement.GetIsSelected();
        });
        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);
        await AddChildren(childElement!, 3, addButton);
        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);
        await Wait.For(async () =>
        {
            await childElement!.LeftClickExpander();
            bool rv = await childElement!.GetIsExpanded();
            if (!rv)
            {
                await Task.Delay(500);
            }
        });

        //Replace child item
        await replaceButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0");
        await AssertTreeItemContent(treeListView, 3, "1_1_r");
        await AssertTreeItemContent(treeListView, 4, "1_2");
        await AssertTreeItemContent(treeListView, 5, "2");

        recorder.Success();
    }

    [Theory]
    [MemberData(nameof(GetTestControls))]
    public async Task WithHierarchicalDataTemplate_CanRemoveTopLevelElement(Type userControlType)
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl(userControlType)).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select second item and add three children
        await AddChildren(secondItem, 3, addButton);

        //Expand item
        await secondItem.LeftClickExpander();

        //Remove second item
        await removeButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "2");
        
        recorder.Success();
    }

    [Theory]
    [MemberData(nameof(GetTestControls))]
    public async Task WithHierarchicalDataTemplate_CanRemoveNestedElement(Type userControlType)
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<Grid> root = (await LoadUserControl(userControlType)).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        //Act
        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select and expand second item
        await AddChildren(secondItem, 3, addButton);

        //Expand it
        await secondItem.LeftClickExpander();

        //Add two children to each of the existing children
        IVisualElement<TreeListViewItem> child1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        await AddChildren(child1, 2, addButton);
        await child1.LeftClickExpander();

        IVisualElement<TreeListViewItem> child2 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[5]");
        await AddChildren(child2, 2, addButton);
        await child2.LeftClickExpander();

        //Remove second item
        await child1.LeftClick();
        await Wait.For(child1.GetIsSelected);
        await removeButton.LeftClick();

        //Assert
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_1", true);
        await AssertTreeItemContent(treeListView, 3, "1_1_0");
        await AssertTreeItemContent(treeListView, 4, "1_1_1");
        await AssertTreeItemContent(treeListView, 5, "1_2");
        await AssertTreeItemContent(treeListView, 6, "2");

        recorder.Success();
    }

    [Fact]
    public async Task DoubleClickOnTreeListViewItem_TogglesExpansion()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select and expand second item
        await AddChildren(secondItem, 3, addButton);

        //NB: Needs to be long enough delay so the first click below does not register as a double click (and the third click as another which effectively collapses the item again)
        await Task.Delay(500);

        //Double click to expand
        await secondItem.LeftClick();
        await secondItem.LeftClick();

        await Wait.For(() => secondItem.GetIsExpanded());

        //NB: Needs to be long enough delay so the next clicks register as a new double click
        await Task.Delay(500);

        //Double click to collapse
        await secondItem.LeftClick();
        await secondItem.LeftClick();

        await Wait.For(async () => await secondItem.GetIsExpanded() == false);

        recorder.Success();
    }

    [Fact]
    public async Task LeftAndRightArrowKeys_CollapseAndExpand()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Select and expand second item
        await AddChildren(secondItem, 3, addButton);

        //Right arrow to expand
        await secondItem.LeftClick();
        await Wait.For(() => secondItem.GetIsSelected());

        await Task.Delay(500);
        await secondItem.SendKeyboardInput($"{Key.Right}");

        await Wait.For(() => secondItem.GetIsExpanded());

        //Add child items to test double left arrow
        IVisualElement<TreeListViewItem> nestedItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
        await AddChildren(nestedItem, 2, addButton);

        await nestedItem.LeftClick();
        await Wait.For(() => nestedItem.GetIsSelected());

        //Left arrow to collapse
        await nestedItem.SendKeyboardInput($"{Key.Left}");
        await Wait.For(async () => await nestedItem.GetIsExpanded() == false);

        //Allow for collapsed animation to complete
        await Task.Delay(500);

        //Left arrow to jump to parent
        await nestedItem.SendKeyboardInput($"{Key.Left}");
        await Wait.For(() => secondItem.GetIsSelected(), message: "Parent item is not selected");

        recorder.Success();
    }

    [Theory]
    [MemberData(nameof(GetTestControls))]
    public async Task AddingChildrenToItemWithAlreadyExpandedChildren_InsertsNewChildAtCorrectIndex(Type userControlType)
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl(userControlType)).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Add child to second item and expand
        await AddChildren(secondItem, 1, addButton);
        await secondItem.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        //Add child to newly added child and expand
        IVisualElement<TreeListViewItem> newChild1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        await AddChildren(newChild1, 1, addButton);
        await newChild1.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        //Select secondItem again, and add another child
        await AddChildren(secondItem, 1, addButton);

        //Assert the child added last is below the child added first (and its children)
        await AssertTreeItemContent(treeListView, 4, "1_1");

        recorder.Success();
    }

    [Theory]
    [MemberData(nameof(GetTestControls))]
    public async Task RemovingChildrenFromItemWithAlreadyExpandedChildren_ShouldDeleteSelectedChild(Type userControlType)
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl(userControlType)).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> removeButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Remove"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Add child to second item and expand
        await AddChildren(secondItem, 3, addButton);
        await secondItem.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        //Add child to newly added child and expand
        IVisualElement<TreeListViewItem> newChild1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        await AddChildren(newChild1, 1, addButton);
        await newChild1.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        // Remove item "1_2"
        IVisualElement<TreeListViewItem> item1_2 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[5]");
        await item1_2.LeftClick();
        await Wait.For(() => item1_2.GetIsSelected());
        await removeButton.LeftClick();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        // Remove item "1_1" (this fails)
        IVisualElement<TreeListViewItem> item1_1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[4]");
        await item1_1.LeftClick();
        await Wait.For(() => item1_1.GetIsSelected());
        await removeButton.LeftClick();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        //Assert the 2 children were successfully removed
        await AssertTreeItemContent(treeListView, 4, "2");

        recorder.Success();
    }

    [Fact]
    public async Task MovingChildItemAfterHavingMovedRootLevelParentItem_ShouldMoveChild()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> downButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> secondItem = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Add children to item "1" item and expand
        await AddChildren(secondItem, 3, addButton);
        await secondItem.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(500);

        //Move parent item down
        await downButton.LeftClick();

        //NB: Wait for the move to take effect
        await Task.Delay(1000);

        //Move child item
        IVisualElement<TreeListViewItem> secondChild = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[4]");
        await secondChild.LeftClick();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        await downButton.LeftClick();

        //NB: Wait for the move to take effect
        await Task.Delay(1000);

        //Assert the child was successfully moved
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "2");
        await AssertTreeItemContent(treeListView, 2, "1", true);
        await AssertTreeItemContent(treeListView, 3, "1_0");
        await AssertTreeItemContent(treeListView, 4, "1_2");
        await AssertTreeItemContent(treeListView, 5, "1_1");

        recorder.Success();
    }

    [Fact]
    public async Task MovingChildItemOfNestedItemAfterHavingMovedNestedItem_ShouldMoveChild()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> downButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> item1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Add children to item "1" and expand
        await AddChildren(item1, 3, addButton);
        await item1.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        // Add children to item "1_1" and expand
        IVisualElement<TreeListViewItem> item11 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
        await AddChildren(item11, 3, addButton);
        await item11.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        //Move parent item down
        await downButton.LeftClick();

        await Task.Delay(1000);
        //Move child item
        IVisualElement<TreeListViewItem> item111 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[6]");
        await item111.LeftClick();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        await downButton.LeftClick();

        //NB: Wait for the move to take effect
        await Task.Delay(1000);

        //Assert the child was successfully moved
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0");
        await AssertTreeItemContent(treeListView, 3, "1_2");
        await AssertTreeItemContent(treeListView, 4, "1_1", true);
        await AssertTreeItemContent(treeListView, 5, "1_1_0");
        await AssertTreeItemContent(treeListView, 6, "1_1_2");
        await AssertTreeItemContent(treeListView, 7, "1_1_1");
        await AssertTreeItemContent(treeListView, 8, "2");

        recorder.Success();
    }

    [Fact]
    public async Task TopLevelItemWithNestedExpandedChild_MovesChildrenMaintainingExpansion()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> upButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Up"));
        IVisualElement<Button> downButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> item1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Add children to item "1" and expand
        await AddChildren(item1, 3, addButton);
        await item1.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        // Add children to item "1_1" and expand
        IVisualElement<TreeListViewItem> item11 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");
        await AddChildren(item11, 3, addButton);
        await item11.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        //Move parent item down
        await item1.LeftClick();
        await upButton.LeftClick();

        await Task.Delay(1000);

        await downButton.LeftClick();
        await Task.Delay(1000);

        //Assert the child was successfully moved
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1", true);
        await AssertTreeItemContent(treeListView, 2, "1_0");
        await AssertTreeItemContent(treeListView, 3, "1_1", true);
        await AssertTreeItemContent(treeListView, 4, "1_1_0");
        await AssertTreeItemContent(treeListView, 5, "1_1_1");
        await AssertTreeItemContent(treeListView, 6, "1_1_2");
        await AssertTreeItemContent(treeListView, 7, "1_2");
        await AssertTreeItemContent(treeListView, 8, "2");

        recorder.Success();
    }

    [Fact]
    public async Task TopLevelItemWhichHasBeenExpandedAndCollapsed_MovesAndMaintainsCollapsedState()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add"));
        IVisualElement<Button> downButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Down"));

        IVisualElement<TreeListViewItem> item1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[1]");

        //Add children to item "1" and expand
        await AddChildren(item1, 3, addButton);
        await item1.LeftClickExpander();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        // Collapse item "1" again
        await item1.LeftClickExpander(false);

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        // Move down
        await downButton.LeftClick();

        //NB: Needs to be long enough delay so the next click does not register as a double click
        await Task.Delay(1000);

        // FIXME: At this point, the expansion state is wrong. The chevron indicates expanded, but in fact it is collapsed. Expanding it then crashes with an ArgumentOutOfRangeException

        // NOTE: This may not be needed, I am not entirely sure.
        item1 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        
        // Expand item "1"
        await item1.LeftClickExpander();

        //Assert the child was successfully moved
        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "2");
        await AssertTreeItemContent(treeListView, 2, "1", true);
        await AssertTreeItemContent(treeListView, 3, "1_0");
        await AssertTreeItemContent(treeListView, 4, "1_1");
        await AssertTreeItemContent(treeListView, 5, "1_2");

        recorder.Success();
    }

    [Fact]
    public async Task TreeListView_WithTemplateSelector_UsesSelectorTemplates()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<TreeListView> treeListView = (await LoadUserControl<TreeListViewTemplateSelector>()).As<TreeListView>();

        IVisualElement<TreeListViewItem> item3 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        IVisualElement<TreeListViewItem> item4 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");

        await item3.LeftClickExpander();
        await Task.Delay(500);
        await item4.LeftClickExpander();
        await Task.Delay(500);

        await AssertTreeItemContent(treeListView, 0, "Foo", Colors.Blue);
        await AssertTreeItemContent(treeListView, 1, "42", Colors.Red);
        await AssertTreeItemContent(treeListView, 2, "24", Colors.Red, true);
        await AssertTreeItemContent(treeListView, 3, "a", Colors.Blue);
        await AssertTreeItemContent(treeListView, 4, "b", Colors.Blue);
        await AssertTreeItemContent(treeListView, 5, "c", Colors.Blue);
        await AssertTreeItemContent(treeListView, 6, "Bar", Colors.Blue, true);
        await AssertTreeItemContent(treeListView, 7, "1", Colors.Red);
        await AssertTreeItemContent(treeListView, 8, "2", Colors.Red);
        await AssertTreeItemContent(treeListView, 9, "3", Colors.Red);

        recorder.Success();
    }

    [Fact]
    public async Task TreeListView_WithCollectionView_RendersItems()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<TreeListView> treeListView = (await LoadUserControl<TreeListViewWithCollectionView>()).As<TreeListView>();

        IVisualElement<TreeListViewItem> item3 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[2]");
        IVisualElement<TreeListViewItem> item4 = await treeListView.GetElement<TreeListViewItem>("/TreeListViewItem[3]");

        await item3.LeftClickExpander();
        await Task.Delay(500);
        await item4.LeftClickExpander();
        await Task.Delay(500);

        await AssertTreeItemContent(treeListView, 0, "Foo");
        await AssertTreeItemContent(treeListView, 1, "42");
        await AssertTreeItemContent(treeListView, 2, "24", true);
        await AssertTreeItemContent(treeListView, 3, "a");
        await AssertTreeItemContent(treeListView, 4, "b");
        await AssertTreeItemContent(treeListView, 5, "c");
        await AssertTreeItemContent(treeListView, 6, "Bar", true);
        await AssertTreeItemContent(treeListView, 7, "1");
        await AssertTreeItemContent(treeListView, 8, "2");
        await AssertTreeItemContent(treeListView, 9, "3");

        recorder.Success();
    }

    [Fact]
    public async Task TreeListView_AddingExpandedItemWithChildren_ShowsExpandedItem()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> root = (await LoadUserControl<TreeListViewDataBinding>()).As<Grid>();
        IVisualElement<TreeListView> treeListView = await root.GetElement<TreeListView>();
        IVisualElement<Button> addWithChildrenButton = await root.GetElement(ElementQuery.PropertyExpression<Button>(x => x.Content, "Add with Children"));

        await addWithChildrenButton.LeftClick();

        await AssertTreeItemContent(treeListView, 0, "0");
        await AssertTreeItemContent(treeListView, 1, "1");
        await AssertTreeItemContent(treeListView, 2, "2");
        await AssertTreeItemContent(treeListView, 3, "3", true);
        await AssertTreeItemContent(treeListView, 4, "3_0");
        await AssertTreeItemContent(treeListView, 5, "3_1");
        await AssertTreeItemContent(treeListView, 6, "3_2");

        recorder.Success();
    }

    private static async Task AssertTreeItemContent(IVisualElement<TreeListView> treeListView, int index, string content, bool isExpanded = false)
    {
        await Wait.For(async () =>
        {
            IVisualElement<TreeListViewItem> treeItem = await treeListView.GetElement<TreeListViewItem>($"/TreeListViewItem[{index}]");
            Assert.Equal(content, await treeItem.GetContentText());
            Assert.Equal(isExpanded, await treeItem.GetIsExpanded());
        });
    }

    private static async Task AssertTreeItemContent(
        IVisualElement<TreeListView> treeListView,
        int index,
        string content,
        Color foreground,
        bool isExpanded = false)
    {
        await Wait.For(async () =>
        {
            IVisualElement<TreeListViewItem> treeItem = await treeListView.GetElement<TreeListViewItem>($"/TreeListViewItem[{index}]");
            Assert.Equal(content, await treeItem.GetContentText());
            Assert.Equal(isExpanded, await treeItem.GetIsExpanded());
            IVisualElement<TextBlock> textBlock = await treeItem.GetElement<TextBlock>();
            Assert.Equal(foreground, await textBlock.GetForegroundColor());
        });
    }

    private static async Task AddChildren(IVisualElement<TreeListViewItem> item, int numChildren, IVisualElement<Button> addButton)
    {
        await item.LeftClick();
        await Wait.For(item.GetIsSelected);
        for (int i = 0; i < numChildren; i++)
        {
            await addButton.LeftClick();
        }
    }
}
