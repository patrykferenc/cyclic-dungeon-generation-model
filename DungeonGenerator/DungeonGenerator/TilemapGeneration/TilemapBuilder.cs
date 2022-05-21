using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration;

public class TilemapBuilder
{
    private readonly Graph _graph;
    private readonly LowResTilemap _lowResTilemap;
    private readonly Tilemap _tilemap;

    public TilemapBuilder(Graph graph, LowResTilemap tilemap)
    {
        _graph = graph;
        _lowResTilemap = tilemap;
        _tilemap = new Tilemap(55, 55);
    }

    public Tilemap Generate()
    {
        // turn grid to 5x5
        TurnLowResGridToTilemap();
        // shrink doors to one tile
        
        // adjust room shapes to not be rectangular
        
        return _tilemap;
    }

    private void TurnLowResGridToTilemap()
    {
        var lrTilemapDimensions = _lowResTilemap.GetDimensions();
        
        for (int lrTilemapY = 1, tileY = 1; lrTilemapY < lrTilemapDimensions.y; lrTilemapY++, tileY += 5)
        {
            for (int lrTilemapX = 1, tileX = 1; lrTilemapX < lrTilemapDimensions.x; lrTilemapX++, tileX += 5)
            {
                var lrTile = _lowResTilemap.GetTile((lrTilemapX, lrTilemapY));
                
                if (lrTile.GetTileType() == LowresTileType.Empty)
                    continue;
                
                FillSpaceWithElements(tileX, tileY, lrTile);
            }
        }
    }

    private void FillSpaceWithElements(int tileX, int tileY, LowResTile lrTile)
    {
        const int size = 5;
        for (var iterationsY = 0; iterationsY < size; iterationsY++)
            for (var iterationsX = 0; iterationsX < size; iterationsX++)
            {
                var position = (x: tileX + iterationsX, y: tileY + iterationsY);
                _tilemap.SetTile(position,
                    lrTile.GetTileType() == LowresTileType.Door
                        ? new Tile(position: position, type: TileType.Door)
                        : new Tile(position: position, type: TileType.Space));
            }
    }
}