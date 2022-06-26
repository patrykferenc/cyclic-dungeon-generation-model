using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Themes.Types;

public interface IThemeType
{
    void Apply(Graph graph);
}