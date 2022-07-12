using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Templates;

public abstract class BaseCycleType
{
    protected static void TurnToPath(List<Node> myCycle)
    {
        foreach (var node in myCycle) node.SetNodeType(NodeType.Path);
    }
}