using System.Text;
using Cyclic.Generation.Model.Core.Common;

namespace Cyclic.Generation.Model.Core.Tilemaps.HighResolution;

public class Tilemap : BaseGrid
{
    public Tilemap(int height, int width) : base(height, width)
    {
        var tilemapDimensions = GetDimensions();

        for (var y = 0; y < tilemapDimensions.y; y++)
            for (var x = 0; x < tilemapDimensions.x; x++)
                Grid[y, x] = new Tile(TileType.Empty, (x, y));
    }

    public Tile GetTile((int x, int y) position)
    {
        return (Tile)GetElement(position);
    }

    public void SetTile((int x, int y) position, Tile tile)
    {
        Grid[position.y, position.x] = tile;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        var dimensions = GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
        {
            for (var x = 0; x < dimensions.x; x++)
                sb.Append(Grid[y, x]);
            sb.Append('\n');
        }

        return sb.ToString();
    }
}