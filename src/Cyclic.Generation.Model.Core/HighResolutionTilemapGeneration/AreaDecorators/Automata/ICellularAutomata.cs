namespace Cyclic.Generation.Model.Core.HighResolutionTilemapGeneration.AreaDecorators.Automata;

public interface ICellularAutomata
{
    AutomataTile[,] GetFinalState(int iterations);

    (int x, int y) GetOffset();
}