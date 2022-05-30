using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building;

public class SimpleCycle : IBaseCycleVariant
{
    public void Generate(Graph graph)
    {
        GenerateDungeonEntrance(graph);
        
        GenerateCycleStart(graph);
        
        GenerateMainCycle(graph, 2);
    }

    private void GenerateDungeonEntrance(Graph graph)
    {
        var undecided = graph.GetAllNodesOfType(NodeType.Undecided);
        GraphBuilderHelpers.GetRandomFromList(undecided).SetNodeType(NodeType.Start);
    }

    private void GenerateCycleStart(Graph graph)
    {
        var start = graph.GetFirstNodeOfType(NodeType.Start);
        if (start == null)
            throw new ArgumentNullException("Starting node" + start +
                                            " can not be null when generating cycle start.");

        var possible = graph.GetNodesInNeighbourhood(start)
            .FindAll(n => n.GetNodeType() == NodeType.Undecided);
        var size = graph.GetDimensions();
        GraphBuilderHelpers.SortListByClosest(possible, (size.x / 2, size.y / 2));

        var cycleEntrance = possible[0];
        cycleEntrance.SetNodeType(NodeType.CycleEntrance);
        Graph.AddEdge(start, cycleEntrance);
    }

    private void GenerateMainCycle(Graph graph, int minimalIterations)
    {
        AddEmptyNodes(graph);

        var last = GenerateWanderingPath(minimalIterations);
        CloseCycle(last);
        GenerateGoal();

        Node GenerateWanderingPath(int maxIterations)
        {
            var cycleStart = graph.GetFirstNodeOfType(NodeType.CycleEntrance);
            if (cycleStart == null)
                throw new ArgumentNullException("Cycle start" + cycleStart +
                                                " can not be null when generating wandering path.");

            // Generate some nodes far from start
            var iteration = 0;
            var lastNode = cycleStart;
            var neighbours = graph.GetNodesInNeighbourhood(lastNode)
                .FindAll(n => n.GetNodeType() == NodeType.Undecided);
            GraphBuilderHelpers.SortListByFarthest(neighbours, cycleStart.GetPosition());
            while (iteration < maxIterations)
            {
                var nextNode = neighbours[0];
                nextNode.SetNodeType(NodeType.Cycle);
                Graph.AddEdge(lastNode, nextNode);

                lastNode = nextNode;
                neighbours = graph.GetNodesInNeighbourhood(lastNode)
                    .FindAll(n => n.GetNodeType() == NodeType.Undecided);
                GraphBuilderHelpers.SortListByFarthest(neighbours, cycleStart.GetPosition());

                iteration++;
            }

            Console.Write("After generating wandering path: \n" + graph + '\n'); // DEBUG!
            return lastNode;
        }

        void CloseCycle(Node startingNode)
        {
            var cycleStart = graph.GetFirstNodeOfType(NodeType.CycleEntrance);
            if (cycleStart == null)
                throw new ArgumentNullException("Cycle start" + cycleStart +
                                                " can not be null when closing cycle.");
            
            var typesToAvoid = new List<NodeType> {NodeType.Start, NodeType.Empty, NodeType.Cycle};
            var nodesToAdd = GraphBuilderHelpers.FindPath(startingNode, cycleStart, graph, typesToAvoid);
            if (nodesToAdd.Count == 0)
                throw new ArgumentException("No path found when closing cycle.");
            
            // Set nodes to cycle and add edges to consecutive nodes
            for (var i = 0; i < nodesToAdd.Count - 1; i++)
            {
                nodesToAdd[i].SetNodeType(NodeType.Cycle);
                Graph.AddEdge(nodesToAdd[i], nodesToAdd[i + 1]);
            }
            var lastNode = nodesToAdd[^1];
            Graph.AddEdge(lastNode, cycleStart);
            
        }

        void GenerateGoal()
        {
            graph.GetAllNodesOfType(NodeType.Empty).ForEach(n => n.SetNodeType(NodeType.Undecided));

            var cycleStart = graph.GetFirstNodeOfType(NodeType.CycleEntrance) ??
                             throw new NullReferenceException("Cycle start not found in graph.");
            Node cycleEnd;
            do
            {
                var neighbours = graph.GetAllNodesOfType(NodeType.Cycle);
                cycleEnd = GraphBuilderHelpers.GetRandomFromList(neighbours);
            } while (Graph.IsNodeInNeighbourhood(cycleEnd, cycleStart));

            cycleEnd.SetNodeType(NodeType.CycleTarget);
            
            // TODO: fix situation when cycleEnd is blocked and has no empty neighbours
            var end = GraphBuilderHelpers.GetRandomFromList(graph
                .GetNodesInNeighbourhood(cycleEnd).FindAll(n => n.GetNodeType() == NodeType.Undecided));
            end.SetNodeType(NodeType.End);

            Graph.AddEdge(cycleEnd, end);
        }
    }

    private void AddEmptyNodes(Graph graph)
    {
        var size = graph.GetDimensions();
        var emptyNode = GraphBuilderHelpers.GetRandomFromList(graph
            .GetNodesInNeighbourhood((size.x / 2, size.y / 2))
            .FindAll(n => n.GetNodeType() == NodeType.Undecided));
        emptyNode.SetNodeType(NodeType.Empty);
    }
}