using System.Collections.Immutable;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class ProjectileMovementSimulator
    {
        public void Tick(SimulationData data)
        {
            ImmutableArray<Projectile>.Builder projetilesBuilder = data.Projectiles;
            for (var i = 0; i < projetilesBuilder.Count; i++)
            {
                Projectile projectile = projetilesBuilder[i];
                projetilesBuilder[i] = projectile with
                {
                    Position = projectile.Position + projectile.Velocity,
                };
            }
        }
    }
}