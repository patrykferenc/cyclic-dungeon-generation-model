using Cyclic.Generation.Model.Core.Common;
using Cyclic.Generation.Model.Core.GraphGeneration.Themes.Types;

namespace Cyclic.Generation.Model.Core.GraphGeneration.Themes.Applying;

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