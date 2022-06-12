using DungeonGenerator.DungeonGenerator.Characteristics.Gates;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating.Types;

public class CycleTypeLockKey : BaseCycleType, ICycleType
{
    // TODO: implement this fr
    public void Decorate(Graph graph)
    {
        var myCycle = CycleHelpers.GetCycle(graph);

        // add obstacles on both paths
        var partA = CycleHelpers.GetCyclePart(myCycle, true);
        var partB = CycleHelpers.GetCyclePart(myCycle, false);

        var lockedDoor = new LockedDoor();
        var key = new DoorKey(lockedDoor);
        GraphBuilderHelpers.GetRandomFromList(partA).GetLocks().Add(lockedDoor);
        GraphBuilderHelpers.GetRandomFromList(partB).GetKeys().Add(key);

        TurnToPath(myCycle);
    }
}