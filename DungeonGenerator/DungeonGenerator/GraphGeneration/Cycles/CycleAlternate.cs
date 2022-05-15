﻿using DungeonGenerator.DungeonGenerator.Characteristics.Obstacles;
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
        GraphBuilderHelpers.GetRandomFromList(partA).GetObstacles().Add(myObstacle);
        myObstacle = new Enemy();
        GraphBuilderHelpers.GetRandomFromList(partB).GetObstacles().Add(myObstacle);

        // change to path
        foreach (var node in myCycle) node.SetNodeType(NodeType.Path);

        return graph;
    }
}