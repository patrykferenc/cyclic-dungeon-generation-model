using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class SimpleCycle : BaseCycleVariant, ICycleVariant
{
    
    private const int WanderingPathLength = 2;
    
    public SimpleCycle(Graph graph) : base(graph)
    {
    }

    public void Generate()
    {
        GenerateDungeonEntrance();
        
        GenerateCycleStart();
        
        GenerateMainCycle();
    }

    protected override void GenerateMainCycle()
    {
        var last = GenerateWanderingPath(WanderingPathLength);
        CloseCycle(last);
        GenerateGoal();
    }

}