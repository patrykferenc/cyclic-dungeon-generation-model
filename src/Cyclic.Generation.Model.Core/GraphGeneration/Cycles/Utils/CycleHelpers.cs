using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Cycles.Utils;

public static class CycleHelpers
{
    public static List<Node> GetCycle(Graph graph)
    {
        var cycle = new List<Node>();

        var currentNode = graph.GetFirstNodeOfType(NodeType.CycleEntrance);
        if (currentNode == null)
            throw new ArgumentNullException("There is no cycle entrance in the graph. " + currentNode);
        Dfs(currentNode);

        return cycle;

        void Dfs(Node startNode)
        {
            //Console.WriteLine("DFS:");
            var visitedNodes = new List<Node>();
            DfsHelper(startNode, visitedNodes);
        }

        void DfsHelper(Node n, ICollection<Node> visitedNodes)
        {
            visitedNodes.Add(n);
            if (IsPartOfCycle(n))
                cycle.Add(n);

            //Console.Write(n.GetNodeType() + "->"); 
            foreach (var neighbour in n.GetNeighbours())
                if (!visitedNodes.Contains(neighbour))
                    DfsHelper(neighbour, visitedNodes);
        }

        static bool IsPartOfCycle(Node n)
        {
            if (n.GetNodeType() == NodeType.CycleEntrance)
                return true;
            if (n.GetNodeType() == NodeType.Cycle)
                return true;
            if (n.GetNodeType() == NodeType.CycleTarget)
                return true;
            return false;
        }
    }

    public static (int aLength, int bLength) GetCycleLengths(Graph graph)
    {
        var cycle = GetCycle(graph);

        return GetCycleLengths(cycle);
    }

    public static (int aLength, int bLength) GetCycleLengths(List<Node> cycle)
    {
        int aLength = 0, bLength = 0;
        var firstHalf = true;
        foreach (var n in cycle)
        {
            if (n.GetNodeType() == NodeType.CycleEntrance)
                continue;
            if (n.GetNodeType() == NodeType.CycleTarget)
            {
                firstHalf = false;
                continue;
            }

            if (firstHalf)
                aLength++;
            else
                bLength++;
        }

        return (aLength, bLength);
    }

    public static List<Node> GetCyclePart(List<Node> cycle, bool isPartA)
    {
        var len = GetCycleLengths(cycle);
        return isPartA ? cycle.GetRange(1, len.aLength) : cycle.GetRange(len.aLength + 1, len.bLength);
    }

    public static List<Node> GetCyclePart(Graph graph, bool isPartA)
    {
        return GetCyclePartHelper(graph, isPartA);
    }

    private static List<Node> GetCyclePartHelper(Graph graph, bool isPartA)
    {
        var len = GetCycleLengths(graph);
        return isPartA
            ? GetCycle(graph).GetRange(1, len.aLength)
            : GetCycle(graph).GetRange(len.aLength + 1, len.bLength);
    }
}