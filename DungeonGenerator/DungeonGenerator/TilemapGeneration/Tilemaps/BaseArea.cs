using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public abstract class BaseArea
{

    private readonly (int x, int y) _position;

    private readonly LowResTile _lowResTile;

    private readonly List<BaseArea> _connectedAreas;

    public abstract List<Tile> GetTiles();
    
    protected BaseArea((int x, int y) position, LowResTile lowResTile)
    {
        _position = position;
        _lowResTile = lowResTile;
        _connectedAreas = new List<BaseArea>();
    }

    public void Connect(BaseArea area)
    {
        _connectedAreas.Add(area);
    }
    
    public List<BaseArea> GetConnectedAreas()
    {
        return _connectedAreas;
    }
    
    public (int x, int y) GetPosition()
    {
        return _position;
    }
    
    public LowResTile GetLowResTile()
    {
        return _lowResTile;
    }

}