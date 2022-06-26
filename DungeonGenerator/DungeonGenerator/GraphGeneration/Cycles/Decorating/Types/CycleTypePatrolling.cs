using DungeonGenerator.DungeonGenerator.Characteristics.Obstacles;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating.Types;

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