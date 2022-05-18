using System.Text;
using DungeonGenerator.DungeonGenerator.DungeonElements;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

public class NodeGrid : BaseGrid
{
    public NodeGrid(int height, int width) : base(height, width)
    {
        var dimensions = GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
            for (var x = 0; x < dimensions.x; x++)
                Grid[y, x] = new Node(NodeType.Undecided, (x, y));
    }

    public Node GetNode((int x, int y) position)
    {
        return (Node)GetElement(position);
    }

    public List<Node> ToList()
    {
        return ToBaseList().Cast<Node>().ToList();
    }

    public static bool AreNodesConnected(Node firstNode, Node secondNode)
    {
        return firstNode.GetNeighbours().Contains(secondNode);
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        var dimensions = GetDimensions();

        for (var y = 0; y < dimensions.y; y++)
        {
            for (var x = 0; x < dimensions.x; x++)
            {
                sb.Append((char)((Node)Grid[y, x]).GetNodeType());
                if (x < dimensions.x - 1)
                    sb.Append(AreNodesConnected((Node)Grid[y, x], (Node)Grid[y, x + 1]) ? '-' : ' ');
            }

            sb.Append('\n');
            if (y < dimensions.y - 1)
                for (var x = 0; x < dimensions.x; x++)
                    sb.Append(AreNodesConnected((Node)Grid[y, x], (Node)Grid[y + 1, x]) ? "| " : "  ");
            sb.Append('\n');
        }

        return sb.ToString();
    }
}