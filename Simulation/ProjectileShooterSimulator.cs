using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Extensions;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class ProjectileShooterSimulator : ISimulator
    {
        public GameData Tick(in GameData game)
        {
            int tick = game.Tick;
            ImmutableArray<Enemy> enemies = game.Enemies;
            ImmutableArray<Tower> towers = game.Towers;
            ImmutableArray<Projectile> projectiles = game.Projectiles;

            if (enemies.IsEmpty)
            {
                return game;
            }

            ImmutableArray<Tower>.Builder? towersBuilder = null;
            ImmutableArray<Projectile>.Builder? projectilesBuilder = null;
            for (var i = 0; i < towers.Length; i++)
            {
                Tower tower = towers[i];
                if (tower.TickForNextShot <= tick)
                {
                    if (towersBuilder == null)
                    {
                        towersBuilder = towers.ToBuilder();
                        projectilesBuilder = projectiles.ToBuilder();
                    }
                    enemies.GetClosestDistanceSqrt(tower.Position, out var closestEnemy);
                    Vector2 distance = closestEnemy.Position - tower.Position;

                    towersBuilder[i] = tower with {TickForNextShot = tower.TickForNextShot + 60};
                    projectilesBuilder!.Add(new Projectile(tower.Position,  distance.Normalized() * 0.05f));
                }
            }

            // If a tower has been updated we need to return the new towers.
            if (towersBuilder != null)
            {
                return game with
                {
                    Towers = towersBuilder.ToImmutable(),
                    Projectiles = projectilesBuilder!.ToImmutable(),
                };
            }
            else
            {
                return game;
            }
            
        }
    }
}