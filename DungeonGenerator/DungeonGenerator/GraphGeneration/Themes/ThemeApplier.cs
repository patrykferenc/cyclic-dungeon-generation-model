using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Themes.Types;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Themes;

public class ThemeApplier
{
    private readonly IThemeType _themeType;
    
    public ThemeApplier(IThemeType themeType)
    {
        _themeType = themeType;
    }
    
    public void ApplyTheme(Graph graph)
    {
        _themeType.Apply(graph);
    }
}