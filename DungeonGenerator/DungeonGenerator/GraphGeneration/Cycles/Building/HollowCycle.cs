﻿using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class HollowCycle : BaseCycleVariant, ICycleVariant
{
    
    private const int MinHoleSize = 1;
    private const int MaxHoleSize = 4;
    
    public HollowCycle(Graph graph) : base(graph)
    {
    }

    public void Generate()
    {
        // Generate a random number of empty nodes
        AddEmptyNodesInTheMiddle();

        GenerateDungeonEntrance();
        
        GenerateCycleStart();
        
        GenerateMainCycle();
    }

    private void AddEmptyNodesInTheMiddle()
    {
        var emptyNodes = new Random().Next(MinHoleSize, MaxHoleSize);
        for (var i = 0; i < emptyNodes; i++)
        {
            AddEmptyNode();
        }
    }

    protected override void GenerateMainCycle()
    {
        throw new NotImplementedException();
    }
    
    private void AddEmptyNode()
    {
        var size = Graph.GetDimensions();
        var emptyNode = GraphBuilderHelpers.GetRandomFromList(Graph
            .GetNodesInNeighbourhood((size.x / 2, size.y / 2))
            .FindAll(n => n.GetNodeType() == NodeType.Undecided));
        emptyNode.SetNodeType(NodeType.Empty);
    }
}