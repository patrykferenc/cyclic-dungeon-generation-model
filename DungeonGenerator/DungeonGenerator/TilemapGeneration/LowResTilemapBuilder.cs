using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration;

public class LowResTilemapBuilder
{
    private readonly Graph _graph;

    public LowResTilemapBuilder(Graph graph)
    {
        _graph = graph;
    }

    public LowResTilemap Generate()
    {
        var tilemap = new LowResTilemap(11, 11); // Hardcoded for now
        tilemap.MapGraphToTilemap(_graph);
        return tilemap;
    }
}