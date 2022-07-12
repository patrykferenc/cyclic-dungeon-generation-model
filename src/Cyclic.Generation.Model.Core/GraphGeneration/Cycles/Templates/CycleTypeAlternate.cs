using Cyclic.Generation.Model.Core.GraphGeneration.Building;
using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Utils;
using Cyclic.Generation.Model.Core.Graphs;
using Cyclic.Generation.Model.Core.Obstacles;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Templates;

public class CycleTypeAlternate : BaseCycleType, ICycleType
{
    public void Decorate(Graph graph)
    {
        var myCycle = CycleHelpers.GetCycle(graph);

        // add obstacles on both paths
        var partA = CycleHelpers.GetCyclePart(myCycle, true);
        var partB = CycleHelpers.GetCyclePart(myCycle, false);

        var myObstacle = new Enemy();
        GraphBuilderHelpers.GetRandomFromList(partA).GetObstacles().Add(myObstacle);
        myObstacle = new Enemy();
        GraphBuilderHelpers.GetRandomFromList(partB).GetObstacles().Add(myObstacle);

        TurnToPath(myCycle);
    }
}