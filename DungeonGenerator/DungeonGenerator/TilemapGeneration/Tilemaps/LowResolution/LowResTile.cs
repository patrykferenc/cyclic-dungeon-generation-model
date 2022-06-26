using DungeonGenerator.DungeonGenerator.DungeonElements;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

public class LowResTile : BaseDungeonElement
{
    private readonly LowResolutionTileType _type;
    private readonly RoomType _roomType;

    public LowResTile(BaseDungeonElement baseElement, (int x, int y) position) :
        base(position, baseElement.GetKeys(), baseElement.GetLocks(), baseElement.GetObstacles(),
            baseElement.GetAdjacent())
    {
        _type = DecideTileType((Node)baseElement);
        _roomType = DecideRoomType((Node)baseElement);
    }

    private RoomType DecideRoomType(Node node)
    {
        return node.GetRoomType() switch
        {
            RoomType.CastleRoom => RoomType.CastleRoom,
            RoomType.Cave => RoomType.Cave,
            _ => throw new ArgumentOutOfRangeException("Wrong parameter during conversion to low-res tile type: " +
                                                       node.GetRoomType())
        };
    }

    public LowResTile(LowResolutionTileType type, (int x, int y) position) : base(position)
    {
        _type = type;
    }

    public LowResolutionTileType GetTileType()
    {
        return _type;
    }

    private static LowResolutionTileType DecideTileType(Node node)
    {
        LowResolutionTileType type = node.GetNodeType() switch
        {
            NodeType.Start => LowResolutionTileType.Room,
            NodeType.End => LowResolutionTileType.Room,
            NodeType.Vault => LowResolutionTileType.Room,
            NodeType.Path => LowResolutionTileType.Room,
            NodeType.Empty => LowResolutionTileType.Empty,
            NodeType.Undecided => LowResolutionTileType.Empty,
            _ => throw new ArgumentOutOfRangeException("Wrong parameter during conversion to low-res tile type: " +
                                                       node.GetNodeType())
        };

        return type;
    }

    public RoomType GetRoomType()
    {
        return _roomType;
    }
}