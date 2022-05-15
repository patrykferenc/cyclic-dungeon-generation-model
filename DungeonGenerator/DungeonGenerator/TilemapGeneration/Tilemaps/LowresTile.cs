using DungeonGenerator.DungeonGenerator.GraphGeneration.Characteristics;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Characteristics.Gates;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps;

public class LowresTile
{
    private readonly List<Key> _keys;

    private readonly List<Lock> _locks;
    private readonly List<Obstacle> _obstacles;

    private readonly (int x, int y) _position;
    private CoreGameplayElement _coreRoomFunction = CoreGameplayElement.None;
    private readonly LowresTileType _type;

    public LowresTile(LowresTileType type, (int x, int y) position, List<Lock> locks, List<Key> keys,
        List<Obstacle> obstacles)
    {
        _type = type;
        _position = position;
        _locks = locks;
        _keys = keys;
        _obstacles = obstacles;
    }

    public LowresTile(LowresTileType type, (int x, int y) position)
    {
        _type = type;
        _position = position;
        _obstacles = new List<Obstacle>();
        _keys = new List<Key>();
        _locks = new List<Lock>();
    }

    public LowresTileType GetTileType()
    {
        return _type;
    }
}