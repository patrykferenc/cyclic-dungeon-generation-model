using System.Text;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs
{
    public class Graph
    {
        // von neumann's neighbourhood
        private static readonly (int x, int y)[] Neighbourhood = { (-1, 0), (0, -1), (1, 0), (0, 1) };

        private readonly Grid _grid;

        public Graph(int nodesHeight, int nodesWidth)
        {
            _grid = new Grid(nodesHeight, nodesWidth);
        }

        public static void AddEdge(Node startNode, Node endNode)
        {
            if (!startNode.GetNeighbours().Contains(endNode))
                startNode.AddNeighbour(endNode);
            if (!endNode.GetNeighbours().Contains(startNode))
                endNode.AddNeighbour(startNode);
        }

        public List<Node> GetNodesInNeighbourhood(Node node)
        {
            return _grid.ToList().FindAll(n => IsPositionInNeighbourhood(n.GetNodeXY(), node.GetNodeXY()));
        }

        public List<Node> GetNodesInNeighbourhood((int x, int y) position)
        {
            return _grid.ToList().FindAll(n => IsPositionInNeighbourhood(n.GetNodeXY(), position));
        }

        public static bool IsNodeInNeighbourhood(Node firstNode, Node secondNode)
        {
            return IsPositionInNeighbourhood(firstNode.GetNodeXY(), secondNode.GetNodeXY());
        }

        public static bool IsPositionInNeighbourhood((int x, int y) firstPosition, (int x, int y) secondPosition)
        {
            for (var i = 0; i < Neighbourhood.Length; i++)
            {
                var positionToCheck = (x: firstPosition.x + Neighbourhood[i].x, y: firstPosition.y + Neighbourhood[i].y);
                if (positionToCheck == secondPosition)
                    return true;
            }

            return false;
        }

        public Node GetNode((int x, int y) position)
        {
            return _grid.GetNode(position);
        }

        public Node? GetFirstNodeOfType(NodeType type)
        {
            return _grid.ToList().Find(n => n.GetNodeType() == type);
        }

        public List<Node> GetAllNodesOfType(NodeType type)
        {
            return _grid.ToList().FindAll(n => n.GetNodeType() == type);
        }
        
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append(_grid);

            return sb.ToString();
        }
    }
}