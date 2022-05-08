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
            GenerateMainCycle(2);
            
            void GenerateStart()
            {
                var undecided = _generatedGraph.GetAllNodesOfType(NodeType.Undecided);
                GetRandomFromList(undecided).SetNodeType(NodeType.Start);

                Console.Write("After generating start: \n" + _generatedGraph + '\n'); // DEBUG!
            }
            // TODO: Add empty nodes on the outlines to minimise the chance of the generator to get stuck.
            void GenerateCycleStart()
            {
                var start = _generatedGraph.GetFirstNodeOfType(NodeType.Start);
                if (start == null)
                    throw new ArgumentNullException("Starting node" + start + " can not be null when generating cycle start.");
                
                var possible = _generatedGraph.GetNodesInNeighbourhood(start).FindAll(n => n.GetNodeType() == NodeType.Undecided);
                SortListByClosest(possible, (_nodesWidth / 2, _nodesHeight / 2));
                
                var cycleEntrance = possible[0];
                cycleEntrance.SetNodeType(NodeType.CycleEntrance);
                Graph.AddEdge(start, cycleEntrance);
                
                Console.Write("After generating cycle start: \n" + _generatedGraph + '\n'); // DEBUG!
            }
            
            void GenerateMainCycle(int minimalIterations)
            {
                AddEmptyNodes();

                var last = GenerateWanderingPath(minimalIterations);
                CloseCycle(last);
                GenerateGoal();
                
                void AddEmptyNodes()
                {
                 var emptyNode = GetRandomFromList(_generatedGraph
                     .GetNodesInNeighbourhood((_nodesWidth / 2, _nodesHeight / 2))
                     .FindAll(n => n.GetNodeType() == NodeType.Undecided));
                 emptyNode.SetNodeType(NodeType.Empty);
                }
                
                Node GenerateWanderingPath(int maxIterations)
                {
                    var cycleStart = _generatedGraph.GetFirstNodeOfType(NodeType.CycleEntrance);
                    if (cycleStart == null)
                        throw new ArgumentNullException("Cycle start" + cycleStart + " can not be null when generating wandering path.");

                    // Generate some nodes far from start
                    var iteration = 0;
                    var lastNode = cycleStart;
                    var neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode).FindAll(n => n.GetNodeType() == NodeType.Undecided);
                    SortListByFarthest(neighbours, cycleStart.GetNodeXY());
                    while (iteration < maxIterations)
                    {
                        var nextNode = neighbours[0];
                        nextNode.SetNodeType(NodeType.Cycle);
                        Graph.AddEdge(lastNode, nextNode);

                        Console.WriteLine("Position of next node: " + nextNode.GetNodeXY());
                        
                        lastNode = nextNode;
                        neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode).FindAll(n => n.GetNodeType() == NodeType.Undecided);
                        SortListByFarthest(neighbours, cycleStart.GetNodeXY());

                        iteration++;
                    }
                    Console.Write("After generating wandering path: \n" + _generatedGraph + '\n'); // DEBUG!
                    return lastNode;
                }

                void CloseCycle(Node startingNode)
                {
                    var cycleStart = _generatedGraph.GetFirstNodeOfType(NodeType.CycleEntrance);
                    if (cycleStart == null)
                        throw new ArgumentNullException("Cycle start" + cycleStart + " can not be null when closing cycle.");
                    
                    // Generate some nodes far from start
                    var lastNode = startingNode;
                    var neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode).FindAll(n => n.GetNodeType() == NodeType.Undecided);
                    SortListByClosest(neighbours, cycleStart.GetNodeXY());
                    while (!CanCloseCycle())
                    {
                        var nextNode = neighbours[0];
                        nextNode.SetNodeType(NodeType.Cycle);
                        Graph.AddEdge(lastNode, nextNode);

                        lastNode = nextNode;
                        neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode).FindAll(n => n.GetNodeType() == NodeType.Undecided);
                        SortListByClosest(neighbours, cycleStart.GetNodeXY());
                    }

                    Graph.AddEdge(lastNode, cycleStart);

                    bool CanCloseCycle()
                    {
                        return _generatedGraph.GetNodesInNeighbourhood(_generatedGraph.GetFirstNodeOfType(NodeType.CycleEntrance)).Contains(lastNode);
                    }
                }

                void GenerateGoal()
                {
                    _generatedGraph.GetAllNodesOfType(NodeType.Empty).ForEach(n => n.SetNodeType(NodeType.Undecided));
                    
                    var neighbours = _generatedGraph.GetAllNodesOfType(NodeType.Cycle);
                    var cycleEnd = GetRandomFromList(neighbours);
                    cycleEnd.SetNodeType(NodeType.CycleTarget);
                    
                    var end = GetRandomFromList(_generatedGraph.GetNodesInNeighbourhood(cycleEnd).FindAll(n => n.GetNodeType() == NodeType.Undecided));
                    end.SetNodeType(NodeType.End);
                    
                    Graph.AddEdge(cycleEnd, end);
                }
                
            }
        }

        private void SetMainCycleType()
        {
            // TODO: Decide which cycle to use
            throw new NotImplementedException();
        }

        private static T GetRandomFromList<T>(IReadOnlyList<T> list)
        {
            var random = new Random();
            var randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }

        private static void SortListByClosest(List<Node> list, (int x, int y) direction)
        {
            list.Sort((a, b) =>
            {
                var d1 = Math.Pow(a.GetNodeXY().x - direction.x, 2) + Math.Pow(a.GetNodeXY().y - direction.y, 2);
                var d2 = Math.Pow(b.GetNodeXY().x - direction.x, 2) + Math.Pow(b.GetNodeXY().y - direction.y, 2);
                return d1.CompareTo(d2);
            });
        }

        private static void SortListByFarthest(List<Node> list, (int x, int y) direction)
        {
            list.Sort((a, b) =>
            {
                var d1 = Math.Pow(a.GetNodeXY().x - direction.x, 2) + Math.Pow(a.GetNodeXY().y - direction.y, 2);
                var d2 = Math.Pow(b.GetNodeXY().x - direction.x, 2) + Math.Pow(b.GetNodeXY().y - direction.y, 2);
                return d2.CompareTo(d1);
            });
        }
    }
}