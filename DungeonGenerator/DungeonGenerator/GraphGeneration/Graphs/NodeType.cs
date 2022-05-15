namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Graphs;

public enum NodeType
{
    Start = 's', // Where the dungeon starts
    End = 'e', // Where the dungeon ends
    Empty = 'X', // Empty space, available to use or not
    Undecided = '.', // Currently not in use
    Cycle = 'c', // Part of the cycle
    CycleEntrance = 'n', // Start of the cycle
    CycleTarget = 't', // End of the cycle
    Vault = 'V', // Room that is not part of a cycle  
    Path = 'P' // Set path
}