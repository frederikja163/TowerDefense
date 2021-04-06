using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Extensions;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class ProjectileShooterSimulator
    {
        public void Tick(SimulationData data)
        {
            int tick = data.Tick;
            ImmutableArray<Enemy>.Builder enemies = data.Enemies;
            ImmutableArray<Tower>.Builder towers = data.Towers;
            ImmutableArray<Projectile>.Builder projectiles = data.Projectiles;

            if (enemies.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < towers.Count; i++)
            {
                Tower tower = towers[i];
                if (tower.TickForNextShot <= tick)
                {
                    enemies.GetClosestDistanceSqrt(tower.Position, out var closestEnemy);
                    Vector2 distance = closestEnemy.Position - tower.Position;

                    towers[i] = tower with {TickForNextShot = tower.TickForNextShot + 60};
                    projectiles!.Add(new Projectile(tower.Position,  distance.Normalized() * 0.05f, data.TotalEntityCount++));
                }
            }
        }
    }
}