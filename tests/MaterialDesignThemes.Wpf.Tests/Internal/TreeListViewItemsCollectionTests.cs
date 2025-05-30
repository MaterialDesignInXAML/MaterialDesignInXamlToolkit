using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using MaterialDesignThemes.Wpf.Internal;

namespace MaterialDesignThemes.Wpf.Tests.Internal;

public class TreeListViewItemsCollectionTests
{
    [Test]
    public async Task Constructor_AcceptsNull()
    {
        TreeListViewItemsCollection collection = new(null);

        await Assert.That(collection).IsEmpty();
    }

    [Test]
    public async Task Constructor_AcceptsObject()
    {
        TreeListViewItemsCollection collection = new(new object());

        await Assert.That(collection).IsEmpty();
    }

    [Test]
    public async Task Constructor_AcceptsIEnumerable()
    {
        IEnumerable<string> enumerable = ["a", "b", "c"];
        TreeListViewItemsCollection collection = new(enumerable);

        await Assert.That(collection).IsEquivalentTo(new[] { "a", "b", "c" });
    }

    [Test]
    public async Task WhenWrappedObjectImplementsIncc_ItHandlesAdditions()
    {
        //Arrange
        ObservableCollection<string> collection = [];

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.Add("a");

        //Assert
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(new[] { "a" });
    }

    [Test]
    public async Task WhenWrappedObjectImplementsIncc_ItHandlesRemovals()
    {
        //Arrange
        ObservableCollection<string> collection = new() { "a" };

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.Remove("a");

        //Assert
        await Assert.That(treeListViewItemsCollection).IsEmpty();
    }

    [Test]
    public async Task WhenWrappedObjectImplementsIncc_ItHandlesReplacements()
    {
        //Arrange
        ObservableCollection<string> collection = new() { "a", "b", "c" };

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        // Simulate expansion
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "b_a", 1);
        treeListViewItemsCollection.InsertWithLevel(6, "c_a", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         * 4.  b_a
         * 5. c
         * 6.  c_a
         */

        //Act
        collection[1] = "x";    // Replace b (and its children) with x (which does not have children); collection only knows about root level items, so index (1) reflects that

        //Assert
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(new[] { "a", "a_a", "a_b", "x", "c", "c_a" });
    }

    [Test]
    public async Task WhenWrappedObjectImplementsIncc_ItHandlesReset()
    {
        //Arrange
        TestableCollection<string> collection = new() { "a" };

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.ReplaceAllItems("b", "c");

        //Assert
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(new[] { "c", "b" });
    }

    [Test]
    public async Task WhenWrappedObjectImplementsIncc_ItHandlesMoves()
    {
        //Arrange
        ObservableCollection<string> collection = ["a", "b", "c"];

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.Move(0, 2);

        //Assert
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(new[] { "b", "c", "a" });
    }

    [Test]
    [Arguments(0, 0)] //x is a sibling of a
    [Arguments(1, 1)] //x nested under a
    [Arguments(2, 1)] //x is a sibling of a_a
    [Arguments(2, 2)] //x is a child of a_a
    [Arguments(3, 1)] //x is a sibling of a_b
    [Arguments(3, 2)] //x is a child of a_b
    [Arguments(4, 1)] //x is a child of b
    [Arguments(4, 0)] //x is a sibling of b
    public async Task WhenAddingItemAtNestedLevel_ItSetsTheItemsLevel(int insertionIndex, int requestedLevel)
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         */

        //Act
        treeListViewItemsCollection.InsertWithLevel(insertionIndex, "x", requestedLevel);

