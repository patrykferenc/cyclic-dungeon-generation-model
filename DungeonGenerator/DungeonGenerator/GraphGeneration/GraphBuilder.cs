using DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration;

public class GraphBuilder
{
    private readonly Graph _generatedGraph;

    public GraphBuilder(int nodesHeight, int nodesWidth)
    {
        _generatedGraph = new Graph(nodesHeight, nodesWidth);
    }

    public Graph Generate()
    {
        GenerateBaseCycle();
        
        DecorateCycle();

        return _generatedGraph;
    }

    private void GenerateBaseCycle()
    {
        var builder = CycleInventorBuilder.Build(_generatedGraph);
        
        builder.InventCycle();
        
        Console.Write("After generating cycle: \n" + _generatedGraph + '\n'); // Debug!
    }
    
    private void DecorateCycle()
    {
        var decorator = CycleDecoratorBuilder.Build(_generatedGraph);
        
        decorator.DecorateCycle(_generatedGraph);
        
        Console.Write("After decorating cycle: \n" + _generatedGraph + '\n'); // Debug!
    }

}