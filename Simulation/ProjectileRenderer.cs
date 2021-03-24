using System.Collections.Immutable;
using TowerDefense.Common;
using TowerDefense.Common.Game;

namespace TowerDefense.Simulation
{
    internal sealed class ProjectileRenderer : ISimulator
    {
        public GameData Tick(in GameData game)
        {
            ImmutableArray<Projectile>.Builder projetilesBuilder = game.Projectiles.ToBuilder();
            for (var i = 0; i < projetilesBuilder.Count; i++)
            {
                Projectile projectile = projetilesBuilder[i];
                projetilesBuilder[i] = projectile with
                {
                    Position = projectile.Position + projectile.Velocity,
                };
            }

            return game with {Projectiles = projetilesBuilder.ToImmutable()};
        }
    }
}