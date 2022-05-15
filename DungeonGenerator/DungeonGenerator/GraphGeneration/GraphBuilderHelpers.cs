using DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration;

public static class GraphBuilderHelpers
{
    public static T GetRandomFromList<T>(IReadOnlyList<T> list)
    {
        var random = new Random();
        var randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }

    // TODO: Test this because it seems to throw exceptions
    public static T GetRandomFromListExcludingEnds<T>(IReadOnlyList<T> list)
    {
        var random = new Random();
        var randomIndex = random.Next(1, list.Count - 1);
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
}