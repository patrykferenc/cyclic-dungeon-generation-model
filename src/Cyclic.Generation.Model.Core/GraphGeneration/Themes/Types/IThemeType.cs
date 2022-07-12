using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Themes.Types;

public interface IThemeType
{
    void Apply(Graph graph);
}