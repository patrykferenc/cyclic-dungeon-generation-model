using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating.Types;

public interface ICycleType
{
    void Decorate(Graph graph);
    
}