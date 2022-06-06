using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;
using DungeonGenerator.DungeonGenerator.Utils;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Decorating;

public static class CycleDecoratorBuilder
{

    private const int LongLength = 4;
    
    public static CycleDecorator Build(Graph graph)
    {
        var lengths = CycleHelpers.GetCycleLengths(graph);

        if (BothSimilar(lengths.aLength, lengths.bLength))
        {
            return OneShort(lengths.aLength, lengths.bLength)
                ? new CycleDecorator(new CycleTypePatrolling())
                : new CycleDecorator(new CycleTypeAlternate());
        }
        if (OneLong(lengths.aLength, lengths.bLength))
        {
            var selections = new Dictionary<ICycleType, float>
            {
                { new CycleTypeLockKey(), 80f },
                { new CycleTypeBlockingDoor(), 20f }
            };
            var type = selections.RandomElementByWeight(s => s.Value).Key;
            return new CycleDecorator(type);
        }
        
        return new CycleDecorator(new CycleTypeAlternate());
    }
    
    private static bool BothSimilar(int a, int b)
    {
        return Math.Abs(a - b) <= 1;
    }
    
    private static bool OneLong(int a, int b)
    {
        return a >= LongLength || b >= LongLength;
    }
    
    private static bool OneShort(int a, int b)
    {
        return a < LongLength || b < LongLength;
    }
    
}