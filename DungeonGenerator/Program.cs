using System.Diagnostics;
using DungeonGenerator.DungeonGenerator;
using DungeonGenerator.DungeonGenerator.Utils;

var random = new ExpandedRandom();
var stopwatch = new Stopwatch();

// Chooses theme at random
var themeNum = random.Next(0, 3);
var theme = themeNum switch
{
    0 => DungeonTheme.Castle,
    1 => DungeonTheme.AbandonedCastle,
    2 => DungeonTheme.CaveSystem,
    _ => DungeonTheme.Castle
};

stopwatch.Start();

var db = new DungeonBuilder(theme);
db.Build();

stopwatch.Stop();
Console.WriteLine("Generated dungeon in {0} ms", stopwatch.ElapsedMilliseconds);