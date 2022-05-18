using DungeonGenerator.DungeonGenerator.DungeonElements;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public class LowResTile : BaseDungeonElement
{
    private readonly LowresTileType _type;

    public LowResTile(BaseDungeonElement baseElement, (int x, int y) position) :
        base(position, baseElement.GetKeys(), baseElement.GetLocks(), baseElement.GetObstacles(), baseElement.GetAdjacent())
    {
        _type = DecideTileType((Node)baseElement);
    }

    public LowResTile(LowresTileType type, (int x, int y) position) : base(position)
    {
        _type = type;
    }

    public LowresTileType GetTileType()
    {
        return _type;
    }

    private static LowresTileType DecideTileType(Node node)
    {
        LowresTileType type;

        switch (node.GetNodeType())
        {
            case NodeType.Start:
            case NodeType.End:
            case NodeType.Vault:
            case NodeType.Path:
                type = LowresTileType.Room;
                break;
            case NodeType.Empty:
            case NodeType.Undecided:
                type = LowresTileType.Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException("Wrong parameter lmao: " + node.GetNodeType());
        }

        return type;
    }
}