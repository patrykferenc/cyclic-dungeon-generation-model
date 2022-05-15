namespace DungeonGenerator.DungeonGenerator.DungeonElements;

public abstract class BaseGrid
{
    private readonly int _height;
    private readonly int _width;
    protected readonly BaseDungeonElement[,] Grid;

    protected BaseGrid(int height, int width)
    {
        _height = height;
        _width = width;
        Grid = new BaseDungeonElement[_height, _width];
    }

    protected BaseDungeonElement GetElement((int x, int y) position)
    {
        if (position.x >= Grid.GetLength(1) && position.x < 0 &&
            position.y >= Grid.GetLength(0) && position.y < 0)
            throw new ArgumentOutOfRangeException("There is no node to return at: " + position);
        return Grid[position.y, position.x];
    }

    protected IEnumerable<BaseDungeonElement> ToBaseList()
    {
        List<BaseDungeonElement> elements = new();

        for (var y = 0; y < _height; y++)
            for (var x = 0; x < _width; x++)
                elements.Add(Grid[y, x]);

        return elements;
    }

    public (int x, int y) GetDimensions()
    {
        return (x: _width, y: _height);
    }
}