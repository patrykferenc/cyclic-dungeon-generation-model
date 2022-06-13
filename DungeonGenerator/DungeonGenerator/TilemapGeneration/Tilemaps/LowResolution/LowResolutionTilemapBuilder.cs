using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

public class LowResolutionTilemapBuilder
{
    private readonly Graph _graph;

    public LowResolutionTilemapBuilder(Graph graph)
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