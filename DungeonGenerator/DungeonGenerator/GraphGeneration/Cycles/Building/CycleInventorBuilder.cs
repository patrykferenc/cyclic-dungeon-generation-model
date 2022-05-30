using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class CycleInventorBuilder
{

    public static CycleInventor Build(Graph graph)
    {
        
        return new CycleInventor(new SimpleCycle(graph));
    }
    
}