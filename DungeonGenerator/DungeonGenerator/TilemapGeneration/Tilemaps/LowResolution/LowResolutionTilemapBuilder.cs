using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

public class LowResolutionTilemapBuilder
{
    private readonly Graph _graph;
    private const int SizeMultiplier = 2; // This can not be a different number as we are doubling the size of the graph.

    public LowResolutionTilemapBuilder(Graph graph)
    {
        _graph = graph;
    }

    public LowResTilemap Generate()
    {
        var graphDimensions = _graph.GetDimensions();
        var tilemap = new LowResTilemap(graphDimensions.y * SizeMultiplier + 1, graphDimensions.x * SizeMultiplier + 1);
        tilemap.MapGraphToTilemap(_graph);
        return tilemap;
    }
}