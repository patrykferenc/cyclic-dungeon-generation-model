using DungeonGenerator.DungeonGenerator.GraphGeneration;

var gb = new GraphBuilder(5, 5);
var graph = gb.GenerateGraph();
Console.WriteLine("Generated graph:");
Console.Write(graph.ToString());
