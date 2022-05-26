using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration;

public class TilemapBuilder
{
    private readonly Graph _graph;
    private readonly LowResTilemap _lowResTilemap;
    private readonly Tilemap _tilemap;
    private readonly List<BaseArea> _areas;

    public TilemapBuilder(Graph graph, LowResTilemap tilemap)
    {
        _graph = graph;
        _lowResTilemap = tilemap;
        _tilemap = new Tilemap(55, 55);
        _areas = new List<BaseArea>();
    }

    public Tilemap Generate()
    {
        // turn grid to 5x5
        TurnLowResGridToTilemap();
        // Console.WriteLine("Tilemap just generated: \n" + _tilemap);
        // shrink doors to one tile
        AddNeighbouringAreas();
        // foreach (var area in _areas)
        // {
        //     Console.WriteLine("Area: " + area.GetPosition());
        //     foreach (var neighbours in area.GetConnectedAreas())
        //     {
        //         Console.WriteLine("nb: " + neighbours.GetPosition());
        //     }
        // }
        FixDoorSpaces();
        
        return _tilemap;
    }

    private void TurnLowResGridToTilemap()
    {
        var lrTilemapDimensions = _lowResTilemap.GetDimensions();
        
        for (int lrTilemapY = 1, tileY = 1; lrTilemapY < lrTilemapDimensions.y; lrTilemapY++, tileY += 5)
        {
            for (int lrTilemapX = 1, tileX = 1; lrTilemapX < lrTilemapDimensions.x; lrTilemapX++, tileX += 5)
            {
                AddRoomFromLowResGrid(lrTilemapX, lrTilemapY, tileX, tileY);
            }
        }
    }

    private void AddRoomFromLowResGrid(int lrTilemapX, int lrTilemapY, int tileX, int tileY)
    {
        var lrTile = _lowResTilemap.GetTile((lrTilemapX, lrTilemapY));

        if (lrTile.GetTileType() == LowresTileType.Empty)
            return;

        var tilesInSpace = FillSpaceWithElements(tileX, tileY, lrTile);
        
        var middlePosition = (x: tileX + 2, y: tileY + 2);
        
        if (lrTile.GetTileType() == LowresTileType.Room)
        {
            _areas.Add(new Room(middlePosition, tilesInSpace, lrTile));
        }
        else if (lrTile.GetTileType() == LowresTileType.Door)
        {
            _areas.Add(new Door(middlePosition, tilesInSpace, lrTile));
        }
    }

    private void AddNeighbouringAreas()
    {
        foreach (var area in _areas)
        {
            var lrNeighbours = _lowResTilemap.GetNeighbours(area.GetLowResTile());
            foreach (var lrNeighbour in lrNeighbours)
            {
                var neighbour = _areas.Find(a => a.GetLowResTile() == lrNeighbour);
                if (neighbour != null)
                {
                    area.Connect(neighbour);
                }
            }
        }
    }

    private void FixDoorSpaces()
    {
        foreach (var area in _areas)
        {
            if (area is Door door)
            {
                var deletedTiles = ShrinkDoorSpaceToOneDoor(door);
                AddRemovedTilesToNeighbouringRooms(deletedTiles, door);
            }
            
        }
    }
    
    private void AddRemovedTilesToNeighbouringRooms(List<Tile> deletedTiles, Door door)
    {
        var connectedRooms = door.GetConnectedAreas();
        var connectedRoomAbove = connectedRooms.Find(r => r.GetPosition().y < door.GetPosition().y);
        var connectedRoomBelow = connectedRooms.Find(r => r.GetPosition().y > door.GetPosition().y);
        var connectedRoomLeft = connectedRooms.Find(r => r.GetPosition().x < door.GetPosition().x);
        var connectedRoomRight = connectedRooms.Find(r => r.GetPosition().x > door.GetPosition().x);

        foreach (var tile in deletedTiles)
        {
            if (connectedRoomAbove != null && tile.GetPosition().y > door.GetPosition().y)
            {
                tile.SetTileType(TileType.Space);
                connectedRoomAbove.GetTiles().Add(tile);
            }
            else if (connectedRoomBelow != null && tile.GetPosition().y < door.GetPosition().y)
            {
                tile.SetTileType(TileType.Space);
                connectedRoomBelow.GetTiles().Add(tile);
            }
            else if (connectedRoomLeft != null && tile.GetPosition().x < door.GetPosition().x)
            {
                tile.SetTileType(TileType.Space);
                connectedRoomLeft.GetTiles().Add(tile);
            }
            else if (connectedRoomRight != null && tile.GetPosition().x > door.GetPosition().x)
            {
                tile.SetTileType(TileType.Space);
                connectedRoomRight.GetTiles().Add(tile);
            }
        }
    }

    private static List<Tile> ShrinkDoorSpaceToOneDoor(Door doorSpace)
    {
        var doorTilesToRemove = new List<Tile>();
        foreach (var tile in doorSpace.GetTiles())
        {
            if (tile.GetPosition() != doorSpace.GetPosition())
            {
                tile.SetTileType(TileType.Empty);
                doorTilesToRemove.Add(tile);
            }
        }
        doorSpace.GetTiles().RemoveAll(doorTilesToRemove.Contains);
        return doorTilesToRemove;
    }
    
    private List<Tile> FillSpaceWithElements(int tileX, int tileY, LowResTile lrTile)
    {
        var tiles = new List<Tile>();
        
        const int size = 5;
        for (var iterationsY = 0; iterationsY < size; iterationsY++)
            for (var iterationsX = 0; iterationsX < size; iterationsX++)
            {
                var position = (x: tileX + iterationsX, y: tileY + iterationsY);
                
                _tilemap.SetTile(position,
                    lrTile.GetTileType() == LowresTileType.Door
                        ? new Tile(position: position, type: TileType.Door)
                        : new Tile(position: position, type: TileType.Space));
                
                var tile = _tilemap.GetTile(position);
                tiles.Add(tile);
            }

        return tiles;
    }
}