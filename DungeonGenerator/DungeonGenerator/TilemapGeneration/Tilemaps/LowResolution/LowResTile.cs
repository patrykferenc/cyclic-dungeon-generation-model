using DungeonGenerator.DungeonGenerator.DungeonElements;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

public class LowResTile : BaseDungeonElement
{
    private readonly LowResolutionTileType _type;

    public LowResTile(BaseDungeonElement baseElement, (int x, int y) position) :
        base(position, baseElement.GetKeys(), baseElement.GetLocks(), baseElement.GetObstacles(), baseElement.GetAdjacent())
    {
        _type = DecideTileType((Node)baseElement);
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
        LowResolutionTileType type;

        switch (node.GetNodeType())
        {
            case NodeType.Start:
            case NodeType.End:
            case NodeType.Vault:
            case NodeType.Path:
                type = LowResolutionTileType.Room;
                break;
            case NodeType.Empty:
            case NodeType.Undecided:
                type = LowResolutionTileType.Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException("Wrong parameter lmao: " + node.GetNodeType());
        }

        return type;
    }
}