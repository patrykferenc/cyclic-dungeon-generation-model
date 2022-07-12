using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Templates;
using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Decorating;

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