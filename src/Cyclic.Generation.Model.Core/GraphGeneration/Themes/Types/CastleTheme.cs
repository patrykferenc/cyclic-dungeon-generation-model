using Cyclic.Generation.Model.Core.Graphs;
using Cyclic.Generation.Model.Core.Rooms;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Themes.Types;

public class CastleTheme : IThemeType
{
    public void Apply(Graph graph)
    {
        var nodes = graph.GetAllNodes();
        foreach (var node in nodes)
        {
            node.SetRoomType(RoomType.CastleRoom);
        }
    }
}