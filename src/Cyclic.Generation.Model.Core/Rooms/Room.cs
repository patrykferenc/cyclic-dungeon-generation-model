using Cyclic.Generation.Model.Core.Tilemaps.HighResolution;
using Cyclic.Generation.Model.Core.Tilemaps.LowResolution;

namespace Cyclic.Generation.Model.Core.Rooms;

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