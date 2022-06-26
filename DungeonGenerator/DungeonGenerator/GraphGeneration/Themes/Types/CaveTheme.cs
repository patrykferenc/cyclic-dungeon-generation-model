using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Themes.Types;

public class CaveTheme : IThemeType
{
    public void Apply(Graph graph)
    {
        var nodes = graph.GetAllNodes();
        foreach (var node in nodes)
        {
            node.SetRoomType(RoomType.Cave);
        }
    }
}