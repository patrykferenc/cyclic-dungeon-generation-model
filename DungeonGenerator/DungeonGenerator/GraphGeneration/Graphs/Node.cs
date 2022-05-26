using DungeonGenerator.DungeonGenerator.DungeonElements;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

public class Node : BaseDungeonElement
{

    private NodeType _type;

    public Node(NodeType roomType, (int x, int y) position) : base(position)
    {
        _type = roomType;
    }

    public void AddNeighbour(Node n)
    {
        GetAdjacent().Add(n);
    }

    public List<Node> GetNeighbours()
    {
        return GetAdjacent().Cast<Node>().ToList();
    }

    public NodeType GetNodeType()
    {
        return _type;
    }

    public void SetNodeType(NodeType rt)
    {
        _type = rt;
    }

    public override string ToString()
    {
        return "Node: " + _type + " " + GetPosition();
    }
}