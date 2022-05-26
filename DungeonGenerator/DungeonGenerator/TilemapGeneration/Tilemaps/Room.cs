using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public class Room : BaseArea
{
    
    private readonly List<Tile> _tiles;

    public Room((int x, int y) position, LowResTile lowResTile) : base(position, lowResTile)
    {
        _tiles = new List<Tile>();
    }

    public Room((int x, int y) position ,List<Tile> tiles, LowResTile lowResTile) : base(position, lowResTile)
    {
        _tiles = tiles;
    }

    public override List<Tile> GetTiles()
    {
        return _tiles;
    }
    
    
    
}