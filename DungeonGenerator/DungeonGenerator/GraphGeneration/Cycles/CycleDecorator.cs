using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles
{
    public class CycleDecorator
    {

        private readonly ICycleType _type;

        public CycleDecorator(ICycleType type)
        {
            _type = type;
        }

        public Graph DecorateCycle(Graph graph)
        {
            return _type.Generate(graph);
        }

    }
}
