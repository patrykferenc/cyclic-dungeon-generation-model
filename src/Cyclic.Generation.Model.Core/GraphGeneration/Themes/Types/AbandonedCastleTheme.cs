using Cyclic.Generation.Model.Core.Graphs;
using Cyclic.Generation.Model.Core.Rooms;
using Cyclic.Generation.Model.Core.Utils;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Themes.Types;

public class AbandonedCastleTheme : IThemeType
{
    private const int CaveChance = 40;
    private readonly ExpandedRandom _random;
    
    public AbandonedCastleTheme()
    {
        _random = new ExpandedRandom();
    }

    public void Apply(Graph graph)
    {
        var nodes = graph.GetAllNodes();
        foreach (var node in nodes)
        {
            var chance = _random.NextBoolChance(CaveChance);
            node.SetRoomType(chance ? RoomType.Cave : RoomType.CastleRoom);
        }
    }
}