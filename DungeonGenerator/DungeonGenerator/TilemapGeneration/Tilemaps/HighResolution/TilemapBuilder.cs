using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.AreaDecorators;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution;

public class TilemapBuilder
{
    private const int SizeMultiplier = 7; // This works best if it is an odd number!
    private readonly List<BaseArea> _areas;

    private readonly LowResTilemap _lowResTilemap;
    private readonly Tilemap _tilemap;

    public TilemapBuilder(LowResTilemap tilemap)
    {
        _lowResTilemap = tilemap;

        var lrDimensions = _lowResTilemap.GetDimensions();
        _tilemap = new Tilemap(lrDimensions.y * SizeMultiplier, lrDimensions.x * SizeMultiplier);

        _areas = new List<BaseArea>();
    }

    public Tilemap Generate()
    {
        TurnLowResGridToTilemap();

        AddNeighbouringAreas();

        FixDoorSpaces();

        DecorateRooms();

        return _tilemap;
    }

    private void DecorateRooms()
    {
        foreach (var area in _areas.Where(area => area is not Door))
        {
            if (area is not Room room) continue; // This should never happen, but just in case.
            if (room.RoomType != RoomType.Cave) continue; // For now we only decorate caves.
            
            var decorator = new CaveDecorator(area);
            decorator.Decorate();
        }
    }

    private void TurnLowResGridToTilemap()
    {
        var lrTilemapDimensions = _lowResTilemap.GetDimensions();

        for (int lrTilemapY = 1, tileY = 1; lrTilemapY < lrTilemapDimensions.y; lrTilemapY++, tileY += SizeMultiplier)
            for (int lrTilemapX = 1, tileX = 1; lrTilemapX < lrTilemapDimensions.x; lrTilemapX++, tileX += SizeMultiplier)
                AddRoomFromLowResGrid(lrTilemapX, lrTilemapY, tileX, tileY);
    }

    private void AddRoomFromLowResGrid(int lrTilemapX, int lrTilemapY, int tileX, int tileY)
    {
        var lrTile = _lowResTilemap.GetTile((lrTilemapX, lrTilemapY));

        if (lrTile.GetTileType() == LowResolutionTileType.Empty)
            return;

        var tilesInSpace = FillSpaceWithElements(tileX, tileY, lrTile);

        var middlePosition = (x: tileX + SizeMultiplier / 2, y: tileY + SizeMultiplier / 2);

        if (lrTile.GetTileType() == LowResolutionTileType.Room)
            _areas.Add(new Room(middlePosition, tilesInSpace, lrTile, lrTile.GetRoomType()));
        else if (lrTile.GetTileType() == LowResolutionTileType.Door)
            _areas.Add(new Door(middlePosition, tilesInSpace, lrTile));
    }

    private void AddNeighbouringAreas()
    {
        foreach (var area in _areas)
        {
            var lrNeighbours = _lowResTilemap.GetNeighbours(area.GetLowResTile());
            foreach (var lrNeighbour in lrNeighbours)
            {
                var neighbour = _areas.Find(a => a.GetLowResTile() == lrNeighbour);
                if (neighbour != null) area.Connect(neighbour);
            }
        }
    }

    private void FixDoorSpaces()
    {
        foreach (var area in _areas)
        {
            if (area is not Door door) continue;
            var deletedTiles = ShrinkDoorSpaceToOneDoor(door);
            AddRemovedTilesToNeighbouringRooms(deletedTiles, door);
        }
    }

    // TODO: This could use some refactoring...
    private void AddRemovedTilesToNeighbouringRooms(List<Tile> deletedTiles, BaseArea door)
    {
        var connectedRooms = door.GetConnectedAreas();
        var connectedRoomAbove = connectedRooms.Find(r => r.GetPosition().y < door.GetPosition().y);
        var connectedRoomBelow = connectedRooms.Find(r => r.GetPosition().y > door.GetPosition().y);
        var connectedRoomLeft = connectedRooms.Find(r => r.GetPosition().x < door.GetPosition().x);
        var connectedRoomRight = connectedRooms.Find(r => r.GetPosition().x > door.GetPosition().x);

        // Need to test this
        foreach (var tile in deletedTiles)
            if (connectedRoomAbove != null && tile.GetPosition().y < door.GetPosition().y)
            {
                tile.SetTileType(TileType.Space);
                connectedRoomAbove.GetTiles().Add(tile);
            }
            else if (connectedRoomBelow != null && tile.GetPosition().y > door.GetPosition().y)
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

    private static List<Tile> ShrinkDoorSpaceToOneDoor(BaseArea doorSpace)
    {
        var doorTilesToRemove = new List<Tile>();
        foreach (var tile in doorSpace.GetTiles())
        {
            if (tile.GetPosition() == doorSpace.GetPosition()) continue;
            tile.SetTileType(TileType.Empty);
            doorTilesToRemove.Add(tile);
        }

        doorSpace.GetTiles().RemoveAll(doorTilesToRemove.Contains);
        return doorTilesToRemove;
    }

    private List<Tile> FillSpaceWithElements(int tileX, int tileY, LowResTile lrTile)
    {
        var tiles = new List<Tile>();

        for (var iterationsY = 0; iterationsY < SizeMultiplier; iterationsY++)
        for (var iterationsX = 0; iterationsX < SizeMultiplier; iterationsX++)
        {
            var position = (x: tileX + iterationsX, y: tileY + iterationsY);

            _tilemap.SetTile(position,
                lrTile.GetTileType() == LowResolutionTileType.Door
                    ? new Tile(position: position, type: TileType.Door)
                    : new Tile(position: position, type: TileType.Space));

            var tile = _tilemap.GetTile(position);
            tiles.Add(tile);
        }

        return tiles;
    }
}