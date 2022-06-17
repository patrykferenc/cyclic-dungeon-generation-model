namespace DungeonGenerator.DungeonGenerator.Utils;

public class ExpandedRandom
{
    private readonly Random _random;

    public ExpandedRandom(int seed)
    {
        _random = new Random(seed);
    }

    public ExpandedRandom()
    {
        _random = new Random();
    }

    public int Next(int min, int max)
    {
        return _random.Next(min, max);
    }

    public int Next(int max)
    {
        return _random.Next(max);
    }

    public bool NextBool()
    {
        return _random.Next(0, 2) == 1;
    }

    public bool NextBoolChance(int chance)
    {
        return _random.Next(0, 100) < chance;
    }

    public double NextGaussian(double mu = 0, double sigma = 1)
    {
        var u1 = _random.NextDouble();
        var u2 = _random.NextDouble();

        var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

        var randNormal = mu + sigma * randStdNormal;

        return randNormal;
    }
}