        //Assert
        List<string> expectedItems = new(["a", "a_a", "a_b", "b"]);
        expectedItems.Insert(insertionIndex, "x");
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);

        List<int> expectedLevels = new([ 0, 1, 1, 0 ]);
        expectedLevels.Insert(insertionIndex, requestedLevel);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    [Arguments(0, 1)]
    [Arguments(1, 2)]
    [Arguments(2, 4)]
    [Arguments(2, 0)]
    [Arguments(0, -1)]
    [Arguments(4, 2)]
    public async Task InsertWithLevel_WhenAddingItemAtNestedLevel_ItThrowsIfRequestIsOutOfRange(int insertionIndex, int requestedLevel)
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         */

        //Act/Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => treeListViewItemsCollection.InsertWithLevel(insertionIndex, "x", requestedLevel));
        await Task.CompletedTask;
    }

    [Test]
    public async Task InsertWithLevel_WhenInsertingFirstSibling_MarksIndexAsExpanded()
    {
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b" });

        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2. b
         */

        await Assert.That(treeListViewItemsCollection.GetAllIsExpanded()).IsEqualTo(new[] { true, false, false });
    }

    [Test]
    [Arguments(0, new[] { "b", "b_a", "c" }, new[] { 0, 1, 0})]
    [Arguments(1, new[] { "a", "a_a", "a_a_a", "a_a_b", "a_b", "c" }, new[] { 0, 1, 2, 2, 1, 0 })]
    [Arguments(2, new[] { "a", "a_a", "a_a_a", "a_a_b", "a_b", "b", "b_a" }, new[] { 0, 1, 2, 2, 1, 0, 1 })]
    public async Task WhenRemovingItem_ItRemovesItemsAndAnyChildren(int indexToRemove, string[] expectedItems, int[] expectedLevels)
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b", "c" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_a_a", 2);
        treeListViewItemsCollection.InsertWithLevel(3, "a_a_b", 2);
        treeListViewItemsCollection.InsertWithLevel(4, "a_b", 1);
        treeListViewItemsCollection.InsertWithLevel(6, "b_a", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.   a_a_a
         * 3.   a_a_b
         * 4.  a_b
         * 5. b
         * 6.  b_a
         * 7. c
         */

        //Act
        treeListViewItemsCollection.RemoveAt(indexToRemove); // RemoveAt() only knows about root level items, so indices in input should reflect that

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    public async Task Move_WhenMovingItemUp_ItMovesChildrenAlongWithIt()
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b", "c" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "b_a", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "b_b", 1);
        treeListViewItemsCollection.InsertWithLevel(6, "b_c", 1);
        treeListViewItemsCollection.InsertWithLevel(8, "c_a", 1);
        treeListViewItemsCollection.InsertWithLevel(9, "c_b", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         * 4.  b_a
         * 5.  b_b
         * 6.  b_c
         * 7. c
         * 8   c_a
         * 9.  c_b
         */

        //Act
        treeListViewItemsCollection.Move(3, 0); //Swap b and a;

        //Assert
        List<string> expectedItems = new(["b", "b_a", "b_b", "b_c", "a", "a_a", "a_b", "c", "c_a", "c_b"]);
        List<int> expectedLevels = new([0, 1, 1, 1, 0, 1, 1, 0, 1, 1]);

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    public async Task Move_WhenMovingItemUpMultipleLevels_ItMovesChildrenAlongWithIt()
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b", "c" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "b_a", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "b_b", 1);
        treeListViewItemsCollection.InsertWithLevel(6, "b_c", 1);
        treeListViewItemsCollection.InsertWithLevel(8, "c_a", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         * 4.  b_a
         * 5.  b_b
         * 6.  b_c
         * 7. c
         * 8   c_a
         */

        //Act
        treeListViewItemsCollection.Move(7, 0); // Move c to a's position; Move() only knows about root level items, so indices reflect that

        //Assert
        List<string> expectedItems = new(["c", "c_a", "a", "a_a", "a_b", "b", "b_a", "b_b", "b_c"]);
        List<int> expectedLevels = new([0, 1, 0, 1, 1, 0, 1, 1, 1]);

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    public async Task Move_WhenMovingItemDown_ItMovesChildrenAlongWithIt()
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b", "c" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "b_a", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "b_b", 1);
        treeListViewItemsCollection.InsertWithLevel(6, "b_c", 1);
        treeListViewItemsCollection.InsertWithLevel(8, "c_a", 1);
        treeListViewItemsCollection.InsertWithLevel(9, "c_b", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         * 4.  b_a
         * 5.  b_b
         * 6.  b_c
         * 7. c
         * 8   c_a
         * 9.  c_b
         */

        //Act
        treeListViewItemsCollection.Move(3, 7); // Swap b and c;

        //Assert
        string[] expectedItems = ["a", "a_a", "a_b", "c", "c_a", "c_b", "b", "b_a", "b_b", "b_c"];
        int[] expectedLevels = [0, 1, 1, 0, 1, 1, 0, 1, 1, 1];

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    public async Task Move_WhenMovingItemDownMultipleLevels_ItMovesChildrenAlongWithIt()
    {
        //Arrange
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b", "c" });
        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "a_b", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "b_a", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "b_b", 1);
        treeListViewItemsCollection.InsertWithLevel(6, "b_c", 1);
        treeListViewItemsCollection.InsertWithLevel(8, "c_a", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2.  a_b
         * 3. b
         * 4.  b_a
         * 5.  b_b
         * 6.  b_c
         * 7. c
         * 8   c_a
         */

        //Act
        treeListViewItemsCollection.Move(0, 7); // Move a to c's position; Move() only knows about root level items, so indices reflect that

        //Assert
        List<string> expectedItems = new(["b", "b_a", "b_b", "b_c", "c", "c_a", "a", "a_a", "a_b"]);
        List<int> expectedLevels = new([0, 1, 1, 1, 0, 1, 0, 1, 1]);

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    public async Task Move_WithWrappedCollection_ItMovesChildrenAlongWithIt()
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "a", "b", "c" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        treeListViewItemsCollection.InsertWithLevel(2, "b_a", 1);
        treeListViewItemsCollection.InsertWithLevel(3, "b_b", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "b_c", 1);
        /*
         * 0. a
         * 1. b
         * 2.  b_a
         * 3.  b_b
         * 4.  b_c
         * 5. c
         */

        //Act
        boundCollection.Move(1, 2); // Move b to c's position;

        //Assert
        List<string> expectedItems = new(["a", "c", "b", "b_a", "b_b", "b_c"]);
        List<int> expectedLevels = new([0, 0, 0, 1, 1, 1]);

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
    }

    [Test]
    public async Task Move_WithExpandedChild_ItMovesChildrenUp()
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        treeListViewItemsCollection.InsertWithLevel(2, "1_0", 1);
        treeListViewItemsCollection.InsertWithLevel(3, "1_1", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "1_1_0", 2);
        treeListViewItemsCollection.InsertWithLevel(5, "1_1_1", 2);
        treeListViewItemsCollection.InsertWithLevel(6, "1_1_2", 2);
        treeListViewItemsCollection.InsertWithLevel(7, "1_2", 1);
        /*
         * 0. 0
         * 1. 1
         * 2.  1_0
         * 3.  1_1
         * 4.    1_1_0
         * 5.    1_1_1
         * 6.    1_1_2
         * 7.  1_2
         * 8. 2
         */

        //Act
        boundCollection.Move(1, 0); // Move 1 to 0's position;

        //Assert
        List<string> expectedItems = 
        [
            "1",
            "1_0",
            "1_1",
            "1_1_0",
            "1_1_1",
            "1_1_2",
            "1_2",
            "0",
            "2",
        ];
        List<int> expectedLevels = [0, 1, 1, 2, 2, 2, 1, 0, 0];
        List<bool> expectedExpanded =
        [
            true,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
        ];
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
        await Assert.That(treeListViewItemsCollection.GetAllIsExpanded()).IsEqualTo(expectedExpanded);
    }

    [Test]
    public async Task Move_WithExpandedChild_ItMovesChildrenDown()
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        treeListViewItemsCollection.InsertWithLevel(1, "0_0", 1);
        treeListViewItemsCollection.InsertWithLevel(2, "0_1", 1);
        treeListViewItemsCollection.InsertWithLevel(3, "0_1_0", 2);
        treeListViewItemsCollection.InsertWithLevel(4, "0_1_1", 2);
        treeListViewItemsCollection.InsertWithLevel(5, "0_1_2", 2);
        treeListViewItemsCollection.InsertWithLevel(6, "0_2", 1);
        /*
         * 0. 0
         * 1.  0_0
         * 2.  0_1
         * 3.    0_1_0
         * 4.    0_1_1
         * 5.    0_1_2
         * 6.  0_2
         * 7. 1
         * 8. 2
         */

        //Act
        boundCollection.Move(0, 1); // Move 0 to 1's position;

        //Assert
        List<string> expectedItems = 
        [
            "1",
            "0",
            "0_0",
            "0_1",
            "0_1_0",
            "0_1_1",
            "0_1_2",
            "0_2",
            "2",
        ];
        List<int> expectedLevels = [ 0, 0, 1, 1, 2, 2, 2, 1, 0 ];
        List<bool> expectedExpanded =
        [
            false,
            true,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
        ];
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
        await Assert.That(treeListViewItemsCollection.GetAllIsExpanded()).IsEqualTo(expectedExpanded);
    }

    [Test]
    public async Task Replace_WithExpandedChild_ItRemovesChildren()
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        treeListViewItemsCollection.InsertWithLevel(3, "2_0", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "2_1", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "2_2", 1);
        /*
         * 0. 0
         * 1. 1
         * 2. 2
         * 3.   2_0
         * 4.   2_1
         * 5.   2_2
         */

        //Act
        boundCollection[2] = "replaced";

        //Assert
        List<string> expectedItems =
        [
            "0",
            "1",
            "replaced",
        ];
        List<int> expectedLevels = [ 0, 0, 0 ];
        List<bool> expectedExpanded =
        [
            false,
            false,
            false,
        ];

        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
        await Assert.That(treeListViewItemsCollection.GetAllIsExpanded()).IsEqualTo(expectedExpanded);
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task Replace_TopLevelItem_IsReplaced(int indexToReplace)
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        
        /*
         * 0. 0
         * 2. 1
         * 3. 2
         */

        //Act
        boundCollection[indexToReplace] = "changed";

        //Assert
        List<string> expectedItems =
        [
            "0",
            "1",
            "2",
        ];
        expectedItems[indexToReplace] = "changed";
        List<int> expectedLevels = [ 0, 0, 0 ];
        List<bool> expectedExpanded =
        [
            false,
            false,
            false,
        ];
        await Assert.That(treeListViewItemsCollection).IsEquivalentTo(expectedItems);
        await Assert.That(treeListViewItemsCollection.GetAllLevels()).IsEqualTo(expectedLevels);
        await Assert.That(treeListViewItemsCollection.GetAllIsExpanded()).IsEqualTo(expectedExpanded);
    }

    [Test]
    [Arguments(-1)]
    [Arguments(3)]
    public async Task GetParent_WithInvalidIndex_ThrowsOutOfRangeException(int index)
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);

        //Act/Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => treeListViewItemsCollection.GetParent(index));
        await Assert.That(ex.ParamName).IsEqualTo("index");
    }

    [Test]
    public async Task GetParent_WithNestedItem_ReturnsParent()
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        treeListViewItemsCollection.InsertWithLevel(2, "1_0", 1);
        treeListViewItemsCollection.InsertWithLevel(3, "1_1", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "1_2", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "1_2_0", 2);
        treeListViewItemsCollection.InsertWithLevel(6, "1_2_1", 2);
        treeListViewItemsCollection.InsertWithLevel(7, "1_2_2", 2);

        /*
         * 0. 0
         * 1. 1
         * 2.  1_0
         * 3.  1_1
         * 4.  1_2
         * 5.    1_2_0
         * 6.    1_2_1
         * 7.    1_2_2
         * 8. 2
         */


        //Act/Assert
        await Assert.That(treeListViewItemsCollection.GetParent(0)).IsNull();
        await Assert.That(treeListViewItemsCollection.GetParent(1)).IsNull();
        await Assert.That(treeListViewItemsCollection.GetParent(2)).IsEqualTo("1");
        await Assert.That(treeListViewItemsCollection.GetParent(3)).IsEqualTo("1");
        await Assert.That(treeListViewItemsCollection.GetParent(4)).IsEqualTo("1");
        await Assert.That(treeListViewItemsCollection.GetParent(5)).IsEqualTo("1_2");
        await Assert.That(treeListViewItemsCollection.GetParent(6)).IsEqualTo("1_2");
        await Assert.That(treeListViewItemsCollection.GetParent(7)).IsEqualTo("1_2");
        await Assert.That(treeListViewItemsCollection.GetParent(8)).IsNull();
    }

    [Test]
    public async Task GetDirectChildrenIndexes_GetsExpectedChildrenIndexes()
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);
        treeListViewItemsCollection.InsertWithLevel(2, "1_0", 1);
        treeListViewItemsCollection.InsertWithLevel(3, "1_1", 1);
        treeListViewItemsCollection.InsertWithLevel(4, "1_2", 1);
        treeListViewItemsCollection.InsertWithLevel(5, "1_2_0", 2);
        treeListViewItemsCollection.InsertWithLevel(6, "1_2_1", 2);
        treeListViewItemsCollection.InsertWithLevel(7, "1_2_2", 2);

        /*
         * 0. 0
         * 1. 1
         * 2.  1_0
         * 3.  1_1
         * 4.  1_2
         * 5.    1_2_0
         * 6.    1_2_1
         * 7.    1_2_2
         * 8. 2
         */


        //Act/Assert
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(0)).IsEmpty();
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(1)).IsEqualTo([2, 3, 4]);
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(2)).IsEmpty();
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(3)).IsEmpty();
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(4)).IsEqualTo([5, 6, 7]);
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(5)).IsEmpty();
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(6)).IsEmpty();
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(7)).IsEmpty();
        await Assert.That(treeListViewItemsCollection.GetDirectChildrenIndexes(8)).IsEmpty();
    }

    private class TestableCollection<T> : ObservableCollection<T>
    {
        private int _blockCollectionChanges;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (Interlocked.CompareExchange(ref _blockCollectionChanges, 0, 0) == 0)
            {
                base.OnCollectionChanged(e);
            }
        }

        public void ReplaceAllItems(params T[] newItems)
        {
            Interlocked.Exchange(ref _blockCollectionChanges, 1);

            Clear();
            foreach (T newItem in newItems)
            {
                Add(newItem);
            }

            Interlocked.Exchange(ref _blockCollectionChanges, 0);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

public static class TreeListViewItemsCollectionExtensions
{
    public static IEnumerable<int> GetAllLevels(this TreeListViewItemsCollection collection)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            yield return collection.GetLevel(i);
        }
    }

    public static IEnumerable<bool> GetAllIsExpanded(this TreeListViewItemsCollection collection)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            yield return collection.GetIsExpanded(i);
        }
    }
}
