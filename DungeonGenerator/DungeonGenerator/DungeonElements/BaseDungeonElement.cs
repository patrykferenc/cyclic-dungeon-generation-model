using DungeonGenerator.DungeonGenerator.Characteristics.Gates;
using DungeonGenerator.DungeonGenerator.Characteristics.Obstacles;

namespace DungeonGenerator.DungeonGenerator.DungeonElements;

public abstract class BaseDungeonElement
{
    private readonly List<Key> _keys;
    private readonly List<Lock> _locks;
    private readonly List<Obstacle> _obstacles;

    private readonly (int x, int y) _position;

    protected BaseDungeonElement((int x, int y) position)
    {
        _position = position;
        _keys = new List<Key>();
        _locks = new List<Lock>();
        _obstacles = new List<Obstacle>();
    }

    protected BaseDungeonElement((int x, int y) position, List<Key> keys, List<Lock> locks, List<Obstacle> obstacles)
    {
        _position = position;
        _keys = keys;
        _locks = locks;
        _obstacles = obstacles;
    }

    public (int x, int y) GetPosition()
    {
        return _position;
    }

    public List<Obstacle> GetObstacles()
    {
        return _obstacles;
    }

    public List<Lock> GetLocks()
    {
        return _locks;
    }

    public List<Key> GetKeys()
    {
        return _keys;
    }
}