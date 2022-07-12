using Cyclic.Generation.Model.Core.GraphGeneration.Building;
using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Utils;
using Cyclic.Generation.Model.Core.Graphs;
using Cyclic.Generation.Model.Core.Obstacles;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Templates;

public class CycleTypePatrolling : BaseCycleType, ICycleType
{
    public void Decorate(Graph graph)
    {
        var myCycle = CycleHelpers.GetCycle(graph);

        // add patrolling enemy to one of the rooms
        var partA = CycleHelpers.GetCyclePart(myCycle, true);

        var myObstacle = new PatrollingEnemy();
        GraphBuilderHelpers.GetRandomFromList(partA).GetObstacles().Add(myObstacle);

        TurnToPath(myCycle);
    }
}