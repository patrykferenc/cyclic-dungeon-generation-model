using Cyclic.Generation.Model.Core.Common;
using Cyclic.Generation.Model.Core.Rooms;

namespace Cyclic.Generation.Model.Core.Graphs;

public class Node : BaseDungeonElement
{
    private NodeType _type;
    private RoomType _roomType;

    public Node(NodeType roomType, (int x, int y) position) : base(position)
    {
        _type = roomType;
        _roomType = RoomType.CastleRoom;
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
    
    public RoomType GetRoomType()
    {
        return _roomType;
    }
    
    public void SetRoomType(RoomType rt)
    {
        _roomType = rt;
    }

    public override string ToString()
    {
        return "Node: " + _type + " " + GetPosition();
    }
}