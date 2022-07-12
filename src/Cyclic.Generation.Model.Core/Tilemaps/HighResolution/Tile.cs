using System.Text;
using Cyclic.Generation.Model.Core.Common;

namespace Cyclic.Generation.Model.Core.Tilemaps.HighResolution;

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