using System.Collections.Immutable;
using System.Linq;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Extensions;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class EnemyDeathSimulator : ISimulator
    {
        private static readonly Box2 ProjectileBounds = new Box2(0, 0, 1, 1);
        
        public GameData Tick(in GameData game)
        {
            ImmutableArray<Enemy>.Builder enemyBuilder = game.Enemies.ToBuilder();
            ImmutableArray<Projectile>.Builder projectileBuilder = game.Projectiles.ToBuilder();

            for (var i = projectileBuilder.Count - 1; i >= 0; i--)
            {
                Projectile projectile = projectileBuilder[i];
                if (enemyBuilder.CheckForCollision(projectile.Position, 0.05f, out _, out int enemyIndex))
                {
                    enemyBuilder.RemoveAt(enemyIndex);
                    projectileBuilder.RemoveAt(i);
                }
                else if (!ProjectileBounds.Contains(projectile.Position))
                {
                    projectileBuilder.RemoveAt(i);
                }               
            }

            while (enemyBuilder.Count > 0 && !ProjectileBounds.Contains(enemyBuilder[0].Position))
            {
                enemyBuilder.RemoveAt(0);
            }

            return game with
            {
                Enemies = enemyBuilder.ToImmutable(),
                Projectiles = projectileBuilder.ToImmutable(),
            };
        }
    }
}