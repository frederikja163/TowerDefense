using System.Collections.Immutable;

namespace TowerDefense.Common.Game
{
    public record GameData (
        ImmutableArray<Enemy> Enemies,
        ImmutableArray<Tower> Towers,
        ImmutableArray<Projectile> Projectiles,
        DraggableTower? DragTower,
        int Tick = 0,
        int TotalEntityCount = 0)
    {
    }
}
