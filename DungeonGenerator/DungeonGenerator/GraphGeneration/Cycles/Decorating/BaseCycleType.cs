using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating;

public abstract class BaseCycleType
{
    
    protected static void TurnToPath(List<Node> myCycle)
    {
        foreach (var node in myCycle) node.SetNodeType(NodeType.Path);
    }
    
}