using DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating.Types;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating;

public class CycleDecorator
{
    
    private readonly ICycleType _type;

    public CycleDecorator(ICycleType type)
    {
        _type = type;
    }

    public void DecorateCycle(Graph graph)
    {
        _type.Decorate(graph);
    }
    
}