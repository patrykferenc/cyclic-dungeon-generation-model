using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Variants;
using Cyclic.Generation.Model.Core.Graphs;
using Cyclic.Generation.Model.Core.Utils;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Building;

public static class CycleInventorBuilder
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