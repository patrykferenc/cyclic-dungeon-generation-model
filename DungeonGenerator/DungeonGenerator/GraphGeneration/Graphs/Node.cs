using DungeonGenerator.DungeonGenerator.DungeonElements;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

public class Node : BaseDungeonElement
{
    //private readonly List<Node> _adjacentNodes;

    private CoreGameplayElement _coreRoomFunction = CoreGameplayElement.None;
    private NodeType _type;

    public Node(NodeType roomType, (int x, int y) position) : base(position)
    {
        _type = roomType;
        //_adjacentNodes = new List<Node>();
    }

    public CoreGameplayElement GetCoreGameplayElement()
    {
        return _coreRoomFunction;
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
        SetCoreGameplayElement(rt);
        _type = rt;
    }

    private void SetCoreGameplayElement(NodeType rt)
    {
        switch (rt)
        {
            case NodeType.Start:
                _coreRoomFunction = CoreGameplayElement.Start;
                break;
            case NodeType.End:
                _coreRoomFunction = CoreGameplayElement.Goal;
                break;
        }
    }
}