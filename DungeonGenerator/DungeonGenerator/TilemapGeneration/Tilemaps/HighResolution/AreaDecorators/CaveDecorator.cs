using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.AreaDecorators.Automata;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Rooms;
using DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.Tiles;
using DungeonGenerator.DungeonGenerator.Utils;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.AreaDecorators;

public class CaveDecorator : IAreaDecorator
{
    private const int MinIterations = 2; // Best over 2
    private const int MaxIterations = 6; // The higher, the higher chance of smooth caves
    private readonly ICellularAutomata _cellularAutomata;

    private readonly BaseArea _myArea;

    private readonly ExpandedRandom _random;

    public CaveDecorator(BaseArea area)
    {
        _myArea = area;
        _random = new ExpandedRandom();
        _cellularAutomata = new Automaton(area.GetTiles());
    }

    public void Decorate()
    {
        var iterations = _random.Next(MinIterations, MaxIterations);
        var generatedTileShape = _cellularAutomata.GetFinalState(iterations);
        ApplyAutomataChanges(generatedTileShape);
    }

    private void EnsureRoomStaysConnected()
    {
        foreach (var area in _myArea.GetConnectedAreas())
        {
            var closestTile = _myArea.GetTiles().OrderBy(tile =>
                    Math.Abs(tile.GetPosition().x - area.GetPosition().x) +
                    Math.Abs(tile.GetPosition().y - area.GetPosition().y))
                .First();

            var startingTile = _myArea.GetTiles().Find(tile => tile.GetPosition() == _myArea.GetPosition());

            if (closestTile == null || startingTile == null)
                throw new Exception("Could not find closest tile or starting tile!");

            var path = FindPath(startingTile, closestTile);

            foreach (var tile in path) tile.SetTileType(TileType.Space);
        }
    }

    private void ApplyAutomataChanges(AutomataTile[,] automataTiles)
    {
        for (var y = 0; y < automataTiles.GetLength(0); y++)
            for (var x = 0; x < automataTiles.GetLength(1); x++)
            {
                var offset = _cellularAutomata.GetOffset();
                var roomTile = _myArea.GetTiles().Find(tile => tile.GetPosition() == (x + offset.x, y + offset.y));

                roomTile?.SetTileType(automataTiles[y, x] == AutomataTile.Floor ? TileType.Space : TileType.Empty);
            }

        EnsureRoomStaysConnected();
        _myArea.GetTiles().RemoveAll(r => r.GetTileType() == TileType.Empty); // Cleaning up room tiles
    }

    private List<Tile> FindPath(Tile start, Tile end)
    {
        (int x, int y)[] myNeighbourhood = { (-1, 0), (0, -1), (1, 0), (0, 1) };

        var previous = new Dictionary<Tile, Tile?>();
        var distances = new Dictionary<Tile, int> { { start, 0 } };

        var queue = new PriorityQueue<Tile, int>();
        foreach (var tile in _myArea.GetTiles())
        {
            if (tile != start)
            {
                distances.Add(tile, int.MaxValue);
                previous.Add(tile, null);
            }

            queue.Enqueue(tile, distances[tile]);
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end) break;

            foreach (var neighbouring in myNeighbourhood)
            {
                (int x, int y) neighbourPosition = (current.GetPosition().x + neighbouring.x,
                    current.GetPosition().y + neighbouring.y);
                var neighbour = _myArea.GetTiles().Find(tile => tile.GetPosition() == neighbourPosition);
                if (neighbour == null) continue;

                var possibleDistance = neighbour.GetTileType() == TileType.Empty
                    ? distances[current] + 2
                    : distances[current] + 1; // the edge could be of different value

                if (possibleDistance < distances[neighbour] && distances[current] != int.MaxValue)
                {
                    distances[neighbour] = possibleDistance;
                    previous[neighbour] = current;
                    queue.Enqueue(neighbour, possibleDistance);
                }
            }
        }

        var path = new List<Tile>();
        var currentTile = end;
        while (currentTile != start)
        {
            path.Add(currentTile);
            currentTile = previous[currentTile] ?? throw new Exception("No path found");
        }

        return path;
    }
}