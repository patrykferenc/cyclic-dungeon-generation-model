using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles;

public static class CycleChooser
{
    // This method builds the decorator and decides type
    public static CycleDecorator BuildDecorator(Graph graph)
    {
        var type = new CycleAlternate();
        return new CycleDecorator(type);
    }
}