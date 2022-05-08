namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs
{
    public class Node
    {
        private NodeType _nodeType;
        private CoreGameplayElement _coreRoomFunction = CoreGameplayElement.None;
        private readonly List<Node> _adjacentNodes;
        private readonly (int x, int y) _nodeXY;

        public Node(NodeType roomType, int nodeX, int nodeY)
        {
            _nodeType = roomType;
            _adjacentNodes = new List<Node>();
            _nodeXY = (nodeX, nodeY);
        }

        public (int x, int y) GetNodeXY()
        {
            return _nodeXY;
        }

        public CoreGameplayElement GetCoreGameplayElement()
        {
            return _coreRoomFunction;
        }

        public void SetCoreGameplayElement(CoreGameplayElement element)
        {
            _coreRoomFunction = element;
        }

        public void AddNeighbour(Node n)
        {
            _adjacentNodes.Add(n);
        }

        public List<Node> GetNeighbours()
        {
            return _adjacentNodes;
        }

        public NodeType GetNodeType()
        {
            return _nodeType;
        }

        public void SetNodeType(NodeType rt)
        {
            _nodeType = rt;
        }
    }
}