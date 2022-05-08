namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs
{
    public class Node
    {
        private NodeType NodeType;
        private CoreGameplayElement CoreRoomFunction = CoreGameplayElement.None;
        private List<Node> AdjacentNodes;
        private (int x, int y) NodeXY;

        public Node(NodeType roomType, int nodeX, int nodeY)
        {
            NodeType = roomType;
            AdjacentNodes = new List<Node>();
            NodeXY = (nodeX, nodeY);
        }

        public (int x, int y) GetNodeXY()
        {
            return NodeXY;
        }

        public CoreGameplayElement GetCoreGameplayElement()
        {
            return CoreRoomFunction;
        }

        public void SetCoreGameplayElement(CoreGameplayElement element)
        {
            CoreRoomFunction = element;
        }

        public void AddNeighbour(Node n)
        {
            AdjacentNodes.Add(n);
        }

        public List<Node> GetNeighbours()
        {
            return AdjacentNodes;
        }

        public NodeType GetNodeType()
        {
            return NodeType;
        }

        public void SetNodeType(NodeType rt)
        {
            NodeType = rt;
        }
    }
}