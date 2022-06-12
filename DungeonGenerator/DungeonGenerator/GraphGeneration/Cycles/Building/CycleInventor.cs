using DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building.Variants;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class CycleInventor
{
    
    private readonly ICycleVariant _variant;
    
    public CycleInventor(ICycleVariant variant)
    {
        _variant = variant;
    }
    
    public void InventCycle()
    {
        _variant.Generate();
    }

}