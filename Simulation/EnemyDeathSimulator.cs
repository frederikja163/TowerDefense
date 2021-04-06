using System.Collections.Immutable;
using System.Linq;
using OpenTK.Mathematics;
using TowerDefense.Common;
using TowerDefense.Common.Extensions;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class EnemyDeathSimulator
    {
        private static readonly Box2 ProjectileBounds = new Box2(0, 0, 1, 1);
        
        public void Tick(SimulationData data)
        {
            ImmutableArray<Enemy>.Builder enemyBuilder = data.Enemies;
            ImmutableArray<Projectile>.Builder projectileBuilder = data.Projectiles;

            for (int i = projectileBuilder.Count - 1; i >= 0; i--)
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
        }
    }
}