using DungeonGenerator.DungeonGenerator.GraphGeneration.Characteristics;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Characteristics.Gates;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs
{
    public class Node
    {
        private NodeType _nodeType;
        private CoreGameplayElement _coreRoomFunction = CoreGameplayElement.None;

        private readonly (int x, int y) _nodeXY;
        private readonly List<Node> _adjacentNodes;
        private readonly List<Lock> _locks;
        private readonly List<Key> _keys;
        private readonly List<Obstacle> _obstacles;


        public Node(NodeType roomType, int nodeX, int nodeY)
        {
            _nodeType = roomType;
            _adjacentNodes = new List<Node>();
            _nodeXY = (nodeX, nodeY);
            _obstacles = new List<Obstacle>();
            _keys = new List<Key>();
            _locks = new List<Lock>();
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

        public List<Obstacle> GetObstacles()
        {
            return _obstacles;
        }

        public void AddObstacle(Obstacle o)
        {
            _obstacles.Add(o);
        }

        public List<Lock> GetLocks()
        {
            return _locks;
        }

        public void AddLock(Lock l)
        {
            _locks.Add(l);
        }

        public List<Key> GetKeys()
        {
            return _keys;
        }

        public void AddKey(Key k)
        {
            _keys.Add(k);
        }

        public NodeType GetNodeType()
        {
            return _nodeType;
        }

        public void SetNodeType(NodeType rt)
        {
            switch (rt)
            {
                case NodeType.Start:
                    SetCoreGameplayElement(CoreGameplayElement.Start);
                    break;
                case NodeType.End:
                    SetCoreGameplayElement(CoreGameplayElement.Goal);
                    break;
            }

            _nodeType = rt;
        }
    }
}