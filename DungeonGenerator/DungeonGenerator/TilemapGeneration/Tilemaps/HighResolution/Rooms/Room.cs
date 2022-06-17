using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;

public class Room : BaseArea
{
    
    private readonly RoomType _roomType;
    private readonly List<Tile> _tiles;

    public Room((int x, int y) position, LowResTile lowResTile, RoomType roomType) : base(position, lowResTile)
    {
        _roomType = roomType;
        _tiles = new List<Tile>();
    }

    public Room((int x, int y) position ,List<Tile> tiles, LowResTile lowResTile, RoomType roomType) : base(position, lowResTile)
    {
        _tiles = tiles;
        _roomType = roomType;
    }

    public RoomType RoomType => _roomType;
    
    public override List<Tile> GetTiles()
    {
        return _tiles;
    }
    
    
    
}