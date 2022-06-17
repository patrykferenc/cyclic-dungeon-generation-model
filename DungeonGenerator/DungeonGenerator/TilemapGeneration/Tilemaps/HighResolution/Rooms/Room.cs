using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;

public class Room : BaseArea
{
    private readonly List<Tile> _tiles;

    public Room((int x, int y) position, List<Tile> tiles, LowResTile lowResTile, RoomType roomType) : base(position,
        lowResTile)
    {
        _tiles = tiles;
        RoomType = roomType;
    }

    public RoomType RoomType { get; }

    public override List<Tile> GetTiles()
    {
        return _tiles;
    }
}