using System.Text;
using DungeonGenerator.DungeonGenerator.Characteristics.Gates;
using DungeonGenerator.DungeonGenerator.Characteristics.Obstacles;
using DungeonGenerator.DungeonGenerator.DungeonElements;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public class Tile : BaseDungeonElement
{

    private Room _belongingRoom;
    private TileType _type;
    
    public Tile(TileType type, (int x, int y) position) : base(position)
    {
        _type = type;
    }

    public Tile((int x, int y) position,
        List<Key> keys,
        List<Lock> locks,
        List<Obstacle> obstacles,
        List<BaseDungeonElement> adjacent) 
        : base(position, keys, locks, obstacles, adjacent)
    {
        
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