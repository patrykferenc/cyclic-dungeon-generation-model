using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.UnitTests;

public class NodeGridTests
{
    
    private readonly int _gridWidth = 5;
    private readonly int _gridHeight = 5;
    
    [Theory]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(20, 20)]
    [InlineData(4, 4)]
    public void NodeGrid_HasCorrectDimensions_AfterCreation(int height, int width)
    {
        var grid = new NodeGrid(height, width);
        Assert.Equal(width, grid.GetDimensions().x);
        Assert.Equal(height, grid.GetDimensions().y);
    }
    
    [Theory]
    [InlineData(5, 5, 25)]
    [InlineData(10, 10, 100)]
    [InlineData(20, 20, 400)]
    [InlineData(4, 4, 16)]
    public void NodeGrid_HasCorrectNumberOfElements_AfterCreation(int height, int width, int expectedNumberOfElements)
    {
        var grid = new NodeGrid(height, width);
        Assert.Equal(expectedNumberOfElements, grid.ToList().Count);
    }
    
    // Test if node grid returns correct node
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(0, 4)]
    [InlineData(4, 0)]
    [InlineData(4, 4)]
    public void NodeGrid_ReturnsCorrectNode_WhenNodeExists(int getX, int getY)
    {
        var grid = new NodeGrid(_gridHeight, _gridWidth);
        
        var nodeByGet = grid.GetNode((getX, getY));
        var nodeByList = grid.ToList().Find(n => n.GetPosition() == (getX, getY));

        Assert.Equal(nodeByGet, nodeByList);
    }
    [Theory]
    [InlineData(0, -1)]
    [InlineData(-1, 0)]
    [InlineData(5, 5)]
    public void NodeGrid_ThrowsException_WhenNodeOutsideOfGrid(int getX, int getY)
    {
        var grid = new NodeGrid(_gridHeight, _gridWidth);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetNode((getX, getY)));
    }
    
    [Fact]
    public void NodeGrid_ReturnsTrue_WhenNodesAreConnected()
    {
        var grid = new NodeGrid(_gridHeight, _gridWidth);
        
        var firstNode = grid.GetNode((0, 0));
        var secondNode = grid.GetNode((1, 0));
        firstNode.AddNeighbour(secondNode);
        secondNode.AddNeighbour(firstNode);
        
        Assert.True(NodeGrid.AreNodesConnected(firstNode, secondNode));
    }
    
    [Fact]
    public void NodeGrid_ReturnsFalse_WhenNodesAreNotConnected()
    {
        var grid = new NodeGrid(_gridHeight, _gridWidth);
        
        var firstNode = grid.GetNode((0, 0));
        var secondNode = grid.GetNode((1, 0));
        
        Assert.False(NodeGrid.AreNodesConnected(firstNode, secondNode));
    }
    
    
}