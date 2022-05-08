using System.Text;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs
{
    public class Graph
    {
        // von neumann's neighbourhood
        private static readonly (int x, int y)[] Neighbourhood = { (-1, 0), (0, -1), (1, 0), (0, 1) };
        private readonly int _nodesHeight;
        private readonly int _nodesWidth;
        private readonly Node[,] _grid;

        private Graph(int nodesHeight, int nodesWidth, NodeType roomType)
        {
            _nodesHeight = nodesHeight;
            _nodesWidth = nodesWidth;
            _grid = new Node[_nodesHeight, _nodesWidth];
            for (var i = 0; i < _nodesHeight; i++)
            {
                for (var j = 0; j < _nodesWidth; j++)
                {
                    _grid[i, j] = new Node(roomType, j, i);
                }
            }
        }

        public Graph(int nodesHeight, int nodesWidth) : this(nodesHeight, nodesWidth, NodeType.Undecided) { }

        public (int x, int y) GetGraphDimensions()
        {
            return (x: _nodesWidth, y: _nodesHeight);
        }

        public void SetNodeType((int x, int y) nodePosition, NodeType nodeType)
        {
            var node = GetNode(nodePosition.x, nodePosition.y);
            if (node == null)
                throw new ArgumentNullException("There is no node to set at coordinates: "  + nodePosition + " .");
            node.SetNodeType(nodeType);
        }

        public void SetNodeType(Node n, NodeType nodeType)
        {
            var node = GetNode(n);
            if (node == null)
                throw new ArgumentNullException("There is no node" + n + " to set at given coordinates");
            node.SetNodeType(nodeType);
        }

        public static void AddEdge(Node startNode, Node endNode)
        {
            if (!startNode.GetNeighbours().Contains(endNode))
                startNode.AddNeighbour(endNode);
            if (!endNode.GetNeighbours().Contains(startNode))
                endNode.AddNeighbour(startNode);
        }

        public static bool AreNodesConnected(Node firstNode, Node secondNode)
        {
            return firstNode.GetNeighbours().Contains(secondNode);
        }

        public bool AreNodesInNeighbourhood(Node firstNode, Node secondNode)
        {
            for (var i = 0; i < Neighbourhood.Length; i++)
            {
                var nodeToCheck = GetNode(firstNode.GetNodeXY().x + Neighbourhood[i].x, firstNode.GetNodeXY().y + Neighbourhood[i].y);
                if (nodeToCheck != null && nodeToCheck == secondNode)
                {
                    return true;
                }
            }
            return false;
        }

        public Node? GetNode((int x, int y) nodePosition)
        {
            return GetNode(nodePosition.x, nodePosition.y);
        }

        private Node? GetNode(int x, int y)
        {
            return (x < _grid.GetLength(1) && x >= 0 &&
                y < _grid.GetLength(0) && y >= 0) ?
                _grid[y, x] : null;
        }

        public Node? GetNode(Node n)
        {
            for (var i = 0; i < _nodesHeight; i++)
            {
                for (var j = 0; j < _nodesWidth; j++)
                {
                    if (_grid[i, j] == n)
                        return _grid[i, j];
                }
            }
            return null;
        }

        public Node? GetNodeOfIndividualType(NodeType nodeType)
        {
            for (var i = 0; i < _nodesHeight; i++)
            {
                for (var j = 0; j < _nodesWidth; j++)
                {
                    if (_grid[i, j].GetNodeType() == nodeType)
                        return _grid[i, j];
                }
            }
            return null;
        }

        private IEnumerable<(int x, int y)> IncludeCentrePositionOfTypeHelper((int x, int y) nodePosition, NodeType type)
        {
            List<(int x, int y)> valid = new();

            var nodeToCheck = GetNode(nodePosition);
            if (nodeToCheck != null && nodeToCheck.GetNodeType() == type)
            {
                valid.Add(nodeToCheck.GetNodeXY());
            }

            return valid;
        }

        private IEnumerable<(int x, int y)> GetValidPositionsOfTypeHelper((int x, int y) nodePosition, NodeType type)
        {
            List<(int x, int y)> valid = new();

            for (var i = 0; i < Neighbourhood.Length; i++)
            {
                var nodeToCheck = GetNode(nodePosition.x + Neighbourhood[i].x, nodePosition.y + Neighbourhood[i].y);
                if (nodeToCheck != null && nodeToCheck.GetNodeType() == type)
                {
                    valid.Add(nodeToCheck.GetNodeXY());
                }
            }

            return valid;
        }

        public List<(int x, int y)> GetValidPositionsOfType((int x, int y) nodePosition, NodeType type, bool includeCentre)
        {
            List<(int x, int y)> validNodes = new();

            validNodes.AddRange(GetValidPositionsOfTypeHelper(nodePosition, type));

            if (includeCentre)
                validNodes.AddRange(IncludeCentrePositionOfTypeHelper(nodePosition, type));

            return validNodes;
        }

        private List<Node> IncludeCentreNodeOfTypeHelper((int x, int y) nodePosition, NodeType type)
        {
            List<Node> valid = new();

            Node? nodeToCheck = GetNode(nodePosition);
            if (nodeToCheck != null && nodeToCheck.GetNodeType() == type)
            {
                valid.Add(nodeToCheck);
            }

            return valid;
        }

        private IEnumerable<Node> GetValidNodesOfTypeHelper((int x, int y) nodePosition, NodeType type)
        {
            List<Node> valid = new();

            for (var i = 0; i < Neighbourhood.Length; i++)
            {
                var nodeToCheck = GetNode(nodePosition.x + Neighbourhood[i].x, nodePosition.y + Neighbourhood[i].y);
                if (nodeToCheck != null && nodeToCheck.GetNodeType() == type)
                {
                    valid.Add(nodeToCheck);
                }
            }

            return valid;
        }

        public List<Node> GetValidNodesOfType((int x, int y) nodePosition, NodeType type, bool includeCentre)
        {
            List<Node> validNodes = new();

            validNodes.AddRange(GetValidNodesOfTypeHelper(nodePosition, type));

            if (includeCentre)
                validNodes.AddRange(IncludeCentreNodeOfTypeHelper(nodePosition, type));

            return validNodes;
        }

        public List<Node> GetAllNodesOfType(NodeType type)
        {
            List<Node> nodes = new();

            for (var i = 0; i < _nodesHeight; i++)
            {
                for (var j = 0; j < _nodesWidth; j++)
                {
                    if(_grid[i, j].GetNodeType() == type)
                        nodes.Add(_grid[i, j]);
                }
            }

            return nodes;
        }


        public override string ToString()
        {
            StringBuilder sb = new();

            for (var i = 0; i < _nodesHeight; i++)
            {
                for (var j = 0; j < _nodesWidth; j++)
                {
                    sb.Append((char)_grid[i, j].GetNodeType());
                    if (j < _nodesWidth - 1) sb.Append(AreNodesConnected(_grid[i, j], _grid[i, j + 1]) ? '-' : ' ');
                }
                sb.Append('\n');
                if (i < _nodesHeight - 1)
                    for (var k = 0; k < _nodesWidth; k++)
                        sb.Append(AreNodesConnected(_grid[i, k], _grid[i + 1, k]) ? "| " : "  ");
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}