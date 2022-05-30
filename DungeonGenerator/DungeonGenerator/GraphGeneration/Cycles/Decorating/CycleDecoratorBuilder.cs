using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating;

public static class CycleDecoratorBuilder
{

    public static CycleDecorator Build(Graph graph)
    {
        var type = new CycleTypeAlternate();
        return new CycleDecorator(type);
    }
    
}