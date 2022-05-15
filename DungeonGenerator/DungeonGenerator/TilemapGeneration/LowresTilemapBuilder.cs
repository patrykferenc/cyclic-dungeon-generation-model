using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration;

public class LowresTilemapBuilder
{
    private static readonly int SizeMultiply = 2;
    private readonly Graph _graph;

    public LowresTilemapBuilder(Graph graph)
    {
        _graph = graph;
    }

    public Tilemap Generate()
    {
        var tilemap = new Tilemap(11, 11);
        tilemap.MapGraphToTilemap(_graph);
        return tilemap;
    }
}