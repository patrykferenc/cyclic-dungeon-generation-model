using System.Text;
using DungeonGenerator.DungeonGenerator.DungeonElements;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.TilemapGeneration.Tilemaps.LowResolution;

public class LowResTilemap : BaseGrid
{
    public LowResTilemap(int height, int width) : base(height, width)
    {
        var tilemapDimensions = GetDimensions();

        for (var y = 0; y < tilemapDimensions.y; y++)
            for (var x = 0; x < tilemapDimensions.x; x++)
                Grid[y, x] = new LowResTile(LowresTileType.Empty, (x, y));
    }
    
    public void MapGraphToTilemap(Graph graph)
    {
        var graphDimensions = graph.GetDimensions();
        
        for (int graphY = 0, tileY = 1; graphY < graphDimensions.y; graphY++, tileY += 2)
        {
            for (int graphX = 0, tileX = 1; graphX < graphDimensions.x; graphX++, tileX += 2)
            {
                AddCorrectTileInPlace(graphX, graphY, tileX, tileY);
                AddDoorsToRightNeighbour(graphX, graphY, tileX, tileY);
                AddDoorsToLowerNeighbour(graphY, graphX, tileX, tileY);
            }
        }

        void AddDoorsToLowerNeighbour(int graphY, int graphX, int tileX, int tileY)
        {
            if (graphY >= graphDimensions.y - 1 
                || !NodeGrid.AreNodesConnected(graph.GetNode((graphX, graphY)), graph.GetNode((graphX, graphY + 1)))) 
                return;
            
            var doorTile = new LowResTile(LowresTileType.Door, (x: tileX, y: tileY + 1));
            Grid[tileY + 1, tileX] = doorTile;
        }

        void AddDoorsToRightNeighbour(int graphX, int graphY, int tileX, int tileY)
        {
            if (graphX >= graphDimensions.x - 1 
                || !NodeGrid.AreNodesConnected(graph.GetNode((graphX, graphY)), graph.GetNode((graphX + 1, graphY))))
                return;
            
            var doorTile = new LowResTile(LowresTileType.Door, (x: tileX + 1, y: tileY));
            Grid[tileY, tileX + 1] = doorTile;
        }

        void AddCorrectTileInPlace(int graphX, int graphY, int tileX, int tileY)
        {
            var tile = new LowResTile(graph.GetNode((graphX, graphY)), (tileX, tileY));
            Grid[tileY, tileX] = tile;
        }
    }

    public LowResTile GetTile((int x, int y) position)
    {
        return (LowResTile)GetElement(position);
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        var dimensions = GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
        {
            for (var x = 0; x < dimensions.x; x++)
                sb.Append((char) ((LowResTile)Grid[y, x]).GetTileType());
            sb.Append('\n');
        }

        return sb.ToString();
    }
}