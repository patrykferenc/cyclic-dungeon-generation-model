using Cyclic.Generation.Model.Core.Gates.Locks;

namespace Cyclic.Generation.Model.Core.Gates.Keys;

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