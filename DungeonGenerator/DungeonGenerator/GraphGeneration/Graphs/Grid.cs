using System.Text;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

public class Grid
{
    private readonly Node[,] _grid;
    private readonly int _nodesHeight;
    private readonly int _nodesWidth;

    public Grid(int nodesHeight, int nodesWidth, NodeType roomType = NodeType.Undecided)
    {
        _nodesHeight = nodesHeight;
        _nodesWidth = nodesWidth;
        _grid = new Node[_nodesHeight, _nodesWidth];
        for (var i = 0; i < _nodesHeight; i++)
        {
            for (var j = 0; j < _nodesWidth; j++)
            {
                _grid[i, j] = new Node(roomType, j, i);
            }
        }
    }
    
    public (int x, int y) GetGraphDimensions()
    {
        return (x: _nodesWidth, y: _nodesHeight);
    }

    public Node GetNode((int x, int y) position)
    {
        return GetNodeHelper(position);
    }

    private Node GetNodeHelper((int x, int y) position)
    {
        if (position.x < _grid.GetLength(1) && position.x >= 0 &&
            position.y < _grid.GetLength(0) && position.y >= 0)
            throw new ArgumentOutOfRangeException("There is no node to return at: " + position);
        return _grid[position.y, position.x];
    }

    public List<Node> ToList()
    {
        List<Node> nodes = new();

        for (var i = 0; i < _nodesHeight; i++)
        {
            for (var j = 0; j < _nodesWidth; j++)
            {
                nodes.Add(_grid[i, j]);
            }
        }

        return nodes;
    }

    private static bool AreNodesConnected(Node firstNode, Node secondNode)
    {
        return firstNode.GetNeighbours().Contains(secondNode);
    }
    
    public override string ToString()
    {
        StringBuilder sb = new();

        for (var i = 0; i < _nodesHeight; i++)
        {
            for (var j = 0; j < _nodesWidth; j++)
            {
                sb.Append((char)_grid[i, j].GetNodeType());
                if (j < _nodesWidth - 1) sb.Append(AreNodesConnected(_grid[i, j], _grid[i, j + 1]) ? '-' : ' ');
            }
            sb.Append('\n');
            if (i < _nodesHeight - 1)
                for (var k = 0; k < _nodesWidth; k++)
                    sb.Append(AreNodesConnected(_grid[i, k], _grid[i + 1, k]) ? "| " : "  ");
            sb.Append('\n');
        }

        return sb.ToString();
    }
    
}