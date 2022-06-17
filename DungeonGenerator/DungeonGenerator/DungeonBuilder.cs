using DungeonGenerator.DungeonGenerator.GraphGeneration;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator;

public class DungeonBuilder
{
    private DungeonTheme _myTheme;

    public DungeonBuilder(DungeonTheme myTheme)
    {
        _myTheme = myTheme;
    }

    public void Build()
    {
        var gb = new GraphBuilder(5, 5);
        var graph = gb.Generate();
        // Used for debug
        Console.WriteLine("Generated graph:");
        Console.Write(graph.ToString());

        var lr = new LowResolutionTilemapBuilder(graph);
        var lrTilemap = lr.Generate();
        // Used for debug
        Console.WriteLine("Generated low-res level:");
        Console.Write(lrTilemap.ToString());

        var tilemapBuilder = new TilemapBuilder(lrTilemap);
        var tilemap = tilemapBuilder.Generate();
        // Used for debug
        Console.WriteLine("Generated level:");
        Console.Write(tilemap.ToString());
    }
}