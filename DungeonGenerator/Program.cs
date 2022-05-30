using System.Diagnostics;
using DungeonGenerator.DungeonGenerator;
using DungeonGenerator.DungeonGenerator.Utils;

var stopwatch = new Stopwatch();
stopwatch.Start();

var db = new DungeonBuilder(DungeonTheme.Castle);
db.Build();

stopwatch.Stop();
Console.WriteLine("Generated dungeon in {0} ms", stopwatch.ElapsedMilliseconds);

Console.WriteLine("Random funny gaussian number: {0}", new ExpandedRandom().NextGaussian());