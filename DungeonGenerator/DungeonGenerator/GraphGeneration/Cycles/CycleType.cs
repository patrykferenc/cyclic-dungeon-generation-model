using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles
{

    public interface ICycleType
    {

        Graph Generate(Graph graph);

    }
}
