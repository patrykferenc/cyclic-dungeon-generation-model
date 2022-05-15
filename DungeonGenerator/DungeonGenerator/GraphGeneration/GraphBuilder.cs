using DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles;
using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration;

public class GraphBuilder
{
    private readonly Graph _generatedGraph;
    private readonly int _nodesHeight;
    private readonly int _nodesWidth;

    public GraphBuilder(int nodesHeight, int nodesWidth)
    {
        _nodesHeight = nodesHeight;
        _nodesWidth = nodesWidth;
        _generatedGraph = new Graph(_nodesHeight, _nodesWidth);
    }

    public Graph Generate()
    {
        // First step - add start and end of the graph, generate cycle
        AddStartAndGoal();

        // Second step - convert the cycle to a chosen type
        DecorateCycle();

        // Third step (not added now), add 1-2 more cycles

        // Fourth step - add vaults

        return _generatedGraph;
    }

    private void AddStartAndGoal()
    {
        GenerateStart();
        GenerateCycleStart();
        GenerateMainCycle(2);

        Console.Write("After generating cycle: \n" + _generatedGraph + '\n');

        void GenerateStart()
        {
            var undecided = _generatedGraph.GetAllNodesOfType(NodeType.Undecided);
            GraphBuilderHelpers.GetRandomFromList(undecided).SetNodeType(NodeType.Start);

            Console.Write("After generating start: \n" + _generatedGraph + '\n'); // DEBUG!
        }

        void GenerateCycleStart()
        {
            var start = _generatedGraph.GetFirstNodeOfType(NodeType.Start);
            if (start == null)
                throw new ArgumentNullException("Starting node" + start +
                                                " can not be null when generating cycle start.");

            var possible = _generatedGraph.GetNodesInNeighbourhood(start)
                .FindAll(n => n.GetNodeType() == NodeType.Undecided);
            GraphBuilderHelpers.SortListByClosest(possible, (_nodesWidth / 2, _nodesHeight / 2));

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
                var emptyNode = GraphBuilderHelpers.GetRandomFromList(_generatedGraph
                    .GetNodesInNeighbourhood((_nodesWidth / 2, _nodesHeight / 2))
                    .FindAll(n => n.GetNodeType() == NodeType.Undecided));
                emptyNode.SetNodeType(NodeType.Empty);
            }

            Node GenerateWanderingPath(int maxIterations)
            {
                var cycleStart = _generatedGraph.GetFirstNodeOfType(NodeType.CycleEntrance);
                if (cycleStart == null)
                    throw new ArgumentNullException("Cycle start" + cycleStart +
                                                    " can not be null when generating wandering path.");

                // Generate some nodes far from start
                var iteration = 0;
                var lastNode = cycleStart;
                var neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode)
                    .FindAll(n => n.GetNodeType() == NodeType.Undecided);
                GraphBuilderHelpers.SortListByFarthest(neighbours, cycleStart.GetPosition());
                while (iteration < maxIterations)
                {
                    var nextNode = neighbours[0];
                    nextNode.SetNodeType(NodeType.Cycle);
                    Graph.AddEdge(lastNode, nextNode);

                    lastNode = nextNode;
                    neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode)
                        .FindAll(n => n.GetNodeType() == NodeType.Undecided);
                    GraphBuilderHelpers.SortListByFarthest(neighbours, cycleStart.GetPosition());

                    iteration++;
                }

                Console.Write("After generating wandering path: \n" + _generatedGraph + '\n'); // DEBUG!
                return lastNode;
            }

            void CloseCycle(Node startingNode)
            {
                var cycleStart = _generatedGraph.GetFirstNodeOfType(NodeType.CycleEntrance);
                if (cycleStart == null)
                    throw new ArgumentNullException("Cycle start" + cycleStart +
                                                    " can not be null when closing cycle.");

                // Generate some nodes far from start
                var lastNode = startingNode;
                var neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode)
                    .FindAll(n => n.GetNodeType() == NodeType.Undecided);
                GraphBuilderHelpers.SortListByClosest(neighbours, cycleStart.GetPosition());
                while (!CanCloseCycle())
                {
                    var nextNode = neighbours[0];
                    nextNode.SetNodeType(NodeType.Cycle);
                    Graph.AddEdge(lastNode, nextNode);

                    lastNode = nextNode;
                    neighbours = _generatedGraph.GetNodesInNeighbourhood(lastNode)
                        .FindAll(n => n.GetNodeType() == NodeType.Undecided);
                    GraphBuilderHelpers.SortListByClosest(neighbours, cycleStart.GetPosition());
                }

                Graph.AddEdge(lastNode, cycleStart);

                bool CanCloseCycle()
                {
                    return _generatedGraph
                        .GetNodesInNeighbourhood(_generatedGraph.GetFirstNodeOfType(NodeType.CycleEntrance) ??
                                                 throw new InvalidOperationException(
                                                     "Cycle entrance not found in graph.")).Contains(lastNode);
                }
            }

            void GenerateGoal()
            {
                _generatedGraph.GetAllNodesOfType(NodeType.Empty).ForEach(n => n.SetNodeType(NodeType.Undecided));

                var neighbours = _generatedGraph.GetAllNodesOfType(NodeType.Cycle);
                var cycleEnd = GraphBuilderHelpers.GetRandomFromList(neighbours);
                cycleEnd.SetNodeType(NodeType.CycleTarget);

                var end = GraphBuilderHelpers.GetRandomFromList(_generatedGraph
                    .GetNodesInNeighbourhood(cycleEnd).FindAll(n => n.GetNodeType() == NodeType.Undecided));
                end.SetNodeType(NodeType.End);

                Graph.AddEdge(cycleEnd, end);
            }
        }
    }

    private void DecorateCycle()
    {
        var decorator = CycleChooser.BuildDecorator(_generatedGraph);
        decorator.DecorateCycle(_generatedGraph);
        Console.Write("After decorating cycle: \n" + _generatedGraph + '\n'); // Debug
    }
}