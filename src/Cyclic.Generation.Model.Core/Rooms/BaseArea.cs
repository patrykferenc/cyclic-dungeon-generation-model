using Cyclic.Generation.Model.Core.Tilemaps.HighResolution;
using Cyclic.Generation.Model.Core.Tilemaps.LowResolution;

namespace Cyclic.Generation.Model.Core.Rooms;

public abstract class BaseArea
{
    private readonly List<BaseArea> _connectedAreas;

    private readonly LowResTile _lowResTile;

    private readonly (int x, int y) _position;

    protected BaseArea((int x, int y) position, LowResTile lowResTile)
    {
        _position = position;
        _lowResTile = lowResTile;
        _connectedAreas = new List<BaseArea>();
    }

    public abstract List<Tile> GetTiles();

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