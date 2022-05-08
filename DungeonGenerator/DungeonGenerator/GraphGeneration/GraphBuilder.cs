using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration
{
    public class GraphBuilder
    {
        private readonly int _nodesHeight;
        private readonly int _nodesWidth;
        private readonly Graph _generatedGraph;

        public GraphBuilder(int nodesHeight, int nodesWidth)
        {
            _nodesHeight = nodesHeight;
            _nodesWidth = nodesWidth;
            _generatedGraph = new Graph(_nodesHeight, _nodesWidth);
        }

        public Graph GenerateGraph()
        {
            // First step - add start and end of the graph, generate cycle
            AddStartAndGoal();

            // Second step - convert the cycle to a chosen type
            //SetMainCycleType();

            // Third step (not added now), add 1-2 more cycles

            // Fourth step - add vaults

            return _generatedGraph;
        }

        private void AddStartAndGoal()
        {
            GenerateStart();
            GenerateCycleStart();
            GenerateMainCycle();

            // Generate a starting point of the dungeon
            void GenerateStart()
            {
                var rand = new Random();
                (int x, int y) coords;
                do
                {
                    coords = (rand.Next(0, _nodesWidth), rand.Next(0, _nodesHeight));
                } while (_generatedGraph.GetNode(coords) == null 
                    || _generatedGraph.GetNode(coords)!.GetNodeType() == NodeType.Empty); // this is actually safe
                _generatedGraph.SetNodeType(coords, NodeType.Start);
                
                Console.Write("Ayo: \n" + _generatedGraph + '\n'); // DEBUG!
            }
            // Draw a random walk - beginning of the cycle
            void GenerateCycleStart()
            {
                var start = _generatedGraph.GetNodeOfIndividualType(NodeType.Start);
                if (start == null)
                    throw new ArgumentNullException("Starting node" + start + " can not be null when generating cycle start.");
                var cycleEntrance = ChooseSkewedUndecidedNodeFromNeighbourhood(start, (_nodesWidth / 2, _nodesHeight / 2));
                cycleEntrance.SetNodeType(NodeType.CycleEntrance);
                Graph.AddEdge(start, cycleEntrance);
            }
            // Add empty nodes and generate a circle (cycle) around them
            void GenerateMainCycle()
            {
                // TODO: Make generating nodes more intelligent, for now it is like so

                // Randomly add two empty nodes
                var cycleStart = _generatedGraph.GetNodeOfIndividualType(NodeType.CycleEntrance);
                if (cycleStart == null)
                    throw new ArgumentNullException("Cycle start" + cycleStart + " can not be null when generating cycle.");
                var emptyNode = ChooseRandomNodeFromNeighbourhood(_nodesWidth / 2, _nodesHeight / 2, true);
                emptyNode.SetNodeType(NodeType.Empty);

                // Generate the cycle
                Console.Write(_generatedGraph.ToString() + '\n'); // Debug
                var nextNodeInLine = ChooseRandomNodeFromNeighbourhood(cycleStart, false); // THIS CAN THROW EXCEPTION
                var tempNode = cycleStart;
                const int minimalIterations = 2;
                var iteration = 0;
                while (iteration < minimalIterations || !_generatedGraph.AreNodesInNeighbourhood(cycleStart, nextNodeInLine))
                {
                    nextNodeInLine.SetNodeType(NodeType.Cycle);
                    Graph.AddEdge(tempNode, nextNodeInLine);
                    tempNode = nextNodeInLine;
                    nextNodeInLine = ChooseSkewedUndecidedNodeFromNeighbourhood(nextNodeInLine, cycleStart.GetNodeXY());
                    iteration++;
                }
                Graph.AddEdge(tempNode, nextNodeInLine);
                nextNodeInLine.SetNodeType(NodeType.Cycle);
                Graph.AddEdge(cycleStart, nextNodeInLine);

                // Choose random node from cycle and add a goal to the random neighbour empty node
                // and set node as goal
                _generatedGraph.GetAllNodesOfType(NodeType.Empty).ForEach(n => n.SetNodeType(NodeType.Undecided));
                var neighbours = _generatedGraph.GetAllNodesOfType(NodeType.Cycle);
                Random random = new();
                var randomIndex = random.Next(neighbours.Count);
                var cycleEnd = neighbours[randomIndex];
                cycleEnd.SetNodeType(NodeType.CycleTarget);
                var end = ChooseRandomNodeFromNeighbourhood(cycleEnd, false);
                end.SetNodeType(NodeType.End);
                Graph.AddEdge(cycleEnd, end);
            }
        }

        private void SetMainCycleType()
        {
            // TODO: Decide which cycle to use
            throw new NotImplementedException();
        }

        private Node ChooseRandomNodeFromNeighbourhood(int x, int y, bool includingCentre)
        {
            return ChooseRandomNodeHelper(x, y, includingCentre);
        }

        private Node ChooseRandomNodeFromNeighbourhood(Node n, bool includingCentre)
        {
            return ChooseRandomNodeHelper(n.GetNodeXY().x, n.GetNodeXY().y, includingCentre);
        }

        private Node ChooseRandomNodeHelper(int x, int y, bool includingCentre)
        {
            var neighbours = _generatedGraph.GetValidNodesOfType((x, y), NodeType.Undecided, includingCentre);

            if (neighbours.Count == 0)
                throw new InvalidOperationException("Could not find empty nodes from: " + x + ", " + y + " .");
            Random random = new();
            var randomIndex = random.Next(neighbours.Count);
            return neighbours[randomIndex];
        }

        private Node ChooseSkewedUndecidedNodeFromNeighbourhood(Node n, (int x, int y) directionXy)
        {
            return ChooseSkewedNodeFromNeighbourhoodHelper(n.GetNodeXY(), directionXy);
        }

        private Node ChooseSkewedNodeFromNeighbourhoodHelper((int x, int y) nodeXy, (int x, int y) directionXy)
        {
            var neighbours = _generatedGraph.GetValidNodesOfType(nodeXy, NodeType.Undecided, false);

            // Sort by distance
            neighbours.OrderBy(n => Math.Sqrt(Math.Pow(n.GetNodeXY().x - directionXy.x, n.GetNodeXY().y - directionXy.y)));

            if (neighbours.Count == 0)
                throw new InvalidOperationException("Could not find undecided nodes from: " + nodeXy + " .");
            return neighbours[0];
        }
    }
}