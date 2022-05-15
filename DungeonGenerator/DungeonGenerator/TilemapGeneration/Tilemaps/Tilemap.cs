using System.Text;
using DungeonGenerator.DungeonGenerator.DungeonElements;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public class Tilemap : BaseGrid
{
    public Tilemap(int height, int width) : base(height, width)
    {
        var dimensions = GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
            for (var x = 0; x < dimensions.x; x++)
                Grid[y, x] = new LowResTile(LowresTileType.Empty, (x, y));
    }

    // TODO: Fix this xd
    public void MapGraphToTilemap(Graph graph)
    {
        var dimensions = graph.GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
            for (var x = 0; x < dimensions.x; x++)
                Grid[y, x] = new LowResTile(graph.GetNode((x, y)), (x, y));
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        var dimensions = GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
        {
            for (var x = 0; x < dimensions.x; x++)
                sb.Append((char)((LowResTile)Grid[y, x]).GetTileType());
            sb.Append('\n');
        }

        return sb.ToString();
    }
}