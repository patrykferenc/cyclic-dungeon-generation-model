using Cyclic.Generation.Model.Core.Common;
using Cyclic.Generation.Model.Core.Graphs;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Building;

public static class GraphBuilderHelpers
{
    public static T GetRandomFromList<T>(IReadOnlyList<T> list)
    {
        var random = new Random();
        var randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }

    public static void SortListByClosest(List<Node> list, (int x, int y) direction)
    {
        list.Sort((a, b) =>
        {
            var d1 = Math.Pow(a.GetPosition().x - direction.x, 2) + Math.Pow(a.GetPosition().y - direction.y, 2);
            var d2 = Math.Pow(b.GetPosition().x - direction.x, 2) + Math.Pow(b.GetPosition().y - direction.y, 2);
            return d1.CompareTo(d2);
        });
    }

    public static void SortListByFarthest(List<Node> list, (int x, int y) direction)
    {
        list.Sort((a, b) =>
        {
            var d1 = Math.Pow(a.GetPosition().x - direction.x, 2) + Math.Pow(a.GetPosition().y - direction.y, 2);
            var d2 = Math.Pow(b.GetPosition().x - direction.x, 2) + Math.Pow(b.GetPosition().y - direction.y, 2);
            return d2.CompareTo(d1);
        });
    }

    public static List<Node> FindPath(Node start, Node end, Graph graph, List<NodeType> nodesToAvoid)
    {
        var openList = new List<Node>();
        var closedList = new List<Node>();
        var cameFrom = new Dictionary<Node, Node>();
        var gScore = new Dictionary<Node, float>();
        var fScore = new Dictionary<Node, float>();

        openList.Add(start);
        gScore[start] = 0;
        fScore[start] = gScore[start] + Heuristic(start, end);

        while (openList.Count > 0)
        {
            var current = GetLowestFScore(openList, fScore);
            if (current == end) return ReconstructPath(cameFrom, current);

            openList.Remove(current);
            closedList.Add(current);

            foreach (var neighbor in graph.GetNodesInNeighbourhood(current))
            {
                if (closedList.Contains(neighbor) || nodesToAvoid.Contains(neighbor.GetNodeType())) continue;

                var tentativeGScore = gScore[current] + 1;
                if (!openList.Contains(neighbor))
                    openList.Add(neighbor);
                else if (tentativeGScore >= gScore[neighbor]) continue;

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, end);
            }
        }

        return new List<Node>();
    }

    private static float Heuristic(BaseDungeonElement start, BaseDungeonElement end)
    {
        return Math.Abs(start.GetPosition().x - end.GetPosition().x) +
               Math.Abs(start.GetPosition().y - end.GetPosition().y);
    }

    private static List<Node> ReconstructPath(IReadOnlyDictionary<Node, Node> cameFrom, Node current)
    {
        var path = new List<Node> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private static Node GetLowestFScore(List<Node> openList, IReadOnlyDictionary<Node, float> fScore)
    {
        var lowest = openList[0];
        foreach (var node in openList)
            if (fScore[node] < fScore[lowest])
                lowest = node;

        return lowest;
    }
}