using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration;

public class TilemapBuilder
{
    private readonly Graph _graph;
    private readonly LowResTilemap _lowResTilemap;

    public TilemapBuilder(Graph graph, LowResTilemap tilemap)
    {
        _graph = graph;
        _lowResTilemap = tilemap;
    }

    public Tilemap Generate()
    {
        var tilemap = new Tilemap(55, 55);
        
        
        
        return tilemap;
    }
    
}