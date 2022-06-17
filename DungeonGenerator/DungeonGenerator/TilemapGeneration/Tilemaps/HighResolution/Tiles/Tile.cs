using System.Text;
using DungeonGenerator.DungeonGenerator.Characteristics.Gates;
using DungeonGenerator.DungeonGenerator.Characteristics.Obstacles;
using DungeonGenerator.DungeonGenerator.DungeonElements;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;

public class Tile : BaseDungeonElement
{
    private TileType _type;

    public Tile(TileType type, (int x, int y) position) : base(position)
    {
        _type = type;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append((char)_type);
        return sb.ToString();
    }

    public void SetTileType(TileType type)
    {
        _type = type;
    }

    public TileType GetTileType()
    {
        return _type;
    }
}