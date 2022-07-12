using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Variants;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Building;

public class CycleInventor
{
    private readonly ICycleVariant _variant;

    public CycleInventor(ICycleVariant variant)
    {
        _variant = variant;
    }

    public void InventCycle()
    {
        _variant.Generate();
    }
}