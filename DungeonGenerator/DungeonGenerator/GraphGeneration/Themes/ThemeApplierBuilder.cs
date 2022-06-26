using DungeonGenerator.DungeonGenerator.GraphGeneration.Themes.Types;

namespace DungeonGenerator.DungeonGenerator.GraphGeneration.Themes;

public class ThemeApplierBuilder
{
    public static ThemeApplier Build(DungeonTheme theme)
    {
        return theme switch
        {
            DungeonTheme.Castle => new ThemeApplier(new CastleTheme()),
            DungeonTheme.CaveSystem => new ThemeApplier(new CaveTheme()),
            DungeonTheme.AbandonedCastle => new ThemeApplier(new AbandonedCastleTheme()),
            _ => throw new Exception("Unknown theme: " + theme + ".")
        };
    }
}