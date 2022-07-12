using System.Text;

namespace Cyclic.Generation.Model.Core.Graphs;

public class Graph
{
    // von neumann's neighbourhood
    private static readonly (int x, int y)[] Neighbourhood = { (-1, 0), (0, -1), (1, 0), (0, 1) };

    private readonly NodeGrid _nodeGrid;

    public Graph(int nodesHeight, int nodesWidth)
    {
        _nodeGrid = new NodeGrid(nodesHeight, nodesWidth);
    }

    public (int x, int y) GetDimensions()
    {
        return _nodeGrid.GetDimensions();
    }

    public static void AddEdge(Node startNode, Node endNode)
    {
        if (!startNode.GetNeighbours().Contains(endNode))
            startNode.AddNeighbour(endNode);
        if (!endNode.GetNeighbours().Contains(startNode))
            endNode.AddNeighbour(startNode);
    }

    public List<Node> GetNodesInNeighbourhood(Node node)
    {
        return _nodeGrid.ToList().FindAll(n => IsPositionInNeighbourhood(n.GetPosition(), node.GetPosition()));
    }

    public List<Node> GetNodesInNeighbourhood((int x, int y) position)
    {
        return _nodeGrid.ToList().FindAll(n => IsPositionInNeighbourhood(n.GetPosition(), position));
    }

    public static bool IsNodeInNeighbourhood(Node firstNode, Node secondNode)
    {
        return IsPositionInNeighbourhood(firstNode.GetPosition(), secondNode.GetPosition());
    }

    public static bool IsPositionInNeighbourhood((int x, int y) firstPosition, (int x, int y) secondPosition)
    {
        for (var i = 0; i < Neighbourhood.Length; i++)
        {
            var positionToCheck = (x: firstPosition.x + Neighbourhood[i].x, y: firstPosition.y + Neighbourhood[i].y);
            if (positionToCheck == secondPosition)
                return true;
        }

        return false;
    }

    public Node GetNode((int x, int y) position)
    {
        return _nodeGrid.GetNode(position);
    }

    public Node? GetFirstNodeOfType(NodeType type)
    {
        return _nodeGrid.ToList().Find(n => n.GetNodeType() == type);
    }

    public List<Node> GetAllNodesOfType(NodeType type)
    {
        return _nodeGrid.ToList().FindAll(n => n.GetNodeType() == type);
    }
    
    public List<Node> GetAllNodes()
    {
        return _nodeGrid.ToList();
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append(_nodeGrid);

        return sb.ToString();
    }
}