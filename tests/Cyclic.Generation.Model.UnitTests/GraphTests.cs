

using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.UnitTests;

public class GraphTests
{
    
    private readonly int _gridWidth = 5;
    private readonly int _gridHeight = 5;
    
    [Fact]
    public void Graph_HasCorrectDimensions_AfterCreation()
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        Assert.Equal(_gridWidth, graph.GetDimensions().x);
        Assert.Equal(_gridHeight, graph.GetDimensions().y);
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(4, 4)]    
    public void Graph_GetsNode_WhenCoordinatesAreValid(int getX, int getY)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var positionCorrect = (x: getX, y: getY);
        var node = graph.GetNode(positionCorrect);

        var positionActual = node.GetPosition();

        Assert.Equal(positionCorrect, positionActual);
    }
    
    [Fact]
    public void Graph_HasCorrectConnections_AfterAddingEdge()
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var firstNode = graph.GetNode((0, 0));
        var secondNode = graph.GetNode((0, 1));
        
        Graph.AddEdge(firstNode, secondNode);
        
        Assert.Contains(secondNode, firstNode.GetAdjacent());
        Assert.Contains(firstNode, secondNode.GetAdjacent());
    }
    
    [Theory]
    [InlineData(0, 0, 1, 0)]
    [InlineData(0, 0, 0, 1)]
    public void Graph_ReturnsTrue_WhenNodesAreInNeighbourhood(int x1, int y1, int x2, int y2)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var firstNode = graph.GetNode((x1, y1));
        var secondNode = graph.GetNode((x2, y2));
        
        Assert.True(Graph.IsNodeInNeighbourhood(firstNode, secondNode));
    }
    
    [Theory]
    [InlineData(0, 0, 2, 0)]
    [InlineData(0, 0, 0, 2)]
    [InlineData(0, 0, 1, 1)]
    public void Graph_ReturnsFalse_WhenNodesAreNotInNeighbourhood(int x1, int y1, int x2, int y2)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var firstNode = graph.GetNode((x1, y1));
        var secondNode = graph.GetNode((x2, y2));
        
        Assert.False(Graph.IsNodeInNeighbourhood(firstNode, secondNode));
    }

    [Theory]
    [InlineData(1, 1)]
    public void Graph_ReturnsCorrectListOfNeighbours_WhenNodeHasFourNeighbours(int x, int y)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var node = graph.GetNode((x, y));
        
        var expectedNeighbours = new List<Node>
        {
            graph.GetNode((x - 1, y)),
            graph.GetNode((x + 1, y)),
            graph.GetNode((x, y - 1)),
            graph.GetNode((x, y + 1))
        };
        
        Assert.All(graph.GetNodesInNeighbourhood(node), item => Assert.Contains(item, expectedNeighbours));
    }
    
    [Theory]
    [InlineData(0, 0)]
    public void Graph_ReturnsCorrectListOfNeighbours_WhenNodeIsInCorner(int x, int y)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var node = graph.GetNode((x, y));
        
        var expectedNeighbours = new List<Node>
        {
            graph.GetNode((x + 1, y)),
            graph.GetNode((x, y + 1))
        };
        
        Assert.All(graph.GetNodesInNeighbourhood(node), item => Assert.Contains(item, expectedNeighbours));
    }
    
    [Theory]
    [InlineData(1, 1)]
    public void Graph_ReturnsCorrectListOfNeighbours_WhenPositionHasFourNeighbours(int x, int y)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var positionCorrect = (x: x, y: y);
        
        var expectedNeighbours = new List<Node>
        {
            graph.GetNode((x - 1, y)),
            graph.GetNode((x + 1, y)),
            graph.GetNode((x, y - 1)),
            graph.GetNode((x, y + 1))
        };
        
        Assert.All(graph.GetNodesInNeighbourhood(positionCorrect), item => Assert.Contains(item, expectedNeighbours));
    }
    
    [Theory]
    [InlineData(0, 0)]
    public void Graph_ReturnsCorrectListOfNeighbours_WhenPositionIsInCorner(int x, int y)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var positionCorrect = (x: x, y: y);
        
        var expectedNeighbours = new List<Node>
        {
            graph.GetNode((x + 1, y)),
            graph.GetNode((x, y + 1))
        };
        
        Assert.All(graph.GetNodesInNeighbourhood(positionCorrect), item => Assert.Contains(item, expectedNeighbours));
    }
    
    [Theory]
    [InlineData(NodeType.Undecided, 25)]
    [InlineData(NodeType.Empty, 5)]
    public void Graph_ReturnsCorrectNumberOfNodesOfGivenType(NodeType type, int expectedNumberOfNodes)
    {
        var graph = new Graph(_gridWidth, _gridHeight);
        
        var nodesToChange = graph.GetAllNodesOfType(NodeType.Undecided);
        nodesToChange = nodesToChange.GetRange(0, expectedNumberOfNodes);
        foreach (var node in nodesToChange)
        {
            node.SetNodeType(type);
        }
        
        var nodesOfType = graph.GetAllNodesOfType(type);
        
        Assert.Equal(expectedNumberOfNodes, nodesOfType.Count);
    }
    
}