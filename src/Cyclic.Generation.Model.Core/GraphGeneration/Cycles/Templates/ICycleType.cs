using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Templates;

public interface ICycleType
{
    void Decorate(Graph graph);
}