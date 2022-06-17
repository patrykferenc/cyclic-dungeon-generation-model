using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;
using DungeonGenerator.DungeonGenerator.Utils;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.AreaDecorators.Automata;

public class Automaton : ICellularAutomata
{
    private const int Density = 55; // 0-100, around half is best

    // Moore's neighborhood
    private static readonly (int x, int y)[] Neighbourhood =
    {
        (0, 1),
        (1, 1),
        (1, 0),
        (1, -1),
        (0, -1),
        (-1, -1),
        (-1, 0),
        (-1, 1)
    };

    private readonly ExpandedRandom _random;

    private readonly List<Tile> _tiles;

    public Automaton(List<Tile> tiles)
    {
        _tiles = tiles;
        _random = new ExpandedRandom();
    }

    public AutomataTile[,] GetFinalState(int iterations)
    {
        var tiles = CreateRoomTiles();


        for (var count = 0; count < iterations; count++) ApplyAutomaton(tiles);

        return tiles;
    }

    public (int x, int y) GetOffset()
    {
        return GetMinXAndY();
    }

    private (int maxX, int maxY) GetMaxXAndY()
    {
        var maxX = 0;
        var maxY = 0;
        foreach (var tile in _tiles)
        {
            if (tile.GetPosition().x > maxX) maxX = tile.GetPosition().x;
            if (tile.GetPosition().y > maxY) maxY = tile.GetPosition().y;
        }

        return (maxX, maxY);
    }

    private (int minX, int minY) GetMinXAndY()
    {
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        foreach (var tile in _tiles)
        {
            if (tile.GetPosition().x < minX) minX = tile.GetPosition().x;
            if (tile.GetPosition().y < minY) minY = tile.GetPosition().y;
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
        var roomTiles2D = new AutomataTile[dimensions.height, dimensions.width];

        // First initialise the matrix with wall tiles.
        for (var y = 0; y < dimensions.height; y++)
            for (var x = 0; x < dimensions.width; x++)
                roomTiles2D[y, x] = AutomataTile.Wall;

        // Assign the tile in the correct position.
        foreach (var tile in _tiles)
        {
            var tilePosition = (x: tile.GetPosition().x - offset.minX, y: tile.GetPosition().y - offset.minY);

            if (_random.NextBoolChance(Density))
                roomTiles2D[tilePosition.y, tilePosition.x] = AutomataTile.Floor;
        }

        return roomTiles2D;
    }

    private void ApplyAutomaton(AutomataTile[,] tiles)
    {
        var temporaryTiles = (AutomataTile[,])tiles.Clone();

        var width = tiles.GetLength(1);
        var height = tiles.GetLength(0);

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var aliveNeighbours = GetNumberOfNeighbours(temporaryTiles, (x, y));
                if (aliveNeighbours > 3)
                    tiles[y, x] = AutomataTile.Floor;
                else
                    tiles[y, x] = AutomataTile.Wall;
            }
    }

    private int GetNumberOfNeighbours(AutomataTile[,] tiles, (int x, int y) position)
    {
        var width = tiles.GetLength(1);
        var height = tiles.GetLength(0);
        var numberOfNeighbours = 0;

        foreach (var (xOffset, yOffset) in Neighbourhood)
        {
            var neighbourPosition = (x: position.x + xOffset, y: position.y + yOffset);

            if (neighbourPosition.x < 0 || neighbourPosition.x >= width || neighbourPosition.y < 0 ||
                neighbourPosition.y >= height)
                continue;

            if (tiles[neighbourPosition.y, neighbourPosition.x] == AutomataTile.Floor) numberOfNeighbours++;
        }

        return numberOfNeighbours;
    }
}