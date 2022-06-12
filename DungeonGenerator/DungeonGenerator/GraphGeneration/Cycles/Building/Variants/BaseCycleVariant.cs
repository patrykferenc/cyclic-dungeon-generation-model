using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles.Building.Variants;

public abstract class BaseCycleVariant
{
    
    protected readonly Graph Graph;
    
    protected BaseCycleVariant(Graph graph)
    {
        Graph = graph;
    }
    
    protected virtual void GenerateDungeonEntrance()
    {
        var undecided = Graph.GetAllNodesOfType(NodeType.Undecided);
        GraphBuilderHelpers.GetRandomFromList(undecided).SetNodeType(NodeType.Start);
    }

    protected virtual void GenerateCycleStart()
    {
        var start = Graph.GetFirstNodeOfType(NodeType.Start);
        if (start == null)
            throw new ArgumentNullException("Starting node" + start +
                                            " can not be null when generating cycle start.");

        var possible = Graph.GetNodesInNeighbourhood(start)
            .FindAll(n => n.GetNodeType() == NodeType.Undecided);
        
        if (possible.Count == 0)
            throw new ArgumentNullException("No possible nodes in the list " + possible + " to connect to.");
        
        var size = Graph.GetDimensions();
        GraphBuilderHelpers.SortListByClosest(possible, (size.x / 2, size.y / 2));

        var cycleEntrance = possible[0];
        cycleEntrance.SetNodeType(NodeType.CycleEntrance);
        Graph.AddEdge(start, cycleEntrance);
    }

    protected abstract void GenerateMainCycle();
    
    protected virtual void CloseCycle(Node startingNode)
    {
        var cycleStart = Graph.GetFirstNodeOfType(NodeType.CycleEntrance);
        if (cycleStart == null)
            throw new ArgumentNullException("Cycle start" + cycleStart +
                                            " can not be null when closing cycle.");
            
        var typesToAvoid = new List<NodeType> {NodeType.Start, NodeType.Empty, NodeType.Cycle};
        var nodesToAdd = GraphBuilderHelpers.FindPath(startingNode, cycleStart, Graph, typesToAvoid);
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

    protected virtual void GenerateGoal()
    {
        Graph.GetAllNodesOfType(NodeType.Empty).ForEach(n => n.SetNodeType(NodeType.Undecided));

        var cycleStart = Graph.GetFirstNodeOfType(NodeType.CycleEntrance) ??
                         throw new NullReferenceException("Cycle start not found in graph.");
        Node cycleEnd;
        do
        {
            var neighbours = Graph.GetAllNodesOfType(NodeType.Cycle);
            cycleEnd = GraphBuilderHelpers.GetRandomFromList(neighbours);
        } while (Graph.IsNodeInNeighbourhood(cycleEnd, cycleStart));

        cycleEnd.SetNodeType(NodeType.CycleTarget);
            
        // TODO: fix situation when cycleEnd is blocked and has no empty neighbours
        var end = GraphBuilderHelpers.GetRandomFromList(Graph
            .GetNodesInNeighbourhood(cycleEnd).FindAll(n => n.GetNodeType() == NodeType.Undecided));
        end.SetNodeType(NodeType.End);

        Graph.AddEdge(cycleEnd, end);
    }
    
    protected virtual Node GenerateWanderingPath(int maxIterations)
    {
        var cycleStart = Graph.GetFirstNodeOfType(NodeType.CycleEntrance);
        if (cycleStart == null)
            throw new ArgumentNullException("Cycle start" + cycleStart +
                                            " can not be null when generating wandering path.");
        
        var iteration = 0;
        var lastNode = cycleStart;
        var neighbours = Graph.GetNodesInNeighbourhood(lastNode)
            .FindAll(n => n.GetNodeType() == NodeType.Undecided);
        GraphBuilderHelpers.SortListByFarthest(neighbours, cycleStart.GetPosition());
        while (iteration < maxIterations)
        {
            var nextNode = neighbours[0];
            nextNode.SetNodeType(NodeType.Cycle);
            Graph.AddEdge(lastNode, nextNode);

            lastNode = nextNode;
            neighbours = Graph.GetNodesInNeighbourhood(lastNode)
                .FindAll(n => n.GetNodeType() == NodeType.Undecided);
            GraphBuilderHelpers.SortListByFarthest(neighbours, cycleStart.GetPosition());

            iteration++;
        }

        Console.Write("After generating wandering path: \n" + Graph + '\n'); // DEBUG!
        return lastNode;
    }
    
}