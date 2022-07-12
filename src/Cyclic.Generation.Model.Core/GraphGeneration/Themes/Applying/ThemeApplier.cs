using Cyclic.Generation.Model.Core.GraphGeneration.Themes.Types;
using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Themes.Applying;

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