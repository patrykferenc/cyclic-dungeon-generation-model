using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration;

public class LowResTilemapBuilder
{
    private readonly Graph _graph;

    public LowResTilemapBuilder(Graph graph)
    {
        _graph = graph;
    }

    public Tilemap Generate()
    {
        var tilemap = new Tilemap(11, 11); // Hardcoded for now
        tilemap.MapGraphToTilemap(_graph);
        return tilemap;
    }
}