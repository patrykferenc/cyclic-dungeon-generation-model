using Cyclic.Generation.Model.Core.Gates.Keys;
using Cyclic.Generation.Model.Core.Gates.Locks;
using Cyclic.Generation.Model.Core.GraphGeneration.Building;
using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Utils;
using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Templates;

public class CycleTypeLockKey : BaseCycleType, ICycleType
{
    // TODO: implement this 
    // For now it's just a copy of the blocking door cycle type
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