using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Cycles;

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
            Console.WriteLine("DFS:");
            var visitedNodes = new List<Node>();
            DfsHelper(startNode, visitedNodes);
        }

        void DfsHelper(Node n, ICollection<Node> visitedNodes)
        {
            visitedNodes.Add(n);
            if (IsPartOfCycle(n))
                cycle.Add(n);

            Console.Write(n.GetNodeType() + "->"); 
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
}