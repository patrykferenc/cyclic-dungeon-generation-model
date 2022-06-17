using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.AreaDecorators;

public class CaveDecorator : IAreaDecorator
{
    private readonly List<Tile> _roomTiles;

    public CaveDecorator(List<Tile> roomTiles)
    {
        _roomTiles = roomTiles;
    }
    
    public void Decorate()
    {
        var tiles = CreateRoomTiles();
        
        // DEBUG: write the tiles in the 2D matrix to the console
        PrintRoom(tiles);
    }

    // DEBUG: Display helper method
    private static void PrintRoom(AutomataTile[,] tiles)
    {
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                Console.Write((char)tiles[i, j]);
            }

            Console.WriteLine();
        }
    }

    private (int maxX, int maxY) GetMaxXAndY()
    {
        var maxX = 0;
        var maxY = 0;
        foreach (var tile in _roomTiles)
        {
            if (tile.GetPosition().x > maxX)
            {
                maxX = tile.GetPosition().x;
            }
            if (tile.GetPosition().y > maxY)
            {
                maxY = tile.GetPosition().y;
            }
        }
        return (maxX, maxY);
    }
    
    private (int minX, int minY) GetMinXAndY()
    {
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        foreach (var tile in _roomTiles)
        {
            if (tile.GetPosition().x < minX)
            {
                minX = tile.GetPosition().x;
            }
            if (tile.GetPosition().y < minY)
            {
                minY = tile.GetPosition().y;
            }
        }
        return (minX, minY);
    }
    
    private (int width, int height) GetDimensions()
    {
        var min = GetMinXAndY();
        var max = GetMaxXAndY();
        var width = max.maxX - min.minX + 1;
        var height = max.maxY - min.minY + 1;
        return (width, height);
    }

    private AutomataTile[,] CreateRoomTiles()
    {
        var dimensions = GetDimensions();
        var offset = GetMinXAndY();
        var roomTiles2D = new AutomataTile[dimensions.height,dimensions.width];

        // First initialise the matrix with wall tiles.
        for (var i = 0; i < dimensions.height; i++)
        {
            for (var j = 0; j < dimensions.width; j++)
            {
                roomTiles2D[i, j] = AutomataTile.Wall;
            }
        }

        // Assign the tile in the correct position.
        foreach (var tile in _roomTiles)
        {
            var tilePosition = (x: tile.GetPosition().x - offset.minX, y: tile.GetPosition().y - offset.minY);
            
            roomTiles2D[tilePosition.y, tilePosition.x] = AutomataTile.Floor;
        }

        return roomTiles2D;
    }

    private enum AutomataTile
    {
        Wall = '#',
        Floor = '0'
    }
}