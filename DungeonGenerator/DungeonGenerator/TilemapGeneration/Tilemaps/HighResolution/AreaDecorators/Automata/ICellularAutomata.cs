namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.HighResolution.AreaDecorators.Automata;

public interface ICellularAutomata
{
    AutomataTile[,] GetFinalState(int iterations);

    (int x, int y) GetOffset();
}