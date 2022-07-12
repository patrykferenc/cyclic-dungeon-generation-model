using Cyclic.Generation.Model.Core.Common;
using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Building;
using Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Decorating;
using Cyclic.Generation.Model.Core.GraphGeneration.Themes.Applying;
using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Building;

public class GraphBuilder
{
    private readonly Graph _generatedGraph;
    private readonly DungeonTheme _theme;

    public GraphBuilder(int nodesHeight, int nodesWidth, DungeonTheme theme)
    {
        _generatedGraph = new Graph(nodesHeight, nodesWidth);
        _theme = theme;
    }

    public Graph Generate()
    {
        GenerateBaseCycle();

        DecorateCycle();
        
        ApplyTheme();

        return _generatedGraph;
    }

    private void GenerateBaseCycle()
    {
        var builder = CycleInventorBuilder.Build(_generatedGraph);

        builder.InventCycle();

        //Console.Write("After generating cycle: \n" + _generatedGraph + '\n'); // Debug!
    }

    private void DecorateCycle()
    {
        var decorator = CycleDecoratorBuilder.Build(_generatedGraph);

        decorator.DecorateCycle(_generatedGraph);

        //Console.Write("After decorating cycle: \n" + _generatedGraph + '\n'); // Debug!
    }
    
    private void ApplyTheme()
    {
        var theme = ThemeApplierBuilder.Build(_theme);

        theme.ApplyTheme(_generatedGraph);
    }
}