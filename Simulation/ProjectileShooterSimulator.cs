using System.Collections.Immutable;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class ProjectileShooterSimulator : ISimulator
    {
        public GameData Tick(in GameData game)
        {
            int tick = game.Tick;
            bool changedGame = false;
            ImmutableArray<Tower> towers = game.Towers;
            ImmutableArray<Projectile> projectiles = game.Projectiles;

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

                    towersBuilder[i] = tower with {TickForNextShot = tower.TickForNextShot + 60};
                    projectilesBuilder!.Add(new Projectile(tower.Position, Vector2.UnitX * 0.05f));
                    changedGame = true;
                }
            }

            if (changedGame)
            {
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
                    return game with
                    {
                        Towers = towers,
                    };
                }
            }
            else
            {
                return game;
            }
            
        }
    }
}