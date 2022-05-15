using System.Text;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public class Tilemap
{
    // von neumann
    private static readonly (int x, int y)[] Neighbourhood = { (-1, 0), (0, -1), (1, 0), (0, 1) };

    private readonly int _height;

    private readonly LowresTile[,] _tiles;
    private readonly int _width;

    public Tilemap(int height, int width)
    {
        _height = height;
        _width = width;
        _tiles = new LowresTile[_height, _width];
        for (var y = 0; y < _height; y++)
            for (var x = 0; x < _width; x++)
                _tiles[y, x] = new LowresTile(LowresTileType.Empty, (x, y));
    }

    // TODO: Fix this xd
    public void MapGraphToTilemap(Graph graph)
    {
        var dimensions = graph.GetDimensions();
        
        for (var y = 0; y < dimensions.y; y++)
            for (var x = 0; x < dimensions.x; x++)
                _tiles[y, x] = GenerateTileFromNode(graph.GetNode((x, y)));
    }

    private LowresTile GenerateTileFromNode(Node node)
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

        var tile = new LowresTile(type, node.GetPosition(), node.GetLocks(), node.GetKeys(), node.GetObstacles());
        return tile;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++) sb.Append((char)_tiles[i, j].GetTileType());
            sb.Append('\n');
        }

        return sb.ToString();
    }
}