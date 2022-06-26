using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;
using DungeonGenerator.DungeonGenerator.Utils;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Themes.Types;

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