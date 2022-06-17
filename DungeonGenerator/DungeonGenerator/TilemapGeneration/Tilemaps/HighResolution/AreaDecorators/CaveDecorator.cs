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
        
        SortTiles();
        var tiles = CreateRoomTiles();
        // write the tiles in the 2D matrix to the console
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (var j = 0; j < tiles.GetLength(1); j++)
            {
                Console.Write((char)tiles[i, j]);
            }
            Console.WriteLine();
        }

    }
    
    // Sort the list of roomTiles by their positions (x,y)
    private void SortTiles()
    {
        _roomTiles.Sort((a, b) => a.GetPosition().CompareTo(b.GetPosition()));
    }
    
    // Get max x and y values of the roomTiles
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
    
    // Get min x and y values of the roomTiles
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
    
    // Get dimensions based on min and max x and y values
    private (int width, int height) GetDimensions()
    {
        var min = GetMinXAndY();
        var max = GetMaxXAndY();
        var width = max.maxX - min.minX + 1;
        var height = max.maxY - min.minY + 1;
        return (width, height);
    }

    // Create a 2D list of tiles that are in the room using their position
    private AutomataTile[,] CreateRoomTiles()
    {
        
        var dimensions = GetDimensions();
        var offset = GetMinXAndY();
        var roomTiles2D = new AutomataTile[dimensions.height,dimensions.width];
        //Console.WriteLine(dimensions);
        // make all roomTiles2D tiles empty
        for (var i = 0; i < dimensions.height; i++)
        {
            for (var j = 0; j < dimensions.width; j++)
            {
                roomTiles2D[i, j] = AutomataTile.Wall;
            }
        }
        
        
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
        Floor = '0',
        Blocked
    }
}