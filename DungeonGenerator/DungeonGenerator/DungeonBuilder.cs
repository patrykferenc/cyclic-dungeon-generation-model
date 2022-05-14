using DungeonGenerator.DungeonGenerator.GraphGeneration;

namespace DungeonGenerator.DungeonGenerator;

public class DungeonBuilder
{
    private DungeonTheme _myTheme;

    public DungeonBuilder(DungeonTheme myTheme)
    {
        this._myTheme = myTheme;
    }

    public void Build()
    {
        var gb = new GraphBuilder(5, 5);
        var graph = gb.GenerateGraph();
        Console.WriteLine("Generated graph:");
        Console.Write(graph.ToString());
    }
    
}
