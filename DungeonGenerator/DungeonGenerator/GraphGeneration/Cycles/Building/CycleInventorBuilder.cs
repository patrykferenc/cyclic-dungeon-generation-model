namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class CycleInventorBuilder
{
    
    public static CycleInventor Build()
    {
        return new CycleInventor(new SimpleCycle());
    }
    
}