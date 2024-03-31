using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using MaterialDesignThemes.Wpf.Internal;

namespace MaterialDesignThemes.Wpf.Tests.Internal;

public class TreeListViewItemsCollectionTests
{
    [Fact]
    public void Constructor_AcceptsNull()
    {
        TreeListViewItemsCollection collection = new(null);

        Assert.Empty(collection);
    }

    [Fact]
    public void Constructor_AcceptsObject()
    {
        TreeListViewItemsCollection collection = new(new object());

        Assert.Empty(collection);
    }

    [Fact]
    public void Constructor_AcceptsIEnumerable()
    {
        IEnumerable<string> enumerable = new[] { "a", "b", "c" };
        TreeListViewItemsCollection collection = new(enumerable);

        Assert.Equal(new[] { "a", "b", "c" }, collection);
    }

    [Fact]
    public void WhenWrappedObjectImplementsIncc_ItHandlesAdditions()
    {
        //Arrange
        ObservableCollection<string> collection = new();

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.Add("a");

        //Assert
        Assert.Equal(new[] { "a" }, treeListViewItemsCollection);
    }

    [Fact]
    public void WhenWrappedObjectImplementsIncc_ItHandlesRemovals()
    {
        //Arrange
        ObservableCollection<string> collection = new() { "a" };

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.Remove("a");

        //Assert
        Assert.Empty(treeListViewItemsCollection);
    }

