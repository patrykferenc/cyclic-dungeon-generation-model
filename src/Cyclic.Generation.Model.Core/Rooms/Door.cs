using Cyclic.Generation.Model.Core.Tilemaps.HighResolution;
using Cyclic.Generation.Model.Core.Tilemaps.LowResolution;

namespace Cyclic.Generation.Model.Core.Rooms;

public class Door : BaseArea
{
    private readonly List<Tile> _tiles;

    public Door((int x, int y) position, List<Tile> tiles, LowResTile lowResTile) : base(position, lowResTile)
    {
        _tiles = tiles;
    }

    public override List<Tile> GetTiles()
    {
        return _tiles;
    }
}