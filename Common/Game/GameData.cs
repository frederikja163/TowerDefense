using System.Collections.Immutable;

namespace TowerDefense.Common.Game
{
    public record GameData (ImmutableArray<Enemy> Enemies, ImmutableArray<Tower> Towers, DraggableTower? DragTower)
    {
    }
}