    [Fact]
    public void WhenWrappedObjectImplementsIncc_ItHandlesReplacements()
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
        Assert.Equal(new[] { "a", "a_a", "a_b", "x", "c", "c_a" }, treeListViewItemsCollection);
    }

    [Fact]
    public void WhenWrappedObjectImplementsIncc_ItHandlesReset()
    {
        //Arrange
        TestableCollection<string> collection = new() { "a" };

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.ReplaceAllItems("b", "c");

        //Assert
        Assert.Equal(new[] { "b", "c" }, treeListViewItemsCollection);
    }

    [Fact]
    public void WhenWrappedObjectImplementsIncc_ItHandlesMoves()
    {
        //Arrange
        ObservableCollection<string> collection = new() { "a", "b", "c" };

        TreeListViewItemsCollection treeListViewItemsCollection = new(collection);

        //Act
        collection.Move(0, 2);

        //Assert
        Assert.Equal(new[] { "b", "c", "a" }, treeListViewItemsCollection);
    }

    [Theory]
    [InlineData(0, 0)] //x is a sibling of a
    [InlineData(1, 1)] //x nested under a
    [InlineData(2, 1)] //x is a sibling of a_a
    [InlineData(2, 2)] //x is a child of a_a
    [InlineData(3, 1)] //x is a sibling of a_b
    [InlineData(3, 2)] //x is a child of a_b
    [InlineData(4, 1)] //x is a child of b
    [InlineData(4, 0)] //x is a sibling of b
    public void WhenAddingItemAtNestedLevel_ItSetsTheItemsLevel(int insertionIndex, int requestedLevel)
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
        List<string> expectedItems = new(new[] { "a", "a_a", "a_b", "b" });
        expectedItems.Insert(insertionIndex, "x");
        Assert.Equal(expectedItems, treeListViewItemsCollection);

        List<int> expectedLevels = new(new[] { 0, 1, 1, 0 });
        expectedLevels.Insert(insertionIndex, requestedLevel);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 4)]
    [InlineData(2, 0)]
    [InlineData(0, -1)]
    [InlineData(4, 2)]
    public void InsertWithLevel_WhenAddingItemAtNestedLevel_ItThrowsIfRequestIsOutOfRange(int insertionIndex, int requestedLevel)
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
    }

    [Fact]
    public void InsertWithLevel_WhenInsertingFirstSibling_MarksIndexAsExpanded()
    {
        TreeListViewItemsCollection treeListViewItemsCollection = new(new[] { "a", "b" });

        treeListViewItemsCollection.InsertWithLevel(1, "a_a", 1);
        /*
         * 0. a
         * 1.  a_a
         * 2. b
         */

        Assert.Equal(new[] { true, false, false }, treeListViewItemsCollection.GetAllIsExpanded());
    }

    [Theory]
    [InlineData(0, new[] { "b", "b_a", "c" }, new[] { 0, 1, 0})]
    [InlineData(1, new[] { "a", "a_a", "a_a_a", "a_a_b", "a_b", "c" }, new[] { 0, 1, 2, 2, 1, 0 })]
    [InlineData(2, new[] { "a", "a_a", "a_a_a", "a_a_b", "a_b", "b", "b_a" }, new[] { 0, 1, 2, 2, 1, 0, 1 })]
    public void WhenRemovingItem_ItRemovesItemsAndAnyChildren(int indexToRemove, string[] expectedItems, int[] expectedLevels)
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

        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Fact]
    public void Move_WhenMovingItemUp_ItMovesChildrenAlongWithIt()
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
        List<string> expectedItems = new(new[] { "b", "b_a", "b_b", "b_c", "a", "a_a", "a_b", "c", "c_a", "c_b" });
        List<int> expectedLevels = new(new[] { 0, 1, 1, 1, 0, 1, 1, 0, 1, 1 });

        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Fact]
    public void Move_WhenMovingItemUpMultipleLevels_ItMovesChildrenAlongWithIt()
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
        List<string> expectedItems = new(new[] { "c", "c_a", "a", "a_a", "a_b", "b", "b_a", "b_b", "b_c" });
        List<int> expectedLevels = new(new[] { 0, 1, 0, 1, 1, 0, 1, 1, 1 });

        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Fact]
    public void Move_WhenMovingItemDown_ItMovesChildrenAlongWithIt()
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
        string[] expectedItems = new[] { "a", "a_a", "a_b", "c", "c_a", "c_b", "b", "b_a", "b_b", "b_c" };
        int[] expectedLevels = new[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 1 };

        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Fact]
    public void Move_WhenMovingItemDownMultipleLevels_ItMovesChildrenAlongWithIt()
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
        List<string> expectedItems = new(new[] { "b", "b_a", "b_b", "b_c", "c", "c_a", "a", "a_a", "a_b" });
        List<int> expectedLevels = new(new[] { 0, 1, 1, 1, 0, 1, 0, 1, 1 });

        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Fact]
    public void Move_WithWrappedCollection_ItMovesChildrenAlongWithIt()
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
        List<string> expectedItems = new(new[] { "a", "c", "b", "b_a", "b_b", "b_c" });
        List<int> expectedLevels = new(new[] { 0, 0, 0, 1, 1, 1 });

        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
    }

    [Fact]
    public void Move_WithExpandedChild_ItMovesChildrenUp()
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
        List<string> expectedItems = new(new[]
        {
            "1",
            "1_0",
            "1_1",
            "1_1_0",
            "1_1_1",
            "1_1_2",
            "1_2",
            "0",
            "2",
        });
        List<int> expectedLevels = new(new[] { 0, 1, 1, 2, 2, 2, 1, 0, 0 });
        List<bool> expectedExpanded = new()
        {
            true,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
        };
        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
        Assert.Equal(expectedExpanded, treeListViewItemsCollection.GetAllIsExpanded());
    }

    [Fact]
    public void Move_WithExpandedChild_ItMovesChildrenDown()
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
        List<string> expectedItems = new(new[]
        {
            "1",
            "0",
            "0_0",
            "0_1",
            "0_1_0",
            "0_1_1",
            "0_1_2",
            "0_2",
            "2",
        });
        List<int> expectedLevels = new(new[] { 0, 0, 1, 1, 2, 2, 2, 1, 0 });
        List<bool> expectedExpanded = new()
        {
            false,
            true,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
        };
        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
        Assert.Equal(expectedExpanded, treeListViewItemsCollection.GetAllIsExpanded());
    }

    [Fact]
    public void Replace_WithExpandedChild_ItRemovesChildren()
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
        List<string> expectedItems = new(new[]
        {
            "0",
            "1",
            "replaced",
        });
        List<int> expectedLevels = new(new[] { 0, 0, 0 });
        List<bool> expectedExpanded = new()
        {
            false,
            false,
            false,
        };
        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
        Assert.Equal(expectedExpanded, treeListViewItemsCollection.GetAllIsExpanded());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Replace_TopLevelItem_IsReplaced(int indexToReplace)
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
        List<string> expectedItems = new(new[]
        {
            "0",
            "1",
            "2",
        });
        expectedItems[indexToReplace] = "changed";
        List<int> expectedLevels = new(new[] { 0, 0, 0 });
        List<bool> expectedExpanded = new()
        {
            false,
            false,
            false,
        };
        Assert.Equal(expectedItems, treeListViewItemsCollection);
        Assert.Equal(expectedLevels, treeListViewItemsCollection.GetAllLevels());
        Assert.Equal(expectedExpanded, treeListViewItemsCollection.GetAllIsExpanded());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void GetParent_WithInvalidIndex_ThrowsOutOfRangeException(int index)
    {
        //Arrange
        ObservableCollection<string> boundCollection = new() { "0", "1", "2" };
        TreeListViewItemsCollection treeListViewItemsCollection = new(boundCollection);

        //Act/Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => treeListViewItemsCollection.GetParent(index));
        Assert.Equal("index", ex.ParamName);
    }

    [Fact]
    public void GetParent_WithNestedItem_ReturnsParent()
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
        Assert.Null(treeListViewItemsCollection.GetParent(0));
        Assert.Null(treeListViewItemsCollection.GetParent(1));
        Assert.Equal("1", treeListViewItemsCollection.GetParent(2));
        Assert.Equal("1", treeListViewItemsCollection.GetParent(3));
        Assert.Equal("1", treeListViewItemsCollection.GetParent(4));
        Assert.Equal("1_2", treeListViewItemsCollection.GetParent(5));
        Assert.Equal("1_2", treeListViewItemsCollection.GetParent(6));
        Assert.Equal("1_2", treeListViewItemsCollection.GetParent(7));
        Assert.Null(treeListViewItemsCollection.GetParent(8));
    }

    [Fact]
    public void GetDirectChildrenIndexes_GetsExpectedChildrenIndexes()
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
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(0));
        Assert.Equal(new[] { 2, 3, 4 }, treeListViewItemsCollection.GetDirectChildrenIndexes(1));
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(2));
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(3));
        Assert.Equal(new[] { 5, 6, 7 }, treeListViewItemsCollection.GetDirectChildrenIndexes(4));
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(5));
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(6));
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(7));
        Assert.Empty(treeListViewItemsCollection.GetDirectChildrenIndexes(8));
        
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
