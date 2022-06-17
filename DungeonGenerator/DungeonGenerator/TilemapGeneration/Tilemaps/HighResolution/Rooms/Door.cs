using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;

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