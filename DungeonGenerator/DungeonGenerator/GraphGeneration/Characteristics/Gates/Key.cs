namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Characteristics.Gates;

public abstract class Key
{
    private readonly Lock _lock;

    public Key(Lock myLock)
    {
        _lock = myLock;
    }

    public Lock getLock()
    {
        return _lock;
    }
}