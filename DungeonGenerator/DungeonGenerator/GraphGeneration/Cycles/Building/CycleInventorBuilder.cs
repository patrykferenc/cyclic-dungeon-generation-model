using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.Utils;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class CycleInventorBuilder
{

    // choose type of cycle based on gaussian distribution
    public static CycleInventor Build(Graph graph)
    {
        var selections = new Dictionary<ICycleVariant, float>
        {
            { new SimpleCycle(graph), 10f },
            { new HollowCycle(graph), 50f }
        };
        var cycleVariant = selections.RandomElementByWeight(s => s.Value).Key;
        return new CycleInventor(cycleVariant);
    }

}