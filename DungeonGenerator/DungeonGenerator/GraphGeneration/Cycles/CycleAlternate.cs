using DungeonGenerator.DungeonGenerator.GraphGeneration.Characteristics;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles;

public class CycleAlternate : AbstractCycle, ICycleType
{
    public Graph Generate(Graph graph)
    {
        var myCycle = CycleHelpers.GetCycle(graph);

        // add obstacles on both paths
        var partA = CycleHelpers.GetCyclePart(myCycle, true);
        var partB = CycleHelpers.GetCyclePart(myCycle, false);

        var myObstacle = new Enemy();
        GraphBuilderHelpers.GetRandomFromList(partA).AddObstacle(myObstacle);
        myObstacle = new Enemy();
        GraphBuilderHelpers.GetRandomFromList(partB).AddObstacle(myObstacle);

        // change to path
        foreach (var node in myCycle) node.SetNodeType(NodeType.Path);

        return graph;
    }
}