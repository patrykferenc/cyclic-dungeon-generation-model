using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class CycleInventor
{
    
    private readonly IBaseCycleVariant _variant;
    
    public CycleInventor(IBaseCycleVariant variant)
    {
        _variant = variant;
    }
    
    public void InventCycle(Graph graph)
    {
        _variant.Generate(graph);
    }

}